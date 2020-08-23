using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoku.Main.Model
{
    public class SDColunaModel
    {
        public SDColunaModel()
        {
            Celulas = new List<SDCelulaModel>();
        }

        public int NumeroSequencial { get; set; }

        public List<SDCelulaModel> Celulas { get; set; }
    }
}
