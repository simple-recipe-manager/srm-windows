using GoogleAnalytics;
using GoogleAnalytics.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
namespace Whiskly
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = Windows.Graphics.Display.DisplayOrientations.Landscape | Windows.Graphics.Display.DisplayOrientations.LandscapeFlipped;

            screenLock();

            commonCompLoad();
        }

        public string adFree;

        //#region Saving / Loading MyData 
        //private const string _myFileLocation = "RecipeStore.txt";

        //public static async Task<List<string>> GetMyData()
        //{
        //    // If you're saving your stuff just on this device
        //    var readStream =
        //        await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_myFileLocation);

        //    // If there is no MyData, then we haven't created our MyData file yet
        //    if (readStream == null)
        //        return new List<string>(); ;

        //    DataContractSerializer stuffSerializer =
        //        new DataContractSerializer(typeof(List<string>));

        //    var setResult = (List<string>)stuffSerializer.ReadObject(readStream);

        //    return setResult;
        //}

        //public static async Task<bool> SaveMyData(List<string> saveData)
        //{
        //    try
        //    {

        //        StorageFile savedStuffFile =
        //            await ApplicationData.Current.LocalFolder.CreateFileAsync(_myFileLocation,
        //            CreationCollisionOption.ReplaceExisting);

        //        using (Stream writeStream =
        //            await savedStuffFile.OpenStreamForWriteAsync())
        //        {
        //            DataContractSerializer stuffSerializer =
        //                new DataContractSerializer(typeof(List<string>));

        //            stuffSerializer.WriteObject(writeStream, saveData);
        //            await writeStream.FlushAsync();
        //            writeStream.Dispose();
        //        }
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("ERROR: unable to save MyData", e);
        //        //return false;
        //    }
        //}
        //#endregion

        private void commonCompLoad()
        {
            CommonCompClass.Categories = new List<CatClass>();
            CommonCompClass.Categories.Add(new CatClass(0, "Appetizer"));
            CommonCompClass.Categories.Add(new CatClass(1, "Breakfast & Brunch"));
            CommonCompClass.Categories.Add(new CatClass(2, "Cocktails"));
            CommonCompClass.Categories.Add(new CatClass(3, "Desserts"));
            CommonCompClass.Categories.Add(new CatClass(4, "Drinks"));
            CommonCompClass.Categories.Add(new CatClass(5, "Main Dishes"));
            CommonCompClass.Categories.Add(new CatClass(6, "Pasta"));
            CommonCompClass.Categories.Add(new CatClass(7, "Salad"));
            CommonCompClass.Categories.Add(new CatClass(8, "Seafood"));
            CommonCompClass.Categories.Add(new CatClass(9, "Soup"));
            CommonCompClass.Categories.Add(new CatClass(10, "Vegetarian"));
        }

        public DateTime startTime = DateTime.Now;

        public void screenLock()
        {
            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1))
            {
                Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = Windows.Graphics.Display.DisplayOrientations.PortraitFlipped | Windows.Graphics.Display.DisplayOrientations.Portrait;
            }
            else
            {
                Windows.Graphics.Display.DisplayInformation.AutoRotationPreferences = Windows.Graphics.Display.DisplayOrientations.Landscape | Windows.Graphics.Display.DisplayOrientations.LandscapeFlipped;
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            screenLock();
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity

            // track a timing (how long it takes your app to run a specific task)
            GoogleAnalytics.EasyTracker.GetTracker().SendTiming(DateTime.Now.Subtract(startTime), "App View Time", "App", "Exit By OnSuspend");
            deferral.Complete();
        }
    }
}
