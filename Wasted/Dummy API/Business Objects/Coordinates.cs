using System;
using System.Collections.Generic;
using System.Text;

namespace Wasted.Dummy_API.Business_Objects
{
    public struct Coordinates
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Coordinates(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
