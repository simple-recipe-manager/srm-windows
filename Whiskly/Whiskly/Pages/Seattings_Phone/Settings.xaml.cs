using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.System.Profile;
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
        public static int major = Package.Current.Id.Version.Major;
        public static int minor = Package.Current.Id.Version.Minor;
        public static int build = Package.Current.Id.Version.Build;
        public static int revision = Package.Current.Id.Version.Revision;
        public string assemblyInformation = "Version: " + major + "." + minor + "." + build + " (" + revision + ")";

        public Settings()
        {
            this.InitializeComponent();

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("Settings");

            var year = DateTime.Now.Year;

            yearInfo.Text = "© " + year + " Whiskly, Inc.";
            appVersionInfo.Text = assemblyInformation;
        }

        private void Close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var LegalWebViewVis = WebView_Legal.Visibility.ToString();
            var FeedbackFrameVis = Frame_Feedback.Visibility.ToString();

            if (LegalWebViewVis == "Visible")
            {
                // track a custom event
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "legalhide_clicked", "Hide Legal: from Settings", 0);

                WebView_Legal.Visibility = Visibility.Collapsed;
            }
            else if (FeedbackFrameVis == "Visible")
            {
                // track a custom event
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "feedbackhide_clicked", "Hide Feedback: from Settings", 0);

                Frame_Feedback.Visibility = Visibility.Collapsed;
            }
            else
            {
                // track a custom event
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "settingclose_clicked", "Settings Close: from Settings", 0);

                SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
            }
        }


        private void Legal_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "legalopen_clicked", "Legal Open: from Settings", 0);

            WebView_Legal.Visibility = Visibility.Visible;
        }

        private void Feedback_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "feedbackopen_clicked", "Feedback Email Open: from Settings", 0);

            ComposeEmail();
        }

        private async void ComposeEmail()
        {
            EmailMessage emailMessage = new EmailMessage();
            emailMessage.To.Add(new EmailRecipient("george@georgehinch.com"));
            string messageSubject = "Whiskly User Feedback";

            var sysInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            var ai = AnalyticsInfo.VersionInfo;
            string sv = ai.DeviceFamilyVersion;
            ulong v = ulong.Parse(sv);
            ulong v1 = (v & 0xFFFF000000000000L) >> 48;
            ulong v2 = (v & 0x0000FFFF00000000L) >> 32;
            ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
            ulong v4 = (v & 0x000000000000FFFFL);

            var timeNow = DateTime.UtcNow;
            var deviceMan = sysInfo.SystemManufacturer;
            var deviceMod = sysInfo.SystemProductName;
            var deviceOS = $"{v1}.{v2}.{v3} ({v4})";
            var deviceArch = Package.Current.Id.Architecture;
            var userID = "";
            var userLang = Windows.Globalization.Language.CurrentInputMethodLanguageTag;
            var userCarrier = sysInfo.SystemHardwareVersion;
            var networkStat = NetworkInformation.GetInternetConnectionProfile();

            string messageBody = "---"
                + "\n" + assemblyInformation
                + "\n" + timeNow
                + "\n" + "Device Manufactuer: " + deviceMan
                + "\n" + "Device Model: " + deviceMod
                + "\n" + "OS: " + deviceOS
                + "\n" + "Architecture: " + deviceArch
                + "\n" + "User ID: " + userID
                + "\n" + "Language: " + userLang
                + "\n" + "Carrier: " + userCarrier
                + "\n" + "Network Status: " + networkStat.ServiceProviderGuid;
            emailMessage.Subject = messageSubject;
            emailMessage.Body = messageBody;

            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }
    }
}
