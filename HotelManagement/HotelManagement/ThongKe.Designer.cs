namespace HotelManagement
{
    partial class ThongKe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThongKe));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_chonthang = new System.Windows.Forms.ComboBox();
            this.btn_xemdoanhthu = new System.Windows.Forms.Button();
            this.dataGridViewThongKe = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_tongdoanhthu = new System.Windows.Forms.TextBox();
            this.btn_thoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThongKe)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(309, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn tháng ";
            // 
            // comboBox_chonthang
            // 
            this.comboBox_chonthang.FormattingEnabled = true;
            this.comboBox_chonthang.Location = new System.Drawing.Point(445, 27);
            this.comboBox_chonthang.Name = "comboBox_chonthang";
            this.comboBox_chonthang.Size = new System.Drawing.Size(121, 24);
            this.comboBox_chonthang.TabIndex = 1;
            // 
            // btn_xemdoanhthu
            // 
            this.btn_xemdoanhthu.Location = new System.Drawing.Point(585, 17);
            this.btn_xemdoanhthu.Name = "btn_xemdoanhthu";
            this.btn_xemdoanhthu.Size = new System.Drawing.Size(93, 40);
            this.btn_xemdoanhthu.TabIndex = 2;
            this.btn_xemdoanhthu.Text = "Xem";
            this.btn_xemdoanhthu.UseVisualStyleBackColor = true;
            this.btn_xemdoanhthu.Click += new System.EventHandler(this.btn_xemdoanhthu_Click);
            // 
            // dataGridViewThongKe
            // 
            this.dataGridViewThongKe.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewThongKe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewThongKe.GridColor = System.Drawing.Color.White;
            this.dataGridViewThongKe.Location = new System.Drawing.Point(12, 71);
            this.dataGridViewThongKe.Name = "dataGridViewThongKe";
            this.dataGridViewThongKe.RowHeadersWidth = 51;
            this.dataGridViewThongKe.RowTemplate.Height = 24;
            this.dataGridViewThongKe.Size = new System.Drawing.Size(1069, 435);
            this.dataGridViewThongKe.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(333, 528);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tổng doanh thu";
            // 
            // txt_tongdoanhthu
            // 
            this.txt_tongdoanhthu.Location = new System.Drawing.Point(527, 531);
            this.txt_tongdoanhthu.Name = "txt_tongdoanhthu";
            this.txt_tongdoanhthu.Size = new System.Drawing.Size(232, 22);
            this.txt_tongdoanhthu.TabIndex = 5;
            // 
            // btn_thoat
            // 
            this.btn_thoat.Location = new System.Drawing.Point(694, 17);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(93, 40);
            this.btn_thoat.TabIndex = 6;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.UseVisualStyleBackColor = true;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // ThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1093, 587);
            this.Controls.Add(this.btn_thoat);
            this.Controls.Add(this.txt_tongdoanhthu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridViewThongKe);
            this.Controls.Add(this.btn_xemdoanhthu);
            this.Controls.Add(this.comboBox_chonthang);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê doanh thu";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThongKe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_chonthang;
        private System.Windows.Forms.Button btn_xemdoanhthu;
        private System.Windows.Forms.DataGridView dataGridViewThongKe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_tongdoanhthu;
        private System.Windows.Forms.Button btn_thoat;
    }
}