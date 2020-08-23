using SmartDoku.Main.Model;
using SmartDoku.Main.IService;
using System.Collections.Generic;
using System.Linq;
using SmartDoku.Main.Service;

namespace SmartDoku.Main.Controller
{
  public class SDUtils
  {
    public void AlteraValorCelula(SDMatrizModel matriz, int linha, int coluna, int? valor)
    {
      using (ISDService sdService = new SDService())
      {
        sdService.AlteraValorCelula(matriz, linha, coluna, valor);
      }
    }

    public void ValidaAlteracaoCelula(SDMatrizModel matriz, SDCelulaModel celulaAlterada)
    {
      using (ISDService sdService = new SDService())
      {
        sdService.AjustaStatusCelula(matriz, celulaAlterada);
      }
    }

    public void GeraDigitosIniciais(SDMatrizModel matriz, int qtdeDigitosIniciais)
    {
      using (ISDService sdService = new SDService())
      {
        sdService.GeraDigitosIniciais(matriz, qtdeDigitosIniciais);
      }
    }
  }
}
