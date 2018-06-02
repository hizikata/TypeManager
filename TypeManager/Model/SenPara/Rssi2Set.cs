using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class Rssi2Set:SenParaBase
    {
        const string Name = "Rssi2Set";
        public Rssi2Set(bool enableTest, float max, float min) : base(Name,enableTest, max, min)
        {
            ParaName = "Rssi2Set";
        }
    }
}
