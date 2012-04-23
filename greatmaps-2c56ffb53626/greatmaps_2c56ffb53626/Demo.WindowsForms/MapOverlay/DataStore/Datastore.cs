using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MapOverlay;

namespace MapOverlay.DataStore
{
    class Datastore
    {
        //XmlDocument xDoc;
        public Dictionary<string, List<StationReading>> precip;

        public Datastore()
        {
            precip = new Dictionary<string, List<StationReading>>();
        }

        public void importRainfallXML()
        {
            rainfallImportXML imp = new rainfallImportXML("c:/DataSource/siteDescInfo.xml", "c:\\DataSource\\data.xml", ref precip);
        }

        public void fillGridDataStoreManager(ref Dictionary<String, GridDataPoints> ds)
        {
            //List<StationReading> lsr;

            foreach (List<StationReading> lsr in precip.Values)
            {
                foreach (StationReading sr in lsr)
                {
                    if (ds.ContainsKey(sr.DataTime))
                    {
                        ds[sr.DataTime].addPoint(makeGDP(sr));
                    }
                    else
                    {
                        GridDataPoints gds = new GridDataPoints(sr.DataTime);
                        gds.addPoint(makeGDP(sr));
                        ds.Add(sr.DataTime,gds);
                    }
                }
            }
        }

        public GridDataPoint makeGDP (StationReading sr)
        {
            return new GridDataPoint(new GMap.NET.RectLatLng(sr.Lat, sr.Lon, .1, .1), Math.Min((int)(sr.Value * 50),255));
        }
    }
}
