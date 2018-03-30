using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using Controllers;

namespace Views
{
    public partial class Personal_busqueda : Form
    {
        public long id { get; set; }

        //ENTIDADES
        fotospersonal fotospersonal;

        //CONTROLADORES
        Controllers.PersonalController personalcontroller = new Controllers.PersonalController();
        public Personal_busqueda()
        {
            InitializeComponent();
        }

        private void Personal_busqueda_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(this.btnBuscar, "De clíc aquí o presione F2 para realizar una búsqueda");

                id = 0;

                dgvPersonal.DataSource = personalcontroller.dataGridViewPersonal();
                dgvPersonal.Columns[0].HeaderText = "Clave";
                dgvPersonal.Columns[1].HeaderText = "Nombre";
                dgvPersonal.Columns[2].HeaderText = "Sexo";
                dgvPersonal.Columns[3].HeaderText = "Fecha de nacimiento";
                dgvPersonal.Columns[4].HeaderText = "Estado civil";
                dgvPersonal.Columns[5].HeaderText = "Teléfono";
                dgvPersonal.Columns[6].HeaderText = "Teléfono móvil";
                dgvPersonal.Columns[7].HeaderText = "Correo electrónico";
                dgvPersonal.Columns[8].Visible = false;
                dgvPersonal.Columns[9].Visible = false;
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

                if (txtNombre.Text == "")
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

                if (txtApellidos.Text == "")
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

                if (bandera1 == 1 && bandera2 == 1)
                {

                    dgvPersonal.DataSource = personalcontroller.dataGridViewbuscarPersonal(txtNombre.Text, txtApellidos.Text, Convert.ToDateTime(dtpFechanacimiento.Value.ToShortDateString()));

                    dgvPersonal.Columns[0].HeaderText = "Clave";
                    dgvPersonal.Columns[1].HeaderText = "Nombre";
                    dgvPersonal.Columns[2].HeaderText = "Sexo";
                    dgvPersonal.Columns[3].HeaderText = "Fecha de nacimiento";
                    dgvPersonal.Columns[4].HeaderText = "Estado civil";
                    dgvPersonal.Columns[5].HeaderText = "Teléfono";
                    dgvPersonal.Columns[6].HeaderText = "Teléfono móvil";
                    dgvPersonal.Columns[7].HeaderText = "Correo electrónico";
                    dgvPersonal.Columns[8].Visible = false;
                    dgvPersonal.Columns[9].Visible = false;

                    if (dgvPersonal.CurrentRow == null)
                    {
                        MessageBox.Show("¡Sin resultados encontrados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtApellidos.Clear();
                        txtNombre.Clear();
                        dtpFechanacimiento.Value = DateTime.Now;

                        txtNombre.Focus();
                    }
                    else
                    {
                        MessageBox.Show("¡Se encontraron resultados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvPersonal.Rows.Count != 0)
                {
                    fotospersonal = personalcontroller.fotoPersonal(Convert.ToInt64(dgvPersonal.CurrentRow.Cells[0].Value.ToString()));

                    if (fotospersonal != null)
                    {
                        byte[] imagenBuffer = fotospersonal.fot_fotoperfil;
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

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void confirmarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPersonal.Rows.Count == 0)
                {
                    MessageBox.Show("¡No hay registros disponibles para seleccionar!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult mensaje = MessageBox.Show("¿Desea confirmar la búsqueda?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        id = Convert.ToInt64(dgvPersonal.CurrentRow.Cells[0].Value.ToString());

                        this.Close();
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
