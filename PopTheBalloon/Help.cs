﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopTheBalloon
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.howtoplay;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
