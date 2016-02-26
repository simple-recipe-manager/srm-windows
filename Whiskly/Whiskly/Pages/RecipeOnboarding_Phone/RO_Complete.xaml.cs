using System;
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
    public sealed partial class RO_Complete : Page
    {
        public RO_Complete()
        {
            this.InitializeComponent();

            // track a page view
            string pageName = this.Name;
            GoogleAnalytics.EasyTracker.GetTracker().SendView("RO_Complete");
        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "back_click", "Back: from RO_Complete", 0);

            RecipeOnboarding.recipeOnboarding.RO_Phone_Frame.Navigate(typeof(RO_Image));
        }

        private void Finish_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "finish_click", "Finish: from RO_Complete", 0);

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
        }
    }
}
