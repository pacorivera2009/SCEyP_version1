using Controllers;
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
    public partial class Contabilidad : Form
    {

        //Controladores
        ContabilidadController contabilidadcontroller = new ContabilidadController();

        public Contabilidad()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //BUSQUEDA DE TODOS LOS PRESTAMOS
                if (radioButton1.Checked == true)
                {
                    var consulta = contabilidadcontroller.contabilidad();
                    var listado = from c in consulta
                                  select new
                                  {
                                      c.con_id,
                                      c.con_concepto,
                                      monto = "$ " + c.con_monto,
                                      interes = "$ " + c.con_interes,
                                      total = "$ " + c.con_total,
                                      c.con_fecha
                                  };

                    dgvPrestamos.DataSource = listado.ToList();

                    dgvPrestamos.Columns[0].HeaderText = "Clave";
                    dgvPrestamos.Columns[1].HeaderText = "Concepto";
                    dgvPrestamos.Columns[2].HeaderText = "Monto";
                    dgvPrestamos.Columns[3].HeaderText = "Intéres";
                    dgvPrestamos.Columns[4].HeaderText = "Total";
                    dgvPrestamos.Columns[5].HeaderText = "Fecha";

                    var contabilidad_totales = contabilidadcontroller.contabilidadTotales(null, null).ToList();

                    foreach (var valores in contabilidad_totales)
                    {
                        txtTotalEntradas.Text = "$ " + valores.totalentradas.ToString();
                        txtTotalSalidas.Text = "$ " + valores.totalsalidas.ToString();
                        txtTotal.Text = "$ " + valores.total.ToString();
                    }
                }
                else //PRESTAMOS BUSCADOS MEDIANTE UN RANGO DE FECHA
                {
                    var consulta = contabilidadcontroller.contabilidadPeriodo(Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString()), Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString()));

                    var listado = from c in consulta
                                  select new
                                  {
                                      c.con_id,
                                      c.con_concepto,
                                      monto = "$ " + c.con_monto,
                                      interes = "$ " + c.con_interes,
                                      total = "$ " + c.con_total,
                                      c.con_fecha
                                  };

                    dgvPrestamos.DataSource = listado.ToList();

                    dgvPrestamos.Columns[0].HeaderText = "Clave";
                    dgvPrestamos.Columns[1].HeaderText = "Concepto";
                    dgvPrestamos.Columns[2].HeaderText = "Monto";
                    dgvPrestamos.Columns[3].HeaderText = "Intéres";
                    dgvPrestamos.Columns[4].HeaderText = "Total";
                    dgvPrestamos.Columns[5].HeaderText = "Fecha";

                    var contabilidad_totales = contabilidadcontroller.contabilidadTotales(dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString()).ToList();

                    foreach (var valores in contabilidad_totales)
                    {
                        txtTotalEntradas.Text = "$ " + valores.totalentradas.ToString();
                        txtTotalSalidas.Text = "$ " + valores.totalsalidas.ToString();
                        txtTotal.Text = "$ " + valores.total.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Contabilidad_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(btnBuscar, "De clíc aquí o presione F2 para buscar");
                notificacion.SetToolTip(btnImprimir, "De clíc aquí para mostrar la contabilidad o presione F10");
                notificacion.SetToolTip(btnCorteMensual, "De clíc aquí para realizar el corte de contabilidad mensual o presione F9");

                //BUSQUEDA DE TODOS LOS PRESTAMOS
                var consulta = contabilidadcontroller.contabilidad();

                var listado = from c in consulta
                              select new
                              {
                                  c.con_id,
                                  c.con_concepto,
                                  monto = "$ " + c.con_monto,
                                  interes = "$ " + c.con_interes,
                                  total = "$ " + c.con_total,
                                  c.con_fecha
                              };

                dgvPrestamos.DataSource = listado.ToList();

                dgvPrestamos.Columns[0].HeaderText = "Clave";
                dgvPrestamos.Columns[1].HeaderText = "Concepto";
                dgvPrestamos.Columns[2].HeaderText = "Monto";
                dgvPrestamos.Columns[3].HeaderText = "Intéres";
                dgvPrestamos.Columns[4].HeaderText = "Total";
                dgvPrestamos.Columns[5].HeaderText = "Fecha";

                var contabilidad_totales = contabilidadcontroller.contabilidadTotales(null, null).ToList();

                foreach(var valores in contabilidad_totales)
                {
                    txtTotalEntradas.Text = "$ " + valores.totalentradas.ToString();
                    txtTotalSalidas.Text = "$ " + valores.totalsalidas.ToString();
                    txtTotal.Text = "$ " + valores.total.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //en caso dado de que este seleccionado el control radiobutton se deshabilita el control groupbox
            if (radioButton1.Checked == true)
            {
                groupBox3.Enabled = false;
            }
            else //se habilita el control en caso que no este seleccionado el control
            {
                groupBox3.Enabled = true;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea mostrar el reporte de contabilidad?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(mensaje == DialogResult.Yes)
            {
                Reportes.Rep_Contabilidad imprimir = new Reportes.Rep_Contabilidad();
                imprimir.ShowDialog();
            }
        }

        private void btnCorteMensual_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Esta seguro de realizar el corte mensual?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (mensaje == DialogResult.Yes)
                {
                    DialogResult mensaje2 = MessageBox.Show("¿Confirmar la acción?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje2 == DialogResult.Yes)
                    {
                        contabilidadcontroller.CorteMensual();

                        //BUSQUEDA DE TODOS LOS PRESTAMOS
                        var consulta = contabilidadcontroller.contabilidad();

                        radioButton1.Checked = true;
                        radioButton2.Checked = false;

                        var listado = from c in consulta
                                      select new
                                      {
                                          c.con_id,
                                          c.con_concepto,
                                          monto = "$ " + c.con_monto,
                                          interes = "$ " + c.con_interes,
                                          total = "$ " + c.con_total,
                                          c.con_fecha
                                      };

                        dgvPrestamos.DataSource = listado.ToList();

                        dgvPrestamos.Columns[0].HeaderText = "Clave";
                        dgvPrestamos.Columns[1].HeaderText = "Concepto";
                        dgvPrestamos.Columns[2].HeaderText = "Monto";
                        dgvPrestamos.Columns[3].HeaderText = "Intéres";
                        dgvPrestamos.Columns[4].HeaderText = "Total";
                        dgvPrestamos.Columns[5].HeaderText = "Fecha";

                        var contabilidad_totales = contabilidadcontroller.contabilidadTotales(null, null).ToList();

                        foreach (var valores in contabilidad_totales)
                        {
                            txtTotalEntradas.Text = "$ " + valores.totalentradas.ToString();
                            txtTotalSalidas.Text = "$ " + valores.totalsalidas.ToString();
                            txtTotal.Text = "$ " + valores.total.ToString();
                        }

                        MessageBox.Show("¡El corte mensual fue realizado existosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void corteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCorteMensual_Click(sender, e);
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnImprimir_Click(sender, e);
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }
    }
}
