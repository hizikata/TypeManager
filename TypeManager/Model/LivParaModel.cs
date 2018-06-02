using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model
{
    public class LivParaModel
    {
        public string ParaName { get; set; }
        public string ParaValue { get; set; }
        public LivParaModel(string paraName,string paraValue)
        {
            this.ParaName = paraName;
            this.ParaValue = paraValue;
        }
    }
}
