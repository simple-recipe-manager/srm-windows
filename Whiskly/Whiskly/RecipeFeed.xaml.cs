﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Whiskly.Pages;
using Whiskly.Pages.Recipes;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class RecipeFeed : Page
    {
        public RecipeFeed()
        {
            this.InitializeComponent();
        }

        private void MenuHamburger_Clicked(object sender, RoutedEventArgs e)
        {
            SplitView.splitviewPage.MainNav.IsPaneOpen = true;
        }

        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(Recipe_HeaderImage));
        }
    }
}
