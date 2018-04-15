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
    public partial class CajaCobro : Form
    {
        asociados asociados;
        fotosasociados foto_perfil;

        decimal pago_realizar = 0;

        SociosController socios_controller = new SociosController();
        CajaCobro_Controller cajacobro_controller = new CajaCobro_Controller();
        public CajaCobro()
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
                else if (e.KeyChar == 13)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = true;
                }

                if (e.KeyChar == 13)
                {
                    if (txtClave.Text == "")
                    {
                        MessageBox.Show("Introduzca la clave del socio", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        groupBox1.Enabled = true;
                        groupBox2.Enabled = false;
                        groupBox3.Enabled = false;
                        groupBox4.Enabled = false;
                        btnCancelar.Enabled = false;
                        btnPagar.Enabled = false;

                        dgvPagos.DataSource = null;

                        txtClave.Focus();
                    }
                    else
                    {
                        asociados = socios_controller.asociados(Convert.ToInt64(txtClave.Text));

                        if (asociados == null)
                        {
                            MessageBox.Show("¡Sin resultados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            groupBox1.Enabled = true;
                            groupBox2.Enabled = false;
                            groupBox3.Enabled = false;
                            groupBox4.Enabled = false;
                            btnCancelar.Enabled = false;
                            btnPagar.Enabled = false;

                            dgvPagos.DataSource = null;

                            txtClave.Clear();
                            txtClave.Focus();

                        }
                        else
                        {
                            groupBox1.Enabled = false;
                            groupBox2.Enabled = true;
                            btnCancelar.Enabled = true;



                            txtNombre.Text = asociados.aso_nombre + " " + asociados.aso_apellidos;
                            txtTelefono.Text = asociados.aso_telefono;
                            txtCelular.Text = asociados.aso_movil;
                            txtCorreo.Text = asociados.aso_correoelectronico;

                            //FOTO DE PERFIL DEL SOCIO//
                            foto_perfil = socios_controller.fotosasociados(asociados.aso_id);

                            if (foto_perfil != null)
                            {
                                byte[] imagenBuffer = foto_perfil.fot_fotoperfil;
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

                                pbxPerfil.Image = Image.FromStream(ms);
                            }

                            //VERIFICAR QUE NO ADEUDA PAGOS ATRASADOS
                            var pagos_atrasados_controller = cajacobro_controller.pagos_atrasados(Convert.ToInt64(txtClave.Text));

                            if (pagos_atrasados_controller == null || pagos_atrasados_controller.Count == 0)
                            {
                                var pagos_pendientes_controller = cajacobro_controller.pagos_socios(Convert.ToInt64(txtClave.Text));

                                //PAGOS PENDIENTES POR REALIZAR
                                if (pagos_pendientes_controller == null || pagos_pendientes_controller.Count == 0)
                                {
                                    groupBox3.Enabled = false;
                                    groupBox4.Enabled = false;
                                    btnPagar.Enabled = false;

                                    MessageBox.Show("¡Sin pagos pendientes por realizar!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                }
                                else
                                {
                                    groupBox3.Enabled = true;
                                    groupBox4.Enabled = true;
                                    btnPagar.Enabled = true;

                                    var pagos_data_grid_view = from pagos_dgv in pagos_pendientes_controller
                                                               select new
                                                               {
                                                                   pagos_dgv.pag_id,
                                                                   pagos_dgv.pag_credito,
                                                                   pagos_dgv.pag_importe,
                                                                   pagos_dgv.pag_interes,
                                                                   pagos_dgv.pag_fechapago,
                                                                   pagos_dgv.pag_pagado,
                                                                   pagos_dgv.pendiente,
                                                                   pagos_dgv.pag_fechapagado,
                                                               };

                                    dgvPagos.DataSource = pagos_data_grid_view.ToList();

                                    dgvPagos.Columns[0].Visible = false;
                                    dgvPagos.Columns[1].HeaderText = "Crédito";
                                    dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                                    dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                                    dgvPagos.Columns[4].HeaderText = "Fecha de pago";
                                    dgvPagos.Columns[5].HeaderText = "Total pagado";
                                    dgvPagos.Columns[6].HeaderText = "Total a pagar";
                                    dgvPagos.Columns[7].HeaderText = "Fecha de último pago";

                                    //foreach (DataGridViewRow fila in dgvPagos.Rows)
                                    //{
                                    //    int i = 0;

                                    //    pago_realizar += Convert.ToDecimal(dgvPagos.Rows[i].Cells[6].Value.ToString());

                                    //    lblPago.Text = "$ " + Math.Ceiling(pago_realizar).ToString();

                                    //    i++;
                                    //}

                                    //                    For Each fila As DataGridViewRow In dgvFacturas.Rows
                                    //If fila.Cells("Total").Value = 0 Then
                                    //    fila.DefaultCellStyle.BackColor = Color.Green
                                    //Else
                                    //    fila.DefaultCellStyle.BackColor = Color.Red
                                    //End If
                                    foreach (DataGridViewRow fila in dgvPagos.Rows)
                                    {

                                        int i = 0;

                                        string fecha_actual = dgvPagos.Rows[i].Cells[4].Value.ToString();

                                        if (Convert.ToDateTime(fecha_actual) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                                        {
                                            dgvPagos.Rows[i].Selected = true;
                                            dgvPagos.Rows[i].ReadOnly = true;
                                        }

                                        i++;
                                    }

                                    //dgvPagos.GridColor = Color.Red;

                                    dgvPagos.ClearSelection();
                                }
                            }
                            else
                            {
                                groupBox3.Enabled = true;
                                groupBox4.Enabled = true;
                                btnPagar.Enabled = true;

                                var pagos_data_grid_view = from pagos_dgv in pagos_atrasados_controller
                                                           select new
                                                           {
                                                               pagos_dgv.pag_id,
                                                               pagos_dgv.pag_credito,
                                                               pagos_dgv.pag_importe,
                                                               pagos_dgv.pag_interes,
                                                               pagos_dgv.pag_fechapago,
                                                               pagos_dgv.pag_pagado,
                                                               pagos_dgv.pendiente,
                                                               pagos_dgv.pag_fechapagado,
                                                           };

                                dgvPagos.DataSource = pagos_data_grid_view.ToList();

                                dgvPagos.Columns[0].Visible = false;
                                dgvPagos.Columns[1].HeaderText = "Crédito";
                                dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                                dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                                dgvPagos.Columns[4].HeaderText = "Fecha de pago";
                                dgvPagos.Columns[5].HeaderText = "Total pagado";
                                dgvPagos.Columns[6].HeaderText = "Total a pagar";
                                dgvPagos.Columns[7].HeaderText = "Fecha de último pago";

                                //dgvPagos.GridColor = Color.Red;

                                foreach (DataGridViewRow fila in dgvPagos.Rows)
                                {

                                    int i = 0;

                                    string fecha_actual = dgvPagos.Rows[i].Cells[4].Value.ToString();

                                    if (Convert.ToDateTime(fecha_actual) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                                    {
                                        dgvPagos.Rows[i].Selected = true;
                                        dgvPagos.Rows[i].ReadOnly = true;
                                        //dgvPagos.Rows[i].ReadOnly = false;
                                    }

                                    i++;
                                }

                                dgvPagos.ClearSelection();
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

        private void dgvPagos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int seleccionadas = dgvPagos.Rows.GetRowCount(DataGridViewElementStates.Selected);

                if (seleccionadas > 0)
                {
                    pago_realizar = 0;

                    for (int i = 0; i < seleccionadas; i++)
                    {


                        pago_realizar += Convert.ToDecimal(dgvPagos.SelectedRows[i].Cells[6].Value.ToString());

                        lblPago.Text = "$ " + Math.Ceiling(Math.Round(pago_realizar, 2)).ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
