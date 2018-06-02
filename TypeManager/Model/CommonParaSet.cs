using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeManager.Model
{
    public class CommonParaSet
    {
        public string ApplicationName { get; set; }
        public string ProductType { get; set; }
        public string ProductStation { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string CurrentTemperature { get; set; }
        public string Memo { get; set; }
        public string OpPerson { get; set; }
        public string OpDate { get; set; }
        public int Id_Key { get; set; }
        public CommonParaSet(string applicationName,string productType,string productStation,string parameterName,
            string parameterValue,string currentTemperature,string memo,string opPerson,string opDate,int id_Key)
        {
            this.ApplicationName = applicationName;
            this.ProductType = productType;
            this.ProductStation = productStation;
            this.ParameterName = parameterName;
            this.ParameterValue = parameterValue;
            this.CurrentTemperature = currentTemperature;
            this.Memo = memo;
            this.OpPerson = opPerson;
            this.OpDate = opDate;
            this.Id_Key = id_Key;
        }

    }
}
