﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Whiskly.Pages.RecipeOnboarding_Phone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RO_Image : Page
    {
        public RO_Image()
        {
            this.InitializeComponent();

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("RO_Image");
        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "back_click", "Back: from RO_Image", 0);

            RecipeOnboarding.recipeOnboarding.RO_Phone_Frame.Navigate(typeof(RO_Prep));
        }

        private void Next_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "next_click", "Next: from RO_Image", 0);

            //Code to store added ingredients on naviagation to new page

            RecipeOnboarding.recipeOnboarding.RO_Phone_Frame.Navigate(typeof(RO_Complete));
        }

        private void UploadPhoto_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "upload_click", "Upload: from RO_Image", 0);

            // do things to get the photo into the webz
        }
    }
}
