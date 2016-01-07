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
    public sealed partial class RO_Ingredients : Page
    {
        public RO_Ingredients()
        {
            this.InitializeComponent();
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
        }

        private void Next_Clicked(object sender, RoutedEventArgs e)
        {
            //Code to store added ingredients on naviagation to new page

            RecipeOnboarding.recipeOnboarding.RO_Phone_Frame.Navigate(typeof(RO_Directions));
        }

        private void Add_Ingredient_Click(object sender, RoutedEventArgs e)
        {
            int stackpanelSize = this.IngredientsStackPanel.Children.Count;

            int currentTextbox = stackpanelSize + 1;

            TextBox IngredientTextbox = new TextBox();
            IngredientTextbox.Name = "Ingredient_" + currentTextbox;
            IngredientTextbox.PlaceholderText = "Ingredient " + currentTextbox;
            IngredientTextbox.Margin = new Thickness(0, 10, 0, 0);

            this.IngredientsStackPanel.Children.Add(IngredientTextbox);
        }
    }
}
