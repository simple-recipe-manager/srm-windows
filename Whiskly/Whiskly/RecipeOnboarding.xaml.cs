using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Whiskly.Pages.RecipeOnboarding_Phone;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.System;
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
    public sealed partial class RecipeOnboarding : Page
    {
        public static RecipeOnboarding recipeOnboarding;

        public DateTime startTime = DateTime.Now;

        public RecipeOnboarding()
        {
            this.InitializeComponent();

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("RecipeOnboarding");

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1))
            {
                RO_Phone_Frame.Navigate(typeof(RO_Title));
                RecipeOnboarding.recipeOnboarding = this;
            }
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "cancel_click", "Cancel: from RecipeOnboarding", 0);

            // track a timing (how long it takes your app to run a specific task)
            GoogleAnalytics.EasyTracker.GetTracker().SendTiming(DateTime.Now.Subtract(startTime), "Recipe Onboarding View Time", "RecipeOnboarding", "Exit By Cancel");

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
            SplitView.splitviewPage.MainNav.IsPaneOpen = true;
        }

        private void Finish_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "finish_click", "Finish: from RecipeOnboarding", 0);

            // track a timing (how long it takes your app to run a specific task)
            GoogleAnalytics.EasyTracker.GetTracker().SendTiming(DateTime.Now.Subtract(startTime), "Recipe Onboarding View Time", "RecipeOnboarding", "Exit By Finish");

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
            SplitView.splitviewPage.MainNav.IsPaneOpen = true;
        }

        private void Add_Ingredient_Click(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "addIngredient_click", "Add Ingredient: from RecipeOnboarding", 0);

            int stackpanelSize = this.IngredientsStackPanel.Children.Count;

            int currentTextbox = stackpanelSize + 1;

            TextBox IngredientTextbox = new TextBox();
            IngredientTextbox.Name = "Ingredient_" + currentTextbox;
            IngredientTextbox.PlaceholderText = "Ingredient " + currentTextbox;
            IngredientTextbox.Margin = new Thickness(0,10,0,0);

            this.IngredientsStackPanel.Children.Add(IngredientTextbox);
        }

        private void Add_Direction_Click(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "addDirection_click", "Add Direction: from RecipeOnboarding", 0);

            int stackpanelSize = this.DirectionsStackPanel.Children.Count;

            int currentStackpanel = stackpanelSize + 1;

            StackPanel DirectionInternalStackPanel = new StackPanel();
            DirectionInternalStackPanel.Name = "DirectionInternalStackpanel_" + currentStackpanel;


            TextBox StepTextbox = new TextBox();
            StepTextbox.Name = "Step_" + currentStackpanel;
            StepTextbox.Header = "Step " + currentStackpanel;
            StepTextbox.PlaceholderText = "Step " + currentStackpanel;
            StepTextbox.Margin = new Thickness(0, 20, 0, 0);

            TextBox DirectionTextbox = new TextBox();
            DirectionTextbox.Name = "Direction_" + currentStackpanel;
            DirectionTextbox.PlaceholderText = "Directions for step " + currentStackpanel;
            DirectionTextbox.Margin = new Thickness(0, 10, 0, 0);
            DirectionTextbox.TextWrapping = TextWrapping.Wrap;

            DirectionInternalStackPanel.Children.Add(StepTextbox);
            DirectionInternalStackPanel.Children.Add(DirectionTextbox);

            this.DirectionsStackPanel.Children.Add(DirectionInternalStackPanel);
        }

        private void UploadPhoto_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "upload_click", "Upload: from RecipeOnboarding", 0);

            photoUploadr();
        }

        private void UploadPhoto_DropCompleted(UIElement sender, DropCompletedEventArgs args)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "upload_drop", "Upload Drop: from RecipeOnboarding", 0);

            photoUploadr();
        }

        private void photoUploadr()
        {
            // do things to get the photo into the webz
        }

        public Frame getRO_Phone_Frame()
        {
            return this.RO_Phone_Frame;
        }

        private void YieldTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(YieldTextBox.Text == "")
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

        private void YieldTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!(e.Key >= VirtualKey.Number0 && e.Key <= VirtualKey.Number9) && !(e.Key >= VirtualKey.NumberPad0 && e.Key <= VirtualKey.NumberPad9))
            {
                e.Handled = true;
            }
        }

        private void TemperatureTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(TemperatureTextBox.Text == "")
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

        private void TemperatureTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!(e.Key >= VirtualKey.Number0 && e.Key <= VirtualKey.Number9) && !(e.Key >= VirtualKey.NumberPad0 && e.Key <= VirtualKey.NumberPad9))
            {
                e.Handled = true;
            }
        }
    }
}
