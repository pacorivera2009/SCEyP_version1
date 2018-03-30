using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using System.IO;
using System.Drawing.Imaging;

namespace Views
{
    public partial class Geolocalizador : Form
    {
        public string domicilio { get; set; }
        public string codigopostal { get; set; }
        public string colonia { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }

        public byte[] captura { get; set; }

        string direccion;
        public Geolocalizador()
        {
            InitializeComponent();
        }

        ChromiumWebBrowser chrome;

        private void Geolocalizador_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(this.btnCapturar, "De clíc aquí para realizar una captura de pantalla");

                pais = "MEXICO";
                CefSettings settings = new CefSettings();

                if (Cef.IsInitialized == false)
                {
                    Cef.Initialize(settings);
                }

                direccion = "https://www.google.com.mx/maps/place/" + domicilio + ",+" + codigopostal + ",+" + colonia + ",+" + ciudad + ",+" + estado + ",+" + pais;
                chrome = new ChromiumWebBrowser(direccion);
                this.panel1.Controls.Add(chrome);
                chrome.Dock = DockStyle.Fill;
                chrome.AddressChanged += Chrome_AddressChanged;
            }
            catch
            {

            }
        }

        private void Chrome_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            try
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    txtDireccion.Text = e.Address;
                }));
            }
            catch
            {
            }
        }

        private void Geolocalizador_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            if (chrome.CanGoBack)
                chrome.Back();
        }

        private void btnAdelante_Click(object sender, EventArgs e)
        {
            if (chrome.CanGoForward)
                chrome.Forward();
        }

        private void btnCapturar_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea capturar la pantalla?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(mensaje == DialogResult.Yes)
            {
                try
                {
                    Graphics gr = this.CreateGraphics();
                    Size fSize = this.Size;
                    Bitmap bm = new Bitmap(fSize.Width, fSize.Height, gr);
                    Graphics gr2 = Graphics.FromImage(bm);
                    gr2.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, fSize);
                    
                    PictureBox imgCaptura = new PictureBox();
                    //mostramos la captura de memoria a la ventana de la aplicación
                    imgCaptura.SizeMode = PictureBoxSizeMode.StretchImage;
                    imgCaptura.Image = bm;
                    imgCaptura.Visible = true;

                    //CONVERTIMOS A BYTES
                    MemoryStream ms = new MemoryStream();
                    imgCaptura.Image.Save(ms, ImageFormat.Jpeg);
                    captura = ms.GetBuffer();

                    this.Close();
                }
                catch (Exception objError)
                {
                    MessageBox.Show(objError.ToString(), "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
