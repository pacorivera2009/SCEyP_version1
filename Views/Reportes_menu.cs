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

        private void lblSubMenu11_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu11.ForeColor = Color.Black;
        }

        private void lblSubMenu11_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu11.ForeColor = Color.Red;
        }

        private void lblSubMenu12_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu12.ForeColor = Color.Black;
        }

        private void lblSubMenu12_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu12.ForeColor = Color.Red;
        }

        private void lblSubMenu21_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu21.ForeColor = Color.Black;
        }

        private void lblSubMenu21_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu21.ForeColor = Color.Red;
        }

        private void lblSubMenu31_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu31.ForeColor = Color.Black;
        }

        private void lblSubMenu31_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu31.ForeColor = Color.Red;
        }

        private void lblSubMenu31_Click(object sender, EventArgs e)
        {
            Contabilidad contabilidad = new Contabilidad();
            contabilidad.ShowDialog();
        }

        private void lblSubMenu21_Click(object sender, EventArgs e)
        {
            Historial_Prestamos historial = new Historial_Prestamos();
            historial.ShowDialog();
        }

        private void lblSubMenu12_Click(object sender, EventArgs e)
        {
            Relación_Socios_Adeudan adeudan = new Relación_Socios_Adeudan();
            adeudan.ShowDialog();
        }
    }
}
