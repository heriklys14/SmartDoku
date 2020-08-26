using SmartDoku.Main.Model;
using System;

namespace SmartDoku.Main.IService
{
  public interface ISDService : IDisposable
  {
    void AlteraValorCelula(SDMatrizModel matriz, SDCelulaModel celulaAlterada, SDCelulaModel oldCelula);

    void AjustaStatusCelula(SDMatrizModel matriz, SDCelulaModel celulaAlterada);

    void GeraDigitosIniciais(SDMatrizModel matriz, int qtdeDigitosIniciais);

    void ResolveSudoku(SDMatrizModel matriz);
  }
}
