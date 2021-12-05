﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DataAPI.DTO;
using Wasted.DummyDataAPI;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted.Dummy_API.Business_Objects
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