using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Whiskly.Pages.Recipes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Whiskly
{
    public partial class RecipeCard_Image : UserControl
    {
        public int rID = 0;

        public RecipeCard_Image()
        {
            this.InitializeComponent();
            this.DefaultStyleKey = typeof(RecipeCard_Image);
            this.DataContext = this;

            //turnPropertiesToTemplate();
        }

        public static readonly DependencyProperty RecipeIDProperty = DependencyProperty.Register(
            "RecipeID",                   // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(RecipeCard_Image),     // The type of the owner of the DependencyProperty
            new PropertyMetadata("")
        );

        public string RecipeID
        {
            get { return (string)GetValue(RecipeIDProperty); }
            set { SetValue(RecipeIDProperty, value); }
        }

        public static readonly DependencyProperty RecipeTitleProperty = DependencyProperty.Register(
            "RecipeTitle",                // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(RecipeCard_Image),     // The type of the owner of the DependencyProperty
            new PropertyMetadata("")
        );

        public string RecipeTitle
        {
            get { return (string)GetValue(RecipeTitleProperty); }
            set { SetValue(RecipeTitleProperty, value); }
        }

        public static readonly DependencyProperty RecipeDescriptionProperty = DependencyProperty.Register(
            "RecipeDescription",          // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(RecipeCard_Image),     // The type of the owner of the DependencyProperty
            new PropertyMetadata("")
        );

        public string RecipeDescription
        {
            get { return (string)GetValue(RecipeDescriptionProperty); }
            set { SetValue(RecipeDescriptionProperty, value); }
        }

        public static readonly DependencyProperty RecipeImageProperty = DependencyProperty.Register(
            "RecipeImage",                // The name of the DependencyProperty
            typeof(string),               // The type of the DependencyProperty
            typeof(RecipeCard_Image),     // The type of the owner of the DependencyProperty
            new PropertyMetadata("")
        );

        public string RecipeImage
        {
            get { return (string)GetValue(RecipeImageProperty); }
            set { SetValue(RecipeImageProperty, value); }
        }

        public string recipeID;
        public string recipeName;
        public string recipeDescription;
        public string recipeImage;

        private void turnPropertiesToTemplate()
        {
            string recipeID = this.GetValue(RecipeIDProperty).ToString();
            string recipeName = this.GetValue(RecipeTitleProperty).ToString();
            string recipeDescription = this.GetValue(RecipeDescriptionProperty).ToString();
            string recipeImage = this.GetValue(RecipeImageProperty).ToString();

            Debug.WriteLine("ID: " + recipeID + "|");
            Debug.WriteLine("Name: " + recipeName + "|");
            Debug.WriteLine("Description: " + recipeDescription + "|");
            Debug.WriteLine("Image: " + recipeImage + "|");

            RecipeName_TextBlock.Text = recipeName;
            RecipeDescription_TextBlock.Text = recipeDescription;
        }

        private void Open_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "open_click", "(" + RecipeID + ") Open: from recipe ID #" + recipeID, 0);

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(Recipe_HeaderImage));
        }
    }
}
