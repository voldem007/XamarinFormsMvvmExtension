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
    public class MainViewModel : CustomViewModelBase, INavigateAwareViewModel
    {
        public MainViewModel()
        {
            Name = "EEEE!!! NICE!!!!";
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public ICommand PushCommand {
            get { return new Command(async () => { await NavigateTo<SecondViewModel>("Hello Lalks"); });}
        }

        public void NavigateTo(object parameter)
        {
            //throw new NotImplementedException();
        }

        public void NavigateFrom()
        {
            //throw new NotImplementedException();
        }
    }
}
