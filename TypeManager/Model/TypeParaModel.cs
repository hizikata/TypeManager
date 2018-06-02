using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeManager.Model
{
    public class TypeParaModel
    {
        public TypeParaModel(string productType,int typeLength,string typeSign,string apdSign,
            string typeCatelog,string snSign,string TeCaculataMethod,string packingType,
            int isSmsrSpotTest,int isImd2SpotTest,int typeVisible,string materialId,string 
            productTypeCommon,int checkSnSubLength,string yearSign,
               string  alignType,int isUploadHwData,int isChangeToRedoData,string housingLaserSign,
               string thOneTestType,string transmitRate,string idKey)
        {
            this.ProductTypeName = productType;
            this.TypeLength = typeLength;
            this.TypeSign = typeSign;
            this.ApdSign = apdSign;
            this.TypeCatalog = typeCatelog;
            this.SnSign = snSign;
            this.TeCaculateMethod = TeCaculataMethod;
            this.PackingType = packingType;
            this.IsSmsrSpotTest = isSmsrSpotTest;
            this.IsImd2SpotTest = isImd2SpotTest;
            this.TypeVisible = typeVisible;
            this.MaterialId = materialId;
            this.ProductTypeCommon = productTypeCommon;
            this.CheckSnSubLength = checkSnSubLength;
            this.YearSign = yearSign;
            this.AlignType = alignType;
            this.IsUploadHwData = isUploadHwData;
            this.IsChangeToRedoData = isChangeToRedoData;
            this.HousingLaserSign = housingLaserSign;
            this.ThOneTestType = thOneTestType;
            this.TransmitRate = transmitRate;
            this.IdKey = idKey;
        }
        //成员名不能与类名相同
        public string ProductTypeName { get; set; }
        public int TypeLength { get; set; }
        public string TypeSign { get; set; }
        public string ApdSign { get; set; }
        public string TypeCatalog { get; set; }
        public string SnSign { get; set; }
        public string TeCaculateMethod { get; set; }
        public string PackingType { get; set; }
        public int IsSmsrSpotTest { get; set; }
        public int IsImd2SpotTest { get; set; }
        public int TypeVisible { get; set; }
        public string MaterialId { get; set; }
        public string ProductTypeCommon { get; set; }
        public int CheckSnSubLength { get; set; }
        public string YearSign { get; set; }
        public string AlignType { get; set; }
        public int IsUploadHwData { get; set; }
        public int IsChangeToRedoData { get; set; }
        public string HousingLaserSign { get; set; }
        public string ThOneTestType { get; set; }
        public string TransmitRate { get; set; }
        public string  IdKey { get; set; }
    }
}
