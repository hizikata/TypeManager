using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class IopSet:SenParaBase
    {
        const string Name = "IopSet";
		#region Properties
        public string AppliedV { get; set; }
        public int DelayTime { get; set; }
        public string IntegType { get; set; }
        public float Range { get; set; }
        public float Complaince { get; set; }
        public float LightPower { get; set; }
        //此节点有个Type属性
        public string VopCaluType { get; set; }
        #endregion
        #region Construtors
        public IopSet(string appliedv,int delaytime,string integtype,float range,
        float complaince,float lightpower,string vopcalutype,bool enableTest,float max,float min): base(Name,enableTest, max, min)
        {
            ParaName = "IopSet";
            this.AppliedV=appliedv;
            this.DelayTime=delaytime;
            this.IntegType=integtype;
            this.Range=range;
            this.Complaince=complaince;
            this.LightPower=lightpower;
            this.VopCaluType=vopcalutype;
        }
        #endregion
    }
}
