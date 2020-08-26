using SmartDoku.Main.IService;
using SmartDoku.Main.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SmartDoku.Main.Service
{
  public class SDService : ISDService
  {
    public void Dispose()
    {
      //this.Dispose();
    }

    public void AlteraValorCelula(SDMatrizModel matriz, SDCelulaModel celulaAlterada, SDCelulaModel oldCelula)
    {
      AjustaStatusCelula(matriz, celulaAlterada);
      ReavaliaCelulasAposAlteracao(matriz, oldCelula);
    }

    public void ReavaliaCelulasAposAlteracao(SDMatrizModel matriz, SDCelulaModel oldCelula)
    {
      var listaCelulasParaReavaliar = new List<SDCelulaModel>();

      listaCelulasParaReavaliar.AddRange(matriz.Linhas.Find(linha => linha.NumeroSequencial == oldCelula.PosicaoLinha)
                                                      .Celulas.Where(celula => celula.Valor == oldCelula.Valor).ToList());
      listaCelulasParaReavaliar.AddRange(matriz.Colunas.Find(coluna => coluna.NumeroSequencial == oldCelula.PosicaoColuna)
                                                      .Celulas.Where(celula => celula.Valor == oldCelula.Valor).ToList());
      listaCelulasParaReavaliar.AddRange(matriz.Quadrantes.Find(quadrante => quadrante.Celulas.Contains(oldCelula))
                                                      .Celulas.Where(celula => celula.Valor == oldCelula.Valor).ToList());

      foreach (var celulaParaReavaliar in listaCelulasParaReavaliar)
      {
        AjustaStatusCelula(matriz, celulaParaReavaliar);
      }
    }

    public void AjustaStatusCelula(SDMatrizModel matriz, SDCelulaModel celulaAlterada)
    {
      var listaCelulasInvalidas = RetornaListaCelulasInvalidas(matriz, celulaAlterada);

      if (listaCelulasInvalidas.Any())
        celulaAlterada.isCelulaValida = false;
      else
        celulaAlterada.isCelulaValida = true;

      foreach (var celulaInvalida in listaCelulasInvalidas)
      {
        celulaInvalida.isCelulaValida = false;
      }
    }

    public void AjustaStatusCelula(SDMatrizModel matriz, List<SDCelulaModel> listCelulasInvalidas)
    {
      foreach (var celulaInvalida in listCelulasInvalidas)
      {
        celulaInvalida.isCelulaValida = false;
      }
    }

    private List<SDCelulaModel> RetornaListaCelulasInvalidas(SDMatrizModel matriz, SDCelulaModel celulaAlterada)
    {
      List<SDCelulaModel> listaCelulasInvalidas = new List<SDCelulaModel>();

      listaCelulasInvalidas.AddRange(matriz.Linhas.
        Find(linha => linha.NumeroSequencial == celulaAlterada.PosicaoLinha).
          Celulas.Where(celula => celula.PosicaoColuna != celulaAlterada.PosicaoColuna
                               && celula.Valor == celulaAlterada.Valor
                               && (celula.Valor > 0 && celula.Valor != null)).ToList());

      listaCelulasInvalidas.AddRange(matriz.Colunas.
        Find(coluna => coluna.NumeroSequencial == celulaAlterada.PosicaoColuna).
          Celulas.Where(celula => celula.PosicaoLinha != celulaAlterada.PosicaoLinha
                               && celula.Valor == celulaAlterada.Valor
                               && (celula.Valor > 0 && celula.Valor != null)).ToList());

      listaCelulasInvalidas.AddRange(matriz.Quadrantes.
        Find(quadrante => quadrante.Celulas.Contains(celulaAlterada)).
          Celulas.Where(celula => celula.PosicaoLinha != celulaAlterada.PosicaoLinha
                               && celula.PosicaoColuna != celulaAlterada.PosicaoColuna
                               && celula.Valor == celulaAlterada.Valor
                               && (celula.Valor >0 && celula.Valor != null)).ToList());

      return listaCelulasInvalidas;
    }

    public void GeraDigitosIniciais(SDMatrizModel matriz, int qtdeDigitosIniciais)
    {
      LimpaValoresCelulas(matriz);

      int digGerados = 0;

      if (qtdeDigitosIniciais > 0)
      {
        List<SDCelulaModel> listaAleatoriaCelulas = new List<SDCelulaModel>();

        listaAleatoriaCelulas.AddRange(matriz.Celulas);
        listaAleatoriaCelulas.Shuffle();

        do
        {
          SDCelulaModel cel = listaAleatoriaCelulas.Where(celula => celula.Valor == 0 || celula.Valor == null).First();
          
          do
          {
            cel.Valor = new Random().Next(1, 10);

          } while (RetornaListaCelulasInvalidas(matriz, cel).Any());

          matriz.Celulas.Where(celula => celula.Equals(cel)).Select(celula => celula.Valor = cel.Valor);

          digGerados++;

        } while (digGerados < qtdeDigitosIniciais);
      }
    }


    private void LimpaValoresCelulas(SDMatrizModel matriz)
    {
      matriz.Celulas.ForEach(celula => celula.Valor = null);
    }
  }

  public static class ThreadSafeRandom
  {
    [ThreadStatic] private static Random Local;

    public static Random ThisThreadsRandom
    {
      get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
  }

  static class MyExtensions
  {
    public static void Shuffle<T>(this IList<T> list)
    {
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }
  }
}
