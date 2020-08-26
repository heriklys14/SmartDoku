using System.Windows.Forms;

namespace SmartDoku
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      this.btnGeraMatriz = new System.Windows.Forms.Button();
      this.tbDigitsIniciais = new System.Windows.Forms.TextBox();
      this.lbDigitsIniciais = new System.Windows.Forms.Label();
      this.btnResetaMatriz = new System.Windows.Forms.Button();
      this.btnResolveSudoku = new System.Windows.Forms.Button();
      this.sdMatrixGrid = new SmartDoku.Components.SDMatrixGrid();
      ((System.ComponentModel.ISupportInitialize)(this.sdMatrixGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // btnGeraMatriz
      // 
      this.btnGeraMatriz.Location = new System.Drawing.Point(28, 49);
      this.btnGeraMatriz.Name = "btnGeraMatriz";
      this.btnGeraMatriz.Size = new System.Drawing.Size(75, 23);
      this.btnGeraMatriz.TabIndex = 2;
      this.btnGeraMatriz.Text = "Gerar Matriz";
      this.btnGeraMatriz.UseVisualStyleBackColor = true;
      this.btnGeraMatriz.Click += new System.EventHandler(this.btnGeraMatriz_Click);
      // 
      // tbDigitsIniciais
      // 
      this.tbDigitsIniciais.Location = new System.Drawing.Point(178, 85);
      this.tbDigitsIniciais.Name = "tbDigitsIniciais";
      this.tbDigitsIniciais.Size = new System.Drawing.Size(32, 20);
      this.tbDigitsIniciais.TabIndex = 3;
      this.tbDigitsIniciais.Text = "17";
      // 
      // lbDigitsIniciais
      // 
      this.lbDigitsIniciais.AutoSize = true;
      this.lbDigitsIniciais.Location = new System.Drawing.Point(25, 88);
      this.lbDigitsIniciais.Name = "lbDigitsIniciais";
      this.lbDigitsIniciais.Size = new System.Drawing.Size(147, 13);
      this.lbDigitsIniciais.TabIndex = 4;
      this.lbDigitsIniciais.Text = "Quantidade de Digitos Iniciais";
      // 
      // btnResetaMatriz
      // 
      this.btnResetaMatriz.Location = new System.Drawing.Point(126, 49);
      this.btnResetaMatriz.Name = "btnResetaMatriz";
      this.btnResetaMatriz.Size = new System.Drawing.Size(84, 23);
      this.btnResetaMatriz.TabIndex = 5;
      this.btnResetaMatriz.Text = "Resetar Matriz";
      this.btnResetaMatriz.UseVisualStyleBackColor = true;
      this.btnResetaMatriz.Click += new System.EventHandler(this.btnResetaMatriz_Click);
      // 
      // btnResolveSudoku
      // 
      this.btnResolveSudoku.Location = new System.Drawing.Point(28, 128);
      this.btnResolveSudoku.Name = "btnResolveSudoku";
      this.btnResolveSudoku.Size = new System.Drawing.Size(75, 23);
      this.btnResolveSudoku.TabIndex = 6;
      this.btnResolveSudoku.Text = "Resolver!";
      this.btnResolveSudoku.UseVisualStyleBackColor = true;
      this.btnResolveSudoku.Click += new System.EventHandler(this.btnResolveSudoku_Click);
      // 
      // sdMatrixGrid
      // 
      this.sdMatrixGrid.AllowUserToAddRows = false;
      this.sdMatrixGrid.AllowUserToDeleteRows = false;
      this.sdMatrixGrid.AllowUserToResizeColumns = false;
      this.sdMatrixGrid.AllowUserToResizeRows = false;
      this.sdMatrixGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.sdMatrixGrid.ColumnHeadersVisible = false;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.sdMatrixGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.sdMatrixGrid.Location = new System.Drawing.Point(263, 49);
      this.sdMatrixGrid.Matriz = null;
      this.sdMatrixGrid.MultiSelect = false;
      this.sdMatrixGrid.Name = "sdMatrixGrid";
      this.sdMatrixGrid.OldCelula = null;
      this.sdMatrixGrid.RowHeadersVisible = false;
      this.sdMatrixGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.sdMatrixGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.sdMatrixGrid.Size = new System.Drawing.Size(360, 360);
      this.sdMatrixGrid.TabIndex = 1;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(931, 490);
      this.Controls.Add(this.btnResolveSudoku);
      this.Controls.Add(this.btnResetaMatriz);
      this.Controls.Add(this.lbDigitsIniciais);
      this.Controls.Add(this.tbDigitsIniciais);
      this.Controls.Add(this.btnGeraMatriz);
      this.Controls.Add(this.sdMatrixGrid);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SmartDoku";
      this.Load += new System.EventHandler(this.MainForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.sdMatrixGrid)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Components.SDMatrixGrid sdMatrixGrid;
    private Button btnGeraMatriz;
    private TextBox tbDigitsIniciais;
    private Label lbDigitsIniciais;
    private Button btnResetaMatriz;
    private Button btnResolveSudoku;
  }
}

