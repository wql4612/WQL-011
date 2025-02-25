// namespace CalculatorApp;

// public partial class Form1 : Form
// {
//     public Form1()
//     {
//         InitializeComponent();
//     }
// }

using System;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private TextBox txtNum1;
        private TextBox txtNum2;
        private ComboBox cmbOperator;
        private Button btnCalculate;
        private Label lblResult;

        public Form1()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // 窗体设置
            this.Text = "简单计算器";
            this.ClientSize = new System.Drawing.Size(600, 400);

            // 第一个数字输入框
            txtNum1 = new TextBox();
            txtNum1.Location = new System.Drawing.Point(40, 40);
            txtNum1.Size = new System.Drawing.Size(200, 40);
            this.Controls.Add(txtNum1);

            // 运算符选择
            cmbOperator = new ComboBox();
            cmbOperator.Location = new System.Drawing.Point(260, 40);
            cmbOperator.Size = new System.Drawing.Size(100, 40);
            cmbOperator.Items.AddRange(new object[] { "+", "-", "×", "÷" });
            cmbOperator.SelectedIndex = 0;
            this.Controls.Add(cmbOperator);

            // 第二个数字输入框
            txtNum2 = new TextBox();
            txtNum2.Location = new System.Drawing.Point(380, 40);
            txtNum2.Size = new System.Drawing.Size(200, 40);
            this.Controls.Add(txtNum2);

            // 计算按钮
            btnCalculate = new Button();
            btnCalculate.Location = new System.Drawing.Point(240, 120);
            btnCalculate.Size = new System.Drawing.Size(200, 60);
            btnCalculate.Text = "计算";
            btnCalculate.Click += BtnCalculate_Click;
            this.Controls.Add(btnCalculate);

            // 结果标签
            lblResult = new Label();
            lblResult.Location = new System.Drawing.Point(40, 200);
            lblResult.AutoSize = true;
            this.Controls.Add(lblResult);
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double num1 = double.Parse(txtNum1.Text);
                double num2 = double.Parse(txtNum2.Text);
                double result = 0;

                switch (cmbOperator.SelectedItem.ToString())
                {
                    case "+":
                        result = num1 + num2;
                        break;
                    case "-":
                        result = num1 - num2;
                        break;
                    case "×":
                        result = num1 * num2;
                        break;
                    case "÷":
                        if (num2 == 0)
                        {
                            MessageBox.Show("除数不能为零！");
                            return;
                        }
                        result = num1 / num2;
                        break;
                }

                lblResult.Text = $"结果：{result}";
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入有效的数字！");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误：{ex.Message}");
            }
        }
    }
}