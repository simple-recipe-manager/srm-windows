using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Whiskly.Pages.RecipeOnboarding_Phone;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI;
using Windows.UI.Popups;
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

            Cat_ComboBox.ItemsSource = CommonCompClass.Categories;
            Cat_ComboBox.DisplayMemberPath = "category";
            Cat_ComboBox.SelectedValuePath = "cat_id";

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

            SolidColorBrush errorRed = new SolidColorBrush(Color.FromArgb(255, 220, 20, 60));

            Debug.WriteLine("category Selected: " + Cat_ComboBox.SelectedIndex + " |");

            if (RecipeName_textBox_desktab.Text == "")
            {
                RecipeName_textBox_desktab.BorderBrush = errorRed;
                return;
            }

            if (Cat_ComboBox.SelectedItem == null)
            {
                Cat_ComboBox.BorderBrush = errorRed;
                return;
            }

            if (YieldTextBox.Text == "")
            {
                YieldTextBox.Text = "1  ";
            }

            if (TemperatureTextBox.Text == "")
            {
                TemperatureTextBox.Text = "0   ";
            }

            // read recipeBook to memory
            RecipeClass newRecipe = new RecipeClass();
            List<RecipeClass> recipeBook = buildRecipeBook(newRecipe);

            string json = JsonConvert.SerializeObject(recipeBook);

            WriteRecipeBook(json);

            // navigate back to recipe feed
            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(RecipeFeed));
            SplitView.splitviewPage.MainNav.IsPaneOpen = true;
        }

        public string category_Selected;

        private List<RecipeClass> buildRecipeBook(RecipeClass newRecipe)
        {
            // read local storage to get the list of existing recipes, and store into a list in memory
            List<RecipeClass> recipeBook = new List<RecipeClass>();

            var roamingSettings = ApplicationData.Current.RoamingSettings;

            List<RecipeClass> recipe_Current;

            Task<List<RecipeClass>> readingTask = ReadRecipeBook();
            readingTask.Start();

            while (!readingTask.IsCompleted)
            {
                Debug.WriteLine("Task status was " + readingTask.Status.ToString());
            }

            if (!readingTask.IsFaulted)
            {
                recipe_Current = ReadRecipeBook().Result;

                Debug.WriteLine("Task is not fualted.");

            } else
            {
                //you're probs fucked
                Debug.WriteLine("Task is else.");
                recipe_Current = null;
            }

            List<RecipeClass> recipeBookStore = recipe_Current;
            if (recipeBookStore != null)
            {
                recipeBook = recipeBookStore;
            }

            // get the new recipe from the UI and store into an object in memory
            newRecipe.ID = Guid.NewGuid();
            newRecipe.Date = DateTime.UtcNow.Ticks;
            newRecipe.Name = RecipeName_textBox_desktab.Text;
            newRecipe.Description = RecipeDescription_textBox_desktab.Text;
            if (Cat_ComboBox.SelectedIndex == 0) { category_Selected = "Appetizer"; } if (Cat_ComboBox.SelectedIndex == 1) { category_Selected = "Breakfast & Brunch"; } if (Cat_ComboBox.SelectedIndex == 2) { category_Selected = "Cocktails"; } if (Cat_ComboBox.SelectedIndex == 3) { category_Selected = "Desserts"; } if (Cat_ComboBox.SelectedIndex == 4) { category_Selected = "Drinks"; } if (Cat_ComboBox.SelectedIndex == 5) { category_Selected = "Main Dishes"; } if (Cat_ComboBox.SelectedIndex == 6) { category_Selected = "Pasta"; } if (Cat_ComboBox.SelectedIndex == 7) { category_Selected = "Salad"; Debug.WriteLine("7: " + category_Selected); } if (Cat_ComboBox.SelectedIndex == 8) { category_Selected = "Seafood"; } if (Cat_ComboBox.SelectedIndex == 9) { category_Selected = "Soup"; } if (Cat_ComboBox.SelectedIndex == 10) { category_Selected = "Vegetarian"; } else { category_Selected = "unknown"; }
            newRecipe.Category = category_Selected;
            Debug.WriteLine("7 Selected: " + category_Selected);
            List<string> ingredList = new List<string>();
                foreach(TextBox t in IngredientsStackPanel.Children)
                {
                    string ingredient = t.Text;
                    ingredList.Add(ingredient);
                }
            newRecipe.Ingredients = ingredList;
                List<DirectionClass> directionList = new List<DirectionClass>();
                foreach(StackPanel sp in DirectionsStackPanel.Children)
                {
                    DirectionClass newDirection = new DirectionClass();
                    newDirection.StepID = DirectionsStackPanel.Children.IndexOf(sp);
                    foreach (TextBox t in sp.Children)
                    {
                        if(t.Tag.ToString() == "step")
                        {
                            newDirection.StepName = t.Text;
                        }
                        if(t.Tag.ToString() == "direction")
                        {
                            newDirection.StepDirection = t.Text;
                        }
                    }
                    directionList.Add(newDirection);
                }
            newRecipe.Directions = directionList;
            newRecipe.Yeild = YieldTextBox.Text.Substring(0, YieldTextBox.Text.Length - 2);
            newRecipe.PrepTime = RecipePrepTime_timepicker_desktab.Time;
            newRecipe.CookTime = RecipeCookTime_timepicker_desktab.Time;
            newRecipe.Temp = TemperatureTextBox.Text.Substring(0, TemperatureTextBox.Text.Length - 3);

            // append the new recipe to the list, and return the list
            recipeBook.Add(newRecipe);

            return recipeBook;
        }

        StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        async void WriteRecipeBook(string json)
        {
            StorageFile recipe = await localFolder.CreateFileAsync("recipeFile.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(recipe, json);
        }

        async Task<List<RecipeClass>> ReadRecipeBook()
        {
            try
            {
                // Getting JSON from file if it exists, or file not found exception if it does not  
                StorageFile textFile = await localFolder.GetFileAsync("recipeFile.txt");
                using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
                {
                    // Read text stream     
                    using (DataReader textReader = new DataReader(textStream))
                    {
                        //get size                       
                        uint textLength = (uint)textStream.Size;
                        await textReader.LoadAsync(textLength);
                        // read it                    
                        string jsonContents = textReader.ReadString(textLength);
                        // deserialize back to our product!  
                        List<RecipeClass> recipe_Current = JsonConvert.DeserializeObject<List<RecipeClass>>(jsonContents);
                        // and send it
                        return recipe_Current;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // Exceptions everywhere
                Debug.WriteLine("This is null from Exception");
                return null;
            }
        }

        //private async void EmptySender()
        //{
        //    // Create a MessageDialog
        //    var messageDialog = new MessageDialog("This device is not connected to the internet. Until an active internet connection is established, the application can not continue. Check the network status and then retry.", "Empty Fields");
        //    // Or create a separate callback for different commands

        //    messageDialog.Commands.Add(new UICommand(
        //        "Retry", new UICommandInvokedHandler(this.CommandInvokedHandler)));

        //    // Set CommandIndex. 0 means default.
        //    messageDialog.DefaultCommandIndex = 0;

        //    // Show MessageDialog
        //    await messageDialog.ShowAsync();
        //}

        //private void CommandInvokedHandler(IUICommand command)
        //{
        //    refreshPage();
        //}

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
            StepTextbox.Tag = "step";
            StepTextbox.PlaceholderText = "Step " + currentStackpanel;
            StepTextbox.Margin = new Thickness(0, 20, 0, 0);

            TextBox DirectionTextbox = new TextBox();
            DirectionTextbox.Name = "Direction_" + currentStackpanel;
            DirectionTextbox.Tag = "direction";
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
