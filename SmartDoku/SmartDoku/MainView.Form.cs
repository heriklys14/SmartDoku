using SmartDoku.Main.Controller;
using SmartDoku.Main.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartDoku
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
      tbDigitsIniciais.Text = "0";
    }

    public SDUtils sdUtils {
      get
      {
        if (_sdUtils == null)
          _sdUtils = new SDUtils();

        return _sdUtils;
      }
    }
    private SDUtils _sdUtils;

    private void btnGeraMatriz_Click(object sender, EventArgs e)
    {
      SDMatrizModel matriz = new SDMatrizModel();
      this.sdMatrixGrid.Matriz = matriz;

      sdUtils.GeraDigitosIniciais(matriz, Convert.ToInt32(this.tbDigitsIniciais.Text));
      this.sdMatrixGrid.AmarraMatrizAoGrid();
    }

    private void sdMatrixGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      var celulaAlterada = this.sdMatrixGrid.Matriz.Celulas.Find(celula => celula.PosicaoLinha == e.RowIndex + 1
                                                                        && celula.PosicaoColuna == e.ColumnIndex + 1);

      celulaAlterada.Valor = SDUtils.ConvertStringToInt32Nullable(this.sdMatrixGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

      sdUtils.AlteraValorCelula(this.sdMatrixGrid.Matriz, celulaAlterada, _oldCelula);
      this.sdMatrixGrid.ColoreCelulas();
    }

    private void sdMatrixGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
      {
        if (this.sdMatrixGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected == true)
        {
          e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
          using (Pen p = new Pen(Color.Red, 1))
          {
            Rectangle rect = e.CellBounds;
            rect.Width -= 2;
            rect.Height -= 2;
            e.Graphics.DrawRectangle(p, rect);
          }
          e.Handled = true;
        }
      }
    }

    private SDCelulaModel _oldCelula;
    private void sdMatrixGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      var celulaEmEdicao = this.sdMatrixGrid.Matriz.Celulas.Find(celula => celula.PosicaoLinha == e.RowIndex + 1
                                                                        && celula.PosicaoColuna == e.ColumnIndex + 1);
      _oldCelula = new SDCelulaModel(celulaEmEdicao);
    }
  }
}
