﻿namespace QLTP.GUI
{
    partial class frm_managerProduct
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_MaSP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbo_TenSP = new System.Windows.Forms.ComboBox();
            this.cbo_LoaiSP = new System.Windows.Forms.ComboBox();
            this.btn_Xoa = new System.Windows.Forms.Button();
            this.btn_Them = new System.Windows.Forms.Button();
            this.btn_Sua = new System.Windows.Forms.Button();
            this.dtpHSD = new System.Windows.Forms.DateTimePicker();
            this.txt_SL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_GiaBan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgv_quanLySanPham = new System.Windows.Forms.DataGridView();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaBan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHSD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiamGia = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.danhMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sảnPhẩmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thêmLoạiSảnPhẩmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thêmMặtHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Dong = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTimTenSP = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_quanLySanPham)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_MaSP);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbo_TenSP);
            this.groupBox1.Controls.Add(this.cbo_LoaiSP);
            this.groupBox1.Controls.Add(this.btn_Xoa);
            this.groupBox1.Controls.Add(this.btn_Them);
            this.groupBox1.Controls.Add(this.btn_Sua);
            this.groupBox1.Controls.Add(this.dtpHSD);
            this.groupBox1.Controls.Add(this.txt_SL);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_GiaBan);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(13, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(974, 160);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin sản phẩm";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txt_MaSP
            // 
            this.txt_MaSP.Location = new System.Drawing.Point(480, 35);
            this.txt_MaSP.Name = "txt_MaSP";
            this.txt_MaSP.ReadOnly = true;
            this.txt_MaSP.Size = new System.Drawing.Size(233, 26);
            this.txt_MaSP.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(355, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 20);
            this.label7.TabIndex = 5;
            this.label7.Text = "Mã Sản phẩm";
            // 
            // cbo_TenSP
            // 
            this.cbo_TenSP.FormattingEnabled = true;
            this.cbo_TenSP.Location = new System.Drawing.Point(103, 70);
            this.cbo_TenSP.Name = "cbo_TenSP";
            this.cbo_TenSP.Size = new System.Drawing.Size(190, 28);
            this.cbo_TenSP.TabIndex = 4;
            // 
            // cbo_LoaiSP
            // 
            this.cbo_LoaiSP.FormattingEnabled = true;
            this.cbo_LoaiSP.Location = new System.Drawing.Point(103, 29);
            this.cbo_LoaiSP.Name = "cbo_LoaiSP";
            this.cbo_LoaiSP.Size = new System.Drawing.Size(190, 28);
            this.cbo_LoaiSP.TabIndex = 2;
            // 
            // btn_Xoa
            // 
            this.btn_Xoa.Location = new System.Drawing.Point(880, 109);
            this.btn_Xoa.Name = "btn_Xoa";
            this.btn_Xoa.Size = new System.Drawing.Size(64, 30);
            this.btn_Xoa.TabIndex = 3;
            this.btn_Xoa.Text = "Xóa";
            this.btn_Xoa.UseVisualStyleBackColor = true;
            this.btn_Xoa.Click += new System.EventHandler(this.btn_Xoa_Click);
            // 
            // btn_Them
            // 
            this.btn_Them.Location = new System.Drawing.Point(740, 109);
            this.btn_Them.Name = "btn_Them";
            this.btn_Them.Size = new System.Drawing.Size(64, 29);
            this.btn_Them.TabIndex = 3;
            this.btn_Them.Text = "Thêm";
            this.btn_Them.UseVisualStyleBackColor = true;
            this.btn_Them.Click += new System.EventHandler(this.btn_Them_Click);
            // 
            // btn_Sua
            // 
            this.btn_Sua.Location = new System.Drawing.Point(810, 109);
            this.btn_Sua.Name = "btn_Sua";
            this.btn_Sua.Size = new System.Drawing.Size(64, 29);
            this.btn_Sua.TabIndex = 3;
            this.btn_Sua.Text = "Sửa";
            this.btn_Sua.UseVisualStyleBackColor = true;
            this.btn_Sua.Click += new System.EventHandler(this.btn_Sua_Click);
            // 
            // dtpHSD
            // 
            this.dtpHSD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHSD.Location = new System.Drawing.Point(436, 115);
            this.dtpHSD.Name = "dtpHSD";
            this.dtpHSD.Size = new System.Drawing.Size(120, 26);
            this.dtpHSD.TabIndex = 3;
            // 
            // txt_SL
            // 
            this.txt_SL.Location = new System.Drawing.Point(103, 110);
            this.txt_SL.Name = "txt_SL";
            this.txt_SL.Size = new System.Drawing.Size(61, 26);
            this.txt_SL.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(355, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Giá bán";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Số lượng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Loại SP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên SP";
            // 
            // txt_GiaBan
            // 
            this.txt_GiaBan.Location = new System.Drawing.Point(436, 77);
            this.txt_GiaBan.Name = "txt_GiaBan";
            this.txt_GiaBan.Size = new System.Drawing.Size(120, 26);
            this.txt_GiaBan.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(355, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "HSD";
            // 
            // dgv_quanLySanPham
            // 
            this.dgv_quanLySanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_quanLySanPham.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSP,
            this.colTenSP,
            this.colLoaiSP,
            this.colSL,
            this.colGiaBan,
            this.colHSD,
            this.colGiamGia});
            this.dgv_quanLySanPham.Location = new System.Drawing.Point(13, 281);
            this.dgv_quanLySanPham.Name = "dgv_quanLySanPham";
            this.dgv_quanLySanPham.RowHeadersWidth = 62;
            this.dgv_quanLySanPham.RowTemplate.Height = 28;
            this.dgv_quanLySanPham.Size = new System.Drawing.Size(1218, 286);
            this.dgv_quanLySanPham.TabIndex = 6;
            this.dgv_quanLySanPham.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_quanLySanPham_CellContentClick);
            // 
            // colMaSP
            // 
            this.colMaSP.HeaderText = "Mã SP";
            this.colMaSP.MinimumWidth = 8;
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.Width = 150;
            // 
            // colTenSP
            // 
            this.colTenSP.HeaderText = "Tên SP";
            this.colTenSP.MinimumWidth = 8;
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.Width = 150;
            // 
            // colLoaiSP
            // 
            this.colLoaiSP.HeaderText = "Loại SP";
            this.colLoaiSP.MinimumWidth = 8;
            this.colLoaiSP.Name = "colLoaiSP";
            this.colLoaiSP.Width = 150;
            // 
            // colSL
            // 
            this.colSL.HeaderText = "SL";
            this.colSL.MinimumWidth = 8;
            this.colSL.Name = "colSL";
            this.colSL.Width = 150;
            // 
            // colGiaBan
            // 
            this.colGiaBan.HeaderText = "Giá bán";
            this.colGiaBan.MinimumWidth = 8;
            this.colGiaBan.Name = "colGiaBan";
            this.colGiaBan.Width = 150;
            // 
            // colHSD
            // 
            this.colHSD.HeaderText = "HSD";
            this.colHSD.MinimumWidth = 8;
            this.colHSD.Name = "colHSD";
            this.colHSD.Width = 150;
            // 
            // colGiamGia
            // 
            this.colGiamGia.HeaderText = "Giảm giá";
            this.colGiamGia.MinimumWidth = 10;
            this.colGiamGia.Name = "colGiamGia";
            this.colGiamGia.ReadOnly = true;
            this.colGiamGia.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colGiamGia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colGiamGia.Width = 200;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.danhMụcToolStripMenuItem,
            this.hóaĐơnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1312, 33);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // danhMụcToolStripMenuItem
            // 
            this.danhMụcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sảnPhẩmToolStripMenuItem});
            this.danhMụcToolStripMenuItem.Name = "danhMụcToolStripMenuItem";
            this.danhMụcToolStripMenuItem.Size = new System.Drawing.Size(103, 29);
            this.danhMụcToolStripMenuItem.Text = "Hệ thống";
            // 
            // sảnPhẩmToolStripMenuItem
            // 
            this.sảnPhẩmToolStripMenuItem.Name = "sảnPhẩmToolStripMenuItem";
            this.sảnPhẩmToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.sảnPhẩmToolStripMenuItem.Text = "Đăng xuất";
            // 
            // hóaĐơnToolStripMenuItem
            // 
            this.hóaĐơnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thêmLoạiSảnPhẩmToolStripMenuItem,
            this.thêmMặtHàngToolStripMenuItem});
            this.hóaĐơnToolStripMenuItem.Name = "hóaĐơnToolStripMenuItem";
            this.hóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(109, 29);
            this.hóaĐơnToolStripMenuItem.Text = "Danh mục";
            // 
            // thêmLoạiSảnPhẩmToolStripMenuItem
            // 
            this.thêmLoạiSảnPhẩmToolStripMenuItem.Name = "thêmLoạiSảnPhẩmToolStripMenuItem";
            this.thêmLoạiSảnPhẩmToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
            this.thêmLoạiSảnPhẩmToolStripMenuItem.Text = "Thêm loại sản phẩm";
            this.thêmLoạiSảnPhẩmToolStripMenuItem.Click += new System.EventHandler(this.thêmLoạiSảnPhẩmToolStripMenuItem_Click);
            // 
            // thêmMặtHàngToolStripMenuItem
            // 
            this.thêmMặtHàngToolStripMenuItem.Name = "thêmMặtHàngToolStripMenuItem";
            this.thêmMặtHàngToolStripMenuItem.Size = new System.Drawing.Size(274, 34);
            this.thêmMặtHàngToolStripMenuItem.Text = "Thêm mặt hàng";
            this.thêmMặtHàngToolStripMenuItem.Click += new System.EventHandler(this.thêmMặtHàngToolStripMenuItem_Click);
            // 
            // btn_Dong
            // 
            this.btn_Dong.Location = new System.Drawing.Point(1156, 573);
            this.btn_Dong.Name = "btn_Dong";
            this.btn_Dong.Size = new System.Drawing.Size(75, 33);
            this.btn_Dong.TabIndex = 9;
            this.btn_Dong.Text = "Đóng";
            this.btn_Dong.UseVisualStyleBackColor = true;
            this.btn_Dong.Click += new System.EventHandler(this.btn_Dong_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(663, 246);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(208, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "Tìm kiếm theo tên sản phẩm";
            // 
            // txtTimTenSP
            // 
            this.txtTimTenSP.Location = new System.Drawing.Point(880, 243);
            this.txtTimTenSP.Name = "txtTimTenSP";
            this.txtTimTenSP.Size = new System.Drawing.Size(275, 26);
            this.txtTimTenSP.TabIndex = 1;
            this.txtTimTenSP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimTenSP_KeyPress);
            // 
            // frm_managerProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 675);
            this.Controls.Add(this.txtTimTenSP);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btn_Dong);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_quanLySanPham);
            this.Controls.Add(this.menuStrip1);
            this.Name = "frm_managerProduct";
            this.Text = "HỆ THỐNG QUẢN LÝ THỰC PHẨM - [Sản Phẩm]";
            this.Load += new System.EventHandler(this.frm_managerProduct_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_quanLySanPham)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbo_LoaiSP;
        private System.Windows.Forms.Button btn_Xoa;
        private System.Windows.Forms.Button btn_Sua;
        private System.Windows.Forms.DateTimePicker dtpHSD;
        private System.Windows.Forms.TextBox txt_SL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_GiaBan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgv_quanLySanPham;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sảnPhẩmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hóaĐơnToolStripMenuItem;
        private System.Windows.Forms.Button btn_Dong;
        private System.Windows.Forms.Button btn_Them;
        private System.Windows.Forms.ComboBox cbo_TenSP;
        private System.Windows.Forms.TextBox txt_MaSP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaBan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHSD;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colGiamGia;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTimTenSP;
        private System.Windows.Forms.ToolStripMenuItem thêmLoạiSảnPhẩmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thêmMặtHàngToolStripMenuItem;
    }
}