﻿using System;
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

            i++
            IngredientTextbox[i] = new TextBox();
            IngredientTextbox[i].Placeholder = "Step " + i;
            IngredientTextbox[i].Style = this.Resources["WhisklyTextbox"] as Style;

            this.IngredientsStackPanel.Children.Add(IngredientTextbox[i]);
        }

        private void Add_Direction_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
