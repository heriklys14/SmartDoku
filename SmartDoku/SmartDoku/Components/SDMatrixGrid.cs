using SmartDoku.Main.Model;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.ComponentModel;
using SmartDoku.Main.Controller;

namespace SmartDoku.Components
{
  public class SDMatrixGrid : DataGridView
  {
    public SDMatrizModel Matriz { get; set; }
    public SDCelulaModel OldCelula { get; set; }
    private int cellHeight = 40;
    private int cellWidth = 40;

    public SDUtils sdUtils
    {
      get
      {
        if (_sdUtils == null)
          _sdUtils = new SDUtils();

        return _sdUtils;
      }
    }
    private SDUtils _sdUtils;

    public SDMatrixGrid() : base()
    {
      this.AutoGenerateColumns = true;
      this.ColumnHeadersVisible = false;
      this.RowHeadersVisible = false;
      this.AllowUserToResizeColumns = false;
      this.AllowUserToResizeRows= false;
      this.ScrollBars = ScrollBars.None;
      this.Font = new Font(this.Font.FontFamily, 14);
      this.EditingControlShowing = new DataGridViewEditingControlShowingEventHandler(SDMatrixGrid_EditingControlShowing);
      this.CellBeginEdit = new DataGridViewCellCancelEventHandler(SDMatrixGrid_CellBeginEdit);
      this.CellValueChanged = new DataGridViewCellEventHandler(SDMatrixGrid_CellValueChanged);
    }

    #region Eventos Customizados

    #region EditingControlShowing

    [Browsable(true)]
    [Category("Action")]
    [Description("Acionado quando o usuário está editando uma célula.")]
    public new event DataGridViewEditingControlShowingEventHandler EditingControlShowing;

    protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
    {
      this.EditingControlShowing?.Invoke(this, e);
    }

    protected void SDMatrixGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
      e.Control.KeyPress -= new KeyPressEventHandler(Cell_KeyPress);
      TextBox tb = e.Control as TextBox;

      if (tb != null)
      {
        tb.KeyPress += new KeyPressEventHandler(Cell_KeyPress);
      }
    }

    #endregion
    
    #region CellKeyPress
    private void Cell_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar)))
      {
        e.Handled = true;
      }
      else if (char.IsDigit(e.KeyChar) && ((DataGridViewTextBoxEditingControl)sender).Text.Length == 1)
      {
        e.Handled = true;
      }
      else if (char.IsDigit(e.KeyChar) && Convert.ToInt32(Convert.ToString(e.KeyChar)) == 0)
      {
        e.Handled = true;
      }
    }

    #endregion

    #region CellBeginEdit

    [Browsable(true)]
    [Category("Action")]
    [Description("Acionado quando o usuário começa a edição de uma célula.")]
    public new event DataGridViewCellCancelEventHandler CellBeginEdit;

    protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
    {
      this.CellBeginEdit?.Invoke(this, e);
    }

    private void SDMatrixGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      var celulaEmEdicao = this.Matriz.Celulas.Find(celula => celula.PosicaoLinha == e.RowIndex + 1
                                                           && celula.PosicaoColuna == e.ColumnIndex + 1);
      OldCelula = new SDCelulaModel(celulaEmEdicao);
    }

    #endregion

    #region CellValueChanged

    [Browsable(true)]
    [Category("Action")]
    [Description("Acionado quando o usuário altera o valor de uma célula.")]
    public new event DataGridViewCellEventHandler CellValueChanged;

    protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
    {
      this.CellValueChanged?.Invoke(this, e);
    }

    private void SDMatrixGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      var celulaAlterada = this.Matriz.Celulas.Find(celula => celula.PosicaoLinha == e.RowIndex + 1
                                                           && celula.PosicaoColuna == e.ColumnIndex + 1);

      if (!string.IsNullOrEmpty(Convert.ToString(this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)))
        celulaAlterada.Valor = Convert.ToInt32(this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
      else
        celulaAlterada.Valor = null;

      if (celulaAlterada.Valor != this.OldCelula.Valor)
      {
        sdUtils.AlteraValorCelula(this.Matriz, celulaAlterada, this.OldCelula);
        ColoreCelulas();
      }
    }

  #endregion

  #endregion

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
          {
            this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.BackColor = Color.FromArgb(220, 220, 220);
            this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.SelectionBackColor = Color.FromArgb(220, 220, 220);
          }
          else
          {
            this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.BackColor = Color.FromArgb(255, 160, 122);
            this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.SelectionBackColor = Color.FromArgb(255, 160, 122);
          }

          if (celula.isCelulaDica || celula.isCelulaInicial)
            this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.Font = new Font(this.Font, FontStyle.Bold);
          else
            this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.Font = new Font(this.Font, FontStyle.Regular);

          this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.ForeColor = Color.FromArgb(0, 0, 0);
          this.Rows[celula.PosicaoLinha - 1].Cells[celula.PosicaoColuna - 1].Style.SelectionForeColor = Color.FromArgb(0, 0, 0);
        });
    }

    public void AmarraMatrizAoGrid()
    {
      DesenhaGrid();
      AjustaElementosGrid();
    }

  }
}
