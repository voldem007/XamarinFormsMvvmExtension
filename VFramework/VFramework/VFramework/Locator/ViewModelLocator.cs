using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using VFramework.ViewModels;

namespace VFramework.Locator
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Register all the used ViewModels, Services et. al. witht the IoC Container
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // ViewModels
            SimpleIoc.Default.Register<MainViewModel>();
           // SimpleIoc.Default.Register<DetailViewModel>();

            // Services
            //SimpleIoc.Default.Register<IPeopleService, PeopleServiceStub>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
 
    }
}
