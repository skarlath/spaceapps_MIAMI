using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using MapOverlay.DataStore;

namespace MapOverlay
{
    class GridDataSetManager
    {
        public Dictionary<String, GridDataPoints> DataSets;
        public Datastore DS;
        public GridDataSetManager()
        {
            DataSets = new Dictionary<string, GridDataPoints>();
            DS = new Datastore();
            init();
        }

        public void init()
        {
            DS.importRainfallXML();
            DS.fillGridDataStoreManager(ref DataSets);
        }
    }
}
