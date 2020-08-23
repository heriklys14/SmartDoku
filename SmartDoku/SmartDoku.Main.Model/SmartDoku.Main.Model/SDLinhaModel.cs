using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoku.Main.Model
{
  public class SDLinhaModel
  {
    public SDLinhaModel()
    {
      Celulas = new List<SDCelulaModel>();
    }

    public int NumeroSequencial { get; set; }

    public List<SDCelulaModel> Celulas { get; set; }
  }
}
