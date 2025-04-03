namespace OrderWinForm
{
    partial class EditForm
    {
        private TextBox txtOrderId;
        private TextBox txtCustomer;
        private Button btnSave;

        private void InitializeComponent()
        {
            // 输入控件
            txtOrderId = new TextBox { Location = new System.Drawing.Point(80, 20) };
            txtCustomer = new TextBox { Location = new System.Drawing.Point(80, 60) };

            // 标签
            var lblId = new Label { Text = "订单号:", Location = new System.Drawing.Point(20, 20) };
            var lblCustomer = new Label { Text = "客户:", Location = new System.Drawing.Point(20, 60) };

            // 保存按钮
            btnSave = new Button
            {
                Text = "保存",
                Location = new System.Drawing.Point(150, 150),
                DialogResult = DialogResult.OK
            };

            // 布局
            this.Controls.AddRange(new Control[] { lblId, txtOrderId, lblCustomer, txtCustomer, btnSave });
            this.Size = new System.Drawing.Size(300, 200);
            this.Text = "编辑订单";
        }
    }
}