using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.UI;
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
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundColor = Color.FromArgb(255, 38, 166, 154);
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundOpacity = 1;
            }

            Email_Textbox_desktab.KeyDown += (sender, args) =>
            {
                if (args.Key == VirtualKey.Enter)
                {
                    Debug.WriteLine("FUUUUUCKCKKCKCKCK!");
                }
            };
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

        private void Focus_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Debug.WriteLine("Enter key is pressed.");
                FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
            }
        }

        private void Submit_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Debug.WriteLine("Submit Enter key is pressed.");
                // Code that controls user password verification
            }
        }
    }
}
