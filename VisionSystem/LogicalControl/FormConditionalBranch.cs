﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionSystem
{
    public partial class FormConditionalBranch : Form
    {
        FormBase frmBase;
        public FormConditionalBranch(FormBase _frmBase)
        {
            InitializeComponent();
            frmBase = _frmBase;
        }
    }
}
