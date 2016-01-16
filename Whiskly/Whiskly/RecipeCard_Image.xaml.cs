﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Whiskly.Pages.Recipes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Whiskly
{
    public sealed partial class RecipeCard_Image : UserControl
    {
        public int rID = 0;

        public RecipeCard_Image()
        {
            this.InitializeComponent();
        }

        private void Open_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "open_click", "(" + rID + ") Open: from recipe ID #" + rID, 0);

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(Recipe_HeaderImage));
        }
    }
}
