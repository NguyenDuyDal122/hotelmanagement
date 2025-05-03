namespace HotelManagement
{
    partial class HoaDon
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HoaDon));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_timkiemhoadon = new System.Windows.Forms.TextBox();
            this.btn_timkiemhoadon = new System.Windows.Forms.Button();
            this.dataGridView_danhsachhoadon = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_trangthai = new System.Windows.Forms.ComboBox();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_thoat = new System.Windows.Forms.Button();
            this.btn_lammoi = new System.Windows.Forms.Button();
            this.btn_xuatexcel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_danhsachhoadon)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_xuatexcel);
            this.groupBox1.Controls.Add(this.btn_lammoi);
            this.groupBox1.Controls.Add(this.btn_thoat);
            this.groupBox1.Controls.Add(this.btn_xoa);
            this.groupBox1.Controls.Add(this.comboBox_trangthai);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dataGridView_danhsachhoadon);
            this.groupBox1.Controls.Add(this.btn_timkiemhoadon);
            this.groupBox1.Controls.Add(this.txt_timkiemhoadon);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1842, 476);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin hóa đơn";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(489, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm theo mã phiếu";
            // 
            // txt_timkiemhoadon
            // 
            this.txt_timkiemhoadon.Location = new System.Drawing.Point(705, 37);
            this.txt_timkiemhoadon.Name = "txt_timkiemhoadon";
            this.txt_timkiemhoadon.Size = new System.Drawing.Size(285, 30);
            this.txt_timkiemhoadon.TabIndex = 1;
            // 
            // btn_timkiemhoadon
            // 
            this.btn_timkiemhoadon.BackColor = System.Drawing.Color.Silver;
            this.btn_timkiemhoadon.ForeColor = System.Drawing.Color.Black;
            this.btn_timkiemhoadon.Location = new System.Drawing.Point(1016, 35);
            this.btn_timkiemhoadon.Name = "btn_timkiemhoadon";
            this.btn_timkiemhoadon.Size = new System.Drawing.Size(125, 34);
            this.btn_timkiemhoadon.TabIndex = 2;
            this.btn_timkiemhoadon.Text = "Tìm kiếm";
            this.btn_timkiemhoadon.UseVisualStyleBackColor = false;
            this.btn_timkiemhoadon.Click += new System.EventHandler(this.btn_timkiemhoadon_Click);
            // 
            // dataGridView_danhsachhoadon
            // 
            this.dataGridView_danhsachhoadon.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_danhsachhoadon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_danhsachhoadon.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_danhsachhoadon.Location = new System.Drawing.Point(6, 88);
            this.dataGridView_danhsachhoadon.MultiSelect = false;
            this.dataGridView_danhsachhoadon.Name = "dataGridView_danhsachhoadon";
            this.dataGridView_danhsachhoadon.RowHeadersWidth = 51;
            this.dataGridView_danhsachhoadon.RowTemplate.Height = 24;
            this.dataGridView_danhsachhoadon.Size = new System.Drawing.Size(1830, 309);
            this.dataGridView_danhsachhoadon.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(462, 422);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Trạng thái";
            // 
            // comboBox_trangthai
            // 
            this.comboBox_trangthai.FormattingEnabled = true;
            this.comboBox_trangthai.Location = new System.Drawing.Point(578, 419);
            this.comboBox_trangthai.Name = "comboBox_trangthai";
            this.comboBox_trangthai.Size = new System.Drawing.Size(207, 33);
            this.comboBox_trangthai.TabIndex = 5;
            // 
            // btn_xoa
            // 
            this.btn_xoa.BackColor = System.Drawing.Color.Silver;
            this.btn_xoa.ForeColor = System.Drawing.Color.Black;
            this.btn_xoa.Location = new System.Drawing.Point(800, 419);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(125, 34);
            this.btn_xoa.TabIndex = 6;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.UseVisualStyleBackColor = false;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_thoat
            // 
            this.btn_thoat.BackColor = System.Drawing.Color.Silver;
            this.btn_thoat.ForeColor = System.Drawing.Color.Black;
            this.btn_thoat.Location = new System.Drawing.Point(941, 419);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(125, 34);
            this.btn_thoat.TabIndex = 7;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.UseVisualStyleBackColor = false;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // btn_lammoi
            // 
            this.btn_lammoi.BackColor = System.Drawing.Color.Silver;
            this.btn_lammoi.ForeColor = System.Drawing.Color.Black;
            this.btn_lammoi.Location = new System.Drawing.Point(1147, 35);
            this.btn_lammoi.Name = "btn_lammoi";
            this.btn_lammoi.Size = new System.Drawing.Size(125, 34);
            this.btn_lammoi.TabIndex = 8;
            this.btn_lammoi.Text = "Làm mới";
            this.btn_lammoi.UseVisualStyleBackColor = false;
            this.btn_lammoi.Click += new System.EventHandler(this.btn_lammoi_Click);
            // 
            // btn_xuatexcel
            // 
            this.btn_xuatexcel.BackColor = System.Drawing.Color.Silver;
            this.btn_xuatexcel.ForeColor = System.Drawing.Color.Black;
            this.btn_xuatexcel.Location = new System.Drawing.Point(1088, 419);
            this.btn_xuatexcel.Name = "btn_xuatexcel";
            this.btn_xuatexcel.Size = new System.Drawing.Size(125, 34);
            this.btn_xuatexcel.TabIndex = 9;
            this.btn_xuatexcel.Text = "Xuất Excel";
            this.btn_xuatexcel.UseVisualStyleBackColor = false;
            this.btn_xuatexcel.Click += new System.EventHandler(this.btn_xuatexcel_Click);
            // 
            // HoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1866, 500);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hóa đơn";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_danhsachhoadon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_timkiemhoadon;
        private System.Windows.Forms.TextBox txt_timkiemhoadon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.ComboBox comboBox_trangthai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView_danhsachhoadon;
        private System.Windows.Forms.Button btn_thoat;
        private System.Windows.Forms.Button btn_lammoi;
        private System.Windows.Forms.Button btn_xuatexcel;
    }
}