

using System.Collections.Generic;

namespace SmartDoku.Main.Model
{
  public class SDCelulaModel
  {
    public SDCelulaModel()
    {
      ModifiedRegisters = new List<object>();
    }

    public List<object> ModifiedRegisters { get; set; }

    public int PosicaoLinha { get; set; }
    public int PosicaoColuna { get; set; }
    public int? Valor
    {
      get { return _valor; }
      set
      {
        _valor = value;
      }
    }
    private int? _valor;

    public bool isCelulaValida { get; set; } = true;

    public override bool Equals(object obj)
    {
      var celula = obj as SDCelulaModel;
      return celula != null &&
             PosicaoLinha == celula.PosicaoLinha &&
             PosicaoColuna == celula.PosicaoColuna;
    }
  }
}
