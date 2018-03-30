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
    public partial class Informacion : Form
    {
        //ID DEL PERSONAL
        public long id { get; set; }

        //ENTIDADES
        personal personal;
        usuarios usuarios;
        fotospersonal fotospersonal;

        //CONTROLADORES
        PersonalController personalcontroller = new PersonalController();
        MenuController menucontroller = new MenuController();

        public Informacion()
        {
            InitializeComponent();
        }

        private void Informacion_Load(object sender, EventArgs e)
        {
            try
            {
                personal = personalcontroller.personal(id);

                if(personal != null)
                {
                    lblNombre.Text = personal.per_nombre + " " + personal.per_apellidos;
                    lblSexo.Text = personal.per_sexo;
                    lblEstadocivil.Text = personal.per_estadocivil;
                    lblFechaNac.Text = personal.per_fechanacimiento.ToShortDateString();

                    usuarios = menucontroller.datosUsuario(id);

                    if (usuarios != null)
                    {
                        lblTipoCuenta.Text = usuarios.usu_cargo;
                    }

                    fotospersonal = personalcontroller.fotoPersonal(id);

                    if(fotospersonal != null)
                    {
                        byte[] imagenBuffer = fotospersonal.fot_fotoperfil;
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

                        pbxPerfil.Image = Image.FromStream(ms);
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
