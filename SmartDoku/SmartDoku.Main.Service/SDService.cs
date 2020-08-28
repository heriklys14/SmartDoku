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
    private Random rng = new Random();

    private readonly List<int?> ListaValoresSudoku = new List<int?>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    private bool rollBack = true;

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

    private void AjustaStatusCelula(SDMatrizModel matriz)
    {
      foreach (var celula in matriz.Celulas)
      {
        AjustaStatusCelula(matriz, celula);
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
                               && (celula.Valor > 0 && celula.Valor != null)).ToList());

      return listaCelulasInvalidas;
    }

    public void GeraDigitosIniciais(SDMatrizModel matriz, int qtdeDigitosIniciais)
    {
      int digGerados = 0;

      if (qtdeDigitosIniciais > 0)
      {
        List<SDCelulaModel> listaAleatoriaCelulas = new List<SDCelulaModel>();

        matriz.Celulas.ForEach(celula =>
        {
          listaAleatoriaCelulas.Add(new SDCelulaModel(celula));
        });
        
        listaAleatoriaCelulas.Shuffle();

        do
        {
          SDCelulaModel cel = listaAleatoriaCelulas.Where(celula => celula.Valor == 0 || celula.Valor == null).First();

          do
          {
            cel.Valor = rng.Next(1, 10);

          } while (RetornaListaCelulasInvalidas(matriz, cel).Any());

          matriz.Celulas.Where(celula => celula.Equals(cel)).First().Valor = cel.Valor;
          matriz.Celulas.Where(celula => celula.Equals(cel)).First().isCelulaDica = true;

          AjustaStatusCelula(matriz, cel);

          digGerados++;

        } while (digGerados < qtdeDigitosIniciais);
      }
    }

    public SDMatrizModel MontaGridInicial()
    {
      SDMatrizModel matriz = RetornaGridResolvida();

      return matriz;
    }

    private SDMatrizModel RetornaGridResolvida()
    {
      SDMatrizModel matriz = new SDMatrizModel();

      while (rollBack)
      {
        matriz = new SDMatrizModel();

        List<SDCelulaModel> listaAleatoriaCelulas = RetornaListaAleatoriaCelulas(matriz);

        GeraPrimeirosNoveDigitos(matriz, listaAleatoriaCelulas);

        PreenchePossiveisValores(matriz);

        Dictionary<int, SDCelulaModel> dictCelulasParaPreencher = RetornaDictCelulasParaPreencher(matriz);

        try
        {
          foreach (var item in dictCelulasParaPreencher)
            PreencheValorCelula(matriz, item, dictCelulasParaPreencher);
        }
        catch (ArgumentOutOfRangeException ex)
        {
          continue;
        }

        AjustaStatusCelula(matriz);

        if (matriz.Celulas.Any() && !matriz.Celulas.Exists(celula => !celula.isCelulaValida))
          rollBack = false;
      }

      return matriz;
    }

    private List<SDCelulaModel> RetornaListaAleatoriaCelulas(SDMatrizModel matriz)
    {
      List<SDCelulaModel> listaAleatoriaCelulas = new List<SDCelulaModel>();

      matriz.Celulas.ForEach(celula =>
      {
        listaAleatoriaCelulas.Add(new SDCelulaModel(celula));
      });

      listaAleatoriaCelulas.Shuffle();
      return listaAleatoriaCelulas;
    }

    private void GeraPrimeirosNoveDigitos(SDMatrizModel matriz, List<SDCelulaModel> listaAleatoriaCelulas)
    {
      for (int i = 1; i <= 9; i++)
      {
        SDCelulaModel cel = listaAleatoriaCelulas.Where(celula => celula.Valor == 0 || celula.Valor == null).First();
        cel.Valor = i;

        matriz.Celulas.Where(celula => celula.Equals(cel)).First().Valor = cel.Valor;
        matriz.Celulas.Where(celula => celula.Equals(cel)).First().isCelulaInicial = true;
        matriz.Celulas.Where(celula => celula.Equals(cel)).First().PossiveisValores.Add(cel.Valor);
      }
    }

    private void PreenchePossiveisValores(SDMatrizModel matriz)
    {
      foreach (var celula in matriz.Celulas)
      {
        if (celula.isCelulaInicial)
          continue;

        var listaValoresBloqueados = RetornaListaValoresBloqueados(matriz, celula);

        if (listaValoresBloqueados.Any())
          celula.PossiveisValores = ListaValoresSudoku
                                      .Where(valor => !listaValoresBloqueados.Contains(valor))
                                      .ToList();
        else
          celula.PossiveisValores = ListaValoresSudoku;
      }
    }

    private void PreenchePossiveisValores(SDMatrizModel matriz, List<SDCelulaModel> listaCelulas)
    {
      foreach (var celula in listaCelulas)
      {
        var listaValoresBloqueados = RetornaListaValoresBloqueados(matriz, celula);

        if (listaValoresBloqueados.Any())
          celula.PossiveisValores = ListaValoresSudoku
                                      .Where(valor => !listaValoresBloqueados.Contains(valor))
                                      .ToList();
        else
          celula.PossiveisValores = ListaValoresSudoku;
      }
    }

    private Dictionary<int, SDCelulaModel> RetornaDictCelulasParaPreencher(SDMatrizModel matriz)
    {
      var dictCelulasParaPreencher = new Dictionary<int, SDCelulaModel>();

      int i = 1;

      matriz.Celulas.Where(celula => !celula.isCelulaInicial).OrderBy(celula => celula.PossiveisValores.Count).ToList()
        .ForEach(celula =>
        {
          dictCelulasParaPreencher.Add(i, new SDCelulaModel(celula));
          i++;
        });

      return dictCelulasParaPreencher;
    }

    private void PreencheValorCelula(SDMatrizModel matriz, KeyValuePair<int, SDCelulaModel> parCelulaAtual, Dictionary<int, SDCelulaModel> dictCelulasParaPreencher)
    {
      if (!matriz.Celulas.Any())
        return;

      var celulaReal = matriz.Celulas.Find(cel => cel.Equals(parCelulaAtual.Value));

      if (celulaReal.PossiveisValores.Count == 1 && !matriz.Celulas.Exists(celula => !celula.PossiveisValores.Any()))
      {
        celulaReal.Valor = celulaReal.PossiveisValores.First();
        return;
      }

      foreach (var valor in celulaReal.PossiveisValores)
      {
        if (celulaReal.Valor == valor)
          continue;

        celulaReal.Valor = valor;
        PreenchePossiveisValores(matriz);

        if (!matriz.Celulas.Exists(celula => !celula.PossiveisValores.Any()))
          return;
      }

      if (parCelulaAtual.Key - 2 < 0)
        throw new ArgumentOutOfRangeException();

      var parCelulaAnterior = dictCelulasParaPreencher.ElementAt(parCelulaAtual.Key - 2);
      PreencheValorCelula(matriz, parCelulaAnterior, dictCelulasParaPreencher);

      PreencheValorCelula(matriz, parCelulaAtual, dictCelulasParaPreencher);
    }

    public void ResolveSudoku(SDMatrizModel matriz)
    {

    }





    private List<int?> RetornaListaValoresBloqueados(SDMatrizModel matriz, SDCelulaModel celulaVerificada)
    {
      List<int?> listaValoresBloqueados = new List<int?>();

      matriz
        .Linhas
          .Find(linha => linha.NumeroSequencial == celulaVerificada.PosicaoLinha)
            .Celulas
              .Where(celula => celula.PosicaoColuna != celulaVerificada.PosicaoColuna
                            && celula.Valor > 0 && celula.Valor != null)
              .ToList()
                .ForEach(celulaDaLinha =>
                {
                  if(!listaValoresBloqueados.Contains(celulaDaLinha.Valor))
                    listaValoresBloqueados.Add(celulaDaLinha.Valor);
                });

      matriz
        .Colunas
          .Find(coluna => coluna.NumeroSequencial == celulaVerificada.PosicaoColuna)
            .Celulas
              .Where(celula => celula.PosicaoLinha != celulaVerificada.PosicaoLinha
                            && celula.Valor > 0 && celula.Valor != null)
              .ToList()
                .ForEach(celulaDaColuna =>
                {
                  if (!listaValoresBloqueados.Contains(celulaDaColuna.Valor))
                    listaValoresBloqueados.Add(celulaDaColuna.Valor);
                });

      matriz
        .Quadrantes
          .Find(quadrante => quadrante.Celulas.Contains(celulaVerificada))
            .Celulas
              .Where(celula => !celula.Equals(celulaVerificada)
                            && celula.Valor > 0 && celula.Valor != null)
              .ToList()
                .ForEach(celulaDoQuadrante =>
                {
                  if (!listaValoresBloqueados.Contains(celulaDoQuadrante.Valor))
                    listaValoresBloqueados.Add(celulaDoQuadrante.Valor);
                });

      return listaValoresBloqueados;
    }

    private int GetValorAleatorioDiferente(int? valorAtual)
    {
      return rng.Next(1, 10);
    }

    private int? GetValorAleatorioDiferente(List<int?> listaValoresDisponiveis)
    {
      return listaValoresDisponiveis.ShuffleReturn().First();
    }
  }

  

  #region Classes auxiliares

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

    public static IList<T> ShuffleReturn<T>(this IList<T> list)
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
      return list;
    }
  }

  #endregion
}
