using SmartDoku.Main.Model;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace SmartDoku.Components
{
  public class SDMatrixGrid : DataGridView
  {
    public SDMatrizModel Matriz { get; set; }
    private int cellHeight = 40;
    private int cellWidth = 40;


    public SDMatrixGrid() : base()
    {
      this.AutoGenerateColumns = true;
      this.ColumnHeadersVisible = false;
      this.RowHeadersVisible = false;
      this.AllowUserToResizeColumns = false;
      this.AllowUserToResizeRows= false;
      this.ScrollBars = ScrollBars.None;
      this.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 220, 220);
    }

    private void DesenhaGrid()
    {
      DataTable dt = new DataTable("MATRIZ");

      for (int i = 1; i <= 9; i++)
      {
        DataColumn column = new DataColumn() { ColumnName = $"column{i}", DataType = typeof(string) };
        dt.Columns.Add(column);
      }

      for (int posLinha = 1; posLinha <= 9; posLinha++)
      {
        var dr = dt.NewRow();

        for (int posColuna = 1; posColuna <= 9; posColuna++)
        {
          var valorCelula = Matriz.GetValorCelula(posLinha, posColuna);

          dr[$"column{posColuna}"] = valorCelula != 0 ? Convert.ToString(valorCelula) : string.Empty;
        }

        dt.Rows.Add(dr);
      }

      this.DataSource = null;
      this.DataSource = dt;
    }

    private void AjustaElementosGrid()
    {
      foreach (DataGridViewTextBoxColumn column in this.Columns)
      {
        column.Width = cellWidth;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      }

      foreach (DataGridViewRow row in this.Rows)
      {
        row.Height = cellHeight;
      }

      ColoreCelulas();
    }

    public void ColoreCelulas()
    {
      this.Matriz.Celulas.
        ForEach(celula =>
        {
          if (celula.isCelulaValida)
            this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.BackColor = Color.FromArgb(220, 220, 220);
          else
            this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.BackColor = Color.FromArgb(255, 160, 122);
        });
    }

    public void AmarraMatrizAoGrid()
    {
      DesenhaGrid();
      AjustaElementosGrid();
    }

  }
}
