using System.Collections.Generic;

namespace SmartDoku.Main.Model
{
  public class SDQuadranteModel
  {
    public SDQuadranteModel(int numeroSequencial)
    {
      Celulas = new List<SDCelulaModel>();
      NumeroSequencial = numeroSequencial;
    }

    public SDQuadranteModel()
    {
      Celulas = new List<SDCelulaModel>();
      Area = new string[9];
    }

    public List<SDCelulaModel> Celulas { get; set; }

    public string[] Area { get; set; }

    public int NumeroSequencial
    {
      get
      {
        return _numeroSequencial;
      }
      set
      {
        _numeroSequencial = value;
        SetArea(_numeroSequencial);
      }
    }
    private int _numeroSequencial;

    public void SetArea(int numeroSeq)
    {
      switch (numeroSeq)
      {
        case 1:
          Area = new string[] { "1|1", "1|2", "1|3",
                                "2|1", "2|2", "2|3",
                                "3|1", "3|2", "3|3"};
          break;

        case 2:
          Area = new string[] { "1|4", "1|5", "1|6",
                                "2|4", "2|5", "2|6",
                                "3|4", "3|5", "3|6"};
          break;

        case 3:
          Area = new string[] { "1|7", "1|8", "1|9",
                                "2|7", "2|8", "2|9",
                                "3|7", "3|8", "3|9"};
          break;

        case 4:
          Area = new string[] { "4|1", "4|2", "4|3",
                                "5|1", "5|2", "5|3",
                                "6|1", "6|2", "6|3"};
          break;

        case 5:
          Area = new string[] { "4|4", "4|5", "4|6",
                                "5|4", "5|5", "5|6",
                                "6|4", "6|5", "6|6"};
          break;

        case 6:
          Area = new string[] { "4|7", "4|8", "4|9",
                                "5|7", "5|8", "5|9",
                                "6|7", "6|8", "6|9"};
          break;

        case 7:
          Area = new string[] { "7|1", "7|2", "7|3",
                                "8|1", "8|2", "8|3",
                                "9|1", "9|2", "9|3"};
          break;

        case 8:
          Area = new string[] { "7|4", "7|5", "7|6",
                                "8|4", "8|5", "8|6",
                                "9|4", "9|5", "9|6"};
          break;

        case 9:
          Area = new string[] { "7|7", "7|8", "7|9",
                                "8|7", "8|8", "8|9",
                                "9|7", "9|8", "9|9"};
          break;

        default:
          break;
      }
    }
  }
}
