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
    public partial class Usuarios : Form
    {
        //SEGURIDAD
        Seguridad seguridad = new Seguridad();

        //ENTIDADES//
        personal personal;
        usuarios usuarios;
        fotospersonal fotospersonal;
        //

        //CONTROLADORES//
        UsuariosController usuarioscontroller = new UsuariosController();
        
        public long id { get; set; }
        public Usuarios()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Personal_busqueda personalbusqueda = new Personal_busqueda();
                personalbusqueda.ShowDialog();

                id = personalbusqueda.id;

                if (id != 0)
                {
                    txtClave.Text = id.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (txtClave.Text == "")
                    {
                        MessageBox.Show("¡Introduce la clave de personal!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtClave.Focus();
                    }
                    else
                    {
                        personal = usuarioscontroller.personal(Convert.ToInt64(txtClave.Text));

                        if (personal != null)
                        {
                            usuarios = usuarioscontroller.usuarios(Convert.ToInt64(txtClave.Text));

                            if (usuarios != null)
                            {
                                MessageBox.Show("¡Búsqueda exitosa!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                groupBox1.Enabled = false;
                                groupBox3.Enabled = true;
                                txtUsuario.Enabled = false;

                                btnActualizar.Enabled = true;
                                btnEliminar.Enabled = true;
                                btnGuardar.Enabled = false;
                                btnCancelar.Enabled = true;

                                txtUsuario.Enabled = false;

                                txtUsuario.Text = usuarios.usu_usuario;
                                txtContrasena.Text = seguridad.Desencriptar(usuarios.usu_contrasena);
                                txtConfirmar.Text = seguridad.Desencriptar(usuarios.usu_contrasena);
                                cbxCargo.Text = usuarios.usu_cargo;
                                cbxEstadoCuenta.Text = usuarios.usu_estadocuenta;

                                txtContrasena.Focus();
                                txtContrasena.SelectionStart = 0;
                                txtContrasena.SelectionLength = txtContrasena.Text.Length;
                            }
                            else
                            {
                                DialogResult mensaje = MessageBox.Show("¡Búsqueda exitosa!, el personal esta registrado en el sistema pero no tiene una cuenta de usuario asignada, ¿desea asignarle una cuenta de usuario?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (mensaje == DialogResult.Yes)
                                {
                                    groupBox1.Enabled = false;
                                    groupBox3.Enabled = true;

                                    btnActualizar.Enabled = false;
                                    btnEliminar.Enabled = false;
                                    btnGuardar.Enabled = true;
                                    btnCancelar.Enabled = true;

                                    txtUsuario.Enabled = true;

                                    txtUsuario.Clear();
                                    txtContrasena.Clear();
                                    txtConfirmar.Clear();
                                    cbxCargo.SelectedIndex = -1;
                                    cbxEstadoCuenta.SelectedIndex = -1;

                                    txtUsuario.Focus();
                                }
                                else
                                {
                                    groupBox1.Enabled = true;
                                    groupBox3.Enabled = false;

                                    txtUsuario.Clear();
                                    txtContrasena.Clear();
                                    txtConfirmar.Clear();
                                    cbxCargo.SelectedIndex = -1;
                                    cbxEstadoCuenta.SelectedIndex = -1;

                                    txtNombre.Clear();
                                    txtSexo.Clear();
                                    txtFechaNacimiento.Clear();
                                    txtEstadoCivil.Clear();
                                    txtMovil.Clear();
                                    txtTelefono.Clear();
                                    txtCorreo.Clear();

                                    pbxPerfil.Image = null;

                                    txtClave.Clear();
                                    txtClave.Focus();

                                }
                            }

                            txtNombre.Text = personal.per_nombre + " " + personal.per_apellidos;
                            txtSexo.Text = personal.per_sexo;
                            txtFechaNacimiento.Text = personal.per_fechanacimiento.ToShortDateString();
                            txtEstadoCivil.Text = personal.per_estadocivil;
                            txtMovil.Text = personal.per_movil;
                            txtTelefono.Text = personal.per_telefono;
                            txtCorreo.Text = personal.per_correoelectronico;

                            fotospersonal = usuarioscontroller.fotospersonal(Convert.ToInt64(txtClave.Text));

                            if (fotospersonal != null)
                            {
                                byte[] fotografia = fotospersonal.fot_fotoperfil;
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(fotografia);
                                pbxPerfil.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            MessageBox.Show("¡Sin resultados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
                {
                    e.Handled = true;
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (mensaje == DialogResult.Yes)
                {
                    groupBox1.Enabled = true;
                    groupBox3.Enabled = false;

                    btnActualizar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelar.Enabled = false;

                    txtUsuario.Clear();
                    txtContrasena.Clear();
                    txtConfirmar.Clear();
                    cbxCargo.SelectedIndex = -1;
                    cbxEstadoCuenta.SelectedIndex = -1;

                    txtNombre.Clear();
                    txtSexo.Clear();
                    txtFechaNacimiento.Clear();
                    txtEstadoCivil.Clear();
                    txtMovil.Clear();
                    txtTelefono.Clear();
                    txtCorreo.Clear();

                    pbxPerfil.Image = null;

                    txtClave.Clear();
                    txtClave.Focus();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    usuarios = usuarioscontroller.usuarios2(txtUsuario.Text);

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
                        usuarioscontroller.agregarUsuario(Convert.ToInt64(txtClave.Text), txtUsuario.Text, txtContrasena.Text, cbxCargo.Text, cbxEstadoCuenta.Text);

                        MessageBox.Show("¡El registro fue ingresado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        groupBox1.Enabled = true;
                        groupBox3.Enabled = false;

                        btnActualizar.Enabled = false;
                        btnGuardar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnCancelar.Enabled = false;

                        txtUsuario.Clear();
                        txtContrasena.Clear();
                        txtConfirmar.Clear();
                        cbxCargo.SelectedIndex = -1;
                        cbxEstadoCuenta.SelectedIndex = -1;

                        txtNombre.Clear();
                        txtSexo.Clear();
                        txtFechaNacimiento.Clear();
                        txtEstadoCivil.Clear();
                        txtMovil.Clear();
                        txtTelefono.Clear();
                        txtCorreo.Clear();

                        pbxPerfil.Image = null;

                        txtClave.Clear();
                        txtClave.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int bandera1 = 0, bandera2 = 0, bandera3 = 0, bandera4 = 0;
                
                if (txtContrasena.Text == "")
                {
                    lblValidacion2.Text = "* Complete este campo";
                    lblValidacion2.Visible = true;
                    bandera1 = 0;
                }
                else
                {
                    lblValidacion2.Visible = false;
                    bandera1 = 1;
                }

                if (txtConfirmar.Text == "")
                {
                    lblValidacion3.Text = "* Complete este campo";
                    lblValidacion3.Visible = true;
                    bandera2 = 0;
                }
                else
                {
                    if (txtContrasena.Text != "")
                    {
                        if (txtContrasena.Text == txtConfirmar.Text)
                        {
                            lblValidacion2.Visible = false;
                            lblValidacion3.Visible = false;
                            bandera2 = 1;
                        }
                        else
                        {
                            lblValidacion2.Text = "* Las contraseñas no coinciden";
                            lblValidacion2.Visible = true;
                            lblValidacion2.Visible = false;
                            bandera2 = 0;
                        }
                    }
                }

                if (cbxCargo.Text == "")
                {
                    lblValidacion4.Text = "* Complete este campo";
                    lblValidacion4.Visible = true;
                    bandera3 = 0;
                }
                else
                {
                    lblValidacion4.Visible = false;
                    bandera3 = 1;
                }

                if (cbxEstadoCuenta.Text == "")
                {
                    lblValidacion5.Text = "* Complete este campo";
                    lblValidacion5.Visible = true;
                    bandera4 = 0;
                }
                else
                {
                    lblValidacion5.Visible = false;
                    bandera4 = 1;
                }

                if (bandera1 == 1 && bandera2 == 1 && bandera3 == 1 && bandera4 == 1)
                {
                    DialogResult mensaje = MessageBox.Show("¿Desea guardar los cambios?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        //AGREGAR USUARIO
                        usuarioscontroller.actualizarUsuario(Convert.ToInt64(txtClave.Text), txtContrasena.Text, cbxCargo.Text, cbxEstadoCuenta.Text);

                        MessageBox.Show("¡El registro ha sido actualizado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        groupBox1.Enabled = true;
                        groupBox3.Enabled = false;

                        btnActualizar.Enabled = false;
                        btnGuardar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnCancelar.Enabled = false;

                        txtUsuario.Clear();
                        txtContrasena.Clear();
                        txtConfirmar.Clear();
                        cbxCargo.SelectedIndex = -1;
                        cbxEstadoCuenta.SelectedIndex = -1;

                        txtNombre.Clear();
                        txtSexo.Clear();
                        txtFechaNacimiento.Clear();
                        txtEstadoCivil.Clear();
                        txtMovil.Clear();
                        txtTelefono.Clear();
                        txtCorreo.Clear();

                        pbxPerfil.Image = null;

                        txtClave.Clear();
                        txtClave.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea eliminar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(mensaje == DialogResult.Yes)
            {
                usuarioscontroller.eliminarUsuario(Convert.ToInt64(txtClave.Text));

                MessageBox.Show("¡El registro ha sido eliminado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                groupBox1.Enabled = true;
                groupBox3.Enabled = false;

                btnActualizar.Enabled = false;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                btnCancelar.Enabled = false;

                txtUsuario.Clear();
                txtContrasena.Clear();
                txtConfirmar.Clear();
                cbxCargo.SelectedIndex = -1;
                cbxEstadoCuenta.SelectedIndex = -1;

                txtNombre.Clear();
                txtSexo.Clear();
                txtFechaNacimiento.Clear();
                txtEstadoCivil.Clear();
                txtMovil.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();

                pbxPerfil.Image = null;

                txtClave.Clear();
                txtClave.Focus();
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnCancelar.Enabled == true)
            {
                btnCancelar_Click(sender, e);
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnGuardar.Enabled==true)
            {
                btnGuardar_Click(sender, e);
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnActualizar.Enabled == true)
            {
                btnActualizar_Click(sender, e);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnEliminar.Enabled == true)
            {
                btnEliminar_Click(sender, e);
            }
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (groupBox1.Enabled == true)
            {
                btnBuscar_Click(sender, e);
            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            ToolTip notificacion = new ToolTip();
            notificacion.AutoPopDelay = 8000;
            notificacion.InitialDelay = 1000;
            notificacion.ReshowDelay = 500;
            notificacion.ShowAlways = true;
            notificacion.SetToolTip(this.btnBuscar, "Si no sabe su número de clave de clíc aquí o presione Ctrl+F2 para buscar");
            notificacion.SetToolTip(this.btnGuardar, "De clíc aquí para agregar el registro o presione F4");
            notificacion.SetToolTip(this.btnCancelar, "De clíc aquí para cancelar la operación actual o presione F3");
            notificacion.SetToolTip(this.btnActualizar, "De clíc aquí para modificar el registro o presione F5");
            notificacion.SetToolTip(this.btnEliminar, "De clíc aquí para eliminar el registro o presione F6");
        }
    }
}
