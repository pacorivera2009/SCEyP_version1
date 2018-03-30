using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WIA;
using Models;
using Controllers;

namespace Views
{
    public partial class Socios : Form
    {
        bool avalboolean = false;
        int foto = 0;
        int identificaciondelantera = 0;
        int identificaciontrasera = 0;
        int comprobante = 0;
        int autorizacionaval = 0;
        public byte[] captura { get; set; }

        //ENTIDADES//
        asociados asociados;
        asociados asociados2;
        fotosasociados fotoasociados;
        fotosasociados fotoasociados2;
        autorizacion autorizacion;
        avales aval;
        comprobante comprobante_ob;
        identificacion identificacion;
        identificacion identificacion2;
        domicilio domicilio;

        //VARIABLE//
        int accion = 0;

        //CONTROLADORES
        SociosController socioscontroller = new SociosController();

        //ID DE SOCIO
        public long id { get; set; }

        //FOTOGRAFIA
        public byte[] fotografia { get; set; }

        public byte[] fotografiaDomicilio { get; set; }

        public byte[] fotoDel_Tra { get; set; }

        public byte[] fotoComp { get; set; }

        public Socios()
        {
            InitializeComponent();
        }

        private void Socios_Load(object sender, EventArgs e)
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
                notificacion.SetToolTip(btnCambiarNIP, "De clíc aquí para modificar el NIP de autorización del socio");


                cbxEstado.DataSource = socioscontroller.comboBoxEstados();
                cbxEstado.DisplayMember = "est_nombreestado";
                cbxEstado.ValueMember = "est_id";

                cbxEstado.SelectedIndex = -1;

                dtpFechanacimiento.MaxDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            txtClave.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Socios_busqueda sociosbusqueda = new Socios_busqueda();
            sociosbusqueda.ShowDialog();
            txtClave.Text = sociosbusqueda.id;
            txtClave.Focus();
        }

