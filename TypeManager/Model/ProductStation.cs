using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeManager.Model
{
    public class ProductStation
    {
        public string StationID { get; set; }
        public string StationName { get; set; }
        public int StationVisible { get; set; }
        public string ProductType { get; set; }
        public int Id_Key { get; set; }
        public ProductStation(string stationID,string stationName,int stationVisible,string productType,int id_Key)
        {
            this.StationID = stationID;
            this.StationName = stationName;
            this.StationVisible = stationVisible;
            this.ProductType = productType;
            Id_Key = id_Key;
        }
    }
}
