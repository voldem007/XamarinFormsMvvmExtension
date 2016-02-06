using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using VFramework.Services;

namespace VFramework.ViewModels
{
    public class CustomViewModelBase : ViewModelBase
    {
        private INavigationAsyncService navigationAsyncService;
        public CustomViewModelBase()
        {
            this.navigationAsyncService = SimpleIoc.Default.GetInstance<INavigationAsyncService>();
        }

        public Task NavigateTo<TViewModel>(object parameter)
        {
            return navigationAsyncService.NavigateTo(GetName(typeof(TViewModel)), parameter);
        }

        private string GetName(Type type)
        {
            string name = type.Name;
            int subIndex = name.IndexOf("ViewModel", StringComparison.Ordinal);
            return name.Remove(subIndex);
        }

        public Task NavigateTo<TViewModel>()
        {
            return NavigateTo<TViewModel>(null);
        }

        public Task NavigateToModal<TViewModel>(object parameter)
        {
            return navigationAsyncService.NavigateToModal(GetName(typeof(TViewModel)), parameter);
        }

        public Task NavigateToModal<TViewModel>()
        {
            return NavigateToModal<TViewModel>(null);
        }

        public Task GoBack()
        {
            return navigationAsyncService.GoBack();
        }

        public async Task SendNotifyTo<TViewModel>()
        {
            
        }
    }
}
