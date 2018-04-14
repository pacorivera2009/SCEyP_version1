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
        SociosController socios_controller = new SociosController();
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
                            asociados asociados = historial_pagos.asociados(long.Parse(txtClave.Text));

                            if(asociados != null)
                            {
                                txtNombre.Text = asociados.aso_nombre + " " + asociados.aso_apellidos;
                                txtTelefono.Text = asociados.aso_telefono;
                                txtCelular.Text = asociados.aso_movil;
                                txtCorreo.Text = asociados.aso_correoelectronico;

                                fotosasociados foto_perfil;

                                //FOTO DE PERFIL DEL SOCIO//
                                foto_perfil = socios_controller.fotosasociados(asociados.aso_id);

                                if (foto_perfil != null)
                                {
                                    byte[] imagenBuffer = foto_perfil.fot_fotoperfil;
                                    System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

                                    pbxPerfil.Image = Image.FromStream(ms);
                                }
                            }

                            var mostrar_resultados = (from a in busqueda_socio_pagos
                                                      select new
                                                      {
                                                          contrato_prestamo = a.pre_id,
                                                          subtotal = a.tra_subtotal,
                                                          interes = a.tra_interes.ToString("C"),
                                                          total = (a.tra_subtotal + a.tra_interes).ToString("C"),
                                                          fecha = a.tra_fecha
                                                          
                                                      }).ToList();

                            txtClave.Enabled = false;

                            dgvPagos.DataSource = mostrar_resultados;

                            dgvPagos.Columns[1].HeaderText = "Contrato";
                            dgvPagos.Columns[2].HeaderText = "Subtotal";
                            dgvPagos.Columns[3].HeaderText = "Intéres";
                            dgvPagos.Columns[4].HeaderText = "Total";
                            dgvPagos.Columns[5].HeaderText = "Fecha";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
