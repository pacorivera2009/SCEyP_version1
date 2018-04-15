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
    public partial class Relación_Socios_Adeudan : Form
    {
        Relacion_Socios_AdeudanController relacionsocios = new Relacion_Socios_AdeudanController();
        public Relación_Socios_Adeudan()
        {
            InitializeComponent();
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void txtClave_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(txtClave.Text != String.Empty && txtClave.Text != "")
                {
                    var consulta = relacionsocios.relacion_socios().Where(a => a.aso_id == long.Parse(txtClave.Text));
                    var listado = from c in consulta
                                  select new
                                  {
                                      c.aso_id,
                                      c.aso_nombre,
                                      c.aso_domicilio,
                                      aso_por_pagar = c.pag_pagar.ToString("C"),
                                      c.pag_fechapago
                                  };

                    if (listado.ToList().Count == 0)
                    {
                        dgvPrestamos.DataSource = null;
                    }
                    else
                    {
                        dgvPrestamos.DataSource = listado.ToList();

                        dgvPrestamos.Columns[0].HeaderText = "No. de socio";
                        dgvPrestamos.Columns[1].HeaderText = "Nombre";
                        dgvPrestamos.Columns[2].HeaderText = "Domicilio";
                        dgvPrestamos.Columns[3].HeaderText = "Pendiente de pago";
                        dgvPrestamos.Columns[4].HeaderText = "Fecha de pago";
                    }
                }
                else
                {
                    var consulta = relacionsocios.relacion_socios();
                    var listado = from c in consulta
                                  select new
                                  {
                                      c.aso_id,
                                      c.aso_nombre,
                                      c.aso_domicilio,
                                      aso_por_pagar = c.pag_pagar.ToString("C"),
                                      c.pag_fechapago
                                  };

                    if (listado.ToList().Count == 0)
                    {
                        dgvPrestamos.DataSource = null;
                    }
                    else
                    {
                        dgvPrestamos.DataSource = listado.ToList();

                        dgvPrestamos.Columns[0].HeaderText = "No. de socio";
                        dgvPrestamos.Columns[1].HeaderText = "Nombre";
                        dgvPrestamos.Columns[2].HeaderText = "Domicilio";
                        dgvPrestamos.Columns[3].HeaderText = "Pendiente de pago";
                        dgvPrestamos.Columns[4].HeaderText = "Fecha de pago";
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Socios_busqueda sociosbusqueda = new Socios_busqueda();
            sociosbusqueda.ShowDialog();
            txtClave.Text = sociosbusqueda.id;
            txtClave.Focus();
        }

        private void Relación_Socios_Adeudan_Load(object sender, EventArgs e)
        {
            try
            {
                var consulta = relacionsocios.relacion_socios();
                var listado = from c in consulta
                              select new
                              {
                                  c.aso_id,
                                  c.aso_nombre,
                                  c.aso_domicilio,
                                  aso_por_pagar = c.pag_pagar.ToString("C"),
                                  c.pag_fechapago
                              };

                if(listado.ToList().Count == 0)
                {
                    dgvPrestamos.DataSource = null;
                }
                else
                {
                    dgvPrestamos.DataSource = listado.ToList();

                    dgvPrestamos.Columns[0].HeaderText = "No. de socio";
                    dgvPrestamos.Columns[1].HeaderText = "Nombre";
                    dgvPrestamos.Columns[2].HeaderText = "Domicilio";
                    dgvPrestamos.Columns[3].HeaderText = "Pendiente de pago";
                    dgvPrestamos.Columns[4].HeaderText = "Fecha de pago";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                var consulta = relacionsocios.relacion_socios();
                var listado = from c in consulta
                              select new
                              {
                                  c.aso_id,
                                  c.aso_nombre,
                                  c.aso_domicilio,
                                  aso_por_pagar = c.pag_pagar.ToString("C"),
                                  c.pag_fechapago
                              };

                if (listado.ToList().Count == 0)
                {
                    MessageBox.Show("¡Sin resultados encontrados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    DialogResult mensaje = MessageBox.Show("¿Desea mostrar el reporte de socios con pagos con demora?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if(mensaje == DialogResult.Yes)
                    {
                        //ENLAZO A REPORTE
                        Reportes.Rep_SociosAdeudan socios = new Reportes.Rep_SociosAdeudan();
                        socios.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtClave.Text != String.Empty && txtClave.Text != "")
                {
                    var consulta = relacionsocios.relacion_socios().Where(a => a.aso_id == long.Parse(txtClave.Text));
                    var listado = from c in consulta
                                  select new
                                  {
                                      c.aso_id,
                                      c.aso_nombre,
                                      c.aso_domicilio,
                                      aso_por_pagar = c.pag_pagar.ToString("C"),
                                      c.pag_fechapago
                                  };

                    if(listado.ToList().Count == 0)
                    {
                        MessageBox.Show("¡Sin resultados encontrados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DialogResult mensaje = MessageBox.Show("¿Desea mostrar el informe de pagos con demora del socio?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if(mensaje == DialogResult.Yes)
                        {
                            Reportes.Rep_SociosDebe socios_desgloce = new Reportes.Rep_SociosDebe();
                            socios_desgloce.id = long.Parse(txtClave.Text);
                            socios_desgloce.ShowDialog();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void busquedaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void reporteIndividualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnImprimir_Click(sender, e);
        }

        private void reporteGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnReporte_Click(sender, e);
        }
    }
}
