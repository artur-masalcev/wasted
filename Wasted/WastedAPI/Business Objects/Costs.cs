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

        /// <summary>
        /// Percentage of current cost compared to previous cost. Used in ordering deals
        /// </summary>
        public double PriceChange()
        {
            return CurrentCost / PreviousCost;
        }
    }
}
