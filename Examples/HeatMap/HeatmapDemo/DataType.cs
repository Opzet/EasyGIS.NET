using System;
using System.Collections.Generic;
using System.Text;

namespace HeatMap
{
    /// <summary>
    /// Original code https://github.com/RainkLH/HeatMapSharp
    /// </summary>
    public class DataType
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Weight { get; set; }

        public DataType(int x, int y, double weight)
        {
            X = x;
            Y = y;
            Weight = weight;
        }

        public DataType()
        {
        }
    }

}
