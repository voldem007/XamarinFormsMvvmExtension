using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VFramework.Services
{
    public class NavigationService : INavigationAsyncService
    {
        public NavigationPage NavigationPage { get; set; }
        private string pageNameTemplate;

        public NavigationService(NavigationPage navigationPage, string pageNameTemplate)
        {
            this.NavigationPage = navigationPage;
            this.pageNameTemplate = pageNameTemplate;
        }

        public async Task GoBack()
        {
            await NavigationPage.PopAsync();
        }

        public async Task NavigateTo(string pageKey)
        {
            await NavigateTo(pageKey, null);
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
                string typeName = string.Format(pageNameTemplate, pageKey);
                Type pageType = Type.GetType(typeName);
                Page page = (Page)Activator.CreateInstance(pageType);

                //call navigate method on view model
                INavigateAwareViewModel nextVM = page.BindingContext as INavigateAwareViewModel;
                if (nextVM != null)
                {
                    nextVM.NavigateTo(parameter);
                }

                // navigate to page from menu
                //if (page is IDetailPage || (NavigationPage.CurrentPage != null && page is DealsPage))
                //{
                //    if (NavigationPage.CurrentPage.GetType() != page.GetType())
                //    {
                //        var firstPage = NavigationPage.Navigation.NavigationStack.ElementAt(0);
                //        NavigationPage.Navigation.InsertPageBefore(page, firstPage);
                //        await NavigationPage.Navigation.PopToRootAsync();
                //    }
                //}
                // navigate to next page on ither case
                //else
                //{
                   await NavigationPage.Navigation.PushAsync(page);
                //}
            }
        }
    }
}

