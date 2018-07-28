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
    public partial class Caja : Form
    {
        long contar = 0;
        //ENTIDADES
        asociados asociados;
        fotosasociados fotosasociados;

        //CONTROLADORES
        SociosController socioscontroller = new SociosController();
        CajaController cajacontroller = new CajaController();
        public Caja()
        {
            InitializeComponent();
        }

        private void Caja_Load(object sender, EventArgs e)
        {
            ToolTip notificacion = new ToolTip();
            notificacion.AutoPopDelay = 8000;
            notificacion.InitialDelay = 1000;
            notificacion.ReshowDelay = 500;
            notificacion.ShowAlways = true;
            notificacion.SetToolTip(this.btnBuscar, "De clíc aquí o presione Ctrl + F2 para buscar un socio");
            notificacion.SetToolTip(this.btnPagar, "De clíc aquí para realizar el pago (presione F4) o si desea cancelar la operación actual presione F3");
            //notificacion.SetToolTip(this.button1, "De clíc aquí para abrir el Menú de búsqueda de número de operaciones");
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

                        groupBox3.Enabled = false;
                        groupBox3.Enabled = true;

                        dgvPagos.DataSource = null;
                    }
                    else
                    {
                        asociados = socioscontroller.asociados(Convert.ToInt64(txtClave.Text));

                        if (asociados != null)
                        {
                            txtNombre.Text = asociados.aso_nombre + " " + asociados.aso_apellidos;
                            txtTelefono.Text = asociados.aso_telefono;
                            txtCelular.Text = asociados.aso_movil;
                            txtCorreo.Text = asociados.aso_correoelectronico;

                            fotosasociados = socioscontroller.fotosasociados(asociados.aso_id);

                            if (fotosasociados != null)
                            {
                                byte[] imagenBuffer = fotosasociados.fot_fotoperfil;
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

                                pbxPerfil.Image = Image.FromStream(ms);
                            }

                            groupBox3.Enabled = true;
                            groupBox1.Enabled = false;

                            txtPagado.Enabled = true;
                            txtRecibido.Enabled = true;
                            btnPagar.Enabled = true;

                            checkBox1.Checked = false;
                            checkBox3.Checked = false;
                            checkBox2.Checked = true;
                        }
                        else
                        {
                            MessageBox.Show("¡Socio no encontrado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtClave.Clear();
                            txtClave.Focus();

                            groupBox3.Enabled = false;
                            groupBox1.Enabled = true;

                            txtPagado.Enabled = false;
                            txtClave.Enabled = true;

                            checkBox1.Checked = false;
                            checkBox3.Checked = false;
                            checkBox2.Checked = false;

                            txtRecibido.Enabled = false;
                            txtPagado.Enabled = false;
                            btnPagar.Enabled = false;

                            dgvPagos.DataSource = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (groupBox3.Enabled == true)
                {
                    if (checkBox1.Checked == true)
                    {
                        checkBox1.Checked = true;
                        checkBox2.Checked = false;
                        checkBox3.Checked = false;

                        var pagos = from pa in cajacontroller.pagosDGV(long.Parse(txtClave.Text))
                                    select new
                                    {
                                        pa.pag_id,
                                        pa.pag_credito,
                                        pa.pag_importe,
                                        pa.pag_interes,
                                        pa.pag_fechapago,
                                        pa.pag_pagado,
                                        pa.C_p_pag_total___p_pag_pagado_,
                                        pa.pag_fechapagado,
                                    };

                        dgvPagos.DataSource = pagos.ToList();

                        dgvPagos.Columns[0].Visible = false;
                        dgvPagos.Columns[1].HeaderText = "Crédito";
                        dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                        dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                        dgvPagos.Columns[4].HeaderText = "Fecha de pago";
                        dgvPagos.Columns[5].HeaderText = "Total pagado";
                        dgvPagos.Columns[6].HeaderText = "Total a pagar";
                        dgvPagos.Columns[7].HeaderText = "Fecha";

                        decimal total = 0;

                        for (int i = 0; i < dgvPagos.RowCount; i++)
                        {
                            total += Convert.ToDecimal(dgvPagos.Rows[i].Cells[6].Value.ToString());
                        }

                        lblTotal.Text = "$ " + total.ToString("N2");
                        contar = 0;
                    }
                    else
                    {
                        checkBox2.Checked = true;

                    }
                }
                else
                {
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (groupBox3.Enabled == true)
                {
                    if (checkBox2.Checked == true)
                    {
                        checkBox1.Checked = false;
                        checkBox2.Checked = true;

                        if (checkBox3.Checked == true)
                        {

                            var pagos = from pa in cajacontroller.pagosDGV(long.Parse(txtClave.Text))
                                        select new
                                        {
                                            pa.pag_id,
                                            pa.pag_credito,
                                            pa.pag_importe,
                                            pa.pag_interes,
                                            pa.pag_fechapago,
                                            pa.pag_pagado,
                                            pa.C_p_pag_total___p_pag_pagado_,
                                            pa.pag_fechapagado,
                                        };

                            dgvPagos.DataSource = pagos.Where(p => p.C_p_pag_total___p_pag_pagado_ > 0).ToList();

                            dgvPagos.Columns[0].Visible = false;
                            dgvPagos.Columns[1].HeaderText = "Crédito";
                            dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                            dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                            dgvPagos.Columns[4].HeaderText = "Fecha de pago";
                            dgvPagos.Columns[5].HeaderText = "Total pagado";
                            dgvPagos.Columns[6].HeaderText = "Total a pagar";
                            dgvPagos.Columns[7].HeaderText = "Fecha";

                            decimal total = 0;

                            for (int i = 0; i < dgvPagos.RowCount; i++)
                            {
                                total += Convert.ToDecimal(dgvPagos.Rows[i].Cells[6].Value.ToString());
                            }

                            lblTotal.Text = "$ " + total.ToString("N2");

                            contar = pagos.Where(p => p.C_p_pag_total___p_pag_pagado_ > 0).LongCount();
                        }
                        else
                        {
                            var pagos = from pa in cajacontroller.pagosDGV(long.Parse(txtClave.Text))
                                        select new
                                        {
                                            pa.pag_id,
                                            pa.pag_credito,
                                            pa.pag_importe,
                                            pa.pag_interes,
                                            pa.pag_fechapago,
                                            pa.pag_pagado,
                                            pa.C_p_pag_total___p_pag_pagado_,
                                            pa.pag_fechapagado,
                                        };

                            dgvPagos.DataSource = pagos.Where(p => p.C_p_pag_total___p_pag_pagado_ > 0 && p.pag_fechapago <= Convert.ToDateTime(DateTime.Now.ToShortDateString())).ToList();

                            dgvPagos.Columns[0].Visible = false;
                            dgvPagos.Columns[1].HeaderText = "Crédito";
                            dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                            dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                            dgvPagos.Columns[4].HeaderText = "Fecha de pago";
                            dgvPagos.Columns[5].HeaderText = "Total pagado";
                            dgvPagos.Columns[6].HeaderText = "Total a pagar";
                            dgvPagos.Columns[7].HeaderText = "Fecha";

                            decimal total = 0;

                            for (int i = 0; i < dgvPagos.RowCount; i++)
                            {
                                total += Convert.ToDecimal(dgvPagos.Rows[i].Cells[6].Value.ToString());
                            }

                            lblTotal.Text = "$ " + total.ToString("N2");

                            contar = pagos.Where(p => p.C_p_pag_total___p_pag_pagado_ > 0 && p.pag_fechapago <= Convert.ToDateTime(DateTime.Now.ToShortDateString())).LongCount();
                        }
                    }
                    else
                    {
                        checkBox2.Checked = false;
                        checkBox3.Checked = false;
                        checkBox1.Checked = true;

                        contar = 0;
                    }
                }
                else
                {
                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            try
            {
                checkBox2_CheckedChanged(sender, e);

                if (contar > 0)
                {
                    if(txtRecibido.Text == "" || decimal.Parse(txtRecibido.Text) <= 0)
                    {
                        MessageBox.Show("¡Introduzca una cantidad de dinero recibido válida!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (txtPagado.Text == "" || decimal.Parse(txtPagado.Text) <= 0)
                        {
                            MessageBox.Show("¡Introduzca una cantidad de pago a realizar válida!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if(decimal.Parse(txtRecibido.Text) < decimal.Parse(txtPagado.Text))
                            {
                                MessageBox.Show("¡Introduce una cantidad a pagar igual o menor a la cantidad de dinero recibido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                DialogResult mensaje = MessageBox.Show("¿Confirma el pago?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                long id_operacion = 0;
                                if (mensaje == DialogResult.Yes)
                                {
                                    decimal pagado = decimal.Parse(txtPagado.Text);

                                    id_operacion = cajacontroller.agregarOperacion(Convert.ToDateTime(DateTime.Now.ToShortDateString()), Convert.ToDateTime(DateTime.Now.ToShortTimeString()));

                                    for (int i = 0; i < dgvPagos.RowCount; i++)
                                    {
                                        decimal valor_monto = decimal.Parse(dgvPagos.Rows[i].Cells[6].Value.ToString());

                                        cajacontroller.agregarTransaccion(Convert.ToDateTime(DateTime.Now.ToShortDateString()), pagado, id_operacion, Convert.ToInt64(dgvPagos.Rows[i].Cells[0].Value.ToString()));

                                        if (pagado <= decimal.Parse(dgvPagos.Rows[i].Cells[6].Value.ToString()))
                                        {
                                            cajacontroller.agregarpago(Convert.ToInt64(dgvPagos.Rows[i].Cells[0].Value.ToString()), pagado);

                                            break;
                                        }
                                        else
                                        {
                                            cajacontroller.agregarpago(Convert.ToInt64(dgvPagos.Rows[i].Cells[0].Value.ToString()), Convert.ToDecimal(dgvPagos.Rows[i].Cells[6].Value.ToString()));
                                        }

                                        pagado = pagado - decimal.Parse(dgvPagos.Rows[i].Cells[6].Value.ToString());
                                    }

                                    cajacontroller.agregarContabilidad(decimal.Parse(txtPagado.Text), 0, "PAGO DE PRESTAMO DEL SOCIO No. " + txtClave.Text + " a nombre de: " + txtNombre.Text);

                                    decimal recibido = decimal.Parse(txtRecibido.Text);
                                    decimal pagado2 = decimal.Parse(txtPagado.Text);
                                    decimal cambio = recibido - pagado2;

                                    if(cambio > 0)
                                    {
                                        MessageBox.Show("¡El pago ha sido realizado con éxito, entregue CAMBIO de: $" +
                                            cambio.ToString() +", No. de operación: " + id_operacion.ToString() + "!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("¡El pago ha sido realizado con éxito No. de operación: " + id_operacion.ToString() +"!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                    Reportes.Ticket ticket_pago = new Reportes.Ticket();
                                    ticket_pago.id = id_operacion;
                                    ticket_pago.ShowDialog();

                                    dgvPagos.DataSource = null;

                                    groupBox3.Enabled = false;
                                    groupBox1.Enabled = true;

                                    btnPagar.Enabled = false;


                                    checkBox1.Checked = false;
                                    checkBox2.Checked = false;
                                    checkBox3.Checked = false;

                                    txtCelular.Clear();
                                    txtTelefono.Clear();
                                    txtNombre.Clear();
                                    txtCorreo.Clear();
                                    pbxPerfil.Image = null;
                                    txtClave.Clear();
                                    lblTotal.Text = "";
                                    txtPagado.Clear();
                                    txtPagado.Enabled = false;
                                    txtRecibido.Clear();
                                    txtRecibido.Enabled = false;
                                    txtClave.Focus();
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¡No hay pagos por realizar!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox3.Checked == true)
                {
                    checkBox2.Checked = true;
                    checkBox1.Checked = false;
                    checkBox3.Checked = true;

                    var pagos = from pa in cajacontroller.pagosDGV(long.Parse(txtClave.Text))
                                select new
                                {
                                    pa.pag_id,
                                    pa.pag_credito,
                                    pa.pag_importe,
                                    pa.pag_interes,
                                    pa.pag_fechapago,
                                    pa.pag_pagado,
                                    pa.C_p_pag_total___p_pag_pagado_,
                                    pa.pag_fechapagado,
                                };

                    dgvPagos.DataSource = pagos.Where(p => p.C_p_pag_total___p_pag_pagado_ > 0).ToList();

                    dgvPagos.Columns[0].Visible = false;
                    dgvPagos.Columns[1].HeaderText = "Crédito";
                    dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                    dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                    dgvPagos.Columns[4].HeaderText = "Fecha de pago";
                    dgvPagos.Columns[5].HeaderText = "Total pagado";
                    dgvPagos.Columns[6].HeaderText = "Total a pagar";
                    dgvPagos.Columns[7].HeaderText = "Fecha";

                    decimal total = 0;

                    for (int i = 0; i < dgvPagos.RowCount; i++)
                    {
                        total += Convert.ToDecimal(dgvPagos.Rows[i].Cells[6].Value.ToString());
                    }

                    lblTotal.Text = "$ " + total.ToString("N2");
                }
                else
                {
                    checkBox2.Checked = true;
                    checkBox1.Checked = false;

                    var pagos = from pa in cajacontroller.pagosDGV(long.Parse(txtClave.Text))
                                select new
                                {
                                    pa.pag_id,
                                    pa.pag_credito,
                                    pa.pag_importe,
                                    pa.pag_interes,
                                    pa.pag_fechapago,
                                    pa.pag_pagado,
                                    pa.C_p_pag_total___p_pag_pagado_,
                                    pa.pag_fechapagado,
                                };

                    dgvPagos.DataSource = pagos.Where(p => p.C_p_pag_total___p_pag_pagado_ > 0 && p.pag_fechapago <= Convert.ToDateTime(DateTime.Now.ToShortDateString())).ToList();

                    dgvPagos.Columns[0].Visible = false;
                    dgvPagos.Columns[1].HeaderText = "Crédito";
                    dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                    dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                    dgvPagos.Columns[4].HeaderText = "Fecha de pago";
                    dgvPagos.Columns[5].HeaderText = "Total pagado";
                    dgvPagos.Columns[6].HeaderText = "Total a pagar";
                    dgvPagos.Columns[7].HeaderText = "Fecha";

                    decimal total = 0;

                    for (int i = 0; i < dgvPagos.RowCount; i++)
                    {
                        total += Convert.ToDecimal(dgvPagos.Rows[i].Cells[6].Value.ToString());
                    }

                    lblTotal.Text = "$ " + total.ToString("N2");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPagado_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtPagado);
        }

        //PARA FORMATEAR EL NUMERO
        public static void Moneda(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;

            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");

                if (n.Equals(""))
                    n = "";
                n = n.PadLeft(3, '0');
                if (n.Length > 3 & n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length - 1);
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;

            }
            catch (Exception)
            {

            }
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
            if(btnBuscar.Enabled == true)
            {
                btnBuscar_Click(sender, e);
            }
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (groupBox3.Enabled == true)
            {
                DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (mensaje == DialogResult.Yes)
                {
                    dgvPagos.DataSource = null;

                    groupBox3.Enabled = false;
                    groupBox1.Enabled = true;

                    txtCelular.Clear();
                    txtTelefono.Clear();
                    txtNombre.Clear();
                    txtCorreo.Clear();
                    pbxPerfil.Image = null;
                    txtClave.Clear();
                    lblTotal.Text = "";
                    txtPagado.Clear();
                    txtPagado.Enabled = false;
                    txtRecibido.Clear();
                    txtRecibido.Enabled = false;

                    checkBox1.Checked = false;
                    checkBox2.Checked = false;
                    checkBox3.Checked = false;

                    btnPagar.Enabled = false;

                    txtClave.Focus();
                }
            }
        }

        private void txtRecibido_TextChanged(object sender, EventArgs e)
        {
            Moneda(ref txtRecibido);
        }
        private void txtRecibido_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPagado_KeyPress(object sender, KeyPressEventArgs e)
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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btnPagar.Enabled == true)
            {
                btnPagar_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Operaciones operaciones = new Operaciones();
            operaciones.ShowDialog();
        }

        private void operacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    if(button1.Enabled == true)
        //    {
        //        button1_Click(sender, e);
        //    }
        }
    }
}
