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
    public partial class FormParent : Form
    {
        FormMain formMain = new FormMain();

        public FormParent()
        {
            InitializeComponent();
        }

        private void FormParent_Load(object sender, EventArgs e)
        {
            formMain.ShowEditForm += this.ShowEditForm;
            ShowMainForm();
        }


        private void ShowMainForm()
        {
            this.orderMainLink.Enabled = false;
            this.orderEditLink.Visible = false;
            this.seperatorLabel.Visible = false;
            showFormInPanel(formMain);
            formMain.QueryAll();
        }

        private void ShowEditForm(FormEdit formEdit)
        {
            this.orderMainLink.Enabled = true;
            this.orderEditLink.Visible = true;
            this.seperatorLabel.Visible = true;
            this.orderEditLink.Text = formEdit.EditFlag ? "修改订单" : "添加订单";
            formEdit.CloseEditFrom += (form => ShowMainForm());
            showFormInPanel(formEdit);
        }

        private void showFormInPanel(Form from)
        {
            this.contentPanel.SuspendLayout();
            this.contentPanel.Controls.Clear();
            from.TopLevel = false;
            from.FormBorderStyle = FormBorderStyle.None;
            from.Dock = DockStyle.Fill;
            from.Visible = true;
            this.contentPanel.Controls.Add(from);
            this.contentPanel.ResumeLayout();
        }


        private void orderMainLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowMainForm();
        }

        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void seperatorLabel_Click(object sender, EventArgs e)
        {

        }

        private void orderEditLink_Click(object sender, EventArgs e)
        {

        }

        private void contentPanel_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