        private void btnEscanear_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    CommonDialogClass commonDialogClass = new CommonDialogClass();
                    Device scannerDevice = commonDialogClass.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);

                    if (scannerDevice != null)
                    {
                        Item scannnerItem = scannerDevice.Items[1];

                        AdjustScannerSettings(scannnerItem, 240, 0, 0, 920, 500, 0, 0);
                        object scanResult = commonDialogClass.ShowTransfer(scannnerItem, WIA.FormatID.wiaFormatPNG, false);
                        if (scanResult != null)
                        {
                            ImageFile image = (ImageFile)scanResult;
                            string fileName = Path.GetTempPath() + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fffffff") + ".jpeg";
                            SaveImageToPNGFile(image, fileName);
                            pbxIdentificacion1.ImageLocation = fileName;

                            identificaciondelantera = 1;
                        }
                        else
                        {
                            if(accion == 0)
                            {
                                identificaciondelantera = 0;
                            }
                            else
                            {
                                identificaciondelantera = 1;
                            }

                        }
                    }
                }
                catch (COMException cex)
                {
                    string error = cex.ToString();
                    MessageBox.Show("¡El dispositivo no esta conectado o no es compatible!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (accion == 0)
                    {
                        identificaciondelantera = 0;
                    }
                    else
                    {
                        identificaciondelantera = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (accion == 0)
                {
                    identificaciondelantera = 0;
                }
                else
                {
                    identificaciondelantera = 1;
                }
            }
        }

        private static void AdjustScannerSettings(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel, int scanStartTopPixel,
            int scanWidthPixels, int scanHeightPixels, int brightnessPercents, int contrastPercents)
        {
            const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
            const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";
            const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
            const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";
            const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";
            const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";
            const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
            const string WIA_SCAN_CONTRAST_PERCENTS = "6155";
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, scanStartLeftPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, scanStartTopPixel);
            SetWIAProperty(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, scanWidthPixels);
            SetWIAProperty(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, scanHeightPixels);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, brightnessPercents);
            SetWIAProperty(scannnerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, contrastPercents);
        }

        private static void SetWIAProperty(IProperties properties, object propName, object propValue)
        {
            Property prop = properties.get_Item(ref propName);
            prop.set_Value(ref propValue);
        }

        private static void SaveImageToPNGFile(ImageFile image, string fileName)
        {
            ImageProcess imgProcess = new ImageProcess();
            object convertFilter = "Convert";
            string convertFilterID = imgProcess.FilterInfos.get_Item(ref convertFilter).FilterID;
            imgProcess.Filters.Add(convertFilterID, 0);
            SetWIAProperty(imgProcess.Filters[imgProcess.Filters.Count].Properties, "FormatID", WIA.FormatID.wiaFormatJPEG);
            image = imgProcess.Apply(image);
            image.SaveFile(fileName);
        }

        private void btnEscanear2_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    CommonDialogClass commonDialogClass = new CommonDialogClass();
                    Device scannerDevice = commonDialogClass.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);

                    if (scannerDevice != null)
                    {
                        Item scannnerItem = scannerDevice.Items[1];

                        AdjustScannerSettings(scannnerItem, 240, 0, 0, 920, 500, 0, 0);
                        object scanResult = commonDialogClass.ShowTransfer(scannnerItem, WIA.FormatID.wiaFormatPNG, false);
                        if (scanResult != null)
                        {
                            ImageFile image = (ImageFile)scanResult;
                            string fileName = Path.GetTempPath() + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fffffff") + ".jpeg";
                            SaveImageToPNGFile(image, fileName);
                            pbxIdentificacion2.ImageLocation = fileName;
                            identificaciontrasera = 1;
                        }
                        else
                        {
                            if (accion == 0)
                            {
                                identificaciontrasera = 0;
                            }
                            else
                            {
                                identificaciontrasera = 1;
                            }
                        }
                    }
                }
                catch (COMException cex)
                {
                    string error = cex.ToString();
                    MessageBox.Show("¡El dispositivo no esta conectado o no es compatible!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (accion == 0)
                    {
                        identificaciontrasera = 0;
                    }
                    else
                    {
                        identificaciontrasera = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (accion == 0)
                {
                    identificaciontrasera = 0;
                }
                else
                {
                    identificaciontrasera = 1;
                }
            }
        }

        private void btnEscanear3_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    CommonDialogClass commonDialogClass = new CommonDialogClass();
                    Device scannerDevice = commonDialogClass.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);

                    if (scannerDevice != null)
                    {
                        Item scannnerItem = scannerDevice.Items[1];

                        AdjustScannerSettings(scannnerItem, 300, 0, 0, 2000, 1500, 0, 0);
                        object scanResult = commonDialogClass.ShowTransfer(scannnerItem, WIA.FormatID.wiaFormatPNG, false);
                        if (scanResult != null)
                        {
                            ImageFile image = (ImageFile)scanResult;
                            string fileName = Path.GetTempPath() + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss-fffffff") + ".jpeg";
                            SaveImageToPNGFile(image, fileName);
                            pbxComprobante.ImageLocation = fileName;
                            comprobante = 1;
                        }
                        else
                        {
                            if (accion == 0)
                            {
                                comprobante = 0;
                            }
                            else
                            {
                                comprobante = 1;
                            }
                        }
                    }
                }
                catch (COMException cex)
                {
                    string error = cex.ToString();
                    MessageBox.Show("¡El dispositivo no esta conectado o no es compatible!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (accion == 0)
                    {
                        comprobante = 0;
                    }
                    else
                    {
                        comprobante = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (accion == 0)
                {
                    comprobante = 0;
                }
                else
                {
                    comprobante = 1;
                }
            }
        }

        private void cbxEstado_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxMunicipio.DataSource = socioscontroller.comboBoxMunicipios(Convert.ToInt64(cbxEstado.SelectedValue.ToString()));
                cbxMunicipio.DisplayMember = "mun_nombremunicipio";
                cbxMunicipio.ValueMember = "mun_id";

                cbxMunicipio.SelectedIndex = -1;

                if (cbxMunicipio.Items.Count == 0)
                {
                    cbxMunicipio.Enabled = false;
                }
                else
                {
                    cbxMunicipio.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxMunicipio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxLocalidad.DataSource = socioscontroller.comboBoxLocalidades(Convert.ToInt64(cbxMunicipio.SelectedValue.ToString()));
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
                cbxColonia.DataSource = socioscontroller.comboBoxColonias(Convert.ToInt64(cbxLocalidad.SelectedValue.ToString()));
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foto = 0;
            }
        }

        private void btnGeolocalizador_Click(object sender, EventArgs e)
        {
            if (!(txtDomicilio.Text == "" && txtCodigo.Text == "" && cbxEstado.Text == "" && cbxLocalidad.Text == "" && cbxMunicipio.Text == "" && cbxColonia.Text == ""))
            {
                Geolocalizador geolocalizador = new Geolocalizador();
                geolocalizador.domicilio = txtDomicilio.Text;
                geolocalizador.codigopostal = txtCodigo.Text;
                geolocalizador.colonia = cbxColonia.Text;
                geolocalizador.ciudad = cbxLocalidad.Text;
                geolocalizador.estado = cbxEstado.Text;

                geolocalizador.ShowDialog();

                captura = geolocalizador.captura;

                if (captura != null)
                {
                    System.IO.MemoryStream msImage = new System.IO.MemoryStream(captura);
                    pbxCapturaDomicilio.Image = Image.FromStream(msImage);
                }
                else
                {
                    pbxCapturaDomicilio.Image = null;
                }
            }
            else
            {
                MessageBox.Show("¡Complete los campos (datos de ubicación [domicilio particular])!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscarAVAL_Click(object sender, EventArgs e)
        {
            Socios_busqueda sociosbusqueda = new Socios_busqueda();
            sociosbusqueda.ShowDialog();
            sociosbusqueda.id = txtClave.Text;
        }

        private void cbxTipoIngreso_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbxTipoIngreso.SelectedIndex == 0)
            {
                txtClaveSocio.Enabled = true;
                btnBuscarAVAL.Enabled = true;

                txtNombreAVAL.Enabled = true;
                txtApellidosAVAL.Enabled = true;
                txtDomicilioAVAL.Enabled = true;
                txtTelefonoAVAL.Enabled = true;
                txtMovilAVAL.Enabled = true;

                avalboolean = false;
            }
            else
            {
                txtClaveSocio.Enabled = false;
                btnBuscarAVAL.Enabled = false;

                txtNombreAVAL.Clear();
                txtApellidosAVAL.Clear();
                txtDomicilioAVAL.Clear();
                txtTelefonoAVAL.Clear();
                txtMovilAVAL.Clear();

                txtNombreAVAL.Enabled = false;
                txtApellidosAVAL.Enabled = false;
                txtDomicilioAVAL.Enabled = false;
                txtTelefonoAVAL.Enabled = false;
                txtMovilAVAL.Enabled = false;

                pbxPerfilAVAL.Image = null;

                avalboolean = true;
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (mensaje == DialogResult.Yes)
            {
                txtApellidos.Clear();
                txtApellidosAVAL.Clear();
                txtMovil.Clear();
                txtClave.Clear();
                txtClaveSocio.Clear();
                txtCodigo.Clear();
                txtCorreo.Clear();
                txtDomicilio.Clear();
                txtDomicilioAVAL.Clear();
                txtDomicilioTrabajo.Clear();
                txtEntreCalles.Clear();
                txtExtension.Clear();
                txtMovilAVAL.Clear();
                txtMovilFamiliar.Clear();
                txtNombre.Clear();
                txtNombreAVAL.Clear();
                txtNombreFamiliar.Clear();
                txtNombreTrabajo.Clear();
                txtPuesto.Clear();
                txtReferenciasDomicilio.Clear();
                txtTelefono.Clear();
                txtTelefonoAVAL.Clear();
                txtTelefonoFamiliar.Clear();
                txtTelefonoTrabajo.Clear();

                cbxColonia.SelectedIndex = -1;
                cbxEstado.SelectedIndex = -1;
                cbxEstadoCivil.SelectedIndex = -1;
                cbxLocalidad.SelectedIndex = -1;
                cbxMunicipio.SelectedIndex = -1;
                cbxParentesco.SelectedIndex = -1;
                cbxProveedor.SelectedIndex = -1;
                cbxSexo.SelectedIndex = -1;
                cbxTipoIngreso.SelectedIndex = -1;

                cbxTipoIngreso.Enabled = true;

                cbxColonia.Enabled = false;
                cbxEstado.Enabled = true;
                cbxLocalidad.Enabled = false;
                cbxMunicipio.Enabled = false;

                pbxCapturaDomicilio.Image = null;
                pbxComprobante.Image = null;
                pbxIdentificacion1.Image = null;
                pbxIdentificacion2.Image = null;
                pbxPerfil.Image = null;
                pbxPerfilAVAL.Image = null;

                lblValidacion1.Visible = false;
                lblValidacion10.Visible = false;
                lblValidacion11.Visible = false;
                lblValidacion12.Visible = false;
                lblValidacion13.Visible = false;
                lblValidacion14.Visible = false;
                lblValidacion15.Visible = false;
                lblValidacion16.Visible = false;
                lblValidacion17.Visible = false;
                lblValidacion18.Visible = false;
                lblValidacion19.Visible = false;
                lblValidacion2.Visible = false;
                lblValidacion20.Visible = false;
                lblValidacion21.Visible = false;
                lblValidacion22.Visible = false;
                lblValidacion23.Visible = false;
                lblValidacion24.Visible = false;
                lblValidacion25.Visible = false;
                lblValidacion26.Visible = false;
                lblValidacion27.Visible = false;
                lblValidacion28.Visible = false;
                lblValidacion29.Visible = false;
                lblValidacion30.Visible = false;
                lblValidacion3.Visible = false;
                lblValidacion33.Visible = false;
                lblValidacion4.Visible = false;
                lblValidacion5.Visible = false;
                lblValidacion6.Visible = false;
                lblValidacion7.Visible = false;
                lblValidacion8.Visible = false;
                lblValidacion9.Visible = false;
               

                foto = 0;
                identificaciondelantera = 0;
                identificaciontrasera = 0;
                comprobante = 0;
                avalboolean = false;

                groupBox1.Enabled = true;
                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;
                btnModificar.Enabled = false;
                btnIngresar.Enabled = true;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;

                dtpFechanacimiento.MaxDate = DateTime.Now;
                dtpFechanacimiento.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                txtClave.Enabled = true;
                btnBuscar.Enabled = true;

                //INVISIBLE BOTON
                btnCambiarNIP.Enabled = false;

                txtClave.Focus();
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //ACTIVAMOS LA ACCION DE INSERCION
                accion = 0;

                int bandera1 = 0, bandera2 = 0, bandera3 = 0, bandera4 = 0, bandera5 = 0, bandera6 = 0, bandera7 = 0, bandera8 = 0, bandera9 = 0, bandera10 = 0, bandera11 = 0, bandera12 = 0, bandera13 = 0, bandera14 = 0, bandera15 = 0, bandera16 = 0, bandera17 = 0, bandera18 = 0, bandera19 = 0, bandera20 = 0, bandera21 = 0, bandera22 = 0, bandera23 = 0, bandera24 = 0, bandera25 = 0;

                if(txtNombre.Text == "")
                {
                    lblValidacion1.Text = "* Complete este campo";
                    lblValidacion1.Visible = true;
                    bandera1 = 0;
                }
                else
                {
                    lblValidacion1.Visible = false;
                    bandera1 = 1;
                }

                if(txtApellidos.Text == "")
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

                if(cbxSexo.Text == "")
                {
                    lblValidacion3.Text = "* Complete este campo";
                    lblValidacion3.Visible = true;
                    bandera3 = 0;
                }
                else
                {
                    lblValidacion3.Visible = false;
                    bandera3 = 1;
                }

                if(cbxEstadoCivil.Text == "")
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

                if (txtTelefono.Text == "(   )   -" && txtMovil.Text == "   -   -")
                {
                    lblValidacion5.Text = "* Introduce al menos un número teléfonico";
                    lblValidacion5.Visible = true;
                    lblValidacion6.Visible = false;
                    bandera5 = 0;
                }
                else
                {
                    if (txtTelefono.Text == "(   )   -")
                    {
                        lblValidacion5.Visible = false;
                        bandera5 = 1;
                    }
                    else
                    {
                        if (txtTelefono.MaskCompleted == true)
                        {
                            lblValidacion5.Visible = false;
                            bandera5 = 1;
                        }
                        else
                        {
                            lblValidacion5.Text = "* Complete este campo";
                            lblValidacion5.Visible = true;
                            bandera5 = 0;
                        }
                    }

                    if (txtMovil.Text == "   -   -")
                    {
                        lblValidacion6.Visible = false;
                        bandera5 = 1;
                    }
                    else
                    {
                        if (txtMovil.MaskCompleted == true)
                        {
                            lblValidacion6.Visible = false;
                            bandera5 = 1;
                        }
                        else
                        {
                            lblValidacion6.Text = "* Complete este campo";
                            lblValidacion6.Visible = true;
                            bandera5 = 0;
                        }
                    }
                }

                if (txtCorreo.Text == "" && cbxProveedor.Text == "")
                {
                    lblValidacion7.Visible = false;
                    bandera6 = 1;
                }
                else
                {
                    if(txtCorreo.Text == "")
                    {
                        lblValidacion7.Text = "* Introduce su nombre de usuario de su correo electrónico";
                        lblValidacion7.Visible = true;
                        bandera6 = 0;
                    }
                    else
                    {
                        lblValidacion7.Visible = false;
                    }

                    if(cbxProveedor.Text == "")
                    {
                        lblValidacion7.Text = "* Seleccione su proveedor de correo electrónico";
                        lblValidacion7.Visible = true;
                        bandera6 = 0;
                    }
                    else
                    {
                        lblValidacion7.Visible = false;
                    }

                    if(txtCorreo.Text != "" && cbxProveedor.Text != "")
                    {
                        bandera6 = 1;
                    }
                }

                if(txtNombreFamiliar.Text == "")
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

                if(cbxParentesco.Text == "")
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

                if(txtTelefonoFamiliar.Text == "(   )   -" && txtMovilFamiliar.Text == "   -   -")
                {
                    lblValidacion10.Text = "* Introduce al menos un número teléfonico";
                    lblValidacion10.Visible = true;
                    lblValidacion11.Visible = false;
                    bandera9 = 0;
                }
                else
                {
                    if (txtTelefonoFamiliar.Text == "(   )   -")
                    {
                        lblValidacion10.Visible = false;
                        bandera9 = 1;
                    }
                    else
                    {
                        if (txtTelefonoFamiliar.MaskCompleted == true)
                        {
                            lblValidacion10.Visible = false;
                            bandera9 = 1;
                        }
                        else
                        {
                            lblValidacion10.Text = "* Complete este campo";
                            lblValidacion10.Visible = true;
                            bandera9 = 0;
                        }
                    }

                    if (txtMovilFamiliar.Text == "   -   -")
                    {
                        lblValidacion11.Visible = false;
                        bandera9 = 1;
                    }
                    else
                    {
                        if (txtMovilFamiliar.MaskCompleted == true)
                        {
                            lblValidacion11.Visible = false;
                            bandera9 = 1;
                        }
                        else
                        {
                            lblValidacion11.Text = "* Complete este campo";
                            lblValidacion11.Visible = true;
                            bandera9 = 0;
                        }
                    }
                }

                if(txtDomicilio.Text == "")
                {
                    lblValidacion12.Text = "* Complete este campo";
                    lblValidacion12.Visible = true;
                    bandera10 = 0;
                }
                else
                {
                    lblValidacion12.Visible = false;
                    bandera10 = 1;
                }

                if(txtReferenciasDomicilio.Text == "")
                {
                    lblValidacion13.Text = "* Complete este campo";
                    lblValidacion13.Visible = true;
                    bandera11 = 0;
                }
                else
                {
                    lblValidacion13.Visible = false;
                    bandera11 = 1;
                }

                if(txtEntreCalles.Text == "")
                {
                    lblValidacion14.Text = "* Complete este campo";
                    lblValidacion14.Visible = true;
                    bandera12 = 0;
                }
                else
                {
                    lblValidacion14.Visible = false;
                    bandera12 = 1;
                }

                if(txtCodigo.Text == "")
                {
                    lblValidacion15.Text = "* Complete este campo";
                    lblValidacion15.Visible = true;
                    bandera13 = 0;
                }
                else
                {
                    lblValidacion15.Visible = false;
                    bandera13 = 1;
                }

                if(cbxEstado.Text == "")
                {
                    lblValidacion16.Text = "* Complete este campo";
                    lblValidacion16.Visible = true;
                    bandera14 = 0;
                }
                else
                {
                    lblValidacion16.Visible = false;
                    bandera14 = 1;
                }

                if(cbxMunicipio.Text == "")
                {
                    lblValidacion17.Text = "* Complete este campo";
                    lblValidacion17.Visible = true;
                    bandera14 = 0;
                }
                else
                {
                    lblValidacion17.Visible = false;
                    bandera14 = 1;
                }

                if(cbxLocalidad.Text == "")
                {
                    lblValidacion18.Text = "* Complete este campo";
                    lblValidacion18.Visible = true;
                    bandera15 = 0;
                }
                else
                {
                    lblValidacion18.Visible = false;
                    bandera15 = 1;
                }

                if(cbxColonia.Text == "")
                {
                    lblValidacion19.Text = "* Complete este campo";
                    lblValidacion19.Visible = true;
                    bandera16 = 0;
                }
                else
                {
                    lblValidacion19.Visible = false;
                    bandera16 = 1;
                }

                if(txtNombreTrabajo.Text == "")
                {
                    lblValidacion20.Text = "* Complete este campo";
                    lblValidacion20.Visible = true;
                    bandera17 = 0;
                }
                else
                {
                    lblValidacion20.Visible = false;
                    bandera17 = 1;
                }

                if(txtDomicilioTrabajo.Text == "")
                {
                    lblValidacion21.Text = "* Complete este campo";
                    lblValidacion21.Visible = true;
                    bandera18 = 0;
                }
                else
                {
                    lblValidacion21.Visible = false;
                    bandera18 = 1;
                }

                if(txtPuesto.Text == "")
                {
                    lblValidacion22.Text = "* Complete este campo";
                    lblValidacion22.Visible = true;
                    bandera19 = 0;
                }
                else
                {
                    lblValidacion22.Visible = false;
                    bandera19 = 1;
                }

                if(txtTelefonoTrabajo.Text == "(   )   -")
                {
                    lblValidacion23.Visible = false;
                    bandera20 = 1;
                }
                else
                {
                    if(txtTelefonoTrabajo.MaskCompleted == true)
                    {
                        lblValidacion23.Visible = false;
                        bandera20 = 1;
                    }
                    else
                    {
                        lblValidacion23.Text = "* Complete este campo";
                        lblValidacion23.Visible = true;
                        bandera20 = 0;
                    }
                }

                if(cbxTipoIngreso.Text == "")
                {
                    lblValidacion24.Text = "* Complete este campo";
                    lblValidacion24.Visible = true;
                    bandera21 = 0;
                }
                else
                {
                    lblValidacion24.Visible = false;
                    bandera21 = 1;
                }

                if(cbxTipoIngreso.Text != "")
                {
                    if (avalboolean == false)
                    {
                        if (txtNombreAVAL.Text == "")
                        {
                            lblValidacion26.Text = "* Complete este campo";
                            lblValidacion26.Visible = true;
                            bandera22 = 0;
                        }
                        else
                        {
                            lblValidacion26.Visible = false;
                            bandera22 = 1;
                        }

                        if (txtApellidosAVAL.Text == "")
                        {
                            lblValidacion27.Text = "* Complete este campo";
                            lblValidacion27.Visible = true;
                            bandera23 = 0;
                        }
                        else
                        {
                            lblValidacion27.Visible = false;
                            bandera23 = 1;
                        }

                        if (txtDomicilioAVAL.Text == "")
                        {
                            lblValidacion28.Text = "* Complete este campo";
                            lblValidacion28.Visible = true;
                            bandera24 = 0;
                        }
                        else
                        {
                            lblValidacion28.Visible = false;
                            bandera24 = 1;
                        }

                        if (txtTelefonoAVAL.Text == "(   )   -" && txtMovilAVAL.Text == "   -   -")
                        {
                            lblValidacion29.Text = "* Introduce al menos un número teléfonico";
                            lblValidacion29.Visible = true;
                            lblValidacion30.Visible = false;
                            bandera25 = 0;
                        }
                        else
                        {
                            if (txtTelefonoAVAL.Text == "(   )   -")
                            {
                                lblValidacion29.Visible = false;
                                bandera25 = 1;
                            }
                            else
                            {
                                if (txtTelefonoFamiliar.MaskCompleted == true)
                                {
                                    lblValidacion29.Visible = false;
                                    bandera25 = 1;
                                }
                                else
                                {
                                    lblValidacion29.Text = "* Complete este campo";
                                    lblValidacion10.Visible = true;
                                    bandera9 = 0;
                                }
                            }

                            if (txtMovilAVAL.Text == "   -   -")
                            {
                                lblValidacion30.Visible = false;
                                bandera25 = 1;
                            }
                            else
                            {
                                if (txtMovilFamiliar.MaskCompleted == true)
                                {
                                    lblValidacion30.Visible = false;
                                    bandera25 = 1;
                                }
                                else
                                {
                                    lblValidacion30.Text = "* Complete este campo";
                                    lblValidacion30.Visible = true;
                                    bandera25 = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        bandera22 = 1; bandera23 = 1; bandera24 = 1; bandera25 = 1;
                    }
                }


                if(bandera1 == 1 && bandera2 == 1 && bandera3==1 && bandera4==1 && bandera5==1 && bandera6==1 && bandera7==1 && bandera8==1 && bandera9==1 && bandera10==1 && bandera11==1 && bandera12==1 && bandera13==1 && bandera14== 1 && bandera15==1 && bandera16== 1 && bandera17==1 && bandera18==1 && bandera19==1 && bandera20==1 && bandera21==1 && bandera22 == 1 && bandera23 == 1 && bandera24 == 1 && bandera25 == 1)
                {
                    //COMPROBAMOS SI LA FOTOGRAFIA DE PERFIL ESTA TOMADA
                    if (foto == 0)
                    {
                        MessageBox.Show("¡Falta la fotografía de identificación del socio a registrar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if(identificaciondelantera == 0)
                    {
                        MessageBox.Show("¡Falta la credencial de identificación del socio a registrar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if(identificaciontrasera == 0)
                    {
                        MessageBox.Show("¡Falta la credencial de identificación del socio (parte trasera) a registrar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if(comprobante == 0)
                    {
                        MessageBox.Show("¡Falta el comprobante de domicilio del socio!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    if (foto == 1 && identificaciondelantera == 1 && identificaciontrasera == 1)
                    {
                        DialogResult mensaje = MessageBox.Show("¿Desea ingresar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if(mensaje == DialogResult.Yes)
                        {
                            string telefono, movil, telefonotrabajo, extension, telefonofamiliar, movilfamiliar, telefonoaval, movilaval;

                            if (txtTelefono.MaskCompleted == true)
                            {
                                telefono = txtTelefono.Text;
                            }
                            else
                            {
                                telefono = "";
                            }

                            if (txtMovil.MaskCompleted == true)
                            {
                                movil = txtMovil.Text;
                            }
                            else
                            {
                                movil = "";
                            }

                            if (txtTelefonoTrabajo.MaskCompleted == true)
                            {
                                telefonotrabajo = txtTelefonoTrabajo.Text;
                                extension = txtExtension.Text;
                            }
                            else
                            {
                                telefonotrabajo = "";
                                extension = "";
                            }

                            if (txtTelefonoFamiliar.MaskCompleted == true)
                            {
                                telefonofamiliar = txtTelefonoFamiliar.Text;
                            }
                            else
                            {
                                telefonofamiliar = "";
                            }

                            if (txtMovilFamiliar.MaskCompleted == true)
                            {
                                movilfamiliar = txtMovilFamiliar.Text;
                            }
                            else
                            {
                                movilfamiliar = "";
                            }

                            if (txtTelefonoAVAL.MaskCompleted == true)
                            {
                                telefonoaval = txtTelefonoAVAL.Text;
                            }
                            else
                            {
                                telefonoaval = "";
                            }

                            if (txtMovilAVAL.MaskCompleted == true)
                            {
                                movilaval = txtMovilAVAL.Text;
                            }
                            else
                            {
                                movilaval = "";
                            }

                            //AGREGAR DATOS DEL SOCIO
                            long id = socioscontroller.agregarSocios(txtNombre.Text, txtApellidos.Text, Convert.ToDateTime(dtpFechanacimiento.Value.ToShortDateString()), cbxSexo.Text, cbxEstadoCivil.Text, txtDomicilio.Text, txtReferenciasDomicilio.Text, txtEntreCalles.Text, Convert.ToInt32(txtCodigo.Text), Convert.ToInt64(cbxColonia.SelectedValue.ToString()), Convert.ToInt64(cbxLocalidad.SelectedValue.ToString()), Convert.ToInt64(cbxMunicipio.SelectedValue.ToString()), Convert.ToInt64(cbxEstado.SelectedValue.ToString()), cbxTipoIngreso.Text, telefono, movil, txtCorreo.Text + "@" + cbxProveedor.Text, txtNombreTrabajo.Text, txtPuesto.Text, txtDomicilioTrabajo.Text, telefonotrabajo, extension, txtNombreFamiliar.Text, cbxParentesco.Text, telefonofamiliar, movilfamiliar, Convert.ToDateTime(DateTime.Now.ToShortDateString()));

                            //AGREGAR FOTO DE PERFIL DEL SOCIO
                            socioscontroller.agregarFoto(id, fotografia);

                            //AGREGAR DATOS DEL COMPROBANTE DE DOMICILIO Y DE IDENTIFICACION

                            //CONVERTIMOS A BYTES --> IDENTIFICACION DELANTERA
                            byte[] identificaciondel;
                            MemoryStream ms1 = new MemoryStream();
                            pbxIdentificacion1.Image.Save(ms1, ImageFormat.Jpeg);
                            identificaciondel = ms1.GetBuffer();
                            socioscontroller.agregarIdentificacion(id, identificaciondel, "DELANTERA");

                            //CONVERTIMOS A BYTES --> IDENTIFICACION TRASERA
                            byte[] identificaciontra;
                            MemoryStream ms2 = new MemoryStream();
                            pbxIdentificacion2.Image.Save(ms2, ImageFormat.Jpeg);
                            identificaciontra = ms2.GetBuffer();
                            socioscontroller.agregarIdentificacion(id, identificaciontra, "TRASERA");

                            //CONVERTIMOS A BYTES --> COMPROBANTE DE DOMICILIO
                            byte[] comprobantedom;
                            MemoryStream ms3 = new MemoryStream();
                            pbxComprobante.Image.Save(ms3, ImageFormat.Jpeg);
                            comprobantedom = ms3.GetBuffer();
                            socioscontroller.agregarComprobante(id, comprobantedom);

                            //AGREGAR FOTO DEL DOMICILIO --> EN CASO QUE SE HAYA TOMADO CAPTURA
                            if (pbxCapturaDomicilio.Image != null)
                            {
                                //CONVERTIMOS A BYTES --> CAPTURA DE DOMICILIO
                                byte[] domiciliofoto;
                                MemoryStream ms4 = new MemoryStream();
                                pbxCapturaDomicilio.Image.Save(ms4, ImageFormat.Jpeg);
                                domiciliofoto = ms4.GetBuffer();
                                socioscontroller.agregarDomicilio(id, domiciliofoto);
                            }

                            if(cbxTipoIngreso.SelectedIndex == 0)
                            {
                                //ES UN AVAL QUE ES SOCIO
                                if(avalboolean == false)
                                {
                                    socioscontroller.agregarAVAL(id, txtNombreAVAL.Text, txtApellidosAVAL.Text, txtDomicilioAVAL.Text, telefonoaval, movilaval, true, Convert.ToInt64(txtClaveSocio.Text));
                                }
                                else // ES UN AVAL QUE NO ES SOCIO
                                {
                                    socioscontroller.agregarAVAL(id, txtNombreAVAL.Text, txtApellidosAVAL.Text, txtDomicilioAVAL.Text, telefonoaval, movilaval, false, Convert.ToInt64("f"));
                                }
                            }

                            Socios_nip socios_nip = new Socios_nip();
                            socios_nip.id = id;
                            socios_nip.accion = 2;
                            socios_nip.ShowDialog();

                            if(socios_nip.nip != "")
                            {
                                socioscontroller.agregarAutorizacion(id, socios_nip.nip);
                            }

                            MessageBox.Show("¡El registro ha sido ingresado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            txtApellidos.Clear();
                            txtApellidosAVAL.Clear();
                            txtMovil.Clear();
                            txtClave.Clear();
                            txtClaveSocio.Clear();
                            txtCodigo.Clear();
                            txtCorreo.Clear();
                            txtDomicilio.Clear();
                            txtDomicilioAVAL.Clear();
                            txtDomicilioTrabajo.Clear();
                            txtEntreCalles.Clear();
                            txtExtension.Clear();
                            txtMovilAVAL.Clear();
                            txtMovilFamiliar.Clear();
                            txtNombre.Clear();
                            txtNombreAVAL.Clear();
                            txtApellidosAVAL.Clear();
                            txtDomicilioAVAL.Clear();
                            txtTelefonoAVAL.Clear();
                            txtMovilAVAL.Clear();
                            txtNombreFamiliar.Clear();
                            txtNombreTrabajo.Clear();
                            txtPuesto.Clear();
                            txtReferenciasDomicilio.Clear();
                            txtTelefono.Clear();
                            txtTelefonoAVAL.Clear();
                            txtTelefonoFamiliar.Clear();
                            txtTelefonoTrabajo.Clear();

                            cbxColonia.SelectedIndex = -1;
                            cbxEstado.SelectedIndex = -1;
                            cbxEstadoCivil.SelectedIndex = -1;
                            cbxLocalidad.SelectedIndex = -1;
                            cbxMunicipio.SelectedIndex = -1;
                            cbxParentesco.SelectedIndex = -1;
                            cbxProveedor.SelectedIndex = -1;
                            cbxSexo.SelectedIndex = -1;
                            cbxTipoIngreso.SelectedIndex = -1;

                            cbxTipoIngreso.Enabled = true;
                            btnBuscarAVAL.Enabled = false;
                            txtClaveSocio.Enabled = false;
                            txtNombreAVAL.Enabled = false;
                            txtApellidosAVAL.Enabled = false;
                            txtDomicilioAVAL.Enabled = false;
                            txtTelefonoAVAL.Enabled = false;
                            txtMovilAVAL.Enabled = false;

                            cbxColonia.Enabled = false;
                            cbxEstado.Enabled = true;
                            cbxLocalidad.Enabled = false;
                            cbxMunicipio.Enabled = false;

                            pbxCapturaDomicilio.Image = null;
                            pbxComprobante.Image = null;
                            pbxIdentificacion1.Image = null;
                            pbxIdentificacion2.Image = null;
                            pbxPerfil.Image = null;
                            pbxPerfilAVAL.Image = null;

                            foto = 0;
                            identificaciondelantera = 0;
                            identificaciontrasera = 0;
                            comprobante = 0;
                            avalboolean = false;

                            groupBox1.Enabled = true;
                            btnActualizar.Enabled = false;
                            btnEliminar.Enabled = false;
                            btnModificar.Enabled = false;
                            btnIngresar.Enabled = true;
                            btnGuardar.Enabled = true;
                            btnCancelar.Enabled = true;

                            txtClave.Enabled = true;
                            btnBuscar.Enabled = true;

                            dtpFechanacimiento.MaxDate = DateTime.Now;
                            dtpFechanacimiento.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                            txtClave.Focus();
                        }
                    }
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
                if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
                {
                    e.Handled = true;
                    return;
                }
                //long id_aval = 0;

                if (e.KeyChar == (char)Keys.Enter)
                {
                    if(txtClave.Text != "")
                    {
                        asociados = socioscontroller.asociados(Convert.ToInt64(txtClave.Text));

                        if (asociados != null)
                        {
                            MessageBox.Show("¡Búsqueda exitosa!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //BOTONES DE FOTOS -- ingreso/modificar foto
                            btnIngresar.Enabled = false;
                            btnModificar.Enabled = true;

                            //TOMAMOS DATOS//
                            txtNombre.Text = asociados.aso_nombre;
                            txtApellidos.Text = asociados.aso_apellidos;
                            cbxSexo.Text = asociados.aso_sexo;
                            dtpFechanacimiento.Value = asociados.aso_fechanacimiento;
                            cbxEstadoCivil.Text = asociados.aso_estadocivil;

                            txtTelefono.Text = asociados.aso_telefono.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "");
                            txtMovil.Text = asociados.aso_movil.Replace("-", "").Replace(" ", "");

                            if (asociados.aso_correoelectronico == null || asociados.aso_correoelectronico == "")
                            {
                                txtCorreo.Clear();
                                cbxProveedor.SelectedIndex = -1;
                                cbxProveedor.Enabled = false;
                            }
                            else
                            {
                                //UBICAMOS EL CARACTER ARROBA (@) Y ANEXAMOS LOS DATOS EN SUS RESPECTIVOS COMBOBOX
                                int ubicacion_arroba = asociados.aso_correoelectronico.IndexOf("@");
                                txtCorreo.Text = asociados.aso_correoelectronico.Substring(0, ubicacion_arroba);
                                cbxProveedor.Text = asociados.aso_correoelectronico.Substring(ubicacion_arroba + 1);
                                cbxProveedor.Enabled = true;
                            }

                            txtNombreFamiliar.Text = asociados.aso_nombrefamiliar;
                            cbxParentesco.Text = asociados.aso_parentesco;

                            txtTelefonoFamiliar.Text = asociados.aso_telefonofamiliar.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "");
                            txtMovilFamiliar.Text = asociados.aso_movilfamiliar.Replace("-", "").Replace(" ", "");

                            txtDomicilio.Text = asociados.aso_domicilio;
                            txtReferenciasDomicilio.Text = asociados.aso_referenciasdomicilio;
                            txtEntreCalles.Text = asociados.aso_referenciascalles;
                            txtCodigo.Text = asociados.aso_codigopostal.ToString();

                            cbxMunicipio.Enabled = true;
                            cbxLocalidad.Enabled = true;
                            cbxColonia.Enabled = true;

                            cbxEstado.SelectedValue = asociados.aso_estado;

                            cbxMunicipio.DataSource = socioscontroller.comboBoxMunicipios(asociados.aso_estado);
                            cbxMunicipio.DisplayMember = "mun_nombremunicipio";
                            cbxMunicipio.ValueMember = "mun_id";
                            cbxMunicipio.SelectedValue = asociados.aso_municipio;

                            cbxLocalidad.DataSource = socioscontroller.comboBoxLocalidades(asociados.aso_municipio);
                            cbxLocalidad.DisplayMember = "loc_nombrelocalidad";
                            cbxLocalidad.ValueMember = "loc_id";
                            cbxLocalidad.SelectedValue = asociados.aso_localidad;

                            cbxColonia.DataSource = socioscontroller.comboBoxColonias(asociados.aso_localidad);
                            cbxColonia.DisplayMember = "col_nombrecolonia";
                            cbxColonia.ValueMember = "col_id";
                            cbxColonia.SelectedValue = asociados.aso_colonia;

                            txtNombreTrabajo.Text = asociados.aso_nombretrabajo;
                            txtDomicilioTrabajo.Text = asociados.aso_domiciliotrabajo;
                            txtPuesto.Text = asociados.aso_ocupacion;
                            txtTelefonoTrabajo.Text = asociados.aso_telefonotrabajo;
                            txtExtension.Text = asociados.aso_extension;

                            fotoasociados = socioscontroller.fotosasociados(Convert.ToInt64(txtClave.Text));
                            if (fotoasociados != null)
                            {
                                System.IO.MemoryStream ms1 = new System.IO.MemoryStream(fotoasociados.fot_fotoperfil);
                                pbxPerfil.Image = Image.FromStream(ms1);
                            }

                            //COMPROBANTE DE DOMICILIO
                            comprobante_ob = socioscontroller.buscarDomicilio(asociados.aso_id);
                            if (comprobante_ob != null)
                            {
                                System.IO.MemoryStream ms2 = new System.IO.MemoryStream(comprobante_ob.com_comprobante);
                                pbxComprobante.Image = Image.FromStream(ms2);
                            }

                            //FOTOGRAFIA DE PERFIL DELANTERA (CREDENCIAL)
                            identificacion = socioscontroller.buscarIdentificacion(Convert.ToInt64(txtClave.Text), "DELANTERA");
                            if (identificacion != null)
                            {
                                fotoDel_Tra = identificacion.ide_identificacion;
                                System.IO.MemoryStream ms3 = new System.IO.MemoryStream(fotoDel_Tra);
                                pbxIdentificacion1.Image = Image.FromStream(ms3);
                            }

                            //FOTOGRAFIA DE PERFIL TRASERA (CREDENCIAL)
                            identificacion2 = socioscontroller.buscarIdentificacion(Convert.ToInt64(txtClave.Text), "TRASERA");
                            if (identificacion2 != null)
                            {
                                fotoDel_Tra = identificacion2.ide_identificacion;
                                System.IO.MemoryStream ms4 = new System.IO.MemoryStream(fotoDel_Tra);
                                pbxIdentificacion2.Image = Image.FromStream(ms4);
                            }

                            //DOMICILIO CAPTURADO DE MAPS
                            domicilio = socioscontroller.buscarMapsCapturadoDom(Convert.ToInt64(txtClave.Text));

                            if (domicilio != null)
                            {
                                System.IO.MemoryStream ms5 = new System.IO.MemoryStream(domicilio.dom_foto);
                                pbxCapturaDomicilio.Image = Image.FromStream(ms5);
                            }

                            //VERIFICACION SI TIENE AVAL O NO
                            if ((cbxTipoIngreso.Text = asociados.aso_tipodeingreso) != "BUEN HISTORIAL CREDITICIO")
                            {
                                aval = socioscontroller.aval(asociados.aso_id);

                                if (aval != null)
                                {
                                    txtClaveSocio.Text = aval.ava_idsocio.ToString();

                                    txtNombreAVAL.Text = aval.ava_nombre;
                                    txtApellidosAVAL.Text = aval.ava_apellidos;
                                    txtDomicilioAVAL.Text = aval.ava_domicilio;
                                    txtTelefonoAVAL.Text = aval.ava_telefono.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "");
                                    txtMovilAVAL.Text = aval.ava_movil.Replace("-", "").Replace(" ", "");

                                    if (aval.ava_socio == true)
                                    {
                                        fotoasociados2 = socioscontroller.fotosasociados(Convert.ToInt64(aval.ava_idsocio));
                                        if (fotoasociados2 != null)
                                        {
                                            System.IO.MemoryStream ms6 = new System.IO.MemoryStream(fotoasociados2.fot_fotoperfil);
                                            pbxPerfilAVAL.Image = Image.FromStream(ms6);
                                        }
                                    }
                                }
                            }
                            //DESHABILITACION DE CONTROLES//
                            txtClaveSocio.Enabled = false;
                            cbxTipoIngreso.Enabled = false;
                            txtClave.Enabled = false;
                            btnBuscar.Enabled = false;
                            txtNombreAVAL.Enabled = false;
                            txtApellidosAVAL.Enabled = false;
                            txtDomicilioAVAL.Enabled = false;
                            txtTelefonoAVAL.Enabled = false;
                            txtMovilAVAL.Enabled = false;
                            btnBuscarAVAL.Enabled = false;

                            //HABILITACION DE BOTONES//
                            btnActualizar.Enabled = true;
                            btnEliminar.Enabled = true;
                            btnGuardar.Enabled = false;
                            btnCancelar.Enabled = true;

                            //VISIBLE BOTON
                            btnCambiarNIP.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("¡Socio no encontrado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            txtClave.Clear();
                            txtClave.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("¡Introduce la clave de socio!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtClave.Focus();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAutorizar_Click(object sender, EventArgs e)
        {
            //BUSCAMOS EL SOCIO//
            autorizacionaval = 0;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            //ACTIVAMOS LA OPCION DE ACTUALIZAR//
            accion = 1;

            //ACTUALIZACION DE LOS DATOS DEL SOCIO//
            int bandera1 = 0, bandera2 = 0, bandera3 = 0, bandera4 = 0, bandera5 = 0, bandera6 = 0, bandera7 = 0, bandera8 = 0, bandera9 = 0, bandera10 = 0, bandera11 = 0, bandera12 = 0, bandera13 = 0, bandera14 = 0, bandera15 = 0, bandera16 = 0, bandera17 = 0, bandera18 = 0, bandera19 = 0, bandera20 = 0, bandera21 = 0, bandera22 = 0;

            if (txtNombre.Text == "")
            {
                lblValidacion1.Text = "* Complete este campo";
                lblValidacion1.Visible = true;
                bandera1 = 0;
            }
            else
            {
                lblValidacion1.Visible = false;
                bandera1 = 1;
            }

            if (txtApellidos.Text == "")
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

            if (cbxSexo.Text == "")
            {
                lblValidacion3.Text = "* Complete este campo";
                lblValidacion3.Visible = true;
                bandera3 = 0;
            }
            else
            {
                lblValidacion3.Visible = false;
                bandera3 = 1;
            }

            if (cbxEstadoCivil.Text == "")
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

            if (cbxSexo.Text == "")
            {
                lblValidacion3.Text = "* Complete este campo";
                lblValidacion3.Visible = true;
                bandera3 = 0;
            }
            else
            {
                lblValidacion3.Visible = false;
                bandera3 = 1;
            }

            if (cbxEstadoCivil.Text == "")
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

            if (txtTelefono.Text == "(   )   -" && txtMovil.Text == "   -   -")
            {
                lblValidacion5.Text = "* Introduce al menos un número teléfonico";
                lblValidacion5.Visible = true;
                lblValidacion6.Visible = false;
                bandera5 = 0;
            }
            else
            {
                if (txtTelefono.Text == "(   )   -")
                {
                    lblValidacion5.Visible = false;
                    bandera5 = 1;
                }
                else
                {
                    if (txtTelefono.MaskCompleted == true)
                    {
                        lblValidacion5.Visible = false;
                        bandera5 = 1;
                    }
                    else
                    {
                        lblValidacion5.Text = "* Complete este campo";
                        lblValidacion5.Visible = true;
                        bandera5 = 0;
                    }
                }

                if (txtMovil.Text == "   -   -")
                {
                    lblValidacion6.Visible = false;
                    bandera5 = 1;
                }
                else
                {
                    if (txtMovil.MaskCompleted == true)
                    {
                        lblValidacion6.Visible = false;
                        bandera5 = 1;
                    }
                    else
                    {
                        lblValidacion6.Text = "* Complete este campo";
                        lblValidacion6.Visible = true;
                        bandera5 = 0;
                    }
                }
            }

            if (txtCorreo.Text == "" && cbxProveedor.Text == "")
            {
                lblValidacion7.Visible = false;
                bandera6 = 1;
            }
            else
            {
                if (txtCorreo.Text == "")
                {
                    lblValidacion7.Text = "* Introduce su nombre de usuario de su correo electrónico";
                    lblValidacion7.Visible = true;
                    bandera6 = 0;
                }
                else
                {
                    lblValidacion7.Visible = false;
                }

                if (cbxProveedor.Text == "")
                {
                    lblValidacion7.Text = "* Seleccione su proveedor de correo electrónico";
                    lblValidacion7.Visible = true;
                    bandera6 = 0;
                }
                else
                {
                    lblValidacion7.Visible = false;
                }

                if (txtCorreo.Text != "" && cbxProveedor.Text != "")
                {
                    bandera6 = 1;
                }
            }

            if (txtNombreFamiliar.Text == "")
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

            if (cbxParentesco.Text == "")
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

            if (txtTelefonoFamiliar.Text == "(   )   -" && txtMovilFamiliar.Text == "   -   -")
            {
                lblValidacion10.Text = "* Introduce al menos un número teléfonico";
                lblValidacion10.Visible = true;
                lblValidacion11.Visible = false;
                bandera9 = 0;
            }
            else
            {
                if (txtTelefonoFamiliar.Text == "(   )   -")
                {
                    lblValidacion10.Visible = false;
                    bandera9 = 1;
                }
                else
                {
                    if (txtTelefonoFamiliar.MaskCompleted == true)
                    {
                        lblValidacion10.Visible = false;
                        bandera9 = 1;
                    }
                    else
                    {
                        lblValidacion10.Text = "* Complete este campo";
                        lblValidacion10.Visible = true;
                        bandera9 = 0;
                    }
                }

                if (txtMovilFamiliar.Text == "   -   -")
                {
                    lblValidacion11.Visible = false;
                    bandera9 = 1;
                }
                else
                {
                    if (txtMovilFamiliar.MaskCompleted == true)
                    {
                        lblValidacion11.Visible = false;
                        bandera9 = 1;
                    }
                    else
                    {
                        lblValidacion11.Text = "* Complete este campo";
                        lblValidacion11.Visible = true;
                        bandera9 = 0;
                    }
                }
            }

            if (txtDomicilio.Text == "")
            {
                lblValidacion12.Text = "* Complete este campo";
                lblValidacion12.Visible = true;
                bandera10 = 0;
            }
            else
            {
                lblValidacion12.Visible = false;
                bandera10 = 1;
            }

            if (txtReferenciasDomicilio.Text == "")
            {
                lblValidacion13.Text = "* Complete este campo";
                lblValidacion13.Visible = true;
                bandera11 = 0;
            }
            else
            {
                lblValidacion13.Visible = false;
                bandera11 = 1;
            }

            if (txtEntreCalles.Text == "")
            {
                lblValidacion14.Text = "* Complete este campo";
                lblValidacion14.Visible = true;
                bandera12 = 0;
            }
            else
            {
                lblValidacion14.Visible = false;
                bandera12 = 1;
            }

            if (txtCodigo.Text == "")
            {
                lblValidacion15.Text = "* Complete este campo";
                lblValidacion15.Visible = true;
                bandera13 = 0;
            }
            else
            {
                lblValidacion15.Visible = false;
                bandera13 = 1;
            }

            if (cbxEstado.Text == "")
            {
                lblValidacion16.Text = "* Complete este campo";
                lblValidacion16.Visible = true;
                bandera14 = 0;
            }
            else
            {
                lblValidacion16.Visible = false;
                bandera14 = 1;
            }

            if (cbxMunicipio.Text == "")
            {
                lblValidacion17.Text = "* Complete este campo";
                lblValidacion17.Visible = true;
                bandera14 = 0;
            }
            else
            {
                lblValidacion17.Visible = false;
                bandera14 = 1;
            }

            if (cbxLocalidad.Text == "")
            {
                lblValidacion18.Text = "* Complete este campo";
                lblValidacion18.Visible = true;
                bandera15 = 0;
            }
            else
            {
                lblValidacion18.Visible = false;
                bandera15 = 1;
            }

            if (cbxColonia.Text == "")
            {
                lblValidacion19.Text = "* Complete este campo";
                lblValidacion19.Visible = true;
                bandera16 = 0;
            }
            else
            {
                lblValidacion19.Visible = false;
                bandera16 = 1;
            }

            if (txtNombreTrabajo.Text == "")
            {
                lblValidacion20.Text = "* Complete este campo";
                lblValidacion20.Visible = true;
                bandera17 = 0;
            }
            else
            {
                lblValidacion20.Visible = false;
                bandera17 = 1;
            }

            if (txtDomicilioTrabajo.Text == "")
            {
                lblValidacion21.Text = "* Complete este campo";
                lblValidacion21.Visible = true;
                bandera18 = 0;
            }
            else
            {
                lblValidacion21.Visible = false;
                bandera18 = 1;
            }

            if (txtPuesto.Text == "")
            {
                lblValidacion22.Text = "* Complete este campo";
                lblValidacion22.Visible = true;
                bandera19 = 0;
            }
            else
            {
                lblValidacion22.Visible = false;
                bandera19 = 1;
            }

            if (txtTelefonoTrabajo.Text == "(   )   -")
            {
                lblValidacion23.Visible = false;
                bandera20 = 1;
            }
            else
            {
                if (txtTelefonoTrabajo.MaskCompleted == true)
                {
                    lblValidacion23.Visible = false;
                    bandera20 = 1;
                }
                else
                {
                    lblValidacion23.Text = "* Complete este campo";
                    lblValidacion23.Visible = true;
                    bandera20 = 0;
                }
            }

            if (bandera1 == 1 && bandera2 == 1 && bandera3 == 1 && bandera4 == 1 && bandera5 == 1 && bandera6 == 1 && bandera7 == 1 && bandera8 == 1
               && bandera9 == 1 && bandera10 == 1 && bandera11 == 1 && bandera12 == 1 && bandera13 == 1 && bandera14 == 1 && bandera15 == 1 && bandera16 == 1
               && bandera17 == 1 && bandera18 == 1 && bandera19 == 1 && bandera20 == 1)
            {
                //REALIZAR LAS ACTUALIZACIONES DE LOS DATOS DEL SOCIO//
                DialogResult mensaje = MessageBox.Show("¿Desea actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (mensaje == DialogResult.Yes)
                {
                    string telefono, movil, telefonotrabajo, extension, telefonofamiliar, movilfamiliar;

                    if (txtTelefono.MaskCompleted == true)
                    {
                        telefono = txtTelefono.Text;
                    }
                    else
                    {
                        telefono = "";
                    }

                    if (txtMovil.MaskCompleted == true)
                    {
                        movil = txtMovil.Text;
                    }
                    else
                    {
                        movil = "";
                    }

                    if (txtTelefonoTrabajo.MaskCompleted == true)
                    {
                        telefonotrabajo = txtTelefonoTrabajo.Text;
                        extension = txtExtension.Text;
                    }
                    else
                    {
                        telefonotrabajo = "";
                        extension = "";
                    }

                    if (txtTelefonoFamiliar.MaskCompleted == true)
                    {
                        telefonofamiliar = txtTelefonoFamiliar.Text;
                    }
                    else
                    {
                        telefonofamiliar = "";
                    }

                    if (txtMovilFamiliar.MaskCompleted == true)
                    {
                        movilfamiliar = txtMovilFamiliar.Text;
                    }
                    else
                    {
                        movilfamiliar = "";
                    }

                    //CONVERTIMOS A BYTES --> IDENTIFICACION DELANTERA
                    byte[] identificaciondel;
                    MemoryStream ms1 = new MemoryStream();
                    pbxIdentificacion1.Image.Save(ms1, ImageFormat.Jpeg);
                    identificaciondel = ms1.GetBuffer();

                    //CONVERTIMOS A BYTES --> IDENTIFICACION TRASERA
                    byte[] identificaciontra;
                    MemoryStream ms2 = new MemoryStream();
                    pbxIdentificacion2.Image.Save(ms2, ImageFormat.Jpeg);
                    identificaciontra = ms2.GetBuffer();

                    //CONVERTIMOS A BYTES --> COMPROBANTE DE DOMICILIO
                    byte[] comprobantedom;
                    MemoryStream ms3 = new MemoryStream();
                    pbxComprobante.Image.Save(ms3, ImageFormat.Jpeg);
                    comprobantedom = ms3.GetBuffer();

                    //CONVERTIMOS A BYTES --> COMPROBANTE DE DOMICILIO
                    byte[] fotoperfil;
                    MemoryStream ms4 = new MemoryStream();
                    pbxPerfil.Image.Save(ms4, ImageFormat.Jpeg);
                    fotoperfil = ms4.GetBuffer();

                    //AGREGAR FOTO DEL DOMICILIO --> EN CASO QUE SE HAYA TOMADO CAPTURA
                    byte[] domiciliofotocap = null;

                    if (pbxCapturaDomicilio.Image != null)
                    {
                        //CONVERTIMOS A BYTES --> CAPTURA DE DOMICILIO
                        MemoryStream ms5 = new MemoryStream();
                        pbxCapturaDomicilio.Image.Save(ms5, ImageFormat.Jpeg);
                        domiciliofotocap = ms5.GetBuffer();
                    }

                    //AGREGAR DATOS DEL SOCIO
                    socioscontroller.actualizaSocio(txtNombre.Text, txtApellidos.Text, Convert.ToDateTime(dtpFechanacimiento.Value.ToShortDateString()), cbxSexo.Text, cbxEstadoCivil.Text, txtDomicilio.Text, txtReferenciasDomicilio.Text, txtEntreCalles.Text, Convert.ToInt32(txtCodigo.Text), Convert.ToInt64(cbxColonia.SelectedValue.ToString()), Convert.ToInt64(cbxLocalidad.SelectedValue.ToString()), Convert.ToInt64(cbxMunicipio.SelectedValue.ToString()), Convert.ToInt64(cbxEstado.SelectedValue.ToString()), cbxTipoIngreso.Text, telefono, movil, txtCorreo.Text + "@" + cbxProveedor.Text, txtNombreTrabajo.Text, txtPuesto.Text, txtDomicilioTrabajo.Text, telefonotrabajo, extension, txtNombreFamiliar.Text, cbxParentesco.Text, telefonofamiliar, movilfamiliar, Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToInt64(txtClave.Text), domiciliofotocap, fotoperfil, identificaciondel, identificaciontra, comprobantedom);

                    MessageBox.Show("¡El registro ha sido actualizado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtClave.Enabled = true;
                    btnBuscar.Enabled = true;

                    cbxTipoIngreso.Enabled = true;
                    btnBuscarAVAL.Enabled = false;
                    txtClaveSocio.Enabled = false;
                    txtNombreAVAL.Enabled = false;
                    txtApellidosAVAL.Enabled = false;
                    txtDomicilioAVAL.Enabled = false;
                    txtTelefonoAVAL.Enabled = false;
                    txtMovilAVAL.Enabled = false;

                    txtApellidos.Clear();
                    txtApellidosAVAL.Clear();
                    txtClave.Clear();
                    txtClaveSocio.Clear();
                    txtCodigo.Clear();
                    txtCorreo.Clear();
                    txtDomicilio.Clear();
                    txtDomicilioAVAL.Clear();
                    txtDomicilioTrabajo.Clear();
                    txtEntreCalles.Clear();
                    txtExtension.Clear();
                    txtMovil.Clear();
                    txtMovilAVAL.Clear();
                    txtMovilFamiliar.Clear();
                    txtNombre.Clear();
                    txtNombreAVAL.Clear();
                    txtNombreFamiliar.Clear();
                    txtNombreTrabajo.Clear();
                    txtPuesto.Clear();
                    txtReferenciasDomicilio.Clear();
                    txtTelefono.Clear();
                    txtTelefonoAVAL.Clear();
                    txtTelefonoFamiliar.Clear();
                    txtTelefonoTrabajo.Clear();

                    cbxColonia.SelectedIndex = -1;
                    cbxEstado.SelectedIndex = -1;
                    cbxEstadoCivil.SelectedIndex = -1;
                    cbxLocalidad.SelectedIndex = -1;
                    cbxMunicipio.SelectedIndex = -1;
                    cbxParentesco.SelectedIndex = -1;
                    cbxProveedor.SelectedIndex = -1;
                    cbxSexo.SelectedIndex = -1;
                    cbxTipoIngreso.SelectedIndex = -1;

                    pbxCapturaDomicilio.Image = null;
                    pbxComprobante.Image = null;
                    pbxIdentificacion1.Image = null;
                    pbxIdentificacion2.Image = null;
                    pbxPerfil.Image = null;
                    pbxPerfilAVAL.Image = null;

                    foto = 0;
                    identificaciondelantera = 0;
                    identificaciontrasera = 0;
                    comprobante = 0;

                    dtpFechanacimiento.MaxDate = DateTime.Now;
                    dtpFechanacimiento.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    //INVISIBLE BOTON
                    btnCambiarNIP.Enabled = false;

                    btnActualizar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnGuardar.Enabled = true;

                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //BAJA DEL SOCIO DEL SISTEMA//
            DialogResult mensaje = MessageBox.Show("¿Desea eliminar al socio?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                if (mensaje == DialogResult.Yes)
                {
                    socioscontroller.eliminarSocio(Convert.ToInt64(txtClave.Text));
                    MessageBox.Show("¡El socio ha sido dado de baja del sistema!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //ACCIONES DESPUES DE ELIMINAR
                    txtApellidos.Clear();
                    txtApellidosAVAL.Clear();
                    txtMovil.Clear();
                    txtClave.Clear();
                    txtClaveSocio.Clear();
                    txtCodigo.Clear();
                    txtCorreo.Clear();
                    txtDomicilio.Clear();
                    txtDomicilioAVAL.Clear();
                    txtDomicilioTrabajo.Clear();
                    txtEntreCalles.Clear();
                    txtExtension.Clear();
                    txtMovilAVAL.Clear();
                    txtMovilFamiliar.Clear();
                    txtNombre.Clear();
                    txtNombreAVAL.Clear();
                    txtNombreFamiliar.Clear();
                    txtNombreTrabajo.Clear();
                    txtPuesto.Clear();
                    txtReferenciasDomicilio.Clear();
                    txtTelefono.Clear();
                    txtTelefonoAVAL.Clear();
                    txtTelefonoFamiliar.Clear();
                    txtTelefonoTrabajo.Clear();

                    cbxColonia.SelectedIndex = -1;
                    cbxEstado.SelectedIndex = -1;
                    cbxEstadoCivil.SelectedIndex = -1;
                    cbxLocalidad.SelectedIndex = -1;
                    cbxMunicipio.SelectedIndex = -1;
                    cbxParentesco.SelectedIndex = -1;
                    cbxProveedor.SelectedIndex = -1;
                    cbxSexo.SelectedIndex = -1;
                    cbxTipoIngreso.SelectedIndex = -1;

                    cbxTipoIngreso.Enabled = true;
                    btnBuscarAVAL.Enabled = false;
                    txtClaveSocio.Enabled = false;
                    txtNombreAVAL.Enabled = false;
                    txtApellidosAVAL.Enabled = false;
                    txtDomicilioAVAL.Enabled = false;
                    txtTelefonoAVAL.Enabled = false;
                    txtMovilAVAL.Enabled = false;

                    cbxColonia.Enabled = false;
                    cbxEstado.Enabled = true;
                    cbxLocalidad.Enabled = false;
                    cbxMunicipio.Enabled = false;

                    pbxCapturaDomicilio.Image = null;
                    pbxComprobante.Image = null;
                    pbxIdentificacion1.Image = null;
                    pbxIdentificacion2.Image = null;
                    pbxPerfil.Image = null;
                    pbxPerfilAVAL.Image = null;

                    foto = 0;
                    identificaciondelantera = 0;
                    identificaciontrasera = 0;
                    comprobante = 0;
                    avalboolean = false;

                    groupBox1.Enabled = true;
                    btnActualizar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnModificar.Enabled = false;
                    btnIngresar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;

                    dtpFechanacimiento.MaxDate = DateTime.Now;
                    dtpFechanacimiento.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    txtClave.Enabled = true;
                    btnBuscar.Enabled = true;

                    //INVISIBLE BOTON
                    btnCambiarNIP.Enabled = false;

                    txtClave.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtClaveSocio_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
                {
                    e.Handled = true;
                    return;
                }
                //long id_aval = 0;

                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (txtClaveSocio.Text != "")
                    {
                        asociados = socioscontroller.asociados(Convert.ToInt64(txtClaveSocio.Text));

                        if (asociados != null)
                        {
                            DialogResult mensaje = MessageBox.Show("¿Confirma la búsqueda?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (mensaje == DialogResult.Yes)
                            {
                                MessageBox.Show("¡Búsqueda exitosa!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //TOMAMOS DATOS//
                                txtNombreAVAL.Text = asociados.aso_nombre;
                                txtApellidosAVAL.Text = asociados.aso_apellidos;
                                txtDomicilioAVAL.Text = asociados.aso_domicilio;
                                string domicilio = socioscontroller.DatosDomicilio(asociados.aso_id);
                                txtDomicilioAVAL.Text += domicilio;
                                txtTelefonoAVAL.Text = asociados.aso_telefono.Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "");
                                txtMovilAVAL.Text = asociados.aso_movil.Replace("-", "").Replace(" ", "");

                                fotoasociados = socioscontroller.fotosasociados(Convert.ToInt64(txtClaveSocio.Text));
                                if (fotoasociados != null)
                                {
                                    System.IO.MemoryStream ms1 = new System.IO.MemoryStream(fotoasociados.fot_fotoperfil);
                                    pbxPerfilAVAL.Image = Image.FromStream(ms1);
                                }

                                txtNombreAVAL.Enabled = false;
                                txtApellidosAVAL.Enabled = false;
                                txtDomicilioAVAL.Enabled = false;
                                txtTelefonoAVAL.Enabled = false;
                                txtMovilAVAL.Enabled = false;
                                txtClaveSocio.Enabled = false;
                                btnBuscarAVAL.Enabled = false;

                                avalboolean = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("¡Socio no encontrado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            txtClaveSocio.Clear();
                            txtClaveSocio.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("¡Introduce la clave de socio!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtClaveSocio.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCancelar_Click(sender, e);
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnGuardar.Enabled == true)
            {
                btnGuardar_Click(sender, e);
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnActualizar.Enabled == true)
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

        private void ingresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnIngresar.Enabled == true)
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

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnBuscar.Enabled == true)
            {
                btnBuscar_Click(sender, e);
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
                foto = 0;
            }
        }

        private void cbxTipoIngreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTipoIngreso.SelectedIndex == 0)
            {
                txtClaveSocio.Enabled = true;
                btnBuscarAVAL.Enabled = true;

                txtNombreAVAL.Enabled = true;
                txtApellidosAVAL.Enabled = true;
                txtDomicilioAVAL.Enabled = true;
                txtTelefonoAVAL.Enabled = true;
                txtMovilAVAL.Enabled = true;

                avalboolean = false;
            }
            else if(cbxTipoIngreso.SelectedIndex == -1)
            {
                avalboolean = false;
            }
            else
            {
                txtClaveSocio.Enabled = false;
                btnBuscarAVAL.Enabled = false;

                txtNombreAVAL.Clear();
                txtApellidosAVAL.Clear();
                txtDomicilioAVAL.Clear();
                txtTelefonoAVAL.Clear();
                txtMovilAVAL.Clear();

                txtNombreAVAL.Enabled = false;
                txtApellidosAVAL.Enabled = false;
                txtDomicilioAVAL.Enabled = false;
                txtTelefonoAVAL.Enabled = false;
                txtMovilAVAL.Enabled = false;

                pbxPerfilAVAL.Image = null;

                avalboolean = true;
            }
        }

        private void btnCambiarNIP_Click(object sender, EventArgs e)
        {
            Socios_nip socios_nip = new Socios_nip();
            socios_nip.id = Convert.ToInt64(txtClave.Text);
            socios_nip.accion = 1;
            socios_nip.ShowDialog();
        }

        private void nIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnCambiarNIP.Enabled == true)
            {
                btnCambiarNIP_Click(sender, e);
            }
        }
    }
}