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
    public partial class Personal_usuarios : Form
    {
        //CONTROLADOR//
        PersonalController personalcontroller = new PersonalController();

        //ENTIDADES
        usuarios usuarios;

        public long id { get; set; }
        public Personal_usuarios()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(mensaje == DialogResult.Yes)
            {
                txtUsuario.Clear();
                txtContrasena.Clear();
                txtConfirmar.Clear();

                cbxCargo.SelectedIndex = -1;
                cbxEstadoCuenta.SelectedIndex = -1;

                lblValidacion1.Visible = false;
                lblValidacion2.Visible = false;
                lblValidacion3.Visible = false;
                lblValidacion4.Visible = false;
                lblValidacion5.Visible = false;

                txtUsuario.Focus();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int bandera1 = 0, bandera2 = 0, bandera3 = 0, bandera4 = 0, bandera5 = 0;

                if (txtUsuario.Text == "")
                {
                    lblValidacion1.Text = "* Complete este campo";
                    lblValidacion1.Visible = true;
                    bandera1 = 0;
                }
                else
                {
                    usuarios = personalcontroller.usuarios(txtUsuario.Text);

                    if (usuarios == null)
                    {
                        lblValidacion1.Visible = false;
                        bandera1 = 1;
                    }
                    else
                    {
                        lblValidacion1.Text = "* El nombre de usuario ya esta registrado";
                        lblValidacion1.Visible = true;
                        bandera1 = 0;
                    }
                }

                if (txtContrasena.Text == "")
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

                if (txtConfirmar.Text == "")
                {
                    lblValidacion3.Text = "* Complete este campo";
                    lblValidacion3.Visible = true;
                    bandera3 = 0;
                }
                else
                {
                    if (txtContrasena.Text != "")
                    {
                        if (txtContrasena.Text == txtConfirmar.Text)
                        {
                            lblValidacion2.Visible = false;
                            lblValidacion3.Visible = false;
                            bandera3 = 1;
                        }
                        else
                        {
                            lblValidacion2.Text = "* Las contraseñas no coinciden";
                            lblValidacion2.Visible = true;
                            lblValidacion3.Visible = false;
                            bandera3 = 0;
                        }
                    }
                }

                if (cbxCargo.Text == "")
                {
                    lblValidacion4.Text = "* Complete este campo";
                    lblValidacion4.Visible = true;
                    bandera4 = 0;
                }
                else
                {
                    lblValidacion4.Visible = false;
                    bandera4 = 1;
                }

                if (cbxEstadoCuenta.Text == "")
                {
                    lblValidacion5.Text = "* Complete este campo";
                    lblValidacion5.Visible = true;
                    bandera5 = 0;
                }
                else
                {
                    lblValidacion5.Visible = false;
                    bandera5 = 1;
                }

                if (bandera1 == 1 && bandera2 == 1 && bandera3 == 1 && bandera4 == 1 && bandera5 == 1)
                {
                    DialogResult mensaje = MessageBox.Show("¿Desea ingresar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        //AGREGAR USUARIO
                        personalcontroller.agregarUsuario(id, txtUsuario.Text, txtContrasena.Text, cbxCargo.Text, cbxEstadoCuenta.Text);

                        MessageBox.Show("¡El registro fue ingresado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnGuardar_Click(sender, e);
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCancelar_Click(sender, e);
        }

        private void Personal_usuarios_Load(object sender, EventArgs e)
        {
            ToolTip notificacion = new ToolTip();
            notificacion.AutoPopDelay = 8000;
            notificacion.InitialDelay = 1000;
            notificacion.ReshowDelay = 500;
            notificacion.ShowAlways = true;
            notificacion.SetToolTip(this.btnCancelar, "De clíc aquí para cancelar la operación actual");
            notificacion.SetToolTip(this.btnGuardar, "De clíc aquí para guardar el registro");
        }
    }
}
