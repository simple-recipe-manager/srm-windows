using System;
using System.Collections.Generic;
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

namespace Whiskly.Pages.RecipeOnboarding_Phone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RO_Title : Page
    {
        public RO_Title()
        {
            this.InitializeComponent();

            Cat_ComboBox.ItemsSource = CommonCompClass.Categories;
            Cat_ComboBox.DisplayMemberPath = "category";
            Cat_ComboBox.SelectedValuePath = "cat_id";

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("RO_Title");
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "cancel_click", "Cancel: from RO_Title", 0);

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
        }

        private void Next_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "next_click", "Next: from RO_Title", 0);

            // Code to store values before continuing recipe onboarding process

            RecipeOnboarding.recipeOnboarding.RO_Phone_Frame.Navigate(typeof(RO_Description));
        }
    }
}
