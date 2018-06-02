using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class ProductType
    {
        #region Properties
        #region Attributes
        public string Name { get; set; }
        public bool Show { get; set; }
        #endregion
        #region Elements
        public TiaSet TiaSet { get; set; }
        public VbrSet VbrSet { get; set; }
        public IopSet IopSet { get; set; }
        public IoSet IoSet { get; set; }
        public IdarkSet IdarkSet { get; set; }
        public TxCrossTalkSet TxCrossTalkSet { get; set; }
        public GainSet GainSet { get; set; }
        public SenSet SenSet { get; set; }
        public SatSet SatSet { get; set; }
        public Rssi1Set Rssi1Set { get; set; }
        public Rssi2Set Rssi2Set { get; set; }
        #endregion


        #endregion

    }
}
