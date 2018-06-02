using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeManager.Model
{
    public class ProductMatchSpecify
    {
        public string ProductType { get; set; }
        public string LD_Spec { get; set; }
        public string APD_Spec { get; set; }
        public string PD_Spec { get; set; }
        public string Isolator_Spec { get; set; }
        public string ZeroFilter_Spec { get; set; }
        public string FortyFiveFilter_Spec { get; set; }
        public int Id_Key { get; set; }
        public ProductMatchSpecify(string productType, string LD_Spec, string APD_Spec, string PD_Spec,
            string isolator_Spec, string zeroFilter_Spec, string fortyFiveFilter_Spec, int id_Key)
        {
            this.ProductType = productType;
            this.LD_Spec = LD_Spec;
            this.APD_Spec = APD_Spec;
            this.PD_Spec = PD_Spec;
            this.Isolator_Spec = isolator_Spec;
            this.ZeroFilter_Spec = zeroFilter_Spec;
            this.FortyFiveFilter_Spec = fortyFiveFilter_Spec;
            this.Id_Key = id_Key;
        }
        //定义索引器
        public string this[string str]
        {
            get
            {
                switch (str)
                {
                    case "ProductType":
                        return this.ProductType;
                    case "LD_Spec":
                        return LD_Spec;
                    case "APD_Spec":
                        return APD_Spec;
                    case "PD_Spec":
                        return PD_Spec;
                    case "Isolator_Spec":
                        return Isolator_Spec;
                    case "ZeroFilter_Spec":
                        return ZeroFilter_Spec;
                    case "FortyFiveFilter_Spec":
                        return FortyFiveFilter_Spec;
                    case "Id_Key":
                        return Id_Key.ToString();
                    default:
                        throw new Exception("索引值错误");
                }
            }
        }
    }
}
