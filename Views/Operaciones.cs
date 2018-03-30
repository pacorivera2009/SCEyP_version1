using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controllers;

namespace Views
{
    public partial class Operaciones : Form
    {
        OperacionesController operacionescontroller = new OperacionesController();
        public Operaciones()
        {
            InitializeComponent();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //VERIFICAMOS SI SE INTRODUCIENDO UN NUMERO O NO.
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (e.KeyChar == (Char)Keys.Enter)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                }

                //BUSQUEDA A PARTIR DE PRESION DE LA TECLA ENTER
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (txtNombre.Text == "")
                    {
                        MessageBox.Show("Introduzca el número de operación", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNombre.Focus();

                        dgvSocios.DataSource = null;
                    }
                    else
                    {
                        var operaciones = operacionescontroller.operaciones(Convert.ToInt64(txtNombre.Text));

                        if (operaciones.Count < 1)
                        {
                            MessageBox.Show("¡Sin resultados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgvSocios.DataSource = null;
                        }
                        else
                        {
                            var consulta = from a in operaciones
                                           select new
                                           {
                                               a.tra_id,
                                               subtotal = "$" + a.tra_subtotal,
                                               interes = "$" + a.tra_interes,
                                               total = "$" + a.tra_total,
                                               a.tra_fecha
                                           };

                            dgvSocios.DataSource = consulta.ToList();
                            dgvSocios.Columns[0].HeaderText = "No. transacción";
                            dgvSocios.Columns[1].HeaderText = "Subtotal pagado";
                            dgvSocios.Columns[2].HeaderText = "Intéres pagado";
                            dgvSocios.Columns[3].HeaderText = "Total pagado";
                            dgvSocios.Columns[4].HeaderText = "Fecha";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Operaciones_Load(object sender, EventArgs e)
        {
            txtNombre.Focus();
        }
    }
}
