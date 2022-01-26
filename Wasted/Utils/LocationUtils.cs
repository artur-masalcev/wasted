using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Wasted.Utils
{
    public class LocationUtils
    {
        public static async Task<Location> GetLocation(Page page)
        {
            Location location = null;
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync() ??
                           await Geolocation.GetLocationAsync(
                               new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(5))
                           );
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Error", ex.Message, "OK");
            }

            return location;
        }
    }
}