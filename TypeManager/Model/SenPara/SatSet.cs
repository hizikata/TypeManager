using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class SatSet:SenParaBase
    {
        const string Name = "SatSet";
        public SatSet(bool enableTest, float max, float min) : base(Name,enableTest, max, min)
        {
            ParaName = "SatSet";
        }
    }
}
