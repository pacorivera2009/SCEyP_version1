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
using Models;

namespace Views
{
    public partial class Historial_Pagos : Form
    {
        Historial_Pagos_Controller historial_pagos = new Historial_Pagos_Controller();
        public Historial_Pagos()
        {
            InitializeComponent();
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
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
                else if (e.KeyChar == 13)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                }

                if (e.KeyChar == 13)
                {
                    if (txtClave.Text == "" || txtClave.Text == String.Empty)
                    {
                        MessageBox.Show("Introduzca la clave del socio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    }
                    else
                    {
                        var busqueda_socio_pagos = historial_pagos.relacion_pagos_socio(long.Parse(txtClave.Text)).ToList();

                        if(busqueda_socio_pagos.Count == 0)
                        {
                            MessageBox.Show("Sin resultados encontrados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            var mostrar_resultados = (from a in busqueda_socio_pagos
                                                      select new
                                                      {
                                                          a.pre_id,
                                                          
                                                      }).ToList();

                            txtClave.Enabled = false;

                            
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
