using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class SenParaBase
    {
        public string ParaName { get; set; }
        public bool EnableTest { get; set; }
        public float Max { get; set; }
        public float Min { get; set; }
        public SenParaBase(string paraName, bool enableTest,float max,float min)
        {
            this.ParaName = paraName;
            this.EnableTest = enableTest;
            this.Max = max;
            this.Min = min;
        }
    }
}
