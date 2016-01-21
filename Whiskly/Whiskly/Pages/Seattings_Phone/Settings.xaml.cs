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

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("Settings");

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
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "legalopen_clicked", "Settings Open: from Settings", 0);

            WebView_Legal.Visibility = Visibility.Visible;
        }

        private async void ComposeEmail(Windows.ApplicationModel.Contacts.Contact recipient, string messageBody, StorageFile attachmentFile)
        {
            var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
            emailMessage.Body = messageBody;

            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);

                var attachment = new Windows.ApplicationModel.Email.EmailAttachment(attachmentFile.Name, stream);

                emailMessage.Attachments.Add(attachment);
            }

            var email = recipient.Emails.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactEmail>();
            if (email != null)
            {
                var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(email.Address);
                emailMessage.To.Add(emailRecipient);
            }

            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }
    }
}
