using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class Rssi1Set:SenParaBase
    {
        const string Name = "Rssi1Set";
        public Rssi1Set(bool enableTest,float max,float min) : base(Name,enableTest, max, min)
        {
            ParaName = "Rssi1Set";
        }
    }
}
