namespace EFaturaApp
{
    partial class FaturaGonder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaturaGonder));
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radCommandBar1 = new Telerik.WinControls.UI.RadCommandBar();
            this.commandBarRowElement1 = new Telerik.WinControls.UI.CommandBarRowElement();
            this.commandBarStripElement1 = new Telerik.WinControls.UI.CommandBarStripElement();
            this.commandBarButton1 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarButton2 = new Telerik.WinControls.UI.CommandBarButton();
            this.commandBarButton3 = new Telerik.WinControls.UI.CommandBarButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radCommandBar1
            // 
            this.radCommandBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radCommandBar1.Location = new System.Drawing.Point(0, 0);
            this.radCommandBar1.Name = "radCommandBar1";
            this.radCommandBar1.Rows.AddRange(new Telerik.WinControls.UI.CommandBarRowElement[] {
            this.commandBarRowElement1});
            this.radCommandBar1.Size = new System.Drawing.Size(905, 45);
            this.radCommandBar1.TabIndex = 1;
            // 
            // commandBarRowElement1
            // 
            this.commandBarRowElement1.MinSize = new System.Drawing.Size(25, 25);
            this.commandBarRowElement1.Name = "commandBarRowElement1";
            this.commandBarRowElement1.Strips.AddRange(new Telerik.WinControls.UI.CommandBarStripElement[] {
            this.commandBarStripElement1});
            // 
            // commandBarStripElement1
            // 
            this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
            this.commandBarStripElement1.Items.AddRange(new Telerik.WinControls.UI.RadCommandBarBaseItem[] {
            this.commandBarButton1,
            this.commandBarButton2,
            this.commandBarButton3});
            this.commandBarStripElement1.Name = "commandBarStripElement1";
            // 
            // commandBarButton1
            // 
            this.commandBarButton1.DisplayName = "Fatura Listesi";
            this.commandBarButton1.DrawText = true;
            this.commandBarButton1.Image = ((System.Drawing.Image)(resources.GetObject("commandBarButton1.Image")));
            this.commandBarButton1.Name = "commandBarButton1";
            this.commandBarButton1.Text = "Fatura Listesi";
            this.commandBarButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.commandBarButton1.Click += new System.EventHandler(this.commandBarButton1_Click);
            // 
            // commandBarButton2
            // 
            this.commandBarButton2.DisplayName = "commandBarButton2";
            this.commandBarButton2.DrawText = true;
            this.commandBarButton2.Image = ((System.Drawing.Image)(resources.GetObject("commandBarButton2.Image")));
            this.commandBarButton2.Name = "commandBarButton2";
            this.commandBarButton2.Text = "Faturaları Gönder";
            this.commandBarButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // commandBarButton3
            // 
            this.commandBarButton3.DisplayName = "commandBarButton3";
            this.commandBarButton3.DrawText = true;
            this.commandBarButton3.Image = ((System.Drawing.Image)(resources.GetObject("commandBarButton3.Image")));
            this.commandBarButton3.Name = "commandBarButton3";
            this.commandBarButton3.Text = "Hepsini Seç";
            this.commandBarButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.commandBarButton3.Click += new System.EventHandler(this.commandBarButton3_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.radGridView1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 45);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(905, 488);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // radGridView1
            // 
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Location = new System.Drawing.Point(3, 3);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowDeleteRow = false;
            this.radGridView1.MasterTemplate.AllowDragToGroup = false;
            this.radGridView1.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewCheckBoxColumn1.Checked = Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
            gridViewCheckBoxColumn1.EnableExpressionEditor = false;
            gridViewCheckBoxColumn1.FieldName = "isaret";
            gridViewCheckBoxColumn1.HeaderText = "isaret";
            gridViewCheckBoxColumn1.Name = "isaret";
            gridViewCheckBoxColumn1.Width = 30;
            gridViewTextBoxColumn1.FieldName = "takipseri";
            gridViewTextBoxColumn1.HeaderText = "T.Seri";
            gridViewTextBoxColumn1.Name = "takipseri";
            gridViewTextBoxColumn1.Width = 70;
            gridViewTextBoxColumn2.FieldName = "TakipNo";
            gridViewTextBoxColumn2.HeaderText = "Fat.No.";
            gridViewTextBoxColumn2.Name = "TakipNo";
            gridViewTextBoxColumn2.Width = 51;
            gridViewTextBoxColumn3.DataType = typeof(System.DateTime);
            gridViewTextBoxColumn3.FieldName = "tarih";
            gridViewTextBoxColumn3.FormatString = "{0: dd.MM.yyyy}";
            gridViewTextBoxColumn3.HeaderText = "Tarih";
            gridViewTextBoxColumn3.Name = "tarih";
            gridViewTextBoxColumn3.Width = 96;
            gridViewTextBoxColumn4.FieldName = "carikod";
            gridViewTextBoxColumn4.HeaderText = "Kodu";
            gridViewTextBoxColumn4.Name = "carikod";
            gridViewTextBoxColumn4.Width = 31;
            gridViewTextBoxColumn5.FieldName = "adi";
            gridViewTextBoxColumn5.HeaderText = "Firma Adı";
            gridViewTextBoxColumn5.Name = "adi";
            gridViewTextBoxColumn5.Width = 280;
            gridViewTextBoxColumn6.FieldName = "yekun";
            gridViewTextBoxColumn6.HeaderText = "Yekun";
            gridViewTextBoxColumn6.Name = "yekun";
            gridViewTextBoxColumn6.Width = 70;
            gridViewTextBoxColumn7.FieldName = "toplamkdv";
            gridViewTextBoxColumn7.HeaderText = "Kdv";
            gridViewTextBoxColumn7.Name = "toplamkdv";
            gridViewTextBoxColumn7.Width = 70;
            gridViewTextBoxColumn8.FieldName = "toplam";
            gridViewTextBoxColumn8.HeaderText = "Toplam";
            gridViewTextBoxColumn8.Name = "toplam";
            gridViewTextBoxColumn8.Width = 72;
            gridViewTextBoxColumn9.FieldName = "alicisube";
            gridViewTextBoxColumn9.HeaderText = "A.Sube";
            gridViewTextBoxColumn9.Name = "alicisube";
            gridViewTextBoxColumn9.Width = 45;
            gridViewTextBoxColumn10.FieldName = "ref";
            gridViewTextBoxColumn10.HeaderText = "FtID";
            gridViewTextBoxColumn10.Name = "@ref";
            gridViewTextBoxColumn10.Width = 73;
            this.radGridView1.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10});
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.ShowNoDataText = false;
            this.radGridView1.Size = new System.Drawing.Size(899, 482);
            this.radGridView1.TabIndex = 0;
            this.radGridView1.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(this.radGridView1_RowFormatting);
            this.radGridView1.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.radGridView1_CellBeginEdit);
            this.radGridView1.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.radGridView1_CellEndEdit);
            this.radGridView1.ValueChanged += new System.EventHandler(this.radGridView1_ValueChanged);
            this.radGridView1.CurrentCellChanged += new Telerik.WinControls.UI.CurrentCellChangedEventHandler(this.radGridView1_CurrentCellChanged);
            this.radGridView1.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(this.radGridView1_CellValueChanged);
            this.radGridView1.Leave += new System.EventHandler(this.radGridView1_Leave);
            // 
            // FaturaGonder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 533);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.radCommandBar1);
            this.Name = "FaturaGonder";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FaturaGonder";
            this.Load += new System.EventHandler(this.FaturaGonder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radCommandBar1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadCommandBar radCommandBar1;
        private Telerik.WinControls.UI.CommandBarRowElement commandBarRowElement1;
        private Telerik.WinControls.UI.CommandBarStripElement commandBarStripElement1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton1;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton2;
        private Telerik.WinControls.UI.CommandBarButton commandBarButton3;
    }
}