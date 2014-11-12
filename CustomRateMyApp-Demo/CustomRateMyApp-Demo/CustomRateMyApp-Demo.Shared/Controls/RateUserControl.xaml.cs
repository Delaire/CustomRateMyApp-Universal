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
using CustomRateMyApp_Demo;
using CustomRateMyApp_Demo.Helper;

namespace CustomRateMyApp_Demo
{
    public partial class RateUserControl : UserControl
    {
        private RectangleGeometry _rg = new RectangleGeometry();

        public RateUserControl()
        {
            InitializeComponent();
        }


        private void grRating_Tapped(Object sender, TappedRoutedEventArgs e)
        {
            ///Finding out how many stars where given for the app.
            Point position = e.GetPosition(GridRating);
            Int32 num = (Int32)Math.Round(position.X * 5 / GridRating.Width + 0.5);
            _rg.Rect = (new Rect(0, 0, (Double)num * GridRating.Width / 5, GridRating.Height));

            //this allows us to know if the user has rated over 3 stars or not, if not
            //then whe ask the user to send a feedback and then to rate the app
            Rating = (num >= 3 ? RatingEnum.Rated : RatingEnum.FeedBack);

            //User rating
            Rate(Rating);
        }

        public async void Rate(RatingEnum rating)
        {
            //so that we dont resee this afterwards
            App.HasSeenReminderInSession = true;

            //user does not wish to rate
            if (rating == RatingEnum.DoNotAsk)
            {
                return;
            }
            else if (rating == RatingEnum.Rated || rating == RatingEnum.FeedBack)
            {
                if (rating == RatingEnum.FeedBack)
                {
                    ShareLogic.SendFeedback();
                }
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