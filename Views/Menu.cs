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
using Views.Reportes;

namespace Views
{
    public partial class Menu : Form
    {
        //ENTIDAD(ES)
        personal personal;
        usuarios usuarios;
        fotospersonal fotospersonal;

        //CONTROLADORES
        MenuController menu = new MenuController();

        int bandera = 1;

        public long id { get; set; }
        public Menu()
        {
            InitializeComponent();
        }

        public void toolTipMethod()
        {
            //METODOS TOOLTIP PARA MOSTRAR AL USUARIO EL PARA QUE SIRVEN LAS OPCIONES//

            //TOOLTIP
            ToolTip notificacion1 = new ToolTip();
            notificacion1.AutoPopDelay = 8000;
            notificacion1.InitialDelay = 1000;
            notificacion1.ReshowDelay = 500;
            notificacion1.ShowAlways = true;
            notificacion1.SetToolTip(this.lblSubMenu11, "De clic aquí y accederá al menú Personal");
            notificacion1.SetToolTip(this.lblSubMenu12, "De clic aquí y mostrará el personal registrado");
            //

            //TOOLTIP
            ToolTip notificacion2 = new ToolTip();
            notificacion2.AutoPopDelay = 8000;
            notificacion2.InitialDelay = 1000;
            notificacion2.ReshowDelay = 500;
            notificacion2.ShowAlways = true;
            notificacion2.SetToolTip(this.lblSubMenu21, "De clic aquí y accederá al menú Usuarios");
            notificacion2.SetToolTip(this.lblSubMenu22, "De clic aquí y mostrará el(los) usuario(s) registrado(s)");
            //

            //TOOLTIP
            ToolTip notificacion3 = new ToolTip();
            notificacion3.AutoPopDelay = 8000;
            notificacion3.InitialDelay = 1000;
            notificacion3.ReshowDelay = 500;
            notificacion3.ShowAlways = true;
            notificacion3.SetToolTip(this.lblSubMenu31, "De clic aquí y accederá al menú Socios");
            notificacion3.SetToolTip(this.lblSubMenu32, "De clic aquí y mostrará el(los) socio(s) registrado(s)");
            //

            //TOOLTIP
            ToolTip notificacion4 = new ToolTip();
            notificacion4.AutoPopDelay = 8000;
            notificacion4.InitialDelay = 1000;
            notificacion4.ReshowDelay = 500;
            notificacion4.ShowAlways = true;
            notificacion4.SetToolTip(this.lblSubMenu41, "De clic aquí y accederá al menú Prestamos");
            notificacion4.SetToolTip(this.lblSubMenu42, "De clic aquí y accederá al menú Caja de cobros");
            notificacion4.SetToolTip(this.lblSubMenu43, "De clic aquí y accederá al menú Estadísticas");
            notificacion4.SetToolTip(this.lblSubMenu44, "De clic aquí y accederá al menú Contabilidad");
            //

            //TOOLTIP
            ToolTip notificacion5 = new ToolTip();
            notificacion5.AutoPopDelay = 8000;
            notificacion5.InitialDelay = 1000;
            notificacion5.ReshowDelay = 500;
            notificacion5.ShowAlways = true;
            notificacion5.SetToolTip(this.lblSubMenu51, "De clic aquí y accederá al menú Catálogo(estados, municipios, localidades y colonias)");
            //

            //TOOLTIP
            ToolTip notificacion6 = new ToolTip();
            notificacion5.AutoPopDelay = 8000;
            notificacion5.InitialDelay = 1000;
            notificacion5.ReshowDelay = 500;
            notificacion5.ShowAlways = true;
            notificacion5.SetToolTip(this.lblSubMenu61, "Acceda a su información personal");
            notificacion5.SetToolTip(this.lblSubMenu62, "De clic aquí y cerrará la sesión actual");
            //
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            try
            {
                toolTipMethod();

                personal = menu.obtenerDatos(id);

                if(personal != null)
                {
                    usuarios = menu.datosUsuario(id);

                    if(usuarios != null)
                    {
                        lblNombreUsuario.Text = personal.per_nombre + " " + personal.per_apellidos;
                        lblPrivilegio.Text = usuarios.usu_cargo;

                        if(usuarios.usu_cargo == "ENCARGADO")
                        {
                            //PRIVILEGIOS DEL USUARIO ENCARGADO
                            lblSubMenu11.Enabled = false;
                            lblSubMenu12.Enabled = true;
                            lblSubMenu21.Enabled = false;
                            lblSubMenu22.Enabled = true;
                            lblSubMenu31.Enabled = false;
                            lblSubMenu32.Enabled = true;
                            lblSubMenu41.Enabled = true;
                            lblSubMenu42.Enabled = true;
                            lblSubMenu43.Enabled = false;
                            lblSubMenu44.Enabled = true;
                            lblSubMenu51.Enabled = false;
                            lblSubMenu61.Enabled = true;
                            lblSubMenu62.Enabled = true;
                        }
                        else if(usuarios.usu_cargo == "COBRADOR")
                        {
                            //PRIVILEGIOS DEL USUARIO COBRADOR 
                            
                            lblSubMenu11.Enabled = false;
                            lblSubMenu12.Enabled = false;
                            lblSubMenu21.Enabled = false;
                            lblSubMenu22.Enabled = false;
                            lblSubMenu31.Enabled = false;
                            lblSubMenu32.Enabled = true;
                            lblSubMenu42.Enabled = true;
                            lblSubMenu41.Enabled = true;
                            lblSubMenu43.Enabled = false;
                            lblSubMenu44.Enabled = false;
                            lblSubMenu51.Enabled = false;
                            lblSubMenu61.Enabled = true;
                            lblSubMenu62.Enabled = true;
                        }
                        else
                        {
                            //PRIVILEGIOS DE UN USUARIO ADMINISTRADOR
                            lblSubMenu11.Enabled = true;
                            lblSubMenu12.Enabled = true;
                            lblSubMenu21.Enabled = true;
                            lblSubMenu22.Enabled = true;
                            lblSubMenu31.Enabled = true;
                            lblSubMenu32.Enabled = true;
                            lblSubMenu41.Enabled = true;
                            lblSubMenu42.Enabled = true;
                            lblSubMenu43.Enabled = true;
                            lblSubMenu44.Enabled = true;
                            lblSubMenu51.Enabled = true;
                            lblSubMenu61.Enabled = true;
                            lblSubMenu62.Enabled = true;
                        }

                        fotospersonal = menu.obtenerFoto(id);

                        if(fotospersonal != null)
                        {
                            byte[] imagenBuffer = fotospersonal.fot_fotoperfil;
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

                            pbxPerfil.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;

                lblTiempo.Text = DateTime.Now.ToLongDateString() + "   " + DateTime.Now.ToShortTimeString();

                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblSubMenu11_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu11.ForeColor = Color.Black;
        }

        private void lblSubMenu12_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu12.ForeColor = Color.Black;
        }

        private void lblSubMenu21_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu21.ForeColor = Color.Black;
        }

        private void lblSubMenu22_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu22.ForeColor = Color.Black;
        }

        private void lblSubMenu31_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu31.ForeColor = Color.Black;
        }

