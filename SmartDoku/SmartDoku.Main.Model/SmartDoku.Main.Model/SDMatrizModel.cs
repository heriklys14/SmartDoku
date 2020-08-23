using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDoku.Main.Model
{
  public class SDMatrizModel
  {
    public SDMatrizModel(int qtdeDigitosIniciais = 5)
    {
      Linhas = new List<SDLinhaModel>();
      Colunas = new List<SDColunaModel>();
      Celulas = new List<SDCelulaModel>();
      Quadrantes = new List<SDQuadranteModel>();

      QtdeDigitosIniciais = qtdeDigitosIniciais;

      IniciaMatriz();
    }

    public void IniciaMatriz()
    {
      CriaLinhasMatriz();
      PreencheCelulas();
      CriaColunasMatriz();
    }

    #region Props

    public List<SDLinhaModel> Linhas { get; set; }

    public List<SDColunaModel> Colunas { get; set; }

    public List<SDQuadranteModel> Quadrantes { get; set; }

    public List<SDCelulaModel> Celulas { get; set; }

    public int QtdeDigitosIniciais { get; set; }

    #endregion

    #region Methods

    public int GetValorCelula(int linha, int coluna)
    {
      return Convert.ToInt32(Celulas
        .Where(celula => celula.PosicaoLinha == linha && celula.PosicaoColuna == coluna)
        .Select(celula => celula.Valor)
        .First());
    }

    private void PreencheCelulas()
    {
      foreach (var linha in Linhas)
      {
        foreach (var celula in linha.Celulas)
        {
          if (!Celulas.Contains(celula))
            Celulas.Add(celula);
        }
      }
    }

    private void CriaLinhasMatriz()
    {
      for (int i = 1; i <= 9; i++)
      {
        var linha = new SDLinhaModel() { NumeroSequencial = i };

        for (int j = 1; j <= 9; j++)
        {
          var celula = new SDCelulaModel() { PosicaoLinha = i, PosicaoColuna = j };
          linha.Celulas.Add(celula);
        }

        Linhas.Add(linha);
      }
    }

    private void CriaColunasMatriz()
    {
      for (int i = 1; i <= 9; i++)
      {
        var coluna = new SDColunaModel() { NumeroSequencial = i };

        for (int j = 1; j <= 9; j++)
        {
          var celula = Celulas.Find(cel => cel.PosicaoColuna == i && cel.PosicaoLinha == j);
          coluna.Celulas.Add(celula);
        }

        Colunas.Add(coluna);
      }
    }

    private void CriaQuadrantesMatriz()
    {

    }









    #endregion
  }
}
