using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
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

namespace Whiskly.Pages.Seattings_Phone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();

            var year = DateTime.Now.Year;

            var major = Package.Current.Id.Version.Major;
            var minor = Package.Current.Id.Version.Minor;
            var build = Package.Current.Id.Version.Build;
            var revision = Package.Current.Id.Version.Revision;
            var assemblyInformation = "Version: " + major + "." + minor + "." + build + " (" + revision + ")";

            yearInfo.Text = "© " + year + " Whiskly, Inc.";
            appVersionInfo.Text = assemblyInformation;
        }

        private void Close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var LegalWebViewVis = WebView_Legal.Visibility.ToString();
            var FeedbackFrameVis = Frame_Feedback.Visibility.ToString();

            if (LegalWebViewVis == "Visible")
            {
                WebView_Legal.Visibility = Visibility.Collapsed;
            }
            else if (FeedbackFrameVis == "Visible")
            {
                Frame_Feedback.Visibility = Visibility.Collapsed;
            }
            else
            {
                SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
            }
        }

        private void Legal_Tapped(object sender, TappedRoutedEventArgs e)
        {
            WebView_Legal.Visibility = Visibility.Visible;
        }
    }
}
