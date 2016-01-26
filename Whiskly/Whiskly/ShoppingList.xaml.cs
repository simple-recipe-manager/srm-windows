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

namespace Whiskly
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShoppingList : Page
    {
        public ShoppingList()
        {
            this.InitializeComponent();
        }

        private void MenuHamburger_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "menu_click", "Menu: from ShoppingList", 0);

            SplitView.splitviewPage.MainNav.IsPaneOpen = true;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if(SearchField.Visibility == Visibility.Collapsed)
            {
              /* DoubleAnimation fadeout = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(2), FillBehavior.HoldEnd);
              fadeout.BeginTime = TimeSpan.FromSeconds(0);
              Storyboard sb = new Storyboard();
              Storyboard.SetTarget(fadeout, SearchField);
              Storyboard.SetTargetProperty(fadeout, new PropertyPath("(Opacity)"));
              sb.Children.Add(fadeout);
              sb.Begin(); */

                SearchField.Visibility = Visibility.Visible;
                SearchField.IsFocused = true;

                SearchStackpanel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 00, 69, 0x5C));
            }
            else if(SearchField.Visibility == Visibility.Visible)
            {
                SearchField.Visibility = Visibility.Collapsed;
                SearchField.IsFocused = false;

                SearchStackpanel.Background = new SolidColorBrush(Color.FromArgb(00, 00, 69, 0x5C));
            }
        }
    }
}
