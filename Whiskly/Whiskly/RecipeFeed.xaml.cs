using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Whiskly.Pages;
using Whiskly.Pages.Recipes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.ViewManagement;
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
    public sealed partial class RecipeFeed : Page
    {
        public RecipeFeed()
        {
            this.InitializeComponent();
            newCard("000000003", "Title of Recipe", "Description of recipe.", null);
            ReadRecipeBook();

            SearchField.Opacity = 0;

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("RecipeFeed");
        }

        async Task ReadRecipeBook()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

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
                        // and show it

                        foreach (RecipeClass recipe in recipe_Current)
                        {
                            RecipeCard_Image newRecipeCard = new RecipeCard_Image();
                            newRecipeCard.RecipeID = recipe.ID.ToString();
                            newRecipeCard.Name = recipe.ID.ToString();
                            newRecipeCard.RecipeTitle = recipe.Name;
                            newRecipeCard.RecipeDescription = recipe.Description;
                            newRecipeCard.RecipeImage = "";
                            newRecipeCard.HorizontalAlignment = HorizontalAlignment.Center;
                            newRecipeCard.VerticalAlignment = VerticalAlignment.Center;
                            if (grid_desktab.Visibility == Visibility.Collapsed)
                            {
                                var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
                                var size = bounds.Width;
                                var sizeMin = size - 50;
                                newRecipeCard.Width = sizeMin;

                                Recipe_FlipView.Items.Add(newRecipeCard);
                            }
                            else
                            {
                                newRecipeCard.MinWidth = 450;
                                Recipe_FlipView_Desktab.Items.Add(newRecipeCard);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // Exceptions everywhere
            }
        }

        private void MenuHamburger_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "menu_click", "Menu: from RecipeFeed", 0);

            SplitView.splitviewPage.MainNav.IsPaneOpen = true;
        }

        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SplitView.splitviewPage.MainContentFrame.Navigate(typeof(Recipe_HeaderImage));
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchField.Opacity = 1;
            if (SearchStackpanel.Tag.ToString() == "Open")
            {
                SearchStackpanel.Tag = "Closed";
                EnterStoryboard.Begin();
                SearchField.Focus(FocusState.Programmatic);

                SearchStackpanel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 00, 69, 0x5C));
                SearchIcon.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));

                // track a custom event
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "searchOpen_click", "Search Open: from RecipeFeed", 0);
            }
            else if (SearchStackpanel.Tag.ToString() == "Closed")
            {
                SearchStackpanel.Tag = "Open";
                ExitStoryboard.Begin();

                SearchStackpanel.Background = new SolidColorBrush(Color.FromArgb(00, 00, 69, 0x5C));
                SearchIcon.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 00, 00, 00));

                // track a custom event
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "searchClose_click", "Search Close: from RecipeFeed", 0);
            }
        }

        private void SearchField_LostFocus(object sender, RoutedEventArgs e)
        {
            ExitStoryboard.Begin();

            SearchStackpanel.Background = new SolidColorBrush(Color.FromArgb(00, 00, 69, 0x5C));
            SearchIcon.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 00, 00, 00));
        }

        private void newCard(string ID, string Title, string Descrition, ImageBrush Image)
        {
            var bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            var size = bounds.Width;
            var sizeMin = size - 50;

            RecipeCard.Width = sizeMin;

            RecipeCard_Image newRecipeCard = new RecipeCard_Image();
            newRecipeCard.RecipeID = "000000001";
            newRecipeCard.Name = newRecipeCard.RecipeID;
            newRecipeCard.RecipeTitle = "Created Title";
            newRecipeCard.RecipeDescription = "Write this down because it exists.";
            newRecipeCard.RecipeImage = "";
            newRecipeCard.Width = sizeMin;
            newRecipeCard.HorizontalAlignment = HorizontalAlignment.Center;
            newRecipeCard.VerticalAlignment = VerticalAlignment.Center;

            Recipe_FlipView.Items.Add(newRecipeCard);
        }
    }
}
