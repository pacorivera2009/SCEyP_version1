using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Views
{
    public partial class Reportes_menu : Form
    {
        public Reportes_menu()
        {
            InitializeComponent();
        }

        private void lblSubMenu11_Click(object sender, EventArgs e)
        {
            Historial_Pagos historial_pagos = new Historial_Pagos();
            historial_pagos.ShowDialog();
        }
    }
}
