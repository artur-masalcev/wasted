﻿using System.Collections.Generic;
using System.Linq;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted.Dummy_API
{
    public static class BusinessUtils
    {
        /// <summary>
        /// Dictionary from place type to places of that type
        /// </summary>
        public static Dictionary<string, List<FoodPlace>> PlaceTypeDictionary { get; set; } 
            = new Dictionary<string, List<FoodPlace>>();
    }

}