using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Whiskly
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShoppingList : Page
    {
        public ShoppingList()
        {
            this.InitializeComponent();

            // sets the search field to 0 opacity when the page is loaded so the animation on click works correctly
            SearchField.Opacity = 0;

            // track a page view
            GoogleAnalytics.EasyTracker.GetTracker().SendView("ShoppingList");
        }

        private void MenuHamburger_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "menu_click", "Menu: from ShoppingList", 0);

            SplitView.splitviewPage.MainNav.IsPaneOpen = true;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            // Opaccity can now be 1 because the storyboard handles all animation from here on out
            SearchField.Opacity = 1;

            // Monitors the state of the stackpanel and performs the appropriate action when the icon is clicked
            if (SearchStackpanel.Tag.ToString() == "Open")
            {
                SearchStackpanel.Tag = "Closed";
                EnterStoryboard.Begin();
                SearchField.Focus(FocusState.Programmatic);

                SearchStackpanel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 00, 69, 0x5C));
                SearchIcon.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF));

                // track a custom event
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "searchOpen_click", "Search Open: from ShoppingList", 0);
            }
            else if (SearchStackpanel.Tag.ToString() == "Closed")
            {
                SearchStackpanel.Tag = "Open";
                ExitStoryboard.Begin();

                SearchStackpanel.Background = new SolidColorBrush(Color.FromArgb(00, 00, 69, 0x5C));
                SearchIcon.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 00, 00, 00));

                // track a custom event
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "searchClose_click", "Search Close: from ShoppingList", 0);
            }
        }

        private void SearchField_LostFocus(object sender, RoutedEventArgs e)
        {
            ExitStoryboard.Begin();

            SearchStackpanel.Background = new SolidColorBrush(Color.FromArgb(00, 00, 69, 0x5C));
            SearchIcon.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 00, 00, 00));
        }

        // Stores the sender button name for the Add To List button 
        public string groupSender;

        // Makes the correct text box visable depending on the button selected
        private void Add_ToList_Click(object sender, RoutedEventArgs e)
        {
            Button control = sender as Button;
            string ButtonName = control.Name;

            if (ButtonName == "Add_ToList_Phone")
            {
                NewIngredient_Phone.Visibility = Visibility.Visible;
                NewIngredient_Phone.Focus(FocusState.Programmatic);
                groupSender = "Add_ToList_Phone";
            }
            else if (ButtonName == "Add_ToList_Desktab")
            {
                NewIngredient_Desktab.Visibility = Visibility.Visible;
                NewIngredient_Desktab.Focus(FocusState.Programmatic);
                groupSender = "Add_ToList_Desktab";
            }
        }

        // When ender key is released, new checkbox created with the textbox text
        private void Add_Ingredient_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            string textboxText = NewIngredient_Phone.Text;
            if (e.Key == VirtualKey.Enter)
            {
                if (groupSender == "Add_ToList_Phone" && !String.IsNullOrWhiteSpace(textboxText))
                {
                    // track a custom event
                    GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "addToList_click", "Add To List: from ShoppingList (phone)", 0);

                    // Creates new checkbox
                    CheckBox newItemCheckbox = new CheckBox();
                    string content = NewIngredient_Phone.Text;
                    newItemCheckbox.Content = content;
                    newItemCheckbox.Name = content.Replace(" ", "");
                    newItemCheckbox.Checked += new RoutedEventHandler(ToPurchased_Checked);

                    // Add new chekbox to the bottom on the stackpanel
                    this.Shoppinglist_Stackpanel_Phone.Children.Add(newItemCheckbox);

                    // Clears an hides the new ingredient textbox
                    NewIngredient_Phone.Text = "";
                    NewIngredient_Phone.Visibility = Visibility.Collapsed;
                }
                else if (groupSender == "Add_ToList_Desktab" && !String.IsNullOrWhiteSpace(textboxText))
                {
                    // track a custom event
                    GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "addToList_click", "Add To List: from ShoppingList (desktab)", 0);

                    // Creates new checkbox
                    CheckBox newItemCheckbox = new CheckBox();
                    string content = NewIngredient_Desktab.Text;
                    newItemCheckbox.Content = content;
                    newItemCheckbox.Name = content.Replace(" ", "");
                    newItemCheckbox.Checked += new RoutedEventHandler(ToPurchased_Checked);

                    // Add new chekbox to the bottom on the stackpanel
                    this.Shoppinglist_Stackpanel_Desktab.Children.Add(newItemCheckbox);

                    // Clears an hides the new ingredient textbox
                    NewIngredient_Desktab.Text = "";
                    NewIngredient_Desktab.Visibility = Visibility.Collapsed;
                }
            }
        }

        // When the new ingredient textboxes loose focus, the are collapsed
        private void Add_Ingredient_LostFocus(object sender, RoutedEventArgs e)
        {
            NewIngredient_Phone.Visibility = Visibility.Collapsed;
            NewIngredient_Desktab.Visibility = Visibility.Collapsed;
        }

        // When checkboxes in the Purchased column are checked, this moves the checkbox to the List
        private void ToPurchased_Checked(object sender, RoutedEventArgs e)
        {
            // Gets name and content from checked checkbox
            CheckBox control = sender as CheckBox;
            string name = control.Name;
            object content = control.Content;

            // Gets parent of sender for use in determining correct destination stackpanel
            FrameworkElement parent = (FrameworkElement)((CheckBox)sender).Parent;
            string ParentName = parent.Name;

            // Creates new checkbox (desktop)
            CheckBox newPurchasedCheckbox = new CheckBox();
            newPurchasedCheckbox.Name = name;
            newPurchasedCheckbox.Content = content;
            newPurchasedCheckbox.IsChecked = true;
            newPurchasedCheckbox.Unchecked += new RoutedEventHandler(ToList_Checked);

            // Removes checkbox from list and moves to purchased
            if (ParentName == "Shoppinglist_Stackpanel_Desktab")
            {
                Shoppinglist_Stackpanel_Desktab.Children.Remove((UIElement)this.FindName(name));
                RecentlyPurchased_Stackpanel_Desktab.Children.Insert(0, newPurchasedCheckbox);
            }
            else if (ParentName == "Shoppinglist_Stackpanel_Phone")
            {
                Shoppinglist_Stackpanel_Phone.Children.Remove((UIElement)this.FindName(name));
                RecentlyPurchased_Stackpanel_Phone.Children.Insert(0, newPurchasedCheckbox);
            }

            // Removes all checkboxes over 10 in the purchased stackpanel
            if (RecentlyPurchased_Stackpanel_Desktab.Children.Count == 10)
            {
                RecentlyPurchased_Stackpanel_Desktab.Children.RemoveAt(9);
            }
            if (RecentlyPurchased_Stackpanel_Phone.Children.Count == 10)
            {
                RecentlyPurchased_Stackpanel_Phone.Children.RemoveAt(9);
            }

            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "purchased_click", "Purchased: from ShoppingList", 0);
        }

        // When checkboxes in the List column are checked, this moves the checkbox to the Purchased column
        private void ToList_Checked(object sender, RoutedEventArgs e)
        {
            // Gets name and content from checked checkbox
            CheckBox control = sender as CheckBox;
            string name = control.Name;
            object content = control.Content;

            // Gets parent of sender for use in determining correct destination stackpanel
            FrameworkElement parent = (FrameworkElement)((CheckBox)sender).Parent;
            string ParentName = parent.Name;

            // Creates new checkbox (desktop)
            CheckBox newListCheckbox = new CheckBox();
            newListCheckbox.Name = name;
            newListCheckbox.Content = content;
            newListCheckbox.IsChecked = false;
            newListCheckbox.Checked += new RoutedEventHandler(ToPurchased_Checked);

            if (ParentName == "RecentlyPurchased_Stackpanel_Desktab")
            {
                // Removes checkbox from purchased and moves to list (desktop)
                RecentlyPurchased_Stackpanel_Desktab.Children.Remove((UIElement)this.FindName(name));
                Shoppinglist_Stackpanel_Desktab.Children.Add(newListCheckbox);
            }
            else if (ParentName == "RecentlyPurchased_Stackpanel_Phone")
            {
                // Removes checkbox from purchased and moves to list (phone0
                RecentlyPurchased_Stackpanel_Phone.Children.Remove((UIElement)this.FindName(name));
                Shoppinglist_Stackpanel_Phone.Children.Add(newListCheckbox);
            }
        }
    }
}
