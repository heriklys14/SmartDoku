using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoku.Main.Model
{
  public class SDQuadranteModel
  {
    public SDQuadranteModel()
    {
      Celulas = new List<SDCelulaModel>();
    }

    public List<SDCelulaModel> Celulas { get; set; }
  }
}
