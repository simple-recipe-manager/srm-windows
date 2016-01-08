﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Whiskly.Pages.RecipeOnboarding_Phone;
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
    public sealed partial class RO_Prep : Page
    {
        public RO_Prep()
        {
            this.InitializeComponent();
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
        }

        private void Next_Clicked(object sender, RoutedEventArgs e)
        {
            // Code to save recipe preperation inputs

            RecipeOnboarding.recipeOnboarding.RO_Phone_Frame.Navigate(typeof(RO_Complete));
        }

        private void YieldTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (YieldTextBox.Text == "")
            {
                YieldTextBox.PlaceholderText = "1 x";
            }
            else
            {
                YieldTextBox.Text = YieldTextBox.Text + " x";
            }
        }

        private void YieldTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            YieldTextBox.Text = "";
        }

        private void TemperatureTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TemperatureTextBox.Text == "")
            {
                TemperatureTextBox.PlaceholderText = "°F";
            }
            else
            {
                TemperatureTextBox.Text = TemperatureTextBox.Text + " °F";
            }
        }

        private void TemperatureTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TemperatureTextBox.Text = "";
        }
    }
}
