﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Foundation;
using ListApp.Services;
using UIKit;

namespace ListApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        IDependencyService dependencyService;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("Brush_Experimental");
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.SetFlags("RadioButton_Experimental");
            global::Xamarin.Forms.Forms.Init();
            dependencyService = Xamarin.Forms.DependencyService.Get<IDependencyService>();
            var sw = new Stopwatch();
            sw.Start();
            LoadApplication(new App(dependencyService, sw));

            return base.FinishedLaunching(app, options);
        }
    }
}
