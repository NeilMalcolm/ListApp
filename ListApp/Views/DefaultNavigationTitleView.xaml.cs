﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefaultNavigationTitleView : ContentView
    {
        public DefaultNavigationTitleView()
        {
            InitializeComponent();
        }
    }
}