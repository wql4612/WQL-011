using System;
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
    public partial class FormDetailEdit : Form
    {
        public OrderDetail Detail { get; set; }

        public FormDetailEdit()
        {
            InitializeComponent();
        }

        public FormDetailEdit(OrderDetail detail) : this()
        {
            this.Detail = detail;
            this.bdsDetail.DataSource = detail;
            using (Context ctx = new Context())
            {
                bdsGoods.DataSource = ctx.Goods.ToList();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void FormDetailEdit_Load(object sender, EventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }
        private void cbxGoods_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
