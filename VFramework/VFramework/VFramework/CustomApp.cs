﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VFramework
{
    public class CustomApp : Application
    {
        public CustomApp()
        {
            Bootstrapper.RegisterServices();
            Bootstrapper.RegisterViewModels();
            MainPage = Bootstrapper.GetMainPage();
        }
    }
}
