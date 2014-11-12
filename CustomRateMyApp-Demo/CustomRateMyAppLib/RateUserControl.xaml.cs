using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CustomRateMyAppLib
{
    public partial class RateUserControl : UserControl
    {
        //private readonly IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private readonly ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public RateUserControl()
        {
            InitializeComponent();
            Loaded += RateUserControl_Loaded;
        }

        void RateUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (localSettings.Values["RateReminderIsAlreadyShowed"] != null
                && localSettings.Values["RateReminderIsAlreadyShowed"].ToString().Contains("true"))
            {
                RateUserGrid.Visibility = Visibility.Collapsed;
            }
        }
        
        private void grRating_Tapped(Object sender, TappedRoutedEventArgs e)
        {
            //User has rated 
            Rate(RatingEnum.Rated);
        }
        
        public async void Rate(RatingEnum rating)
        {
            localSettings.Values["RateReminderIsAlreadyShowed"] = "true";

            if (rating == RatingEnum.DoNotAsk)
            {
                return;
            }
            else if (rating == RatingEnum.Rated)
            {
                #if WINDOWS_PHONE_APP
                    var marketplaceReviewTask = new MarketplaceReviewTask();
                    marketplaceReviewTask.Show();
                    return;
                #endif

                Uri uri = new Uri(String.Concat("ms-windows-store:REVIEW?PFN=", Package.Current.Id.FamilyName));
                await Launcher.LaunchUriAsync(uri);
                return;
            }
        }

        private void tbDoNotAsk_Tapped(Object sender, TappedRoutedEventArgs gestureEventArgs)
        {
            Rate(RatingEnum.DoNotAsk);
        }

        private void tbLater_Tapped(Object sender, TappedRoutedEventArgs gestureEventArgs)
        {
            Rate(RatingEnum.Later);
        }

        //public event EventHandler Rated;
        public RatingEnum Rating
        {
            get;
            set;
        }
    }
}