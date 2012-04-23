using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace MapOverlay.DataStore
{
    class rainfallImportXML
    {
        XmlDataDocument Stations;
        XmlDataDocument Readings;
        Dictionary<string, Station> dStations;
        List<Reading> dReadings;
        Dictionary<string, StationReading> dStationReadings;


        public rainfallImportXML(String StationsFile, String ReadingsFile, ref Dictionary<string, List<StationReading>> precip)
        {
            Stations = new XmlDataDocument();
            Readings = new XmlDataDocument();
            dStations = new Dictionary<string, Station>();
            dReadings = new List<Reading>();
            dStationReadings = new Dictionary<string, StationReading>();

            StreamReader sr = new StreamReader(StationsFile);
            String xmlStr = sr.ReadToEnd();

            Stations.LoadXml(xmlStr);
            sr = new StreamReader(ReadingsFile);
            xmlStr = sr.ReadToEnd();

            Readings.LoadXml(xmlStr);

            XmlNodeList root = Stations.GetElementsByTagName("site");
            foreach (XmlNode n in root)
            {
                dStations.Add(n.ChildNodes[1].InnerXml, new Station(n.ChildNodes[1].InnerXml, Double.Parse(n.ChildNodes[3].InnerXml), Double.Parse(n.ChildNodes[4].InnerXml), n.ChildNodes[2].InnerXml));    
            }
            root = Readings.GetElementsByTagName("element");
            foreach (XmlNode n in root)
            {
                dReadings.Add(new Reading(n.Attributes.GetNamedItem("station_id").Value, n.Attributes.GetNamedItem("date").Value, n.Attributes.GetNamedItem("precip_interval").Value, Double.Parse(n.Attributes.GetNamedItem("value").Value)));
            }
            Station curStation;
            foreach (Reading r in dReadings)
            {
                if (precip.ContainsKey(r.DataTime))
                {
                    curStation = dStations[r.StationID];
                    precip[r.DataTime].Add(new StationReading(curStation.Lat, curStation.Lon, r.Date, r.DataTime, r.Value));
                }
                else
                {
                    List<StationReading> lsr = new List<StationReading>();
                    curStation = dStations[r.StationID];
                    lsr.Add(new StationReading(curStation.Lat, curStation.Lon, r.Date, r.DataTime, r.Value));
                    precip.Add(r.DataTime, lsr);
                }
            }


        }
       
    }

    class StationReading
    {
        public double Lat;
        public double Lon;
        public String Date;
        public String DataTime;
        public double Value;

        public StationReading(double lat, double lon, String date, String dataTime, double value)
        {
            Lat = lat;
            Lon = lon;
            Date = date;
            DataTime = dataTime;
            Value = value;
        }
    }

    class Station
    {
        public String ID;
        public double Lat;
        public double Lon;
        public String Name;

        public Station(string id, double Latitude, double Longitude, String StationName)
        {
            ID = id;
            Lat = Latitude;
            Lon = Longitude;
            Name = StationName;
        }


    }

    class Reading
    {
        public String StationID;
        public String Date;
        public String DataTime;
        public double Value;

        public Reading(String statID, String date, String time, double value)
        {
            StationID = statID;
            Date = date;
            DataTime = time;
            Value = value;
        }
    }
}
