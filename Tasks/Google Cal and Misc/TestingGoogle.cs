﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasks
{
    public partial class TestingGoogle : Form
    {
        public TestingGoogle()
        {
            InitializeComponent();

            Controls.Add(new GoogleCalendar(DateTime.Today));
        }
    }
}
