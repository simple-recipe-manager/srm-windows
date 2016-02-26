using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Whiskly.Pages.Recipes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Recipe_AddNote : Page
    {
        public Recipe_AddNote()
        {
            this.InitializeComponent();
        }

        private void Source_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Made it here.");
            Debug.WriteLine("Selected Index: " + Source_ComboBox.SelectedIndex.ToString() + " |");

            if (Source_ComboBox.SelectedIndex == 0)
            {
                Debug.WriteLine("Made it to web");
                WebSource_Stackpanel.Visibility = Visibility.Visible;
                BookSource_Stackpanel.Visibility = Visibility.Collapsed;
            }
            if (Source_ComboBox.SelectedIndex == 1)
            {
                Debug.WriteLine("Made it to Book");
                WebSource_Stackpanel.Visibility = Visibility.Collapsed;
                BookSource_Stackpanel.Visibility = Visibility.Visible;
            }
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "cancel_click", "Cancel: from Recipe_AddNote", 0);

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(Recipe_HeaderImage));
        }

        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "add_click", "Add: from Recipe_AddNote", 0);

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(Recipe_HeaderImage));
        }
    }
}
