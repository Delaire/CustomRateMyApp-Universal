using System;
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

namespace CustomRateMyApp_Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ShowReminders();
        }

        #region Rate My App

        private void ShowReminders()
        {
            if (!App.HasSeenReminderInSession)
            {
                ///we could image that if the user has not rated the app 
                ///and clicked on Later we could show the rate my app later
                //if (App.RunCount%10 != 0)
                {
                    ///We could also image that we show the rate my app only once the 
                    /// app has been launched 5 times for example and also we check to see if
                    /// the user has not already rated the app. These two properties would need 
                    /// to be saved somewhere so that we could load then at the start of the application
                    /// Another projet on Github will be publish to show you how I store data in a  
                    /// settings class
                    // if (App.RunCount >= 5 && !App.RateReminderIsAlreadyShowed)
                    {
                        //Rating
                        ShowRateUserControl();
                    }
                }
            }
        }

        public void ShowRateUserControl()
        {
            //Show Rating
            RateControlMain.Visibility = Visibility.Visible;
        }

        private void MainGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //if main windows is tapped and User control is visible
            if (RateControlMain.Visibility == Visibility.Visible)
            {
                RateControlMain.Visibility = Visibility.Collapsed;
                RateControlMain.Rate(RatingEnum.Later);
            }
        }
        #endregion
    }
}
