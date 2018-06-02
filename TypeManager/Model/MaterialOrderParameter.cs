using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeManager.Model
{
    public class MaterialOrderParameter
    {
        public string MaterialName { get; set; }
        public string MaterialSpecify { get; set; }
        public string MaterialSupplier { get; set; }
        public string MaterialMakePlace { get; set; }
        public int Id_Key { get; set; }
        public MaterialOrderParameter(string materialName,string materialSpecify,string materialSupplier,
            string materialMakePlace,int id_Key)
        {
            this.MaterialName = materialName;
            this.MaterialSpecify = materialSpecify;
            this.MaterialSupplier = materialSupplier;
            this.MaterialMakePlace = materialMakePlace;
            this.Id_Key = id_Key;
        }
    }
}