        private void lblSubMenu33_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void lblSubMenu41_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu41.ForeColor = Color.Black;
        }

        private void lblSubMenu42_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu42.ForeColor = Color.Black;
        }

        private void lblSubMenu43_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu43.ForeColor = Color.Black;
        }

        private void lblSubMenu44_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu44.ForeColor = Color.Black;
        }

        private void lblSubMenu51_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu51.ForeColor = Color.Black;
        }

        private void lblSubMenu61_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu61.ForeColor = Color.Black;
        }

        private void lblSubMenu62_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu62.ForeColor = Color.Black;
        }

        private void lblSubMenu11_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu11.ForeColor = Color.Red;
        }

        private void lblSubMenu12_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu12.ForeColor = Color.Red;
        }

        private void lblSubMenu21_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu21.ForeColor = Color.Red;
        }

        private void lblSubMenu22_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu22.ForeColor = Color.Red;
        }

        private void lblSubMenu31_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu31.ForeColor = Color.Red;
        }
        private void lblSubMenu33_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void lblSubMenu41_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu41.ForeColor = Color.Red;
        }

        private void lblSubMenu42_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu42.ForeColor = Color.Red;
        }

        private void lblSubMenu43_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu43.ForeColor = Color.Red;
        }

        private void lblSubMenu44_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu44.ForeColor = Color.Red;
        }

        private void lblSubMenu51_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu51.ForeColor = Color.Red;
        }

        private void lblSubMenu61_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu61.ForeColor = Color.Red;
        }

        private void lblSubMenu62_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu62.ForeColor = Color.Red;
        }

        private void lblSubMenu62_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿" + lblNombreUsuario.Text + ", esta seguro de cerrar su sesión?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (mensaje == DialogResult.Yes)
            {
                bandera = 0;
                this.Close();
            }
            else
            {
                bandera = 1;
            }
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bandera == 0)
            {
                Login login = new Login();
                login.Show();
                this.Hide();
            }
            else
            {

                DialogResult mensaje;
                mensaje = MessageBox.Show("¿Desea cerrar la aplicación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (mensaje == DialogResult.Yes)
                {
                    bandera = 1;
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }

            }
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (bandera == 1)
            {
                Application.Exit();
            }
        }

        private void lblSubMenu11_Click(object sender, EventArgs e)
        {
            Personal personal = new Personal();
            personal.ShowDialog();
        }

        private void lblSubMenu12_Click(object sender, EventArgs e)
        {
            Rep_Personal rep_personal = new Rep_Personal();
            rep_personal.ShowDialog();
        }

        private void lblSubMenu21_Click(object sender, EventArgs e)
        {
            Usuarios usuarios = new Usuarios();
            usuarios.ShowDialog();
        }

        private void lblSubMenu22_Click(object sender, EventArgs e)
        {
            Rep_Usuarios repusuarios = new Rep_Usuarios();
            repusuarios.ShowDialog();
        }

        private void lblSubMenu31_Click(object sender, EventArgs e)
        {
            Socios socios = new Socios();
           socios.ShowDialog();
        }

        private void lblSubMenu33_Click(object sender, EventArgs e)
        {
            
        }

        private void lblSubMenu61_Click(object sender, EventArgs e)
        {
            Informacion informacion = new Informacion();
            informacion.id = id;
            informacion.ShowDialog();

        }

        private void lblSubMenu41_Click(object sender, EventArgs e)
        {
            Prestamos prestamos = new Prestamos();
            prestamos.ShowDialog();
        }

        private void lblSubMenu42_Click(object sender, EventArgs e)
        {
            //CajaCobro caja = new CajaCobro();
            Caja caja = new Caja();
            caja.ShowDialog();
        }

        private void lblSubMenu43_Click(object sender, EventArgs e)
        {
            Estadisticas estadisticas = new Estadisticas();
            estadisticas.ShowDialog();
        }

        private void lblSubMenu51_Click(object sender, EventArgs e)
        {
            CatalogoEMLC catalogoEMLC = new CatalogoEMLC();
            catalogoEMLC.ShowDialog();
        }

        private void lblSubMenu44_Click(object sender, EventArgs e)
        {
            //Contabilidad contabilidad = new Contabilidad();
            //contabilidad.ShowDialog();

            Reportes_menu menu = new Reportes_menu();
            menu.ShowDialog();
        }

        private void lblSubMenu32_Click(object sender, EventArgs e)
        {
            Consultasocios consultasocios = new Consultasocios();
            consultasocios.ShowDialog();
        }

        private void lblSubMenu32_MouseLeave(object sender, EventArgs e)
        {
            lblSubMenu32.ForeColor = Color.Black;
        }

        private void lblSubMenu32_MouseMove(object sender, MouseEventArgs e)
        {
            lblSubMenu32.ForeColor = Color.Red;
        }
    }
}