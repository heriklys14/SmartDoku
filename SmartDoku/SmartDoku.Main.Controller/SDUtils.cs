using SmartDoku.Main.Model;
using SmartDoku.Main.IService;
using SmartDoku.Main.Service;
using System;

namespace SmartDoku.Main.Controller
{
  public class SDUtils
  {
    public void AlteraValorCelula(SDMatrizModel matriz, SDCelulaModel celulaAlterada, SDCelulaModel oldCelula)
    {
      using (ISDService sdService = new SDService())
      {
        sdService.AlteraValorCelula(matriz, celulaAlterada, oldCelula);
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

    public static int? ConvertStringToInt32Nullable(object objeto)
    {
      string value = Convert.ToString(objeto);

      if (string.IsNullOrEmpty(value))
      {
        return null;
      }
      else
      {
        int? result = Convert.ToInt32(value);

        return result;
      }
    }
  }
}
