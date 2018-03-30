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
    public partial class Socios_nip : Form
    {
        //CONTROLADORES//
        SociosController socioscontroller = new SociosController();
        Seguridad seguridad = new Seguridad();

        //variables
        public string nip { get; set; }
        public long id { get; set; }
        public int accion { get; set; }

        int cerrarbandera = 0;
        public Socios_nip()
        {
            InitializeComponent();
        }

        private void Socios_nip_Load(object sender, EventArgs e)
        {
            cerrarbandera = 0;

            ToolTip notificacion = new ToolTip();
            notificacion.AutoPopDelay = 8000;
            notificacion.InitialDelay = 1000;
            notificacion.ReshowDelay = 500;
            notificacion.ShowAlways = true;
            notificacion.SetToolTip(this.btnConfirmar, "De clíc aquí para confirmar las credenciales introducidas");
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (accion == 0) //CONFIRMAR AUTORIZACION DE UN SOCIO
                {
                    if (txtNIP.Text == "" && txtConfirmarNIP.Text == "")
                    {
                        MessageBox.Show("¡Complete los campos!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cerrarbandera = 0;
                    }
                    else
                    {
                        if (txtNIP.Text != txtConfirmarNIP.Text)
                        {
                            MessageBox.Show("¡Las contraseñas no coinciden!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cerrarbandera = 0;
                        }
                        else
                        {
                            nip = txtNIP.Text;
                            cerrarbandera = 1;
                            this.Close();
                        }
                    }
                }
                else if (accion == 1)
                {
                    if (txtNIP.Text == "" && txtConfirmarNIP.Text == "")
                    {
                        MessageBox.Show("¡Complete los campos!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cerrarbandera = 1;
                    }
                    else
                    {
                        if (txtNIP.Text != txtConfirmarNIP.Text)
                        {
                            MessageBox.Show("¡Las contraseñas no coinciden!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cerrarbandera = 1;
                        }
                        else
                        {
                            DialogResult mensaje = MessageBox.Show("¿Desea modificar el NIP de autorización del socio?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (mensaje == DialogResult.Yes)
                            {
                                string nipaut = txtNIP.Text;
                                socioscontroller.cambiarNIP(id, nipaut);
                                MessageBox.Show("¡El nip de autorización ha sido modificado exitosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cerrarbandera = 1;
                                this.Close();
                            }
                        }
                    }

                }
                else //PARA AGREGAR EL NIP DE UN SOCIO
                {
                    if (txtNIP.Text == "" && txtConfirmarNIP.Text == "")
                    {
                        MessageBox.Show("¡Complete los campos!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cerrarbandera = 0;
                    }
                    else
                    {
                        if (txtNIP.Text != txtConfirmarNIP.Text)
                        {
                            MessageBox.Show("¡Las contraseñas no coinciden!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cerrarbandera = 0;
                        }
                        else
                        {
                            DialogResult mensaje = MessageBox.Show("¿Confirma la contraseña?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (mensaje == DialogResult.Yes)
                            {
                                nip = txtNIP.Text;
                                cerrarbandera = 1;
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Socios_nip_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cerrarbandera == 1 || accion == 1)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void confirmarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnConfirmar_Click(sender, e);
        }
    }
}
