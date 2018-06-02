using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeManager.Model
{
    public class ProductParameter
    {
        
        public string ProductType { get; set; }
        public string ProductStation { get; set; }
        public string ParameterName { get; set; }
        public string ParameterMax { get; set; }
        public string ParameterMin { get; set; }
        public string ParameterMemo { get; set; }
        public string TempSign { get; set; }
        public int Id_Key { get; set; }
        public ProductParameter(string productType,string productStation,string parameterName,string parameterMax,
            string parameterMin,string parameterMemo,string tempSign,int idKey)
        {
            this.ProductType = productType;
            this.ProductStation = productStation;
            this.ParameterName = parameterName;
            this.ParameterMax = parameterMax;
            this.ParameterMin = parameterMin;
            this.ParameterMemo = parameterMemo;
            this.TempSign = tempSign;
            this.Id_Key = idKey;

        }
    }
}
