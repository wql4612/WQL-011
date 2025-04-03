using OrderCore;
using System.Windows.Forms;
using System.ComponentModel;

namespace OrderWinForm
{
    public partial class EditForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Order CurrentOrder { get; private set; }

        public EditForm(Order order = null)
        {
            InitializeComponent();
            CurrentOrder = order ?? new Order();
            InitializeBindings();
        }

        private void InitializeBindings()
        {
            txtOrderId.DataBindings.Add("Text", CurrentOrder, "OrderId");
            txtCustomer.DataBindings.Add("Text", CurrentOrder.Customer, "Name");
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}