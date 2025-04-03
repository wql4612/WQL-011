// MainForm.cs
using System.Windows.Forms;
using OrderCore;

namespace OrderWinForm
{
    public partial class MainForm : Form
    {
        private readonly OrderService _orderService = new OrderService();
        private BindingSource _ordersBinding = new BindingSource();

        public MainForm()
        {
            // 正确的构造函数内初始化 ---------------------------
            InitializeComponent();
            InitializeDataBinding();
            SetupEventHandlers(); // 事件处理单独封装
        }

        private void InitializeDataBinding()
        {
            _ordersBinding.DataSource = _orderService.orders;
            dgvOrders.DataSource = _ordersBinding;//?
        }

        private void SetupEventHandlers()
        {
            // 正确的事件订阅方式 -------------------------------
            btnAdd.Click += (s, e) =>
            {
                using var editForm = new EditForm();
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    _orderService.AddOrder(editForm.CurrentOrder);
                    _ordersBinding.ResetBindings(false);
                }
            };

            btnDelete.Click += (s, e) =>
            {
                if (dgvOrders.CurrentRow?.DataBoundItem is Order order)
                {
                    _orderService.RemoveOrder(order.OrderId);
                    _ordersBinding.ResetBindings(false);
                }
            };
        }
    }
}