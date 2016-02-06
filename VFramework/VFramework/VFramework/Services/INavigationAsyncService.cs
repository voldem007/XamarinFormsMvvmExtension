using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VFramework.Services
{
    public interface INavigationAsyncService
    {
        NavigationPage NavigationPage { get; set; }
        Task NavigateTo(string pageName, object parameter);
        Task NavigateTo(string pageName);
        Task NavigateToModal(string pageName, object parameter);
        Task NavigateToModal(string pageName);
        Task GoBack();
    }
}
