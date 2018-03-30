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
    public partial class Prestamos : Form
    {
        //CONTROLADORES
        SociosController socioscontroller = new SociosController();
        PrestamosController prestamoscontroller = new PrestamosController();

        //ENTIDADES
        asociados asociados;
        fotosasociados fotosasociados;
        estados estados;
        municipios municipios;
        localidades localidades;
        colonias colonias;
        autorizacion autorizacion;

        //VARIABLES PARA CALCULO DEL PRESTAMO//
        decimal prestamo = 0;
        int cuotas = 0;
        int porcentaje = 0;
        decimal importe = 0;
        decimal balancefinal = 0;
        decimal interes = 0;
        decimal total = 0;
        int meses = 0;
        int semanas = 0;
        decimal pagointeres = 0;
        decimal cuotainteres = 0;
        public Prestamos()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                int bandera1 = 0, bandera2 = 0, bandera3 = 0;

                if(txtMonto.Text == "")
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

                if(txtCuotas.Text == "")
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

                if(cbxTipo.Text == "")
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

                if (bandera1 == 1 && bandera2 == 1 && bandera3 == 1)
                {
                    //LIMPIAR DGV
                    dgvCalculadora.Columns.Clear();
                    dgvCalculadora.Rows.Clear();

                    //AGREGAMOS COLUMNAS
                    DataGridViewTextBoxColumn numeroDGV = new DataGridViewTextBoxColumn();
                    numeroDGV.HeaderText = "No.";
                    dgvCalculadora.Columns.Add(numeroDGV);

                    DataGridViewTextBoxColumn creditoDGV = new DataGridViewTextBoxColumn();
                    creditoDGV.HeaderText = "Crédito";
                    dgvCalculadora.Columns.Add(creditoDGV);

                    DataGridViewTextBoxColumn importeDGV = new DataGridViewTextBoxColumn();
                    importeDGV.HeaderText = "Monto";
                    dgvCalculadora.Columns.Add(importeDGV);

                    DataGridViewTextBoxColumn interesDGV = new DataGridViewTextBoxColumn();
                    interesDGV.HeaderText = "Intéres";
                    dgvCalculadora.Columns.Add(interesDGV);

                    DataGridViewTextBoxColumn pagoDGV = new DataGridViewTextBoxColumn();
                    pagoDGV.HeaderText = "Pago";
                    dgvCalculadora.Columns.Add(pagoDGV);

                    DataGridViewTextBoxColumn fechaDGV = new DataGridViewTextBoxColumn();
                    fechaDGV.HeaderText = "Fecha";
                    dgvCalculadora.Columns.Add(fechaDGV);

                    prestamo = Convert.ToDecimal(Convert.ToDecimal(txtMonto.Text).ToString("N2"));
                    cuotas = Convert.ToInt32(txtCuotas.Text);
                    importe = prestamo / cuotas;
                    balancefinal = prestamo;
                    DateTime fecha = Convert.ToDateTime(dtpPrimer.Value.ToShortDateString());

                    if (cbxTipo.Text == "DIARIO")
                    {
                        semanas = (int) Math.Ceiling(cuotas / 7d);
                        meses = (int)Math.Ceiling(cuotas/30d);
                    }
                    else if (cbxTipo.Text == "SEMANAL")
                    {
                        semanas = cuotas;
                        meses = (int)Math.Ceiling(semanas / 4d);
                    }
                    else if (cbxTipo.Text == "QUINCENAL")
                    {
                        semanas = cuotas*2;
                        meses = (int)Math.Ceiling(semanas / 4d);
                    }
                    else
                    {
                        semanas = cuotas*4;
                        meses = (int)Math.Ceiling(semanas / 4d);
                    }

                    porcentaje = Convert.ToInt32(numInteres.Value.ToString());

                    interes = meses * porcentaje;

                    total = Math.Round(prestamo + (prestamo * interes / 100));

                    pagointeres = total - prestamo;

                    cuotainteres = pagointeres / cuotas;

                    for (int i = 0; i < cuotas; i++)
                    {

                        dgvCalculadora.Rows.Insert(i, Convert.ToInt32(i + 1), balancefinal.ToString("N2"), importe.ToString("N2"), cuotainteres.ToString("N2"), (importe + cuotainteres).ToString("N2"), fecha.ToShortDateString());

                        balancefinal -= importe;

                        //CALCULO DE LAS FECHAS DE LOS PAGOS//

                        if (cbxTipo.Text == "DIARIO")
                        {
                            fecha = fecha.AddDays(1);
                        }
                        else if (cbxTipo.Text == "SEMANAL")
                        {
                            fecha = fecha.AddDays(7);
                        }
                        else if (cbxTipo.Text == "QUINCENAL")
                        {
                            fecha = fecha.AddDays(15);
                        }
                        else
                        {
                            fecha = fecha.AddMonths(1);
                        }
                    }

                    lblPagofinal.Text = "$" + total.ToString("N2");
                    lblPagofinal.Visible = true;
                    lblSemanas.Text = semanas.ToString();
                    lblSemanas.Visible = true;
                    lblMeses.Text = meses.ToString();
                    lblMeses.Visible = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Prestamos_Load(object sender, EventArgs e)
        {
            ToolTip notificacion = new ToolTip();
            notificacion.AutoPopDelay = 8000;
            notificacion.InitialDelay = 1000;
            notificacion.ReshowDelay = 500;
            notificacion.ShowAlways = true;
            notificacion.SetToolTip(this.btnBuscar, "De clíc aquí o presione Ctrl + F2 para buscar un socio");
            notificacion.SetToolTip(this.btnCancelar, "De clíc aquí para cancelar la operación actual");
            notificacion.SetToolTip(this.btnCalcular, "De clíc aquí para calcular los pagos que realizará el socio del prestamo a solicitar");
            notificacion.SetToolTip(this.btnConfirmar, "De clíc aquí para confirmar el prestamo solicitado");
            notificacion.SetToolTip(this.btnContratos, "De clíc aquí para mostrar los contratos (prestamos) o tarjetones de pago del socio");
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }

        }

        private void txtCuotas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //VERIFICAMOS SI SE INTRODUCIENDO UN NUMERO O NO.
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (e.KeyChar == (Char)Keys.Enter)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                }

                //BUSQUEDA A PARTIR DE PRESION DE LA TECLA ENTER
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (txtClave.Text == "")
                    {
                        MessageBox.Show("Introduzca la clave del socio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtClave.Focus();
                    }
                    else
                    {
                        asociados = socioscontroller.asociados(Convert.ToInt64(txtClave.Text));

                        if (asociados != null)
                        {
                            txtNombre.Text = asociados.aso_nombre + " " + asociados.aso_apellidos;
                            txtTelefono.Text = asociados.aso_telefono;
                            txtMovil.Text = asociados.aso_movil;
                            txtCorreo.Text = asociados.aso_correoelectronico;

                            estados = socioscontroller.estados(asociados.aso_estado);

                            if (estados != null)
                            {
                                municipios = socioscontroller.municipios(asociados.aso_municipio);

                                if (municipios != null)
                                {
                                    localidades = socioscontroller.localidades(asociados.aso_localidad);

                                    if (localidades != null)
                                    {
                                        colonias = socioscontroller.colonias(asociados.aso_colonia);

                                        if (colonias != null)
                                        {
                                            txtDomicilio.Text = asociados.aso_domicilio + ", C.P.: " + asociados.aso_codigopostal.ToString() + ", Colonia: " + colonias.col_nombrecolonia + ", Localidad: " + localidades.loc_nombrelocalidad + ", Municipio: " + municipios.mun_nombremunicipio + ", Estado: " + estados.est_nombreestado;
                                        }
                                    }
                                }
                            }

                            fotosasociados = socioscontroller.fotosasociados(asociados.aso_id);

                            if (fotosasociados != null)
                            {
                                byte[] imagenBuffer = fotosasociados.fot_fotoperfil;
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

                                pbxPerfil.Image = Image.FromStream(ms);
                            }

                            cbxTipo.SelectedIndex = -1;
                            groupBox1.Enabled = false;
                            groupBox3.Enabled = true;
                            groupBox4.Enabled = true;
                            btnCalcular.Enabled = true;

                            btnConfirmar.Enabled = true;
                            btnCancelar.Enabled = true;
                            btnContratos.Enabled = true;

                            txtMonto.Focus();
                        }
                        else
                        {
                            MessageBox.Show("¡Socio no encontrado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClave.Clear();
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

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            if(txtClave.Enabled == true)
            {
                txtClave.Focus();
            }
            else
            {
                txtMonto.Focus();
            }
        }

        private void txtSexo_Enter(object sender, EventArgs e)
        {
            if (txtClave.Enabled == true)
            {
                txtClave.Focus();
            }
            else
            {
                txtMonto.Focus();
            }
        }

        private void txtFecha_Enter(object sender, EventArgs e)
        {
            if (txtClave.Enabled == true)
            {
                txtClave.Focus();
            }
            else
            {
                txtMonto.Focus();
            }
        }

        private void txtEstadocivil_Enter(object sender, EventArgs e)
        {
            if (txtClave.Enabled == true)
            {
                txtClave.Focus();
            }
            else
            {
                txtMonto.Focus();
            }
        }

        private void txtDomicilio_Enter(object sender, EventArgs e)
        {
            if (txtClave.Enabled == true)
            {
                txtClave.Focus();
            }
            else
            {
                txtMonto.Focus();
            }
        }

        private void txtTelefono_Enter(object sender, EventArgs e)
        {
            if (txtClave.Enabled == true)
            {
                txtClave.Focus();
            }
            else
            {
                txtMonto.Focus();
            }
        }

        private void txtMovil_Enter(object sender, EventArgs e)
        {
            if (txtClave.Enabled == true)
            {
                txtClave.Focus();
            }
            else
            {
                txtMonto.Focus();
            }
        }

        private void txtCorreo_Enter(object sender, EventArgs e)
        {
            if (txtClave.Enabled == true)
            {
                txtClave.Focus();
            }
            else
            {
                txtMonto.Focus();
            }
        }

        private void txtNombreAVAL_Enter(object sender, EventArgs e)
        {
            if (txtClave.Enabled == true)
            {
                txtClave.Focus();
            }
            else
            {
                txtMonto.Focus();
            }
        }

        private void btnContratos_Leave(object sender, EventArgs e)
        {
            txtMonto.Focus();
        }

        private void dgvCalculadora_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {

            try
            {
                if (dgvCalculadora.CurrentRow == null)
                {
                    MessageBox.Show("¡Falta por calcular sus cuotas a pagar por el prestamo!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    int bandera1 = 0, bandera2 = 0, bandera3 = 0;

                    if (txtMonto.Text == "")
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

                    if (txtCuotas.Text == "")
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

                    if (cbxTipo.Text == "")
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

                    if (bandera1 == 1 && bandera2 == 1 && bandera3 == 1)
                    {
                        Prestamos_nip prestamosnip = new Prestamos_nip();
                        prestamosnip.ShowDialog();
                        string nip = prestamosnip.nip;

                        if (nip == null)
                        {
                            btnConfirmar.Focus();
                        }
                        else
                        {
                            autorizacion = prestamoscontroller.autorizacion(Convert.ToInt64(txtClave.Text), nip);

                            if (autorizacion != null)
                            {
                                DialogResult mensaje = MessageBox.Show("¿Confirma la autorización del prestamo?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (mensaje == DialogResult.Yes)
                                {
                                    //SE AUTORIZA EL PRESTAMO//

                                    //ALTA DEL PRESTAMO
                                    long id = prestamoscontroller.agregarPrestamo(Convert.ToInt64(txtClave.Text), Convert.ToDecimal(txtMonto.Text), Convert.ToInt32(txtCuotas.Text), Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToInt32(numInteres.Value.ToString()), Convert.ToInt32(lblMeses.Text), Convert.ToDateTime(dtpPrimer.Value.ToShortDateString()), Convert.ToInt32(numInteres.Value.ToString()) * Convert.ToInt32(lblMeses.Text), txtObservaciones.Text, Convert.ToInt32(lblSemanas.Text), cbxTipo.Text, Convert.ToDecimal(lblPagofinal.Text.Replace("$", "").Replace(",", "")), decimal.Parse(lblPagofinal.Text.Replace("$", "").Replace(",", "")) - decimal.Parse(txtMonto.Text));

                                    //PAGOS DEL PRESTAMO - ALTA
                                    for (int i = 0; i < dgvCalculadora.RowCount; i++)
                                    {
                                        prestamoscontroller.agregarPago(decimal.Parse(dgvCalculadora.Rows[i].Cells[1].Value.ToString()), Convert.ToDateTime(dgvCalculadora.Rows[i].Cells[5].Value.ToString()), id, decimal.Parse(dgvCalculadora.Rows[i].Cells[4].Value.ToString()), decimal.Parse(dgvCalculadora.Rows[i].Cells[3].Value.ToString()), decimal.Parse(dgvCalculadora.Rows[i].Cells[2].Value.ToString()));
                                    }

                                    //AGREGAR A CONTABILIDAD EL PRESTAMO OTORGADO
                                    ContabilidadController contabilidadcontroller = new ContabilidadController();
                                    contabilidadcontroller.agregarContabilidad("Prestamo a socio No. " + asociados.aso_id.ToString() + " a nombre de: " + asociados.aso_nombre + " " + asociados.aso_apellidos, - Convert.ToDecimal(txtMonto.Text), 0, - Convert.ToDecimal(txtMonto.Text), DateTime.Now, "S");

                                    //MOSTRAMOS EL CONTRATO GENERADO
                                    Reportes.Rep_Prestamos rep_prestamos = new Reportes.Rep_Prestamos();
                                    rep_prestamos.idprestamo = id;
                                    rep_prestamos.ShowDialog();

                                    MessageBox.Show("¡El prestamo ha sido otorgado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    btnCalcular.Enabled = false;
                                    btnCancelar.Enabled = false;
                                    btnConfirmar.Enabled = false;
                                    btnContratos.Enabled = false;

                                    btnBuscar.Enabled = true;

                                    lblPagofinal.Visible = false;
                                    lblMeses.Visible = false;
                                    lblSemanas.Visible = false;

                                    groupBox1.Enabled = true;
                                    groupBox3.Enabled = false;
                                    groupBox4.Enabled = false;

                                    txtClave.Clear();
                                    txtCorreo.Clear();
                                    txtCuotas.Clear();
                                    txtDomicilio.Clear();
                                    txtMonto.Clear();
                                    txtMovil.Clear();
                                    txtNombre.Clear();
                                    txtTelefono.Clear();
                                    txtObservaciones.Clear();
                                    cbxTipo.SelectedIndex = -1;

                                    //LIMPIAR DGV
                                    dgvCalculadora.Columns.Clear();
                                    dgvCalculadora.Rows.Clear();

                                    numInteres.Value = 0;
                                    dtpPrimer.Value = DateTime.Now;

                                    txtClave.Enabled = true;
                                    txtClave.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("¡El NIP de autorización es incorrecto!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnContratos_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea mostrar los contratos de prestamo del socio?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(mensaje == DialogResult.Yes)
                {
                    Prestamos_nip prestamosnip = new Prestamos_nip();
                    prestamosnip.ShowDialog();
                    string nip = prestamosnip.nip;

                    if (nip == null)
                    {
                        btnConfirmar.Focus();
                    }
                    else
                    {
                        autorizacion = prestamoscontroller.autorizacion(Convert.ToInt64(txtClave.Text), nip);

                        if (autorizacion != null)
                        {
                            Prestamos_contratos prestamoscontratos = new Prestamos_contratos();
                            prestamoscontratos.id = Convert.ToInt32(txtClave.Text);
                            prestamoscontratos.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("¡El NIP de autorización es incorrecto!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtMonto);
        }

        //PARA FORMATEAR EL NUMERO
        public static void Moneda(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;

            try
            {
                n = txt.Text.Replace(",", "").Replace(".","");

                if (n.Equals(""))
                    n = "";
                n = n.PadLeft(3, '0');
                if(n.Length > 3 &  n.Substring(0,1) == "0")
                    n = n.Substring(1, n.Length - 1);
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;

            }
            catch (Exception)
            {

            }
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            Prestamos_Cancelacion prestamoscancelacion = new Prestamos_Cancelacion();
            prestamoscancelacion.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Socios_busqueda sociosbusqueda = new Socios_busqueda();
            sociosbusqueda.ShowDialog();
            txtClave.Text = sociosbusqueda.id;
            txtClave.Focus();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnBuscar.Enabled == true)
            {
                btnBuscar_Click(sender, e);
            }
        }

        private void confirmarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnConfirmar.Enabled == true)
            {
                btnConfirmar_Click(sender, e);
            }
        }

        private void contratosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnContratos.Enabled == true)
            {
                btnContratos_Click(sender, e);
            }
        }

        private void calcularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnCalcular.Enabled == true)
            {
                btnCalcular_Click(sender, e);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (mensaje == DialogResult.Yes)
            {
                btnCalcular.Enabled = false;
                btnConfirmar.Enabled = false;
                btnCancelar.Enabled = false;
                btnContratos.Enabled = false;

                lblPagofinal.Visible = false;
                lblMeses.Visible = false;
                lblSemanas.Visible = false;

                groupBox1.Enabled = true;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;

                txtClave.Clear();
                txtCorreo.Clear();
                txtCuotas.Clear();
                txtDomicilio.Clear();
                txtMonto.Clear();
                txtMovil.Clear();
                txtNombre.Clear();
                txtObservaciones.Clear();
                txtTelefono.Clear();
                cbxTipo.SelectedIndex = -1;

                //LIMPIAR DGV
                dgvCalculadora.Columns.Clear();
                dgvCalculadora.Rows.Clear();

                numInteres.Value = 0;
                dtpPrimer.Value = DateTime.Now;

                txtClave.Enabled = true;
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
    }
}
