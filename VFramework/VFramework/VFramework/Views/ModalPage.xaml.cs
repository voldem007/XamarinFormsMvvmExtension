using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VFramework.Interfaces;
using Xamarin.Forms;

namespace VFramework.Views
{
    public partial class ModalPage : IModalPage
    {
        public TaskCompletionSource<bool> tcs { get; set; }
        public ModalPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            tcs.SetResult(true);
            base.OnDisappearing();
        }
    }
}
