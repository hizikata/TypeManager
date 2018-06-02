using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class VbrSet:SenParaBase
    {
        const string Name = "VbrSet";
        #region Properties
        public bool MasurementType { get; set; }
        public float TargetCurrent { get; set; }
        public int ComplainceVolt { get; set; }
        public int DelayTime { get; set; }  
        public string IntegType { get; set; }
        public string Range { get; set; }
        public float VbrOffSet { get; set; }
        public int AssignedVbr { get; set; }
        public float ComplainceCurrent { get; set; }
        public bool VbrMultiply { get; set; }
        public int StartVol { get; set; }
        public int StopVol { get; set; }
        public float StepVol { get; set; }        
        #endregion  

        #region Constructors
        public VbrSet(bool measureType,float targetcurrent,int complaincevolt,
        int delaytime,string integtype,string range,float vbroffset,int assignedvbr,
        float complaincecurrent,bool vbrmultiply,int startvol,int stopvol,
        float stepvol,bool enableTest,float max,float min):base(Name,enableTest,max,min)
        {
            this.MasurementType=measureType;
            this.TargetCurrent=targetcurrent;
            this.ComplainceVolt=complaincevolt;
            this.DelayTime=delaytime;
            this.IntegType=integtype;
            this.Range=range;
            this.VbrOffSet=vbroffset;
            this.AssignedVbr=assignedvbr;
            this.ComplainceCurrent=complaincecurrent;
            this.VbrMultiply=vbrmultiply;
            this.StartVol=startvol;
            this.StopVol=stopvol;
            this.StepVol=stepvol;
        }
        #endregion
    }
}
