using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using VFramework.Services;
using VFramework.ViewModels;
using VFramework.Views;
using Xamarin.Forms;

namespace VFramework
{
    public static class Bootstrapper
    {
        public static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SecondViewModel>();
            SimpleIoc.Default.Register<ModalViewModel>();
        }

        public static void RegisterServices()
        {
            SimpleIoc.Default.Register<INavigationAsyncService>(() => new NavigationService(new NavigationPage()));
        }

        public static Page GetMainPage()
        {
            SimpleIoc.Default.GetInstance<INavigationAsyncService>().NavigateTo("Main");
            return SimpleIoc.Default.GetInstance<INavigationAsyncService>().NavigationPage;
        }
    }
}
