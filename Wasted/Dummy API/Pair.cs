using System;
using System.Collections.Generic;
using System.Text;

namespace Wasted.Dummy_API
{
    public class Pair<T, U>
    {
        public T First { get; set; }
        public U Second { get; set; }

        public Pair(T first, U second)
        {
            First = first;
            Second = second;
        }    
    };
}
