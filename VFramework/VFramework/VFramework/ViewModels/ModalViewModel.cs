using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VFramework.Interfaces;
using Xamarin.Forms;

namespace VFramework.ViewModels
{
    public class ModalViewModel : CustomViewModelBase, INavigateAwareViewModel
    {

        public ModalViewModel()
        {
            
        }

        public ICommand DiscardCommand
        {
            get { return new Command(async ()=> await GoBack());}
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
