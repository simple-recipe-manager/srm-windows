﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Whiskly
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplitView : Page
    {
        public static SplitView splitviewPage;

        public SplitView()
        {
            this.InitializeComponent();

            MainContentFrame.Navigate(typeof(RecipeFeed));
            SplitView.splitviewPage = this;

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundColor = Colors.White;
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundOpacity = 1;
            }
        }

        private void New_Recipe_Onboarding(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Navigate(typeof(RecipeOnboarding));
            SplitView.splitviewPage.MainNav.IsPaneOpen = false;
        }

        public Windows.UI.Xaml.Controls.SplitView getMenuPane()
        {
            return this.MainNav;
        }
    }
}
