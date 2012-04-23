using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using GMap.NET;
using System.Drawing;
using GMap.NET.WindowsForms;
//using GMap.NET.MapProviders;
using Demo.WindowsForms;

namespace MapOverlay
{
    class OverlayProcessor
    {
        public GMap.NET.WindowsForms.GMapControl Control;
        public Bitmap curLayer;
        public List<Bitmap> Layers;
        public Graphics g;
        public RectLatLng ViewArea;
        public GridDataPoints activePoints;
        public GridDataPoints allPoints;
        public GridDataSetManager GDSM;
        GMapOverlay mainOverlay;

        public OverlayProcessor(ref Map control, ref GridDataSetManager PointManager)
        {
            Control = control;
            GDSM = PointManager;
            Layers = new List<Bitmap>();
            mainOverlay = new GMapOverlay();
            Control.Overlays.Add(mainOverlay);
        }

        public void makeLayer(String RedDataSetName, String GreenDataSetName, String BlueDataSetName)//, int Alpha)
        {
            //blue only
            ViewArea = Control.ViewArea;
            //int x = (int)Control.FromLatLngToLocal(ViewArea.LocationRightBottom).X;
            curLayer = new Bitmap((int)Control.FromLatLngToLocal(ViewArea.LocationRightBottom).X,(int) Control.FromLatLngToLocal(ViewArea.LocationRightBottom).Y);
            g = System.Drawing.Graphics.FromImage(curLayer);
            //g.Clear(Color.Black);
            //
            allPoints = GDSM.DataSets[BlueDataSetName];
            Console.WriteLine(BlueDataSetName + GDSM.DataSets.ContainsKey(BlueDataSetName));
               
            activePoints = activeData(allPoints, ViewArea, BlueDataSetName);
            GPoint TL;
            GPoint BR;
            foreach (GridDataPoint p in activePoints.points)
            {
                TL = Control.FromLatLngToLocal(p.loc.LocationTopLeft);
                BR = Control.FromLatLngToLocal(p.loc.LocationRightBottom);
                //Console.WriteLine("Drawing: [" + TL.X + ", " + TL.Y + "] [" + BR.X + ", " + BR.Y + "] - " + p.value + " - (" + p.loc.LocationRightBottom.Lat + ", " + p.loc.LocationRightBottom.Lng +")");
                g.FillRectangle(new SolidBrush(Color.FromArgb(125, Color.FromArgb(0,0,p.value))),new Rectangle((int)TL.X, (int)TL.Y, (int)(BR.X - TL.X), (int)(BR.Y - TL.Y)));
            }
        
            g.Flush();
            Layers.Add(curLayer);

        }

        public void addLayers()
        {
            
           
            

            GMapImage layer;
            foreach (Bitmap b in Layers)
            {
                layer = new GMapImage(ViewArea.LocationTopLeft);
                layer.Image = b;
                mainOverlay.Markers.Clear();
                mainOverlay.Markers.Add(layer);
            }
        }

        public GridDataPoints activeData(GridDataPoints allPoints, RectLatLng bounds, String id)
        {
            GridDataPoints activePoints = new GridDataPoints(id);
            foreach (GridDataPoint p in allPoints.points)
            {
                if (OverlayProcessor.RectLatLngCollide(p.loc,bounds))
                {
                    activePoints.addPoint(p);
                }
            }
            return activePoints;
        }



        public static bool RectLatLngCollide(RectLatLng a, RectLatLng b)
        {
            if ((a.Left > b.Left && a.Left < b.Right) || (b.Left > a.Left && b.Left > a.Right))
            {
                if ((a.Top < b.Top && a.Top < b.Top) || (b.Top > a.Top && b.Top > a.Top))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
