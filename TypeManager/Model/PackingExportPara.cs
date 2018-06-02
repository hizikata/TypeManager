using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeManager.Model
{
    public class PackingExportPara
    {
        public string ProductType { get; set; }
        public int StartRowIndex { get; set; }
        public int EndColIndex { get; set; }
        public string ExportText { get; set; }
        public string SetRowType { get; set; }
        public int IsNeedPrintPackingLabel { get; set; }
        public int IsExportAboutHW { get; set; }
        public string HwSnColNames { get; set; }
        public string HwVopColNames { get; set; }
        public string HwVbrColNames { get; set; }
        public string FillDataContent { get; set; }
        public int Id_Key { get; set; }
        public PackingExportPara(string productType,int startRowIndex,int endColIndex,string exportText,
            string setRowType,int isNeedPrintPackingLabel,int isExportAboutHW,string hwSnColNames,
            string hwVopColNames,string hwVbrColName,string fillDataContent,int id_Key)
        {
            this.ProductType = productType;
            this.StartRowIndex = startRowIndex;
            this.EndColIndex = endColIndex;
            this.ExportText = exportText;
            this.SetRowType = setRowType;
            this.IsNeedPrintPackingLabel = isNeedPrintPackingLabel;
            this.IsExportAboutHW = isExportAboutHW;
            this.HwSnColNames = hwSnColNames;
            this.HwVopColNames = hwVopColNames;
            this.HwVbrColNames = hwVbrColName;
            this.FillDataContent = fillDataContent;
            this.Id_Key = id_Key;
                
                
        }
    }
}
