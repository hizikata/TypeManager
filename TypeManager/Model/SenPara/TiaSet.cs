using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeManager.Model.SenPara
{
    public class TiaSet : SenParaBase
    {
        const String Name = "TiaSet";
        #region Properties
        public float DriveVolt { get; set; }
        public int HoldTime { get; set; }
        public float TiaOffsetCurrent { get; set; }
        #endregion
        #region Constructors
        public TiaSet(float driveVolt, int holdTime, float tiaoffset, bool enableTest,
            float max, float min) : base(Name,enableTest, max, min)
        {
            this.DriveVolt = driveVolt;
            this.HoldTime = holdTime;
            this.TiaOffsetCurrent = tiaoffset;
        }
        #endregion   
    }
}
