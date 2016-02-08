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

            SearchField.Opacity = 0;

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("RecipeFeed");
        }

        #region Saving / Loading MyData 
        private const string _myFileLocation = "RecipeStore.txt";

        public static async Task<List<string>> GetMyData()
        {
            // If you're saving your stuff just on this device
            var readStream =
                await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_myFileLocation);

            // If there is no MyData, then we haven't created our MyData file yet
            if (readStream == null)
                return new List<string>(); ;

            DataContractSerializer stuffSerializer =
                new DataContractSerializer(typeof(List<string>));

            var setResult = (List<string>)stuffSerializer.ReadObject(readStream);

            return setResult;
        }

        public static async Task<bool> SaveMyData(List<string> saveData)
        {
            try
            {

                StorageFile savedStuffFile =
                    await ApplicationData.Current.LocalFolder.CreateFileAsync(_myFileLocation,
                    CreationCollisionOption.ReplaceExisting);

                using (Stream writeStream =
                    await savedStuffFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer stuffSerializer =
                        new DataContractSerializer(typeof(List<string>));

                    stuffSerializer.WriteObject(writeStream, saveData);
                    await writeStream.FlushAsync();
                    writeStream.Dispose();
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: unable to save MyData", e);
                //return false;
            }
        }
        #endregion

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
