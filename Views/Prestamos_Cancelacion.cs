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
    public partial class Prestamos_Cancelacion : Form
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

        public Prestamos_Cancelacion()
        {
            InitializeComponent();
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

                            var consulta = prestamoscontroller.prestamos(long.Parse(txtClave.Text));

                            if(consulta.LongCount() > 0)
                            {
                                var resultado = from p in consulta
                                                select new
                                                {
                                                    p.pre_id,
                                                    prestamopedido = "$ " + p.pre_credito,
                                                    p.pre_cuotas,
                                                    p.pre_tipo,
                                                    p.pre_fechaprestamo
                                                };
                                //SOLO MOSTRARA LOS PRESTAMOS QUE TIENEN UN DIA DE HABERLOS HECHO O LOS DEL DIA DE HOY
                                dgvPrestamos.DataSource = resultado.Where(pre => pre.pre_fechaprestamo == Convert.ToDateTime(DateTime.Now.ToShortDateString()) || pre.pre_fechaprestamo == DateTime.Today.AddDays(-1)).ToList();

                                dgvPrestamos.Columns[0].HeaderText = "Contrato";
                                dgvPrestamos.Columns[1].HeaderText = "Crédito solicitado";
                                dgvPrestamos.Columns[2].HeaderText = "Cuotas";
                                dgvPrestamos.Columns[3].HeaderText = "Tipo";
                                dgvPrestamos.Columns[4].HeaderText = "Fecha de solicitud";

                                btnCancelar.Enabled = true;

                                panel2.Enabled = true;
                            }
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvPrestamos.CurrentRow == null)
                {
                    MessageBox.Show("¡No hay prestamos disponibles para seleccionar!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult mensaje = MessageBox.Show("¿Desea cancelar el prestamo?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        Prestamos_nip prestamosnip = new Prestamos_nip();
                        prestamosnip.ShowDialog();
                        string nip = prestamosnip.nip;

                        if (nip == null)
                        {
                            MessageBox.Show("¡No introdució la clave de autorización!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            autorizacion = prestamoscontroller.autorizacion(Convert.ToInt64(txtClave.Text), nip);

                            if (autorizacion != null)
                            {
                                prestamoscontroller.eliminarPagos(long.Parse(dgvPrestamos.CurrentRow.Cells[0].Value.ToString()));

                                prestamoscontroller.eliminarPrestamo(long.Parse(dgvPrestamos.CurrentRow.Cells[0].Value.ToString()));

                                MessageBox.Show("¡El prestamo ha sido cancelado exitosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                txtClave.Clear();
                                txtCorreo.Clear();
                                txtDomicilio.Clear();
                                txtMovil.Clear();
                                txtNombre.Clear();
                                txtTelefono.Clear();
                                pbxPerfil.Image = null;
                                dgvPrestamos.DataSource = null;

                                btnCancelar.Enabled = true;

                                panel2.Enabled = true;
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
    }
}
