﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagement
{
    public partial class HomeStaff : Form
    {
        private int userId;
        public HomeStaff(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void HomeStaff_Load(object sender, EventArgs e)
        {

        }
    }
}
