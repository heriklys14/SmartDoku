using SmartDoku.Main.Model;
using System;

namespace SmartDoku.Main.IService
{
  public interface ISDService : IDisposable
  {
    void AlteraValorCelula(SDMatrizModel matriz, int linha, int coluna, int? valor);

    void AjustaStatusCelula(SDMatrizModel matriz, SDCelulaModel celulaAlterada);

    void GeraDigitosIniciais(SDMatrizModel matriz, int qtdeDigitosIniciais);
  }
}
