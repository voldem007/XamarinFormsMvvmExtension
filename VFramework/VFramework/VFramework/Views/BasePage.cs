using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using VFramework.ViewModels;
using Xamarin.Forms;

namespace VFramework.Views
{
    public class CustomContentPage<TViewModel> : ContentPage
    {
        public CustomContentPage()
        {
            ViewModel = SimpleIoc.Default.GetInstanceWithoutCaching<TViewModel>();
        }
        public TViewModel ViewModel
        {
            get { return (TViewModel)BindingContext; }
            set { BindingContext = value; }
        }
    }
}
