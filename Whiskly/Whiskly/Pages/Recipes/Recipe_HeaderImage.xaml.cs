using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class Recipe_HeaderImage : Page
    {
        private string rID;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rID = e.Parameter as string;
            pageStart();
        }

        public Recipe_HeaderImage()
        {
            this.InitializeComponent();
        }

        public void pageStart()
        {
            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("Recipe ID #" + rID);
        }

        private void Back_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "back_click", "Back: from recipe ID #" + rID, 0);

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
        }
    }
}
