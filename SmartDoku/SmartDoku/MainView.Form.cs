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

      sdUtils.GeraDigitosIniciais(matriz, 5);
      this.sdMatrixGrid.AmarraMatrizAoGrid();
    }

    private void sdMatrixGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      int? valor = null;

      try
      {
        valor = Convert.ToInt32(this.sdMatrixGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
      }
      catch { }
      finally
      {
        sdUtils.AlteraValorCelula(this.sdMatrixGrid.Matriz, e.RowIndex + 1, e.ColumnIndex + 1, valor);
        this.sdMatrixGrid.ColoreCelulas();
      }

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
  }
}
