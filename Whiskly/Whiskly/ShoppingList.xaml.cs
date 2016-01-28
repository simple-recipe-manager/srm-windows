﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
            SearchField.Opacity = 1;
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

        private void Add_ToList_Clicked(object sender, RoutedEventArgs e)
        {
            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "addToList_click", "Add To List: from ShoppingList", 0);

            CheckBox newItemCheckbox = new CheckBox();
            newItemCheckbox.Name = "Ingredient_";
            newItemCheckbox.Content = "Ingredient_";

            this.Shoppinglist_Stackpanel_Desktab.Children.Add(newItemCheckbox);
        }

        private void SearchField_LostFocus(object sender, RoutedEventArgs e)
        {
            ExitStoryboard.Begin();

            SearchStackpanel.Background = new SolidColorBrush(Color.FromArgb(00, 00, 69, 0x5C));
            SearchIcon.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 00, 00, 00));
        }

        private void ToPurchased_Checked(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            //CheckBox control = sender as CheckBox;
            String name = control.Name;

            CheckBox newPurchasedCheckbox = new CheckBox();
            newPurchasedCheckbox.Name = name;
            newPurchasedCheckbox.Content = name;
            newPurchasedCheckbox.IsChecked = true;
            newPurchasedCheckbox.Checked += new RoutedEventHandler(ToList_Checked);

            //Shoppinglist_Stackpanel_Desktab.Children.IndexOf(sender as UIElement));
            Shoppinglist_Stackpanel_Desktab.Children.Remove((UIElement)this.FindName(name));
            RecentlyPurchased_Stackpanel_Desktab.Children.Insert(0, newPurchasedCheckbox);

            // track a custom event
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("ui_action", "purchased_click", "Purchased: from ShoppingList", 0);
        }

        private void ToList_Checked(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            //CheckBox control = sender as CheckBox;
            String name = control.Name;

            CheckBox newListCheckbox = new CheckBox();
            newListCheckbox.Name = name;
            newListCheckbox.Content = name;
            newListCheckbox.IsChecked = false;
            newListCheckbox.Checked += new RoutedEventHandler(ToPurchased_Checked);

            RecentlyPurchased_Stackpanel_Desktab.Children.Remove((UIElement)this.FindName(name));
            Shoppinglist_Stackpanel_Desktab.Children.Add(newListCheckbox);
        }
    }
}
