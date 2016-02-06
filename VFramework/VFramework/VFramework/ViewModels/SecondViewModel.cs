using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VFramework.Interfaces;
using VFramework.Services;
using Xamarin.Forms;

namespace VFramework.ViewModels
{
    public class SecondViewModel : CustomViewModelBase, INavigateAwareViewModel
    {
        public void NavigateTo(object parameter)
        {
            MainText = "IT'S WORK!";
            MainText = parameter.ToString();
        }

        public void NavigateFrom()
        {
        }

        private string mainText;

        public string MainText
        {
            get { return mainText; }
            set
            {
                mainText = value;
                RaisePropertyChanged(() => MainText);
            }
        }

        public ICommand GoBackCommand
        {
            get { return new Command(async () => { await GoBack(); }); }
        }

        public ICommand GoModalCommand
        {
            get { return new Command(async () => { await NavigateToModal<ModalViewModel>(); }); }
        }
    }
}
