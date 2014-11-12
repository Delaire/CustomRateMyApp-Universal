using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;
using Windows.System;

namespace CustomRateMyApp_Demo.Helper
{
    public class ShareLogic
    {
        public static async void SendFeedback()
        {
            Object[] major = new Object[] { Package.Current.Id.Version.Major, Package.Current.Id.Version.Minor, Package.Current.Id.Version.Build, Package.Current.Id.Version.Revision };

            Object[] feedbackEmail = new Object[] { "YOUR.EMAIL@Outlouk.com", null, null };
            Object[] applicationFullTitle = new Object[] { String.Format("{0}.{1}.{2}.{3}", major) };
            feedbackEmail[1] = String.Format("{0}, Feedback, Windows 8", applicationFullTitle);
            feedbackEmail[2] = "Description of a problem goes here.";
            Uri uri = new Uri(String.Format("mailto:{0}?subject={1}&body={2}", feedbackEmail));
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
