using System.Collections.Generic;
using Wasted.DummyAPI.BusinessObjects;

namespace Wasted.Utils
{
    public static class Extensions
    {
        public static void PutDefaultKey(this Dictionary<string, List<FoodPlace>> dictionary, string value, FoodPlace place)
        {
            if (!dictionary.ContainsKey(value))
                dictionary[value] = new List<FoodPlace>();
            dictionary[value].Add(place);                
        }
    }
}
