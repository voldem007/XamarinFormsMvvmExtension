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
            if (NavigationPage.Navigation.ModalStack.Count > 1)
            {
                return NavigationPage.Navigation.PopModalAsync();
            }
            else
            {
                return NavigationPage.Navigation.PopAsync();
            }
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

        public async Task NavigateToModal(string pageKey, object parameter)
        {
            //NavigateTo
            if (pageKey != String.Empty)
            {
                // use reflection to create page by key
                Type pageType = GetPageType(pageKey);
                Page page = (Page)Activator.CreateInstance(pageType);

                //call navigate method on view model
                INavigateAwareViewModel nextVM = page.BindingContext as INavigateAwareViewModel;
                if (nextVM != null)
                {
                    nextVM.NavigateTo(parameter);
                }

                await NavigationPage.Navigation.PushModalAsync(page);
            }
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
            if (pageKey != String.Empty)
            {
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
}

