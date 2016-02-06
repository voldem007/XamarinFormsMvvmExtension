using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFramework.Interfaces
{
    public interface INavigateAwareViewModel
    {
        void NavigateTo(object parameter);
        void NavigateFrom();
    }
}
