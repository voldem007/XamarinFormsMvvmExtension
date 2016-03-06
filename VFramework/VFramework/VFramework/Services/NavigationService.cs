using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VFramework.Interfaces;
using Xamarin.Forms;

namespace VFramework.Services
{
    public class NavigationService : INavigationAsyncService
    {
        public NavigationPage NavigationPage { get; set; }
        private List<TypeInfo> pages;

        public NavigationService(NavigationPage navigationPage)
        {
            this.NavigationPage = navigationPage;
        }

        public Task NavigateToModal(string pageKey)
        {
            return NavigateToModal(pageKey, null);
        }

        public Task GoBack()
        {
            return NavigationPage.Navigation.PopAsync();
        }

        public Task GoBackModal()
        {
            return NavigationPage.Navigation.PopModalAsync();
        }

        public Task NavigateTo(string pageKey)
        {
            return NavigateTo(pageKey, null);
        }

        /// <summary>
        /// Serches for specified page using reflection
        /// </summary>
        private Type GetPageType(string pageKey)
        {
            if (pages == null)
            {
                Assembly currentAssembly = typeof(NavigationService).GetTypeInfo().Assembly;
                pages = currentAssembly.DefinedTypes.Where(t => t.Name.EndsWith("Page")).ToList();
            }
            return pages.Single(t => t.FullName.EndsWith("." + pageKey + "Page")).AsType();
        }

        public Task NavigateToModal(string pageKey, object parameter)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            //NavigateToModal
            // use reflection to create page by key
            Type pageType = GetPageType(pageKey);
            Page page = (Page)Activator.CreateInstance(pageType);

            //call navigate method on view model
            INavigateAwareViewModel nextVM = page.BindingContext as INavigateAwareViewModel;
            if (nextVM != null)
            {
                nextVM.NavigateTo(parameter);
            }
            //trying to make try modal view
            IModalPage modalPage = page as IModalPage;
            if (modalPage != null)
            {
                modalPage.tcs = tcs;
            }
            NavigationPage.Navigation.PushModalAsync(page);
            return tcs.Task;
        }

        public async Task NavigateTo(string pageKey, object parameter)
        {
            // NavigateFrom
            if (NavigationPage.CurrentPage != null)
            {
                INavigateAwareViewModel prevVM = NavigationPage.CurrentPage.BindingContext as INavigateAwareViewModel;
                if (prevVM != null)
                {
                    prevVM.NavigateFrom();
                }
            }

            //NavigateTo
            // use reflection to create page by key
            Type pageType = GetPageType(pageKey);
            Page page = (Page)Activator.CreateInstance(pageType);

            //call navigate method on view model
            INavigateAwareViewModel nextVM = page.BindingContext as INavigateAwareViewModel;
            if (nextVM != null)
            {
                nextVM.NavigateTo(parameter);
            }

            // navigate to page from menu
            if (page is IDetailPage)
            {
                if (NavigationPage.CurrentPage.GetType() != page.GetType())
                {
                    var firstPage = NavigationPage.Navigation.NavigationStack.ElementAt(0);
                    NavigationPage.Navigation.InsertPageBefore(page, firstPage);
                    await NavigationPage.Navigation.PopToRootAsync();
                }
            }
            //navigate to next page on other case
            else
            {
                await NavigationPage.Navigation.PushAsync(page);
            }
        }
    }
}

