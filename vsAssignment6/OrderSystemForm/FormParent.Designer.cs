namespace OrderSystemForm
{
    partial class FormParent
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.orderMainLink = new System.Windows.Forms.LinkLabel();
            this.seperatorLabel = new System.Windows.Forms.Label();
            this.orderEditLink = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.contentPanel);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(970, 591);
            this.panel1.TabIndex = 2;
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 42);
            this.contentPanel.Margin = new System.Windows.Forms.Padding(4);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(970, 549);
            this.contentPanel.TabIndex = 1;
            this.contentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.contentPanel_Paint_1);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.orderMainLink);
            this.flowLayoutPanel1.Controls.Add(this.seperatorLabel);
            this.flowLayoutPanel1.Controls.Add(this.orderEditLink);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(15, 15, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(970, 42);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // orderMainLink
            // 
            this.orderMainLink.AutoSize = true;
            this.orderMainLink.Enabled = false;
            this.orderMainLink.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.orderMainLink.Location = new System.Drawing.Point(15, 15);
            this.orderMainLink.Margin = new System.Windows.Forms.Padding(0);
            this.orderMainLink.Name = "orderMainLink";
            this.orderMainLink.Size = new System.Drawing.Size(96, 28);
            this.orderMainLink.TabIndex = 0;
            this.orderMainLink.TabStop = true;
            this.orderMainLink.Text = "订单管理";
            this.orderMainLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.orderMainLink_LinkClicked);
            // 
            // seperatorLabel
            // 
            this.seperatorLabel.AutoSize = true;
            this.seperatorLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.seperatorLabel.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.seperatorLabel.Location = new System.Drawing.Point(111, 15);
            this.seperatorLabel.Margin = new System.Windows.Forms.Padding(0);
            this.seperatorLabel.Name = "seperatorLabel";
            this.seperatorLabel.Size = new System.Drawing.Size(18, 25);
            this.seperatorLabel.TabIndex = 2;
            this.seperatorLabel.Text = "|";
            this.seperatorLabel.Visible = false;
            // 
            // orderEditLink
            // 
            this.orderEditLink.AutoSize = true;
            this.orderEditLink.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.orderEditLink.Location = new System.Drawing.Point(129, 15);
            this.orderEditLink.Margin = new System.Windows.Forms.Padding(0);
            this.orderEditLink.Name = "orderEditLink";
            this.orderEditLink.Size = new System.Drawing.Size(96, 28);
            this.orderEditLink.TabIndex = 3;
            this.orderEditLink.Text = "添加订单";
            this.orderEditLink.Visible = false;
            this.orderEditLink.Click += new System.EventHandler(this.orderEditLink_Click);
            // 
            // FormParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 591);
            this.Controls.Add(this.panel1);
            this.Name = "FormParent";
            this.Text = "FormParent";
            this.Load += new System.EventHandler(this.FormParent_Load);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.LinkLabel orderMainLink;
        private System.Windows.Forms.Label seperatorLabel;
        private System.Windows.Forms.Label orderEditLink;
    }
}