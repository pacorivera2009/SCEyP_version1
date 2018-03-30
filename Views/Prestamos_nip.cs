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
    public partial class Prestamos_nip : Form
    {
        public string nip { get; set; }
        public Prestamos_nip()
        {
            InitializeComponent();
        }

        private void confirmarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnConfirmar_Click(sender, e);
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtNIP.Text == "" || txtConfirmarNIP.Text == "")
            {
                MessageBox.Show("¡Complete los campos!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (txtNIP.Text != txtConfirmarNIP.Text)
                {
                    MessageBox.Show("¡Los campos no coinciden!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult mensaje = MessageBox.Show("¿Confirma la contraseña?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        nip = txtNIP.Text;
                    }
                    else
                    {
                        nip = string.Empty;
                    }

                    this.Close();
                }
            }
        }

        private void Prestamos_nip_Load(object sender, EventArgs e)
        {
            ToolTip notificacion = new ToolTip();
            notificacion.AutoPopDelay = 8000;
            notificacion.InitialDelay = 1000;
            notificacion.ReshowDelay = 500;
            notificacion.ShowAlways = true;
            notificacion.SetToolTip(this.btnConfirmar, "De clíc aquí para confirmar las credenciales introducidas");
        }
    }
}
