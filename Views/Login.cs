using System;
using System.Windows.Forms;
using Models;
using Controllers;

namespace Views
{
    public partial class Login : Form
    {
        LoginController logincontroller = new LoginController();

        usuarios usuarios;
        personal personal;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                int bandera1 = 0, bandera2 = 0;

                if (txtUsername.Text == "")
                {
                    lblValidacion1.Text = "* Introduzca su nombre de usuario";
                    lblValidacion1.Visible = true;
                    bandera1 = 0;
                }
                else
                {
                    lblValidacion1.Visible = false;
                    bandera1 = 1;
                }

                if (txtPassword.Text == "")
                {
                    lblValidacion2.Text = "* Introduzca su contraseña de acceso";
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
                    usuarios = logincontroller.inicioSesion(txtUsername.Text, txtPassword.Text);

                    if (usuarios != null)
                    {
                        if (usuarios.usu_estadocuenta == "ACTIVADA")
                        {
                            Menu menu = new Menu();
                            menu.id = usuarios.usu_personal;

                            personal = logincontroller.obtenerDatos(usuarios.usu_personal);

                            if (personal != null)
                            {
                                MessageBox.Show("BIENVENIDO(A) al sistema, " + personal.per_nombre + " " + personal.per_apellidos + ", a continuación accederá al menú principal.", "ACCESANDO AL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                menu.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Su cuenta no se encuentra activa, acuda a un usuario administrativo para habilitar su acceso", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUsername.Clear();
                            txtPassword.Clear();
                            txtUsername.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Las credenciales introducidas son incorrectas", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtUsername.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (Char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(this.btnLogin, "De clíc aquí o presione la tecla Enter para iniciar sesión en el sistema");

                if (logincontroller.personalRegistrado() == 0)
                {
                    Personal personal = new Personal();
                    personal.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
