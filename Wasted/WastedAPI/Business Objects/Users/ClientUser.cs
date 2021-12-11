using System.Collections.Generic;
using DataAPI.DTO;
using Wasted.Pages.Place;
using Xamarin.Forms;

namespace Wasted.WastedAPI.Business_Objects.Users
{
    public class ClientUser : User
    {
        public List<RatingDTO> Ratings { get; set; }

        public override void PushPage(ContentPage page)
        {
            PageManager.PushClientPage(page);
        }

        public override void CreateUser()
        {
            DataProvider.CreateClientUser(this);
        }
    }
}