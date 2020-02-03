namespace EFaturaApp
{
    partial class EArsivGonder
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EArsivGonder));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.isaret = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.temelFaturaGönderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ticariFataruGönderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarButton1 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarButton2 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarButton3 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarButton4 = new Telerik.WinControls.UI.CommandBarButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isaret});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1266, 751);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // isaret
            // 
            this.isaret.DataPropertyName = "isaret";
            this.isaret.HeaderText = "isaret";
            this.isaret.Name = "isaret";
            this.isaret.Width = 50;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.temelFaturaGönderToolStripMenuItem,
            this.ticariFataruGönderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 48);
            // 
            // temelFaturaGönderToolStripMenuItem
            // 
            this.temelFaturaGönderToolStripMenuItem.Name = "temelFaturaGönderToolStripMenuItem";
            this.temelFaturaGönderToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.temelFaturaGönderToolStripMenuItem.Text = "Temel Fatura Gönder";
            // 
            // ticariFataruGönderToolStripMenuItem
            // 
            this.ticariFataruGönderToolStripMenuItem.Name = "ticariFataruGönderToolStripMenuItem";
            this.ticariFataruGönderToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.ticariFataruGönderToolStripMenuItem.Text = "Ticari Fataru Gönder";
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(1272, 70);
            this.radCommandBar1.TabIndex = 3;
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Name = "commandBarRowElement1";
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            this.commandBarRowElement1.Text = "";
            this.commandBarRowElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarRowElement1.UseCompatibleTextRendering = false;
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.commandBarButton1,
            this.commandBarButton2,
            this.commandBarButton3,
            this.commandBarButton4});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            this.commandBarStripElement1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarStripElement1.UseCompatibleTextRendering = false;
            // 
            // commandBarButton1
            // 
            this.commandBarButton1.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarButton1.DisplayName = "Fatura Listesi";
            this.commandBarButton1.DrawText = true;
            this.commandBarButton1.Image = ((System.Drawing.Image)(resources.GetObject("commandBarButton1.Image")));
            this.commandBarButton1.Name = "commandBarButton1";
            this.commandBarButton1.Text = "Fatura Listesi";
            this.commandBarButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.commandBarButton1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarButton1.UseCompatibleTextRendering = false;
            this.commandBarButton1.Click += new System.EventHandler(this.commandBarButton1_Click);
            // 
            // commandBarButton2
            // 
            this.commandBarButton2.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarButton2.DisplayName = "commandBarButton2";
            this.commandBarButton2.DrawText = true;
            this.commandBarButton2.Image = ((System.Drawing.Image)(resources.GetObject("commandBarButton2.Image")));
            this.commandBarButton2.Name = "commandBarButton2";
            this.commandBarButton2.Text = "Faturaları Gönder";
            this.commandBarButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.commandBarButton2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarButton2.UseCompatibleTextRendering = false;
            this.commandBarButton2.Click += new System.EventHandler(this.commandBarButton2_Click);
            // 
            // commandBarButton3
            // 
            this.commandBarButton3.DisabledTextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarButton3.DisplayName = "commandBarButton3";
            this.commandBarButton3.DrawText = true;
            this.commandBarButton3.Image = ((System.Drawing.Image)(resources.GetObject("commandBarButton3.Image")));
            this.commandBarButton3.Name = "commandBarButton3";
            this.commandBarButton3.Text = "Hepsini Seç";
            this.commandBarButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.commandBarButton3.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.commandBarButton3.UseCompatibleTextRendering = false;
            // 
            // commandBarButton4
            // 
            this.commandBarButton4.DisplayName = "commandBarButton4";
            this.commandBarButton4.DrawText = true;
            this.commandBarButton4.Image = ((System.Drawing.Image)(resources.GetObject("commandBarButton4.Image")));
            this.commandBarButton4.Name = "commandBarButton4";
            this.commandBarButton4.Text = "Kapat";
            this.commandBarButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.commandBarButton4.Click += new System.EventHandler(this.commandBarButton4_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 70);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1272, 757);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // EArsivGonder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 827);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.radCommandBar1);
            this.Name = "EArsivGonder";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "EArsivGonder";
            this.Load += new System.EventHandler(this.EArsivGonder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isaret;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem temelFaturaGönderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ticariFataruGönderToolStripMenuItem;
        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton1;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton2;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton4;
    }
}