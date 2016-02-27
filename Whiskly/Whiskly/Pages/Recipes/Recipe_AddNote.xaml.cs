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

namespace Whiskly.Pages.Recipes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Recipe_AddNote : Page
    {
        public Recipe_AddNote()
        {
            this.InitializeComponent();
        }

        private void Source_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Made it here.");
            Debug.WriteLine("Selected Index: " + Source_ComboBox.SelectedIndex.ToString() + " |");

            if (Source_ComboBox.SelectedIndex == 0)
            {
                Debug.WriteLine("Made it to web");
                WebSource_Stackpanel.Visibility = Visibility.Visible;
                BookSource_Stackpanel.Visibility = Visibility.Collapsed;
            }
            if (Source_ComboBox.SelectedIndex == 1)
            {
                Debug.WriteLine("Made it to Book");
                WebSource_Stackpanel.Visibility = Visibility.Collapsed;
                BookSource_Stackpanel.Visibility = Visibility.Visible;
            }
        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "cancel_click", "Cancel: from Recipe_AddNote", 0);

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(Recipe_HeaderImage));
        }

        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "add_click", "Add: from Recipe_AddNote", 0);

            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;

            // read recipeBook to memory
            RecipeClass newRecipe = new RecipeClass();
            List<RecipeClass> recipeBook = buildRecipeBook(newRecipe);
            //roamingSettings.Values["recipeBook"] = recipeBook;

            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(Recipe_HeaderImage));
        }

        private List<RecipeClass> buildRecipeBook(RecipeClass newRecipe)
        {
            // read local storage to get the list of existing recipes, and store into a list in memory
            List<RecipeClass> recipeBook = new List<RecipeClass>();

            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            List<RecipeClass> recipeBookStore = (List<RecipeClass>)roamingSettings.Values["recipeBook"];
            if (recipeBookStore != null)
            {
                recipeBook = recipeBookStore;
            }

            if (NotesSource_Pivot.SelectedIndex == 0)
            {
                // get the new notes from the UI and store into an object in memory
                List<string> noteList = new List<string>();
                foreach (TextBox t in IndividualNote_Stackpanel_Phone.Children)
                {
                    string note = t.Text;
                    noteList.Add(note);
                }
                newRecipe.Notes = noteList;
            }

            if (NotesSource_Pivot.SelectedIndex == 1)
            {
                // get the new source from the UI and store into an object in memory
                SourceClass source = new SourceClass();

                if (Source_ComboBox.SelectedIndex == 0)
                {
                    source.Type = "Web";
                    source.Website = WebTitle_Phone.Text;
                    source.Address = WebAddress_Phone.Text;
                }
                if (Source_ComboBox.SelectedIndex == 1)
                {
                    source.Type = "Book";
                    source.Title = BookTitle_Phone.Text;
                    source.Author = BookAuthor_Phone.Text;
                    source.Page = BookPage_Phone.Text;
                    source.ISBN = BookISBN_Phone.Text;
                }

                newRecipe.Source = source;
            }

            // append the new note/source to the list, and return the list
            recipeBook.Add(newRecipe);

            return recipeBook;
        }

        private void Add_Note_Click(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "addNote_click", "Add Note: from Recipe_AddNote", 0);

            int stackpanelSize = this.IndividualNote_Stackpanel_Phone.Children.Count;

            int currentTextbox = stackpanelSize + 1;

            TextBox NoteTextbox = new TextBox();
            NoteTextbox.Name = "Note_" + currentTextbox;
            NoteTextbox.PlaceholderText = "recipe note";
            NoteTextbox.TextWrapping = TextWrapping.Wrap;
            NoteTextbox.Margin = new Thickness(0, 10, 0, 0);

            this.IndividualNote_Stackpanel_Phone.Children.Add(NoteTextbox);
        }
    }
}
