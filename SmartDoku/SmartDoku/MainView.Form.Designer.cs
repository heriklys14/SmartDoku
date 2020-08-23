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
      this.sdMatrixGrid = new SmartDoku.Components.SDMatrixGrid();
      ((System.ComponentModel.ISupportInitialize)(this.sdMatrixGrid)).BeginInit();
      this.SuspendLayout();
      // 
      // btnGeraMatriz
      // 
      this.btnGeraMatriz.Location = new System.Drawing.Point(25, 30);
      this.btnGeraMatriz.Name = "btnGeraMatriz";
      this.btnGeraMatriz.Size = new System.Drawing.Size(75, 23);
      this.btnGeraMatriz.TabIndex = 2;
      this.btnGeraMatriz.Text = "Gerar Matriz";
      this.btnGeraMatriz.UseVisualStyleBackColor = true;
      this.btnGeraMatriz.Click += new System.EventHandler(this.btnGeraMatriz_Click);
      // 
      // sdMatrixGrid
      // 
      this.sdMatrixGrid.AllowUserToAddRows = false;
      this.sdMatrixGrid.AllowUserToDeleteRows = false;
      this.sdMatrixGrid.AllowUserToResizeColumns = false;
      this.sdMatrixGrid.AllowUserToResizeRows = false;
      this.sdMatrixGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.sdMatrixGrid.ColumnHeadersVisible = false;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.sdMatrixGrid.DefaultCellStyle = dataGridViewCellStyle1;
      this.sdMatrixGrid.Location = new System.Drawing.Point(263, 30);
      this.sdMatrixGrid.Matriz = null;
      this.sdMatrixGrid.Name = "sdMatrixGrid";
      this.sdMatrixGrid.RowHeadersVisible = false;
      this.sdMatrixGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.sdMatrixGrid.Size = new System.Drawing.Size(360, 360);
      this.sdMatrixGrid.TabIndex = 1;
      this.sdMatrixGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.sdMatrixGrid_CellValueChanged);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(931, 490);
      this.Controls.Add(this.btnGeraMatriz);
      this.Controls.Add(this.sdMatrixGrid);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.sdMatrixGrid)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Components.SDMatrixGrid sdMatrixGrid;
    private System.Windows.Forms.Button btnGeraMatriz;
  }
}

