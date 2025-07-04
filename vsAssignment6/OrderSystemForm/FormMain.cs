﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderManagementSystem;

namespace OrderSystemForm
{
    public partial class FormMain : Form
    {
        OrderService orderService = new OrderService();

        public event Action<FormEdit> ShowEditForm = (f => { });
        public String Keyword { get; set; }

        public FormMain()
        {
            InitializeComponent();
            InitOrders();
            bdsOrders.DataSource = orderService.GetAllOrders();
            cbxField.SelectedIndex = 0;
            txtKeyword.DataBindings.Add("Text", this, "Keyword");
        }

        private void InitOrders()
        {
            Order order = new Order(1, new Customer("1", "li"), new List<OrderDetail>());
            order.AddDetail(new OrderDetail(1, new Product("1", "apple", 100.0), 10));
            order.AddDetail(new OrderDetail(2, new Product("2", "egg", 50.0), 61));
            orderService.AddOrder(order);
            Order order2 = new Order(2, new Customer("2", "zhang"), new List<OrderDetail>());
            order2.AddDetail(new OrderDetail(1, new Product("2", "egg", 200.0), 10));
            orderService.AddOrder(order2);
        }

        public void QueryAll()
        {
            bdsOrders.DataSource = orderService.GetAllOrders();
            bdsOrders.ResetBindings(false);
        }


        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cbxField.SelectedIndex)
                {
                    case 0://所有订单
                        bdsOrders.DataSource = orderService.GetAllOrders();
                        break;
                    case 1://根据ID查询
                        int id = Convert.ToInt32(Keyword);
                        bdsOrders.DataSource = orderService.GetOrder(id);
                        break;
                    case 2://根据客户查询
                        bdsOrders.DataSource = orderService.QueryOrdersByCustomerName(Keyword);
                        break;
                    case 3://根据货物查询
                        bdsOrders.DataSource = orderService.QueryOrdersByProductName(Keyword);
                        break;
                    case 4://根据总价格查询（大于某个总价）
                        float totalPrice = Convert.ToInt32(Keyword);
                        bdsOrders.DataSource = orderService.QueryByTotalAmount(totalPrice);
                        break;
                }
                bdsOrders.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormEdit formEdit = new FormEdit(new Order(), false, orderService);
            ShowEditForm(formEdit);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            EditOrder();
        }

        private void dbvOrders_DoubleClick(object sender, EventArgs e)
        {
            EditOrder();
        }

        private void EditOrder()
        {
            Order order = bdsOrders.Current as Order;
            if (order == null)
            {
                MessageBox.Show("请选择一个订单进行修改");
                return;
            }
            FormEdit form2 = new FormEdit(order, true, orderService);
            ShowEditForm(form2);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Order order = bdsOrders.Current as Order;
            if (order == null)
            {
                MessageBox.Show("请选择一个订单进行删除");
                return;
            }
            DialogResult result = MessageBox.Show($"确认要删除Id为{order.OrderId}的订单吗？", "删除", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                orderService.RemoveOrder(order.OrderId);
                QueryAll();
            }
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String fileName = saveFileDialog1.FileName;
                orderService.Export(fileName);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog1.FileName;
                orderService.Import(fileName);
                QueryAll();
            }
        }

        private void dbvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelQuery_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbxField_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void dbvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bdsOrders_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void bdsDetails_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelButtons_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
