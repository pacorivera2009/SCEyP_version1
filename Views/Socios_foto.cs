using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Views
{
    public partial class Socios_foto : Form
    {
        public int foto { get; set; }
        public byte[] fotografia { get; set; }

        //METODOS DE VIDEO-CAMARA
        private bool ExisteDispositivo = false;
        private FilterInfoCollection DispositivoDeVideo;
        private VideoCaptureDevice FuenteDeVideo = null;
        public void CargarDispositivos(FilterInfoCollection Dispositivos)
        {
            for (int i = 0; i < Dispositivos.Count; i++) ;

            cbxDispositivos.Items.Add(Dispositivos[0].Name.ToString());
            cbxDispositivos.Text = cbxDispositivos.Items[0].ToString();

        }

        public void BuscarDispositivos()
        {
            DispositivoDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (DispositivoDeVideo.Count == 0)
            {
                ExisteDispositivo = false;
            }

            else
            {
                ExisteDispositivo = true;
                CargarDispositivos(DispositivoDeVideo);

            }
        }

        public void TerminarFuenteDeVideo()
        {
            if (!(FuenteDeVideo == null))
                if (FuenteDeVideo.IsRunning)
                {
                    FuenteDeVideo.SignalToStop();
                    FuenteDeVideo = null;
                }

        }

        public void Video_NuevoFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen = (Bitmap)eventArgs.Frame.Clone();
            pbxPerfil.Image = Imagen;
        }

        //FIN DE METODOS DE VIDEO-CAMARA
        public Socios_foto()
        {
            InitializeComponent();
            BuscarDispositivos();
        }

        private void Socios_foto_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(this.btnCamara, "De clíc aquí para tomar una fotográfia");
                notificacion.SetToolTip(this.btnGuardar, "De clíc aquí para confirmar la fotografía");

                if (ExisteDispositivo)
                {
                    FuenteDeVideo = new VideoCaptureDevice(DispositivoDeVideo[cbxDispositivos.SelectedIndex].MonikerString);
                    FuenteDeVideo.NewFrame += new NewFrameEventHandler(Video_NuevoFrame);
                    FuenteDeVideo.Start();
                    //Estado.Text = "Ejecutando Dispositivo..."
                    cbxDispositivos.Enabled = true;
                    //groupBox1.Text = DispositivoDeVideo[cbxDispositivos.SelectedIndex].Name.ToString();

                    btnCamara.Enabled = true;
                    btnGuardar.Enabled = false;

                    foto = 0;
                }
                else
                {
                    foto = 0;
                    MessageBox.Show("¡No se encuentra el dispositivo!", "¡Problema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                foto = 0;

                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCamara_Click(object sender, EventArgs e)
        {
            try
            {
                pbxPerfil.Image = pbxPerfil.Image;

                TerminarFuenteDeVideo();

                btnCamara.Enabled = false;
                cbxDispositivos.Enabled = true;
                btnGuardar.Enabled = true;

                btnGuardar.Enabled = true;

                foto = 0;
            }
            catch (Exception ex)
            {
                foto = 0;

                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Confirma la foto de perfil?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (mensaje == DialogResult.Yes)
                {
                    foto = 1;

                    //CONVERTIMOS A BYTES
                    MemoryStream ms = new MemoryStream();
                    pbxPerfil.Image.Save(ms, ImageFormat.Jpeg);
                    fotografia = ms.GetBuffer();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                foto = 0;

                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxDispositivos_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                foto = 0;

                if (FuenteDeVideo.IsRunning)
                {
                    TerminarFuenteDeVideo();

                    cbxDispositivos.Enabled = true;
                    btnCamara.Enabled = false;
                    btnGuardar.Enabled = false;

                    pbxPerfil.Image = null;
                }

                if (ExisteDispositivo)
                {
                    FuenteDeVideo = new VideoCaptureDevice(DispositivoDeVideo[cbxDispositivos.SelectedIndex].MonikerString);
                    FuenteDeVideo.NewFrame += new NewFrameEventHandler(Video_NuevoFrame);
                    FuenteDeVideo.Start();
                    //Estado.Text = "Ejecutando Dispositivo..."
                    cbxDispositivos.Enabled = true;
                    //groupBox1.Text = DispositivoDeVideo[cbxDispositivos.SelectedIndex].Name.ToString();

                    btnCamara.Enabled = true;
                    btnGuardar.Enabled = false;
                }
                else
                {
                    MessageBox.Show("¡No se encuentra el dispositivo!", "¡Problema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                foto = 0;

                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
