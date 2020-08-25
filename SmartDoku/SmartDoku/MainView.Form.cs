using SmartDoku.Main.Controller;
using SmartDoku.Main.Model;
using System;
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

      sdUtils.GeraDigitosIniciais(matriz, Convert.ToInt32(this.tbDigitsIniciais.Text));
      this.sdMatrixGrid.AmarraMatrizAoGrid();
    }

    private void sdMatrixGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}
