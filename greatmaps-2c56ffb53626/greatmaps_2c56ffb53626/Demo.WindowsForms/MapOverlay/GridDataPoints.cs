using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace MapOverlay
{
    class GridDataPoints
    {
        public List<GridDataPoint> points;
        public string DataSetName;
        public GridDataPoints(String name)
        {
            points = new List<GridDataPoint>();
            DataSetName = name;
        }

        public void addPoint(GridDataPoint point)
        {
            points.Add(point);
        }

        public void fillME()
        {

            for (int i = 0; i < 50; i++)
            {
                
                addPoint(new GridDataPoint(new GMap.NET.RectLatLng(24.25 + ((i % 10) * .05), -81 + ((i / 10) * .05), .05,  .05), i + 50));
            }
        }
    }
}
