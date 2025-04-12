namespace HotelManagement
{
    partial class ManageService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageService));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_tendichvu = new System.Windows.Forms.TextBox();
            this.btn_themtang = new System.Windows.Forms.Button();
            this.btn_sua = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_lammoi = new System.Windows.Forms.Button();
            this.btn_xuatexcel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView_danhsachdichvu = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_mota = new System.Windows.Forms.TextBox();
            this.txt_gia = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_danhsachdichvu)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(137, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(386, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "QUẢN LÝ DỊCH VỤ KHÁCH SẠN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(139, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tên dịch vụ:";
            // 
            // txt_tendichvu
            // 
            this.txt_tendichvu.Location = new System.Drawing.Point(265, 58);
            this.txt_tendichvu.Name = "txt_tendichvu";
            this.txt_tendichvu.Size = new System.Drawing.Size(258, 22);
            this.txt_tendichvu.TabIndex = 20;
            // 
            // btn_themtang
            // 
            this.btn_themtang.BackColor = System.Drawing.Color.Gray;
            this.btn_themtang.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_themtang.Location = new System.Drawing.Point(12, 195);
            this.btn_themtang.Name = "btn_themtang";
            this.btn_themtang.Size = new System.Drawing.Size(102, 46);
            this.btn_themtang.TabIndex = 21;
            this.btn_themtang.Text = "Thêm";
            this.btn_themtang.UseVisualStyleBackColor = false;
            this.btn_themtang.Click += new System.EventHandler(this.btn_themtang_Click);
            // 
            // btn_sua
            // 
            this.btn_sua.BackColor = System.Drawing.Color.Gray;
            this.btn_sua.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sua.Location = new System.Drawing.Point(120, 195);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(102, 46);
            this.btn_sua.TabIndex = 22;
            this.btn_sua.Text = "Sửa";
            this.btn_sua.UseVisualStyleBackColor = false;
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.BackColor = System.Drawing.Color.Gray;
            this.btn_xoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoa.Location = new System.Drawing.Point(228, 195);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(102, 46);
            this.btn_xoa.TabIndex = 23;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.UseVisualStyleBackColor = false;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_lammoi
            // 
            this.btn_lammoi.BackColor = System.Drawing.Color.Gray;
            this.btn_lammoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_lammoi.Location = new System.Drawing.Point(336, 195);
            this.btn_lammoi.Name = "btn_lammoi";
            this.btn_lammoi.Size = new System.Drawing.Size(102, 46);
            this.btn_lammoi.TabIndex = 24;
            this.btn_lammoi.Text = "Tải";
            this.btn_lammoi.UseVisualStyleBackColor = false;
            this.btn_lammoi.Click += new System.EventHandler(this.btn_lammoi_Click);
            // 
            // btn_xuatexcel
            // 
            this.btn_xuatexcel.BackColor = System.Drawing.Color.Gray;
            this.btn_xuatexcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xuatexcel.Location = new System.Drawing.Point(444, 195);
            this.btn_xuatexcel.Name = "btn_xuatexcel";
            this.btn_xuatexcel.Size = new System.Drawing.Size(102, 46);
            this.btn_xuatexcel.TabIndex = 25;
            this.btn_xuatexcel.Text = "Xuất";
            this.btn_xuatexcel.UseVisualStyleBackColor = false;
            this.btn_xuatexcel.Click += new System.EventHandler(this.btn_xuatexcel_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gray;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(552, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 46);
            this.button1.TabIndex = 26;
            this.button1.Text = "Thoát";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView_danhsachdichvu
            // 
            this.dataGridView_danhsachdichvu.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_danhsachdichvu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_danhsachdichvu.GridColor = System.Drawing.Color.Gray;
            this.dataGridView_danhsachdichvu.Location = new System.Drawing.Point(31, 247);
            this.dataGridView_danhsachdichvu.Name = "dataGridView_danhsachdichvu";
            this.dataGridView_danhsachdichvu.RowHeadersWidth = 51;
            this.dataGridView_danhsachdichvu.RowTemplate.Height = 24;
            this.dataGridView_danhsachdichvu.Size = new System.Drawing.Size(604, 415);
            this.dataGridView_danhsachdichvu.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(139, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 25);
            this.label3.TabIndex = 28;
            this.label3.Text = "Mô tả:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(137, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 25);
            this.label4.TabIndex = 29;
            this.label4.Text = "Giá:";
            // 
            // txt_mota
            // 
            this.txt_mota.Location = new System.Drawing.Point(265, 109);
            this.txt_mota.Name = "txt_mota";
            this.txt_mota.Size = new System.Drawing.Size(258, 22);
            this.txt_mota.TabIndex = 30;
            // 
            // txt_gia
            // 
            this.txt_gia.Location = new System.Drawing.Point(263, 160);
            this.txt_gia.Name = "txt_gia";
            this.txt_gia.Size = new System.Drawing.Size(260, 22);
            this.txt_gia.TabIndex = 31;
            // 
            // ManageService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(668, 674);
            this.Controls.Add(this.txt_gia);
            this.Controls.Add(this.txt_mota);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView_danhsachdichvu);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_xuatexcel);
            this.Controls.Add(this.btn_lammoi);
            this.Controls.Add(this.btn_xoa);
            this.Controls.Add(this.btn_sua);
            this.Controls.Add(this.btn_themtang);
            this.Controls.Add(this.txt_tendichvu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManageService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý dịch vụ";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_danhsachdichvu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_tendichvu;
        private System.Windows.Forms.Button btn_themtang;
        private System.Windows.Forms.Button btn_sua;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Button btn_lammoi;
        private System.Windows.Forms.Button btn_xuatexcel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView_danhsachdichvu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_mota;
        private System.Windows.Forms.TextBox txt_gia;
    }
}