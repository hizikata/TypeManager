using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class GainSet:SenParaBase
    {
        const string Name = "GainSet";
        public GainSet(bool enableTest,float max,float min):base(Name,enableTest,max,min)
        {
            
        }
    }
}
