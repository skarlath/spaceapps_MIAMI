using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using GMap.NET;

namespace MapOverlay
{
    class GridDataPoint
    {
        //public PointLatLng coord;
        public int value;
        //public SizeLatLng scale;
        public RectLatLng loc;
        public GridDataPoint(RectLatLng location, int Value)
        {
            value = Value;
            //Console.WriteLine("Point created: " + location.Top + ", " + location.Left + ", " + location.Right + ", " + location.Bottom + " - " + Value);
            loc = location;
        }
    }

    
}
