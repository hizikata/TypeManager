using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class SenSet:SenParaBase
    {
        const string Name = "SenSet";
        #region Properties

        #endregion
        public SenSet(bool enableTest, float max, float min) : base(Name,enableTest, max, min)
        {
            ParaName = "SenSet";
        }
    }
}
