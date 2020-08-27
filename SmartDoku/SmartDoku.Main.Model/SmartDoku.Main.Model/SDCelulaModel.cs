using System.Collections.Generic;

namespace SmartDoku.Main.Model
{
  public class SDCelulaModel
  {
    public SDCelulaModel()
    {
      PossiveisValores = new List<int?>();
    }

    public SDCelulaModel(SDCelulaModel celula)
    {
      this.PosicaoLinha = celula.PosicaoLinha;
      this.PosicaoColuna = celula.PosicaoColuna;
      this.Valor = celula.Valor;
      this.isCelulaValida = celula.isCelulaValida;
      this.isCelulaDica = celula.isCelulaDica;
      this.PossiveisValores = celula.PossiveisValores;
    }

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

    public bool isCelulaDica { get; set; } = false;

    public List<int?> PossiveisValores { get; set; }

    public override bool Equals(object obj)
    {
      var celula = obj as SDCelulaModel;
      return celula != null &&
             PosicaoLinha == celula.PosicaoLinha &&
             PosicaoColuna == celula.PosicaoColuna;
    }
  }
}
