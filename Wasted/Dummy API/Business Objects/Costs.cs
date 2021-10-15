using System;
using System.Collections.Generic;
using System.Text;

namespace Wasted.Dummy_API.Business_Objects
{
    public struct Costs
    {
        public double PreviousCost { get; set; }
        public double CurrentCost { get; set; }

        public Costs(double previousCost, double currentCost)
        {
            PreviousCost = previousCost;
            CurrentCost = currentCost;
        }

        public double Change()
        {
            return CurrentCost / PreviousCost;
        }
    }
}
