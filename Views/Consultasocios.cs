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
    public partial class Consultasocios : Form
    {
        //CONTROLADORES
        SociosController socioscontroller = new SociosController();
        Rep_SociosController reportes = new Rep_SociosController();

        fotosasociados fotosasociados;
        public Consultasocios()
        {
            InitializeComponent();
        }

        private void Consultasocios_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(this.btnImprimir, "De clíc aquí para mostrar el informe de socios registrados");
                notificacion.SetToolTip(this.btnSocios, "De clíc aquí para mostrar la información del socio seleccionado");
                notificacion.SetToolTip(this.btnBuscar, "De clíc aquí para buscar");


                dgvSocios.DataSource = socioscontroller.dataGridViewSocios();
                dgvSocios.Columns[0].HeaderText = "Clave";
                dgvSocios.Columns[1].HeaderText = "Nombre";
                dgvSocios.Columns[2].HeaderText = "Sexo";
                dgvSocios.Columns[3].HeaderText = "Fecha de nacimiento";
                dgvSocios.Columns[4].HeaderText = "Estado civil";
                dgvSocios.Columns[5].HeaderText = "Teléfono";
                dgvSocios.Columns[6].HeaderText = "Teléfono móvil";
                dgvSocios.Columns[7].HeaderText = "Correo electrónico";
                dgvSocios.Columns[8].Visible = false;
                dgvSocios.Columns[9].Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                int bandera1 = 0, bandera2 = 0;

                if(txtNombre.Text == "")
                {
                    lblValidacion1.Text = "* Complete este campo";
                    lblValidacion1.Visible = true;
                    bandera1 = 0;
                }
                else
                {
                    lblValidacion1.Visible = false;
                    bandera1 = 1;
                }

                if(txtApellidos.Text == "")
                {
                    lblValidacion2.Text = "* Complete este campo";
                    lblValidacion2.Visible = true;
                    bandera2 = 0;
                }
                else
                {
                    lblValidacion2.Visible = false;
                    bandera2 = 1;
                }

                if(bandera1 == 1 && bandera2 == 1)
                {
                    DateTime fecha_nacimiento = Convert.ToDateTime(Convert.ToString(dtpFechanacimiento.Value.ToShortDateString()));
                    dgvSocios.DataSource = socioscontroller.dataGridViewSocios().Where(s => s.aso_nombre == txtNombre.Text && s.aso_apellidos == txtApellidos.Text && s.aso_fechanacimiento == fecha_nacimiento);
                    dgvSocios.Columns[0].HeaderText = "Clave";
                    dgvSocios.Columns[1].HeaderText = "Nombre";
                    dgvSocios.Columns[2].HeaderText = "Sexo";
                    dgvSocios.Columns[3].HeaderText = "Fecha de nacimiento";
                    dgvSocios.Columns[4].HeaderText = "Estado civil";
                    dgvSocios.Columns[5].HeaderText = "Teléfono";
                    dgvSocios.Columns[6].HeaderText = "Teléfono móvil";
                    dgvSocios.Columns[7].HeaderText = "Correo electrónico";
                    dgvSocios.Columns[8].Visible = false;
                    dgvSocios.Columns[9].Visible = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSocios.CurrentRow == null)
                {
                    MessageBox.Show("¡Sin registros!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult mensaje = MessageBox.Show("¿Desea mostrar el informe de los socios registrados en el sistema?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        Reportes.Rep_Socios socios = new Reportes.Rep_Socios();
                        socios.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSocios_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSocios.CurrentRow == null)
                {
                    MessageBox.Show("¡No hay socios solicitados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult mensaje = MessageBox.Show("¿Desea mostrar la información del socio seleccionado?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if(mensaje == DialogResult.Yes)
                    {
                        string tipo_ingreso = reportes.tipo_ingreso(Convert.ToInt64(dgvSocios.CurrentRow.Cells[0].Value.ToString()));

                        if (tipo_ingreso != "BUEN HISTORIAL CREDITICIO") //CUANDO TIENE AVAL
                        {
                            Reportes.Rep_SociosIndividuales_2 socios2 = new Reportes.Rep_SociosIndividuales_2();
                            socios2.id_asociado = Convert.ToInt64(dgvSocios.CurrentRow.Cells[0].Value.ToString());
                            socios2.ShowDialog();
                        }
                        else
                        {
                            Reportes.Rep_SociosIndividuales_1 socios1 = new Reportes.Rep_SociosIndividuales_1();
                            socios1.id_asociado = Convert.ToInt64(dgvSocios.CurrentRow.Cells[0].Value.ToString());
                            socios1.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sociosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnImprimir_Click(sender, e);
        }

        private void sociosIndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSocios_Click(sender, e);
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void dgvSocios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvSocios.Rows.Count != 0)
                {
                    fotosasociados = socioscontroller.fotosasociados(Convert.ToInt64(dgvSocios.CurrentRow.Cells[0].Value.ToString()));

                    if (fotosasociados != null)
                    {
                        byte[] imagenBuffer = fotosasociados.fot_fotoperfil;
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

                        pbxPerfil.Image = Image.FromStream(ms);
                    }
                    else
                    {
                        pbxPerfil.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
