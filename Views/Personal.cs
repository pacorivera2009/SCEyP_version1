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
    public partial class Personal : Form
    {
        //ID DE PERSONAL
        public long id { get; set; }

        //FOTOGRAFIA
        public byte[] fotografia { get; set; }

        //ENTIDADES
        personal personal;
        fotospersonal fotospersonal;

        //CONTROLADOR
        PersonalController personalcontroller = new PersonalController();

        int foto = 0;
        public Personal()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(mensaje == DialogResult.Yes)
            {
                lblValidacion10.Visible = false;
                lblValidacion11.Visible = false;
                lblValidacion12.Visible = false;
                lblValidacion13.Visible = false;
                lblValidacion14.Visible = false;
                lblValidacion2.Visible = false;
                lblValidacion3.Visible = false;
                lblValidacion4.Visible = false;
                lblValidacion5.Visible = false;
                lblValidacion6.Visible = false;
                lblValidacion7.Visible = false;
                lblValidacion8.Visible = false;
                lblValidacion9.Visible = false;

                txtApellidos.Clear();
                txtCelular.Clear();
                txtClave.Clear();
                txtCodigo.Clear();
                txtCorreo.Clear();
                txtDomicilio.Clear();
                txtNombre.Clear();
                txtTelefono.Clear();

                cbxColonia.SelectedIndex = -1;
                cbxEstado.SelectedIndex = -1;
                cbxEstadoCivil.SelectedIndex = -1;
                cbxLocalidad.SelectedIndex = -1;
                cbxMunicipio.SelectedIndex = -1;
                cbxProveedor.SelectedIndex = -1;
                cbxSexo.SelectedIndex = -1;

                pbxPerfil.Image = null;

                dtpFechanacimiento.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnIngresar.Enabled = true;
                btnModificar.Enabled = false;

                groupBox1.Enabled = true;

                foto = 0;

                txtClave.Focus();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string correo;

                int bandera1 = 0, bandera2 = 0, bandera3 = 0, bandera4 = 0, bandera5 = 0, bandera6 = 0, bandera7 = 0, bandera8 = 0, bandera9 = 0, bandera10 = 0, bandera11 = 0, bandera12 = 0, bandera13 = 0;

                if (txtNombre.Text == "")
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

                if (txtApellidos.Text == "")
                {
                    lblValidacion3.Text = "* Complete este campo";
                    lblValidacion3.Visible = true;
                    bandera2 = 0;
                }
                else
                {
                    lblValidacion3.Visible = false;
                    bandera2 = 1;
                }

                if (cbxSexo.Text == "")
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

                if (cbxEstadoCivil.Text == "")
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

                if (txtDomicilio.Text == "")
                {
                    lblValidacion6.Text = "* Complete este campo";
                    lblValidacion6.Visible = true;
                    bandera5 = 0;
                }
                else
                {
                    lblValidacion6.Visible = false;
                    bandera5 = 1;
                }

                if (txtCodigo.Text == "")
                {
                    lblValidacion7.Text = "* Complete este campo";
                    lblValidacion7.Visible = true;
                    bandera6 = 0;
                }
                else
                {
                    lblValidacion7.Visible = false;
                    bandera6 = 1;
                }

                if (cbxEstado.Text == "")
                {
                    lblValidacion8.Text = "* Complete este campo";
                    lblValidacion8.Visible = true;
                    bandera7 = 0;
                }
                else
                {
                    lblValidacion8.Visible = false;
                    bandera7 = 1;
                }

                if (cbxMunicipio.Text == "")
                {
                    lblValidacion9.Text = "* Complete este campo";
                    lblValidacion9.Visible = true;
                    bandera8 = 0;
                }
                else
                {
                    lblValidacion9.Visible = false;
                    bandera8 = 1;
                }

                if (cbxLocalidad.Text == "")
                {
                    lblValidacion10.Text = "* Complete este campo";
                    lblValidacion10.Visible = true;
                    bandera9 = 0;
                }
                else
                {
                    lblValidacion10.Visible = false;
                    bandera9 = 1;
                }

                if (cbxColonia.Text == "")
                {
                    lblValidacion11.Text = "* Complete este campo";
                    lblValidacion11.Visible = true;
                    bandera10 = 0;
                }
                else
                {
                    lblValidacion11.Visible = false;
                    bandera10 = 1;
                }

                if (txtTelefono.Text.Replace("(", "").Replace("-", "").Replace(")", "") == "")
                {
                    lblValidacion12.Visible = false;
                    bandera11 = 1;
                }
                else
                {
                    if (txtTelefono.MaskCompleted == true)
                    {
                        lblValidacion12.Visible = false;
                        bandera11 = 1;
                    }
                    else
                    {
                        lblValidacion12.Text = "* Complete este campo";
                        lblValidacion12.Visible = true;
                        bandera11 = 0;
                    }
                }

                if (txtCelular.Text.Replace("-", "") == "")
                {
                    lblValidacion13.Visible = false;
                    bandera12 = 1;
                }
                else
                {
                    if (txtCelular.MaskCompleted == true)
                    {
                        lblValidacion13.Visible = false;
                        bandera12 = 1;
                    }
                    else
                    {
                        lblValidacion13.Text = "* Complete este campo";
                        lblValidacion13.Visible = true;
                        bandera12 = 0;
                    }
                }

                if (txtCorreo.Text == "" && cbxProveedor.Text == "")
                {
                    correo = string.Empty;
                    lblValidacion14.Visible = false;
                    bandera13 = 1;
                }
                else
                {
                    if (txtCorreo.Text == "")
                    {
                        correo = string.Empty;
                        lblValidacion14.Text = "* Introduce tu nombre de usuario de correo electrónico";
                        lblValidacion14.Visible = true;
                        bandera13 = 0;
                    }
                    else
                    {
                        if (cbxProveedor.Text == "")
                        {
                            correo = string.Empty;
                            lblValidacion14.Text = "* Seleccione su proveedor de correo electrónico";
                            lblValidacion14.Visible = true;
                            bandera13 = 0;
                        }
                        else
                        {
                            correo = txtCorreo.Text + "@" + cbxProveedor.Text;
                            lblValidacion14.Visible = false;
                            bandera13 = 1;
                        }
                    }
                }


                if (bandera1 == 1 && bandera2 == 1 && bandera3 == 1 && bandera4 == 1 && bandera5 == 1 && bandera6 == 1 && bandera7 == 1 && bandera8 == 1 && bandera9 == 1 && bandera10 == 1 && bandera11 == 1 && bandera12 == 1 && bandera13 == 1)
                {
                    if (foto == 1)
                    {
                        DialogResult mensaje = MessageBox.Show("¿Desea ingresar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if(mensaje == DialogResult.Yes)
                        {
                            //INGRESAR PERSONAL//

                            long id = 0;

                            if(txtTelefono.MaskCompleted == false && txtCelular.MaskCompleted == false)
                            {
                                id = personalcontroller.agregarPersonal(txtNombre.Text, txtApellidos.Text, cbxSexo.Text, Convert.ToDateTime(dtpFechanacimiento.Value.ToShortDateString()), cbxEstadoCivil.Text, txtDomicilio.Text, Convert.ToInt32(txtCodigo.Text), Convert.ToInt64(cbxEstado.SelectedValue.ToString()), Convert.ToInt64(cbxMunicipio.SelectedValue.ToString()), Convert.ToInt64(cbxLocalidad.SelectedValue.ToString()), Convert.ToInt64(cbxColonia.SelectedValue.ToString()), "", "", correo);
                            }
                            else if(txtTelefono.MaskCompleted == false && txtCelular.MaskCompleted == true)
                            {
                                id = personalcontroller.agregarPersonal(txtNombre.Text, txtApellidos.Text, cbxSexo.Text, Convert.ToDateTime(dtpFechanacimiento.Value.ToShortDateString()), cbxEstadoCivil.Text, txtDomicilio.Text, Convert.ToInt32(txtCodigo.Text), Convert.ToInt64(cbxEstado.SelectedValue.ToString()), Convert.ToInt64(cbxMunicipio.SelectedValue.ToString()), Convert.ToInt64(cbxLocalidad.SelectedValue.ToString()), Convert.ToInt64(cbxColonia.SelectedValue.ToString()), "", txtCelular.Text, correo);
                            }
                            else if(txtTelefono.MaskCompleted == true && txtCelular.MaskCompleted == false)
                            {
                                id = personalcontroller.agregarPersonal(txtNombre.Text, txtApellidos.Text, cbxSexo.Text, Convert.ToDateTime(dtpFechanacimiento.Value.ToShortDateString()), cbxEstadoCivil.Text, txtDomicilio.Text, Convert.ToInt32(txtCodigo.Text), Convert.ToInt64(cbxEstado.SelectedValue.ToString()), Convert.ToInt64(cbxMunicipio.SelectedValue.ToString()), Convert.ToInt64(cbxLocalidad.SelectedValue.ToString()), Convert.ToInt64(cbxColonia.SelectedValue.ToString()), txtTelefono.Text, "", correo);
                            }
                            else
                            {
                                id = personalcontroller.agregarPersonal(txtNombre.Text, txtApellidos.Text, cbxSexo.Text, Convert.ToDateTime(dtpFechanacimiento.Value.ToShortDateString()), cbxEstadoCivil.Text, txtDomicilio.Text, Convert.ToInt32(txtCodigo.Text), Convert.ToInt64(cbxEstado.SelectedValue.ToString()), Convert.ToInt64(cbxMunicipio.SelectedValue.ToString()), Convert.ToInt64(cbxLocalidad.SelectedValue.ToString()), Convert.ToInt64(cbxColonia.SelectedValue.ToString()), txtTelefono.Text, txtCelular.Text, correo);
                            }

                            //INGRESO DE LA FOTO DE PERFIL//
                            personalcontroller.agregarFoto(id, fotografia);

                            mensaje = MessageBox.Show("El registro ha sido completado, ¿desea ingresar una cuenta de usuario?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if(mensaje == DialogResult.Yes)
                            {
                                Personal_usuarios personalusuarios = new Personal_usuarios();
                                personalusuarios.id = id;
                                personalusuarios.ShowDialog();
                            }

                            lblValidacion10.Visible = false;
                            lblValidacion11.Visible = false;
                            lblValidacion12.Visible = false;
                            lblValidacion13.Visible = false;
                            lblValidacion14.Visible = false;
                            lblValidacion2.Visible = false;
                            lblValidacion3.Visible = false;
                            lblValidacion4.Visible = false;
                            lblValidacion5.Visible = false;
                            lblValidacion6.Visible = false;
                            lblValidacion7.Visible = false;
                            lblValidacion8.Visible = false;
                            lblValidacion9.Visible = false;

                            txtApellidos.Clear();
                            txtCelular.Clear();
                            txtClave.Clear();
                            txtCodigo.Clear();
                            txtCorreo.Clear();
                            txtDomicilio.Clear();
                            txtNombre.Clear();
                            txtTelefono.Clear();

                            cbxColonia.SelectedIndex = -1;
                            cbxEstado.SelectedIndex = -1;
                            cbxEstadoCivil.SelectedIndex = -1;
                            cbxLocalidad.SelectedIndex = -1;
                            cbxMunicipio.SelectedIndex = -1;
                            cbxProveedor.SelectedIndex = -1;
                            cbxSexo.SelectedIndex = -1;

                            pbxPerfil.Image = null;

                            dtpFechanacimiento.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                            btnActualizar.Enabled = false;
                            btnEliminar.Enabled = false;
                            btnGuardar.Enabled = true;
                            btnCancelar.Enabled = true;
                            btnIngresar.Enabled = true;
                            btnModificar.Enabled = false;

                            groupBox1.Enabled = true;

                            foto = 0;

                            txtClave.Focus();

                        }
                    }
                    else
                    {
                        MessageBox.Show("¡Ingrese una foto de perfil!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(btnBuscar, "Si no sabe su número de clave de clíc aquí o presione Ctrl+F2 para buscar");
                notificacion.SetToolTip(btnGuardar, "De clíc aquí para agregar el registro o presione F4");
                notificacion.SetToolTip(btnCancelar, "De clíc aquí para cancelar la operación actual o presione F3");
                notificacion.SetToolTip(btnActualizar, "De clíc aquí para modificar el registro o presione F5");
                notificacion.SetToolTip(btnEliminar, "De clíc aquí para eliminar el registro o presione F6");
                notificacion.SetToolTip(btnIngresar, "De clíc aquí para iniciar la cámara web e ingresar la foto de perfíl o presione F1");
                notificacion.SetToolTip(btnModificar, "De clíc aquí para iniciar la cámara web y modificar la foto de perfíl o presione F2");

                cbxEstado.DataSource = personalcontroller.comboBoxEstados();
                cbxEstado.DisplayMember = "est_nombreestado";
                cbxEstado.ValueMember = "est_id";

                cbxEstado.SelectedIndex = -1;

                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxEstado_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxMunicipio.DataSource = personalcontroller.comboBoxMunicipios(Convert.ToInt64(cbxEstado.SelectedValue.ToString()));
                cbxMunicipio.DisplayMember = "mun_nombremunicipio";
                cbxMunicipio.ValueMember = "mun_id";

                cbxMunicipio.SelectedIndex = -1;

                if(cbxMunicipio.Items.Count == 0)
                {
                    cbxMunicipio.Enabled = false;
                }
                else
                {
                    cbxMunicipio.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxMunicipio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxLocalidad.DataSource = personalcontroller.comboBoxLocalidades(Convert.ToInt64(cbxMunicipio.SelectedValue.ToString()));
                cbxLocalidad.DisplayMember = "loc_nombrelocalidad";
                cbxLocalidad.ValueMember = "loc_id";

                cbxLocalidad.SelectedIndex = -1;

                if (cbxLocalidad.Items.Count == 0)
                {
                    cbxLocalidad.Enabled = false;
                }
                else
                {
                    cbxLocalidad.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxLocalidad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxColonia.DataSource = personalcontroller.comboBoxColonias(Convert.ToInt64(cbxLocalidad.SelectedValue.ToString()));
                cbxColonia.DisplayMember = "col_nombrecolonia";
                cbxColonia.ValueMember = "col_id";

                cbxColonia.SelectedIndex = -1;

                if (cbxColonia.Items.Count == 0)
                {
                    cbxColonia.Enabled = false;
                }
                else
                {
                    cbxColonia.Enabled = true;
                }
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
                Personal_busqueda personalbusqueda = new Personal_busqueda();
                personalbusqueda.ShowDialog();

                id = personalbusqueda.id;

                if (id != 0)
                {
                    txtClave.Text = id.ToString();
                }

                txtClave.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                Personal_foto personalfoto = new Personal_foto();
                personalfoto.ShowDialog();

                if (personalfoto.foto == 1)
                {
                    fotografia = personalfoto.fotografia;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(fotografia);
                    pbxPerfil.Image = Image.FromStream(ms);

                    foto = 1;
                }
                else
                {
                    foto = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void txtCorreo_KeyUp(object sender, KeyEventArgs e)
        {
            if(txtCorreo.Text == "")
            {
                cbxProveedor.Enabled = false;
            }
            else
            {
                cbxProveedor.Enabled = true;
            }
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
            }
            else if(e.KeyChar >= 48 && e.KeyChar <= 59)
            {
                e.Handled = false;
            }
            else if(e.KeyChar >= 65 && e.KeyChar <= 90)
            {
                e.Handled = false;
            }
            else if(e.KeyChar == 95)
            {
                e.Handled = false;
            }
            else if(e.KeyChar >= 97 && e.KeyChar <= 122)
            {
                e.Handled = false;
            }
            else if(e.KeyChar == (Char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void ingresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnIngresar.Enabled == true)
            {
                btnIngresar_Click(sender, e);
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnModificar.Enabled == true)
            {
                btnModificar_Click(sender, e);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                Personal_foto personalfoto = new Personal_foto();
                personalfoto.ShowDialog();

                if (personalfoto.foto == 1)
                {
                    fotografia = personalfoto.fotografia;
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(fotografia);
                    pbxPerfil.Image = Image.FromStream(ms);

                    foto = 1;
                }
                else
                {
                    foto = 0;
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
                string correo;

                int bandera1 = 0, bandera2 = 0, bandera3 = 0, bandera4 = 0, bandera5 = 0, bandera6 = 0, bandera7 = 0, bandera8 = 0, bandera9 = 0, bandera10 = 0, bandera11 = 0, bandera12 = 0, bandera13 = 0;

                if (txtNombre.Text == "")
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

                if (txtApellidos.Text == "")
                {
                    lblValidacion3.Text = "* Complete este campo";
                    lblValidacion3.Visible = true;
                    bandera2 = 0;
                }
                else
                {
                    lblValidacion3.Visible = false;
                    bandera2 = 1;
                }

                if (cbxSexo.Text == "")
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

                if (cbxEstadoCivil.Text == "")
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

                if (txtDomicilio.Text == "")
                {
                    lblValidacion6.Text = "* Complete este campo";
                    lblValidacion6.Visible = true;
                    bandera5 = 0;
                }
                else
                {
                    lblValidacion6.Visible = false;
                    bandera5 = 1;
                }

                if (txtCodigo.Text == "")
                {
                    lblValidacion7.Text = "* Complete este campo";
                    lblValidacion7.Visible = true;
                    bandera6 = 0;
                }
                else
                {
                    lblValidacion7.Visible = false;
                    bandera6 = 1;
                }

                if (cbxEstado.Text == "")
                {
                    lblValidacion8.Text = "* Complete este campo";
                    lblValidacion8.Visible = true;
                    bandera7 = 0;
                }
                else
                {
                    lblValidacion8.Visible = false;
                    bandera7 = 1;
                }

                if (cbxMunicipio.Text == "")
                {
                    lblValidacion9.Text = "* Complete este campo";
                    lblValidacion9.Visible = true;
                    bandera8 = 0;
                }
                else
                {
                    lblValidacion9.Visible = false;
                    bandera8 = 1;
                }

                if (cbxLocalidad.Text == "")
                {
                    lblValidacion10.Text = "* Complete este campo";
                    lblValidacion10.Visible = true;
                    bandera9 = 0;
                }
                else
                {
                    lblValidacion10.Visible = false;
                    bandera9 = 1;
                }

                if (cbxColonia.Text == "")
                {
                    lblValidacion11.Text = "* Complete este campo";
                    lblValidacion11.Visible = true;
                    bandera10 = 0;
                }
                else
                {
                    lblValidacion11.Visible = false;
                    bandera10 = 1;
                }

                if (txtTelefono.Text.Replace("(", "").Replace("-", "").Replace(")", "") == "")
                {
                    lblValidacion12.Visible = false;
                    bandera11 = 1;
                }
                else
                {
                    if (txtTelefono.MaskCompleted == true)
                    {
                        lblValidacion12.Visible = false;
                        bandera11 = 1;
                    }
                    else
                    {
                        lblValidacion12.Text = "* Complete este campo";
                        lblValidacion12.Visible = true;
                        bandera11 = 0;
                    }
                }

                if (txtCelular.Text.Replace("-", "") == "")
                {
                    lblValidacion13.Visible = false;
                    bandera12 = 1;
                }
                else
                {
                    if (txtCelular.MaskCompleted == true)
                    {
                        lblValidacion13.Visible = false;
                        bandera12 = 1;
                    }
                    else
                    {
                        lblValidacion13.Text = "* Complete este campo";
                        lblValidacion13.Visible = true;
                        bandera12 = 0;
                    }
                }

                if (txtCorreo.Text == "" && cbxProveedor.Text == "")
                {
                    correo = string.Empty;
                    lblValidacion14.Visible = false;
                    bandera13 = 1;
                }
                else
                {
                    if (txtCorreo.Text == "")
                    {
                        correo = string.Empty;
                        lblValidacion14.Text = "* Introduce tu nombre de usuario de correo electrónico";
                        lblValidacion14.Visible = true;
                        bandera13 = 0;
                    }
                    else
                    {
                        if (cbxProveedor.Text == "")
                        {
                            correo = string.Empty;
                            lblValidacion14.Text = "* Seleccione su proveedor de correo electrónico";
                            lblValidacion14.Visible = true;
                            bandera13 = 0;
                        }
                        else
                        {
                            correo = txtCorreo.Text + "@" + cbxProveedor.Text;
                            lblValidacion14.Visible = false;
                            bandera13 = 1;
                        }
                    }
                }


                if (bandera1 == 1 && bandera2 == 1 && bandera3 == 1 && bandera4 == 1 && bandera5 == 1 && bandera6 == 1 && bandera7 == 1 && bandera8 == 1 && bandera9 == 1 && bandera10 == 1 && bandera11 == 1 && bandera12 == 1 && bandera13 == 1)
                {
                    DialogResult mensaje = MessageBox.Show("¿Desea guardar los cambios?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        //ACTUALIZAR PERSONAL//
                        personalcontroller.actualizarPersonal(Convert.ToInt64(txtClave.Text), txtNombre.Text, txtApellidos.Text, cbxSexo.Text, Convert.ToDateTime(dtpFechanacimiento.Value.ToShortDateString()), cbxEstadoCivil.Text, txtDomicilio.Text, Convert.ToInt32(txtCodigo.Text), Convert.ToInt64(cbxEstado.SelectedValue.ToString()), Convert.ToInt64(cbxMunicipio.SelectedValue.ToString()), Convert.ToInt64(cbxLocalidad.SelectedValue.ToString()), Convert.ToInt64(cbxColonia.SelectedValue.ToString()), txtTelefono.Text.Replace("(", "").Replace("-", "").Replace(")", ""), txtCelular.Text.Replace("-", ""), correo);

                        if(foto == 1)
                        {
                            //ACTUALIZACION DE LA FOTO DE PERFIL//
                            personalcontroller.actualizarFoto(Convert.ToInt64(txtClave.Text), fotografia);
                        }

                        MessageBox.Show("¡El registro ha sido actualizado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblValidacion10.Visible = false;
                        lblValidacion11.Visible = false;
                        lblValidacion12.Visible = false;
                        lblValidacion13.Visible = false;
                        lblValidacion14.Visible = false;
                        lblValidacion2.Visible = false;
                        lblValidacion3.Visible = false;
                        lblValidacion4.Visible = false;
                        lblValidacion5.Visible = false;
                        lblValidacion6.Visible = false;
                        lblValidacion7.Visible = false;
                        lblValidacion8.Visible = false;
                        lblValidacion9.Visible = false;

                        txtApellidos.Clear();
                        txtCelular.Clear();
                        txtClave.Clear();
                        txtCodigo.Clear();
                        txtCorreo.Clear();
                        txtDomicilio.Clear();
                        txtNombre.Clear();
                        txtTelefono.Clear();

                        cbxColonia.SelectedIndex = -1;
                        cbxEstado.SelectedIndex = -1;
                        cbxEstadoCivil.SelectedIndex = -1;
                        cbxLocalidad.SelectedIndex = -1;
                        cbxMunicipio.SelectedIndex = -1;
                        cbxProveedor.SelectedIndex = -1;
                        cbxSexo.SelectedIndex = -1;

                        pbxPerfil.Image = null;

                        dtpFechanacimiento.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                        btnActualizar.Enabled = false;
                        btnEliminar.Enabled = false;
                        btnGuardar.Enabled = true;
                        btnCancelar.Enabled = true;
                        btnIngresar.Enabled = true;
                        btnModificar.Enabled = false;

                        groupBox1.Enabled = true;

                        foto = 0;

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
                //ELIMINAR FOTO DE PERFIL//
                personalcontroller.eliminarFoto(Convert.ToInt64(txtClave.Text));
                //ELIMINAR USUARIO//
                personalcontroller.eliminarUsuario(Convert.ToInt64(txtClave.Text));
                //ELIMINAR PERSONAL//
                personalcontroller.eliminarPersonal(Convert.ToInt64(txtClave.Text));

                MessageBox.Show("¡El registro ha sido eliminado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblValidacion10.Visible = false;
                lblValidacion11.Visible = false;
                lblValidacion12.Visible = false;
                lblValidacion13.Visible = false;
                lblValidacion14.Visible = false;
                lblValidacion2.Visible = false;
                lblValidacion3.Visible = false;
                lblValidacion4.Visible = false;
                lblValidacion5.Visible = false;
                lblValidacion6.Visible = false;
                lblValidacion7.Visible = false;
                lblValidacion8.Visible = false;
                lblValidacion9.Visible = false;

                txtApellidos.Clear();
                txtCelular.Clear();
                txtClave.Clear();
                txtCodigo.Clear();
                txtCorreo.Clear();
                txtDomicilio.Clear();
                txtNombre.Clear();
                txtTelefono.Clear();

                cbxColonia.SelectedIndex = -1;
                cbxEstado.SelectedIndex = -1;
                cbxEstadoCivil.SelectedIndex = -1;
                cbxLocalidad.SelectedIndex = -1;
                cbxMunicipio.SelectedIndex = -1;
                cbxProveedor.SelectedIndex = -1;
                cbxSexo.SelectedIndex = -1;

                pbxPerfil.Image = null;

                dtpFechanacimiento.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnIngresar.Enabled = true;
                btnModificar.Enabled = false;

                groupBox1.Enabled = true;

                foto = 0;

                txtClave.Focus();
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCancelar_Click(sender, e);
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnGuardar.Enabled == true)
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
            if(btnEliminar.Enabled == true)
            {
                btnEliminar_Click(sender, e);
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
                        e.Handled = false;

                        personal = personalcontroller.personal(Convert.ToInt64(txtClave.Text));

                        if (personal != null)
                        {
                            MessageBox.Show("¡Búsqueda exitosa!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            btnActualizar.Enabled = true;
                            btnEliminar.Enabled = true;
                            btnGuardar.Enabled = false;
                            btnCancelar.Enabled = true;
                            btnIngresar.Enabled = false;
                            btnModificar.Enabled = true;

                            groupBox1.Enabled = false;

                            txtNombre.Text = personal.per_nombre;
                            txtApellidos.Text = personal.per_apellidos;
                            cbxSexo.Text = personal.per_sexo;
                            dtpFechanacimiento.Value = personal.per_fechanacimiento;
                            cbxEstadoCivil.Text = personal.per_estadocivil;

                            cbxMunicipio.Enabled = true;
                            cbxLocalidad.Enabled = true;
                            cbxColonia.Enabled = true;

                            txtDomicilio.Text = personal.per_domicilio;
                            txtCodigo.Text = personal.per_codigopostal.ToString();
                            cbxEstado.SelectedValue = personal.per_estado;

                            cbxMunicipio.DataSource = personalcontroller.comboBoxMunicipios(personal.per_estado);
                            cbxMunicipio.DisplayMember = "mun_nombremunicipio";
                            cbxMunicipio.ValueMember = "mun_id";
                            cbxMunicipio.SelectedValue = personal.per_municipio;

                            cbxLocalidad.DataSource = personalcontroller.comboBoxLocalidades(personal.per_municipio);
                            cbxLocalidad.DisplayMember = "loc_nombrelocalidad";
                            cbxLocalidad.ValueMember = "loc_id";
                            cbxLocalidad.SelectedValue = personal.per_localidad;

                            cbxColonia.DataSource = personalcontroller.comboBoxColonias(personal.per_localidad);
                            cbxColonia.DisplayMember = "col_nombrecolonia";
                            cbxColonia.ValueMember = "col_id";
                            cbxColonia.SelectedValue = personal.per_colonia;

                            txtTelefono.Text = personal.per_telefono.Replace("(", "").Replace(")", "").Replace("-", "");
                            txtCelular.Text = personal.per_movil.Replace("-", "");

                            if (personal.per_correoelectronico == null || personal.per_correoelectronico == "")
                            {
                                txtCorreo.Clear();
                                cbxProveedor.SelectedIndex = -1;
                                cbxProveedor.Enabled = false;
                            }
                            else
                            {
                                //UBICAMOS EL CARACTER ARROBA (@) Y ANEXAMOS LOS DATOS EN SUS RESPECTIVOS COMBOBOX
                                int ubicacion_arroba = personal.per_correoelectronico.IndexOf("@");
                                txtCorreo.Text = personal.per_correoelectronico.Substring(0, ubicacion_arroba);
                                cbxProveedor.Text = personal.per_correoelectronico.Substring(ubicacion_arroba + 1);
                                cbxProveedor.Enabled = true;
                            }

                            fotospersonal = personalcontroller.fotoPersonal(personal.per_id);

                            if (fotospersonal != null)
                            {
                                fotografia = fotospersonal.fot_fotoperfil;
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(fotografia);
                                pbxPerfil.Image = Image.FromStream(ms);
                            }

                            foto = 0;

                            txtNombre.Focus();
                            txtNombre.SelectionStart = 0;
                            txtNombre.SelectionLength = txtNombre.Text.Length;
                        }
                        else
                        {
                            MessageBox.Show("¡Sin resultados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            lblValidacion10.Visible = false;
                            lblValidacion11.Visible = false;
                            lblValidacion12.Visible = false;
                            lblValidacion13.Visible = false;
                            lblValidacion14.Visible = false;
                            lblValidacion2.Visible = false;
                            lblValidacion3.Visible = false;
                            lblValidacion4.Visible = false;
                            lblValidacion5.Visible = false;
                            lblValidacion6.Visible = false;
                            lblValidacion7.Visible = false;
                            lblValidacion8.Visible = false;
                            lblValidacion9.Visible = false;

                            txtApellidos.Clear();
                            txtCelular.Clear();
                            txtClave.Clear();
                            txtCodigo.Clear();
                            txtCorreo.Clear();
                            txtDomicilio.Clear();
                            txtNombre.Clear();
                            txtTelefono.Clear();

                            cbxColonia.SelectedIndex = -1;
                            cbxEstado.SelectedIndex = -1;
                            cbxEstadoCivil.SelectedIndex = -1;
                            cbxLocalidad.SelectedIndex = -1;
                            cbxMunicipio.SelectedIndex = -1;
                            cbxProveedor.SelectedIndex = -1;
                            cbxSexo.SelectedIndex = -1;

                            pbxPerfil.Image = null;

                            dtpFechanacimiento.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                            btnActualizar.Enabled = false;
                            btnEliminar.Enabled = false;
                            btnGuardar.Enabled = true;
                            btnCancelar.Enabled = true;
                            btnIngresar.Enabled = true;
                            btnModificar.Enabled = false;

                            groupBox1.Enabled = true;

                            foto = 0;

                            txtClave.Focus();
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

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (groupBox1.Enabled == true)
            {
                btnBuscar_Click(sender, e);
            }
        }
    }
}
