namespace Calculator;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.TextBox txtNum1;
    private System.Windows.Forms.TextBox txtNum2;
    private System.Windows.Forms.ComboBox cmbOperator;
    private System.Windows.Forms.Button btnCalculate;
    private System.Windows.Forms.Label lblResult;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.txtNum1 = new System.Windows.Forms.TextBox();
        this.txtNum2 = new System.Windows.Forms.TextBox();
        this.cmbOperator = new System.Windows.Forms.ComboBox();
        this.btnCalculate = new System.Windows.Forms.Button();
        this.lblResult = new System.Windows.Forms.Label();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Form1";
        this.btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
    }

    #endregion
}
