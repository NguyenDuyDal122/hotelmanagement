namespace HotelManagement
{
    partial class ThemPhong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemPhong));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_sophong = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_them = new System.Windows.Forms.Button();
            this.btn_ = new System.Windows.Forms.Button();
            this.comboBox_loaiphong = new System.Windows.Forms.ComboBox();
            this.comboBox_tang = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_giatheongay = new System.Windows.Forms.TextBox();
            this.txt_giatheogio = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(236, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "THÊM PHÒNG";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(125, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "Số phòng:";
            // 
            // txt_sophong
            // 
            this.txt_sophong.Location = new System.Drawing.Point(241, 89);
            this.txt_sophong.Name = "txt_sophong";
            this.txt_sophong.Size = new System.Drawing.Size(275, 22);
            this.txt_sophong.TabIndex = 20;
            this.txt_sophong.TextChanged += new System.EventHandler(this.txt_maxroom_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(125, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 25);
            this.label3.TabIndex = 21;
            this.label3.Text = "Loại phòng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(125, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 25);
            this.label4.TabIndex = 22;
            this.label4.Text = "Tầng:";
            // 
            // btn_them
            // 
            this.btn_them.BackColor = System.Drawing.Color.Gray;
            this.btn_them.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_them.Location = new System.Drawing.Point(199, 373);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(102, 46);
            this.btn_them.TabIndex = 27;
            this.btn_them.Text = "Thêm";
            this.btn_them.UseVisualStyleBackColor = false;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // btn_
            // 
            this.btn_.BackColor = System.Drawing.Color.Gray;
            this.btn_.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_.Location = new System.Drawing.Point(331, 373);
            this.btn_.Name = "btn_";
            this.btn_.Size = new System.Drawing.Size(102, 46);
            this.btn_.TabIndex = 28;
            this.btn_.Text = "Thoát";
            this.btn_.UseVisualStyleBackColor = false;
            this.btn_.Click += new System.EventHandler(this.btn__Click);
            // 
            // comboBox_loaiphong
            // 
            this.comboBox_loaiphong.FormattingEnabled = true;
            this.comboBox_loaiphong.Location = new System.Drawing.Point(241, 145);
            this.comboBox_loaiphong.Name = "comboBox_loaiphong";
            this.comboBox_loaiphong.Size = new System.Drawing.Size(275, 24);
            this.comboBox_loaiphong.TabIndex = 29;
            this.comboBox_loaiphong.SelectedIndexChanged += new System.EventHandler(this.comboBox_loaiphong_SelectedIndexChanged);
            // 
            // comboBox_tang
            // 
            this.comboBox_tang.FormattingEnabled = true;
            this.comboBox_tang.Location = new System.Drawing.Point(241, 207);
            this.comboBox_tang.Name = "comboBox_tang";
            this.comboBox_tang.Size = new System.Drawing.Size(275, 24);
            this.comboBox_tang.TabIndex = 30;
            this.comboBox_tang.SelectedIndexChanged += new System.EventHandler(this.comboBox_tang_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(125, 261);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 25);
            this.label6.TabIndex = 32;
            this.label6.Text = "Giá ngày:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(122, 315);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 25);
            this.label7.TabIndex = 33;
            this.label7.Text = "Giá giờ:";
            // 
            // txt_giatheongay
            // 
            this.txt_giatheongay.Location = new System.Drawing.Point(241, 261);
            this.txt_giatheongay.Name = "txt_giatheongay";
            this.txt_giatheongay.Size = new System.Drawing.Size(275, 22);
            this.txt_giatheongay.TabIndex = 34;
            // 
            // txt_giatheogio
            // 
            this.txt_giatheogio.Location = new System.Drawing.Point(241, 319);
            this.txt_giatheogio.Name = "txt_giatheogio";
            this.txt_giatheogio.Size = new System.Drawing.Size(275, 22);
            this.txt_giatheogio.TabIndex = 35;
            // 
            // ThemPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(663, 508);
            this.Controls.Add(this.txt_giatheogio);
            this.Controls.Add(this.txt_giatheongay);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox_tang);
            this.Controls.Add(this.comboBox_loaiphong);
            this.Controls.Add(this.btn_);
            this.Controls.Add(this.btn_them);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_sophong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemPhong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm phòng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_sophong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.Button btn_;
        private System.Windows.Forms.ComboBox comboBox_loaiphong;
        private System.Windows.Forms.ComboBox comboBox_tang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_giatheongay;
        private System.Windows.Forms.TextBox txt_giatheogio;
    }
}