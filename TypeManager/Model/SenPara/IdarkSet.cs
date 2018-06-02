using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class IdarkSet:SenParaBase
    {
        const string Name = "IdarkSet";
        #region Properties
        public string AppliedV { get; set; }
        public int DelayTime { get; set; }
        public string IntegType { get; set; }
        public float Range { get; set; }
        public float Complaince { get; set; }
        public float LightPower { get; set; }
        #endregion
        #region Construtors
        public IdarkSet(string appliedv,int delaytime,string integtype,float range,
        float complaince,float lightpower,bool enableTest,float max,float min): base(Name,enableTest, max, min)
        {
            ParaName = "IdarkSet";
            this.AppliedV=appliedv;
            this.DelayTime=delaytime;
            this.IntegType=integtype;
            this.Range=range;
            this.Complaince=complaince;
            this.LightPower=lightpower;
        }
        #endregion

    }
}
