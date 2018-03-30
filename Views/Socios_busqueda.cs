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
    public partial class Socios_busqueda : Form
    {
        public string id { get; set; }

        //ENTIDADES//
        fotosasociados fotosasociados;

        //CONTROLADOR//
        SociosController socioscontroller = new SociosController();
        public Socios_busqueda()
        {
            InitializeComponent();
        }

        private void Socios_busqueda_Load(object sender, EventArgs e)
        {
            try
            {
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
            catch (Exception ex)
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
                    dgvSocios.DataSource = socioscontroller.dataGridViewbuscarSocios(txtNombre.Text, txtApellidos.Text, Convert.ToDateTime(dtpFechanacimiento.Value.ToShortDateString()));
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

                    if (dgvSocios.CurrentRow == null)
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void dgvPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
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

        private void confirmarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSocios.Rows.Count == 0)
                {
                    MessageBox.Show("¡No hay registros disponibles para seleccionar!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult mensaje = MessageBox.Show("¿Desea confirmar la búsqueda?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        id = dgvSocios.CurrentRow.Cells[0].Value.ToString();

                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buscarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }
    }
}
