namespace OrderWinForm
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvOrders;
        private DataGridView dgvDetails;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnEdit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // 数据表格
            dgvOrders = new DataGridView();
            dgvDetails = new DataGridView();

            // 按钮
            btnAdd = new Button { Text = "新增", Top = 10 };
            btnDelete = new Button { Text = "删除", Top = 50 };
            btnEdit = new Button { Text = "修改", Top = 90 };

            // 布局容器
            var splitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                SplitterDistance = 300
            };

            // 控件关系
            splitContainer.Panel1.Controls.Add(dgvOrders);
            splitContainer.Panel2.Controls.Add(dgvDetails);
            this.Controls.Add(splitContainer);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnEdit);

            // 窗口设置
            this.Text = "订单管理系统";
            this.Size = new System.Drawing.Size(800, 600);

            // 在MainForm构造函数中添加
            this.Resize += (s, e) =>
            {
                dgvOrders.Width = this.ClientSize.Width / 2;
                btnAdd.Left = this.ClientSize.Width - 100;
            };
        }
    }
}