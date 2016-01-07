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
    public sealed partial class RecipeOnboarding : Page
    {
        public RecipeOnboarding()
        {
            this.InitializeComponent();
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
        }

        private void Cancel_Desktab_Clicked(object sender, RoutedEventArgs e)
        {
            Cancel_Clicked(sender, e);
            SplitView.splitviewPage.MainNav.IsPaneOpen = true;
        }

        private void Add_Ingredient_Click(object sender, RoutedEventArgs e)
        {
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
    }
}
