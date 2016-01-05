﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private void Login_Amazon_Click(object sender, RoutedEventArgs e)
        {
            MainPage.mainPage.MainFrame.Navigate(typeof(SplitView));
        }

        private void Login_Facebook_Click(object sender, RoutedEventArgs e)
        {
            MainPage.mainPage.MainFrame.Navigate(typeof(SplitView));
        }

        private void Login_Guest_Click(object sender, RoutedEventArgs e)
        {
            MainPage.mainPage.MainFrame.Navigate(typeof(SplitView));
        }
    }
}
