using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeManager.Model
{
    public class TemplateRedoSN
    {
        public string ProductSign { get; set; }
        public string PrintProductType { get; set; }
        public string ProductType { get; set; }
        public string SnType { get; set; }
        public string TrayLabelTemplate { get; set; }
        public string BoxingLabelTemplate { get; set; }
        public string OutBoxLabelTemplate { get; set; }
        public string HwUpdataPN { get; set; }
        public string Memo { get; set; }
        public int Id_Key { get; set; }
        public TemplateRedoSN(string productSign,string printProductType,string productType,
            string snType,string trayLabelTemplate,string boxingLabelTemplate,string outBoxLabelTemplate,
            string hwUpdatePN,string memo,int id_Key)
        {
            this.ProductSign = productSign;
            this.PrintProductType = printProductType;
            this.ProductType = productType;
            this.SnType = snType;
            this.TrayLabelTemplate = trayLabelTemplate;
            this.BoxingLabelTemplate = boxingLabelTemplate;
            this.OutBoxLabelTemplate = outBoxLabelTemplate;
            this.HwUpdataPN = hwUpdatePN;
            this.Memo = memo;
            this.Id_Key = id_Key;
        }
    }
}
