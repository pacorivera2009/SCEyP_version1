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

                        btnBuscar.Enabled = true;
                        btnCancelar.Enabled = false;
                        btnImprimir.Enabled = false;

                        dgvPagos.DataSource = null;

                        txtClave.Enabled = true;
                        txtClave.Clear();
                        txtClave.Focus();
                    }
                    else
                    {
                        var busqueda_socio_pagos = historial_pagos.relacion_pagos_socio(long.Parse(txtClave.Text)).ToList();

                        if(busqueda_socio_pagos.Count == 0)
                        {
                            MessageBox.Show("Sin resultados encontrados", "Información", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);

                            btnBuscar.Enabled = true;
                            btnCancelar.Enabled = false;
                            btnImprimir.Enabled = false;

                            dgvPagos.DataSource = null;

                            txtClave.Enabled = true;
                            txtClave.Clear();
                            txtClave.Focus();
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
                                                          a.ope_id,
                                                          contrato_prestamo = a.pre_id,
                                                          subtotal = a.tra_subtotal.ToString("C"),
                                                          interes = a.tra_interes.ToString("C"),
                                                          total = (a.tra_subtotal + a.tra_interes).ToString("C"),
                                                          fecha = a.ope_fecha
                                                          
                                                      }).ToList();

                            txtClave.Enabled = false;
                            btnImprimir.Enabled = true;
                            btnBuscar.Enabled = false;
                            btnCancelar.Enabled = true;


                            dgvPagos.DataSource = mostrar_resultados;

                            dgvPagos.Columns[0].HeaderText = "No. de operación";
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if(mensaje == DialogResult.Yes)
            {
                btnBuscar.Enabled = true;
                btnCancelar.Enabled = false;
                btnImprimir.Enabled = false;

                txtCelular.Clear();
                txtCorreo.Clear();
                txtNombre.Clear();
                txtTelefono.Clear();

                pbxPerfil.Image = null;

                dgvPagos.DataSource = null;

                txtClave.Enabled = true;
                txtClave.Clear();
                txtClave.Focus();
            }
            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if(dgvPagos.CurrentRow == null)
            {
                MessageBox.Show("No hay registros de operaciones realizadas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                DialogResult mensaje = MessageBox.Show("¿Desea mostrar el historial de prestamos del socio?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(mensaje == DialogResult.Yes)
                {
                    Reportes.Rep_HistorialPagosSocio historial_pagos = new Reportes.Rep_HistorialPagosSocio();
                    historial_pagos.id = long.Parse(txtClave.Text);
                    historial_pagos.ShowDialog();
                }
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnImprimir.Enabled == true)
            {
                btnImprimir_Click(sender, e);
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnCancelar.Enabled == true)
            {
                btnCancelar_Click(sender, e);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Socios_busqueda sociosbusqueda = new Socios_busqueda();
            sociosbusqueda.ShowDialog();
            txtClave.Text = sociosbusqueda.id;
            txtClave.Focus();
        }

        private void busquedaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnBuscar.Enabled == true)
            {
                btnBuscar_Click(sender, e);
            }
        }
    }
}
