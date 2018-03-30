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

namespace Views
{
    public partial class Estadisticas : Form
    {
        //CONTROLADORES
        PrestamosController prestamoscontroller = new PrestamosController();
        EstadisticasController estadisticascontroller = new EstadisticasController();
        Rep_DeudoresController deudorescontroller = new Rep_DeudoresController();
        public Estadisticas()
        {
            InitializeComponent();
        }

        private void btnDeudores1_Click(object sender, EventArgs e)
        {
            Reportes.Rep_DeudoresDatos repdeudoresdatos = new Reportes.Rep_DeudoresDatos();
            repdeudoresdatos.ShowDialog();
        }

        private void btnDeudores3_Click(object sender, EventArgs e)
        {
            Reportes.Rep_Pagospendientes reppagospendientes = new Reportes.Rep_Pagospendientes();
            reppagospendientes.ShowDialog();
        }

        private void btnDeudores4_Click(object sender, EventArgs e)
        {
            Reportes.Rep_Totalapagar reptotalapagar = new Reportes.Rep_Totalapagar();
            reptotalapagar.ShowDialog();
        }

        private void btnDeudores2_Click(object sender, EventArgs e)
        {
            Reportes.Rep_Prestamosporpagar repprestamosporpagar = new Reportes.Rep_Prestamosporpagar();
            repprestamosporpagar.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //BUSQUEDA DE TODOS LOS PRESTAMOS
                if (radioButton1.Checked == true)
                {
                    var consulta = prestamoscontroller.prestamoscontrol();

                    var listado = from p in consulta
                                  select new
                                  {
                                      p.aso_id,
                                      p.aso_nombre,
                                      p.pre_id,
                                      credito = "$ " + p.pre_credito,
                                      p.pre_cuotas,
                                      p.pre_tipo,
                                      p.pre_fechasolicitud
                                  };

                    dgvPrestamos.DataSource = listado.OrderBy(f => f.pre_fechasolicitud).ToList();

                    dgvPrestamos.Columns[0].HeaderText = "Clave";
                    dgvPrestamos.Columns[1].HeaderText = "Nombre";
                    dgvPrestamos.Columns[2].HeaderText = "No. de contrato";
                    dgvPrestamos.Columns[3].HeaderText = "Crédito solicitado";
                    dgvPrestamos.Columns[4].HeaderText = "Coutas a pagar";
                    dgvPrestamos.Columns[5].HeaderText = "Tipo de prestamo";
                    dgvPrestamos.Columns[6].HeaderText = "Fecha de solicitud";
                }
                else //PRESTAMOS BUSCADOS MEDIANTE UN RANGO DE FECHA
                {
                    var consulta = prestamoscontroller.prestamosPeriodo(Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString()), Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString()));

                    var listado = from p in consulta
                                  select new
                                  {
                                      p.aso_id,
                                      p.aso_nombre,
                                      p.pre_id,
                                      credito = "$ " + p.pre_credito,
                                      p.pre_cuotas,
                                      p.pre_tipo,
                                      p.pre_fechasolicitud
                                  };

                    dgvPrestamos.DataSource = listado.OrderBy(f => f.pre_fechasolicitud).ToList();

                    dgvPrestamos.Columns[0].HeaderText = "Clave";
                    dgvPrestamos.Columns[1].HeaderText = "Nombre";
                    dgvPrestamos.Columns[2].HeaderText = "No. de contrato";
                    dgvPrestamos.Columns[3].HeaderText = "Crédito solicitado";
                    dgvPrestamos.Columns[4].HeaderText = "Coutas a pagar";
                    dgvPrestamos.Columns[5].HeaderText = "Tipo de prestamo";
                    dgvPrestamos.Columns[6].HeaderText = "Fecha de solicitud de prestamo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //en caso dado de que este seleccionado el control radiobutton se deshabilita el control groupbox
            if (radioButton1.Checked == true)
            {
                groupBox3.Enabled = false;
            }
            else //se habilita el control en caso que no este seleccionado el control
            {
                groupBox3.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //se habilita el control en caso que no este seleccionado el control
            if (radioButton2.Checked == true)
            {
                groupBox3.Enabled = true;
            }
            else //en caso dado de que este seleccionado el control radiobutton se deshabilita el control groupbox
            {
                groupBox3.Enabled = false;
            }
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(tabControl1.SelectedIndex == 0) ////REPORTE GNERAL DE PRESTAMOS
                {
                    //HABILITAMOS LA SELECCION DEL CONTROL RADIOBUTTON
                    radioButton1.Checked = true; //seleccionamos el elemento radiobutton1
                    groupBox3.Enabled = false; //deshabilitamos el control groupbox3

                    //BUSQUEDA DE TODOS LOS PRESTAMOS
                    if (radioButton1.Checked == true)
                    {
                        var consulta = prestamoscontroller.prestamoscontrol();

                        var listado = from p in consulta
                                      select new
                                      {
                                          p.aso_id,
                                          p.aso_nombre,
                                          p.pre_id,
                                          credito = "$ " + p.pre_credito,
                                          p.pre_cuotas,
                                          p.pre_tipo,
                                          p.pre_fechasolicitud
                                      };

                        dgvPrestamos.DataSource = listado.OrderBy(f => f.pre_fechasolicitud).ToList();

                        dgvPrestamos.Columns[0].HeaderText = "Clave(socio)";
                        dgvPrestamos.Columns[1].HeaderText = "Nombre del socio";
                        dgvPrestamos.Columns[2].HeaderText = "No. de contrato";
                        dgvPrestamos.Columns[3].HeaderText = "Crédito solicitado";
                        dgvPrestamos.Columns[4].HeaderText = "Coutas a pagar";
                        dgvPrestamos.Columns[5].HeaderText = "Tipo de prestamo";
                        dgvPrestamos.Columns[6].HeaderText = "Fecha de solicitud de prestamo";
                    }
                }
                else if(tabControl1.SelectedIndex == 1) //REPORTE GENERAL DE PAGOS REALIZADOS
                {
                    radioButton4.Checked = true; //seleccionamos el elemento radiobutton1

                    var consulta = estadisticascontroller.reportePagosGeneral();

                    var result = from pa in consulta
                                 select new
                                 {
                                     pa.aso_id,
                                     pa.aso_nombre,
                                     pa1 = "$ " + pa.pag_importe,
                                     pa2 = "$ " + pa.pag_interes,
                                     pa3 = "$ " + pa.pag_total,
                                     pa.pag_fechapago,
                                     pa4 = "$" + pa.pag_porpagar,
                                     pa.pag_fechapagado
                                 };

                    dgvPagos.DataSource = result.OrderBy(f => f.pag_fechapagado).ToList();
                    dgvPagos.Columns[0].HeaderText = "Clave(socio)";
                    dgvPagos.Columns[1].HeaderText = "Nombre del socio";
                    dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                    dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                    dgvPagos.Columns[4].HeaderText = "Total a pagar";
                    dgvPagos.Columns[5].HeaderText = "Fecha de pago";
                    dgvPagos.Columns[6].HeaderText = "Total (falta por pagar)";
                    dgvPagos.Columns[7].HeaderText = "Fecha (último pago)";
                }
                else if(tabControl1.SelectedIndex == 2) //REPORTE GENERAL DE TRANSACCIONES
                {
                    radioButton6.Checked = true; //seleccionamos el elemento radiobutton1

                    var consulta = estadisticascontroller.reporteTransacciones();

                    var result = from tra in consulta
                                 select new
                                 {
                                     tra.aso_id,
                                     tra.aso_nombre,
                                     tra1 = "$ " + tra.his_subtotal,
                                     tra2 = "$ " + tra.his_interes,
                                     tra3 = "$ " + tra.his_total,
                                     tra.his_fecha
                                 };

                    dgvTransacciones.DataSource = result.OrderBy(f => f.his_fecha).ToList();
                    dgvTransacciones.Columns[0].HeaderText = "Clave(socio)";
                    dgvTransacciones.Columns[1].HeaderText = "Nombre del socio";
                    dgvTransacciones.Columns[2].HeaderText = "Subtotal";
                    dgvTransacciones.Columns[3].HeaderText = "Intéres";
                    dgvTransacciones.Columns[4].HeaderText = "Total";
                    dgvTransacciones.Columns[5].HeaderText = "Fecha";
                }
                else //REPORTE GENERAL DE DEUDORES
                {
                    cbxOpcion.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            try
            {
                if(radioButton4.Checked == true) //CUANDO ES SELECCIONADO TODOS
                {
                    var consulta = estadisticascontroller.reportePagosGeneral();

                    var result = from pa in consulta
                                 select new
                                 {
                                     pa.aso_id,
                                     pa.aso_nombre,
                                     pa1 = "$ " + pa.pag_importe,
                                     pa2 = "$ " + pa.pag_interes,
                                     pa3 = "$ " + pa.pag_total,
                                     pa.pag_fechapago,
                                     pa4 = "$" + pa.pag_porpagar,
                                     pa.pag_fechapagado
                                 };

                    dgvPagos.DataSource = result.OrderBy(f => f.pag_fechapagado).ToList();
                    dgvPagos.Columns[0].HeaderText = "Clave(socio)";
                    dgvPagos.Columns[1].HeaderText = "Nombre del socio";
                    dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                    dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                    dgvPagos.Columns[4].HeaderText = "Total a pagar";
                    dgvPagos.Columns[5].HeaderText = "Fecha de pago";
                    dgvPagos.Columns[6].HeaderText = "Total (falta por pagar)";
                    dgvPagos.Columns[7].HeaderText = "Fecha (último pago)";
                    
                }
                else //POR PERIODO
                {
                    var consulta = estadisticascontroller.reportePagosGeneralPeriodo(Convert.ToDateTime(dateTimePicker4.Value.ToShortDateString()), Convert.ToDateTime(dateTimePicker3.Value.ToShortDateString()));

                    var result = from pa in consulta
                                 select new
                                 {
                                     pa.aso_id,
                                     pa.aso_nombre,
                                     pa1 = "$ " + pa.pag_importe,
                                     pa2 = "$ " + pa.pag_interes,
                                     pa3 = "$ " + pa.pag_total,
                                     pa.pag_fechapago,
                                     pa4 = "$" + pa.pag_porpagar,
                                     pa.pag_fechapagado
                                 };

                    dgvPagos.DataSource = result.OrderBy(f => f.pag_fechapagado).ToList();
                    dgvPagos.Columns[0].HeaderText = "Clave(socio)";
                    dgvPagos.Columns[1].HeaderText = "Nombre del socio";
                    dgvPagos.Columns[2].HeaderText = "Importe a pagar";
                    dgvPagos.Columns[3].HeaderText = "Intéres a pagar";
                    dgvPagos.Columns[4].HeaderText = "Total a pagar";
                    dgvPagos.Columns[5].HeaderText = "Fecha de pago";
                    dgvPagos.Columns[6].HeaderText = "Total (falta por pagar)";
                    dgvPagos.Columns[7].HeaderText = "Fecha (último pago)";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //en caso dado de que este seleccionado el control radiobutton se deshabilita el control groupbox
            if (radioButton4.Checked == true)
            {
                groupBox5.Enabled = false;
            }
            else //se habilita el control en caso que no este seleccionado el control
            {
                groupBox5.Enabled = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //se habilita el control en caso que no este seleccionado el control
            if (radioButton3.Checked == true)
            {
                groupBox5.Enabled = true;
            }
            else //en caso dado de que este seleccionado el control radiobutton se deshabilita el control groupbox
            {
                groupBox5.Enabled = false;
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            //en caso dado de que este seleccionado el control radiobutton se deshabilita el control groupbox
            if (radioButton6.Checked == true)
            {
                groupBox8.Enabled = false;
            }
            else //se habilita el control en caso que no este seleccionado el control
            {
                groupBox8.Enabled = true;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            //se habilita el control en caso que no este seleccionado el control
            if (radioButton5.Checked == true)
            {
                groupBox8.Enabled = true;
            }
            else //en caso dado de que este seleccionado el control radiobutton se deshabilita el control groupbox
            {
                groupBox8.Enabled = false;
            }
        }

        private void btnBuscar3_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton6.Checked == true) //CUANDO ES SELECCIONADO TODOS
                {
                    var consulta = estadisticascontroller.reporteTransacciones();

                    var result = from tra in consulta
                                 select new
                                 {
                                     tra.aso_id,
                                     tra.aso_nombre,
                                     tra1 = "$ " + tra.his_subtotal,
                                     tra2 = "$ " + tra.his_interes,
                                     tra3 = "$ " + tra.his_total,
                                     tra.his_fecha
                                 };

                    dgvTransacciones.DataSource = result.OrderBy(f => f.his_fecha).ToList();
                    dgvTransacciones.Columns[0].HeaderText = "Clave(socio)";
                    dgvTransacciones.Columns[1].HeaderText = "Nombre del socio";
                    dgvTransacciones.Columns[2].HeaderText = "Subtotal";
                    dgvTransacciones.Columns[3].HeaderText = "Intéres";
                    dgvTransacciones.Columns[4].HeaderText = "Total";
                    dgvTransacciones.Columns[5].HeaderText = "Fecha";

                }
                else //POR PERIODO
                {
                    var consulta = estadisticascontroller.reporteTransaccionesPeriodo(Convert.ToDateTime(dateTimePicker6.Value.ToShortDateString()), Convert.ToDateTime(dateTimePicker5.Value.ToShortDateString()));

                    var result = from tra in consulta
                                 select new
                                 {
                                     tra.aso_id,
                                     tra.aso_nombre,
                                     tra1 = "$ " + tra.his_subtotal,
                                     tra2 = "$ " + tra.his_interes,
                                     tra3 = "$ " + tra.his_total,
                                     tra.his_fecha
                                 };

                    dgvTransacciones.DataSource = result.OrderBy(f => f.his_fecha).ToList();
                    dgvTransacciones.Columns[0].HeaderText = "Clave(socio)";
                    dgvTransacciones.Columns[1].HeaderText = "Nombre del socio";
                    dgvTransacciones.Columns[2].HeaderText = "Subtotal";
                    dgvTransacciones.Columns[3].HeaderText = "Intéres";
                    dgvTransacciones.Columns[4].HeaderText = "Total";
                    dgvTransacciones.Columns[5].HeaderText = "Fecha";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Estadisticas_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(this.btnBuscar, "De clíc aquí para buscar");
                notificacion.SetToolTip(this.btnBuscar2, "De clíc aquí para cancelar la operación actual");
                notificacion.SetToolTip(this.btnBuscar3, "De clíc aquí para guardar el registro");
                notificacion.SetToolTip(this.btnImprimir, "De clíc aquí para mostrar el reporte de los socios que demoran de pago");

                //HABILITAMOS LA SELECCION DEL CONTROL RADIOBUTTON
                radioButton1.Checked = true; //seleccionamos el elemento radiobutton1
                groupBox3.Enabled = false; //deshabilitamos el control groupbox3

                //BUSQUEDA DE TODOS LOS PRESTAMOS
                if (radioButton1.Checked == true)
                {
                    var consulta = prestamoscontroller.prestamoscontrol();

                    var listado = from p in consulta
                                  select new
                                  {
                                      p.aso_id,
                                      p.aso_nombre,
                                      p.pre_id,
                                      credito = "$ " + p.pre_credito,
                                      p.pre_cuotas,
                                      p.pre_tipo,
                                      p.pre_fechasolicitud
                                  };

                    dgvPrestamos.DataSource = listado.OrderBy(f => f.pre_fechasolicitud).ToList();

                    dgvPrestamos.Columns[0].HeaderText = "Clave(socio)";
                    dgvPrestamos.Columns[1].HeaderText = "Nombre del socio";
                    dgvPrestamos.Columns[2].HeaderText = "No. de contrato";
                    dgvPrestamos.Columns[3].HeaderText = "Crédito solicitado";
                    dgvPrestamos.Columns[4].HeaderText = "Coutas a pagar";
                    dgvPrestamos.Columns[5].HeaderText = "Tipo de prestamo";
                    dgvPrestamos.Columns[6].HeaderText = "Fecha de solicitud de prestamo";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker2.MinDate = dateTimePicker1.Value;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker3.MinDate = dateTimePicker3.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePicker5.MinDate = dateTimePicker6.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MUESTRA LA OPCION 1
            if(cbxOpcion.SelectedIndex == 0)
            {
                var consulta = deudorescontroller.socios();

                var result = from tra in consulta
                             select new
                             {
                                 tra.aso_id,
                                 tra.aso_nombre,
                                 tra.aso_domicilio,
                                 tra.aso_telefono,
                                 tra.aso_movil,
                                 tra.aso_correoelectronico
                             };

                dgvDeudores.DataSource = result.OrderBy(a => a.aso_id).ToList();
                dgvDeudores.Columns[0].HeaderText = "Clave(socio)";
                dgvDeudores.Columns[1].HeaderText = "Nombre del socio";
                dgvDeudores.Columns[2].HeaderText = "Domicilio";
                dgvDeudores.Columns[3].HeaderText = "Teléfono";
                dgvDeudores.Columns[4].HeaderText = "Móvil";
                dgvDeudores.Columns[5].HeaderText = "Correo electrónico";
            }
            else if(cbxOpcion.SelectedIndex == 1) //MUESTRA LA OPCION 2
            {
                var consulta = deudorescontroller.prestamos();

                var result = from tra in consulta
                             select new
                             {
                                 tra.aso_id,
                                 tra.pre_id,
                                 credito = "$" + tra.pre_credito,
                                 cuotas = tra.pre_cuotas,
                                 tipo = tra.pre_tipo,
                                 porcentaje = tra.pre_interes + "%",
                                 tra.pre_fechaprestamo
                             };

                dgvDeudores.DataSource = result.OrderBy(a => a.aso_id).ToList();
                dgvDeudores.Columns[0].HeaderText = "Clave(socio)";
                dgvDeudores.Columns[1].HeaderText = "Contrato de prestamo";
                dgvDeudores.Columns[2].HeaderText = "Crédito solicitado";
                dgvDeudores.Columns[3].HeaderText = "Cuotas a pagar";
                dgvDeudores.Columns[4].HeaderText = "Tipo";
                dgvDeudores.Columns[5].HeaderText = "Intéres mensual";
                dgvDeudores.Columns[6].HeaderText = "Fecha de solicitud";
            }
            else if(cbxOpcion.SelectedIndex == 2) //MUESTRA LA OPCION 3
            {
                var consulta = deudorescontroller.pagos();

                var result = from tra in consulta
                             select new
                             {
                                 tra.aso_id,
                                 tra.pag_importe,
                                 tra.pag_interes,
                                 tra.pag_total,
                                 tra.pag_fechapago,
                                 tra.pag_pagado,
                                 tra.pa_pago_pendiente,
                                 tra.pag_fechapagado
                             };

                dgvDeudores.DataSource = result.OrderBy(a => a.aso_id).ToList();
                dgvDeudores.Columns[0].HeaderText = "Clave(socio)";
                dgvDeudores.Columns[1].HeaderText = "Importe a pagar";
                dgvDeudores.Columns[2].HeaderText = "Intéres a pagar";
                dgvDeudores.Columns[3].HeaderText = "Total a pagar";
                dgvDeudores.Columns[4].HeaderText = "Fecha de pago";
                dgvDeudores.Columns[5].HeaderText = "Total pagado";
                dgvDeudores.Columns[6].HeaderText = "Total por pagar";
                dgvDeudores.Columns[7].HeaderText = "Fecha de último pago";
            }
            else
            {
                MessageBox.Show("¡Seleccione una opción!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea mostrar el informe?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(mensaje == DialogResult.Yes)
            {
                Reportes.Rep_Deudores reporte = new Reportes.Rep_Deudores();
                reporte.ShowDialog();
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 3)
            {
                btnImprimir_Click(sender, e);
            }
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 0)
            {
                btnBuscar_Click(sender, e);
            }

            if(tabControl1.SelectedIndex == 1)
            {
                btnBuscar2_Click(sender, e);
            }

            if(tabControl1.SelectedIndex == 2)
            {
                btnBuscar3_Click(sender, e);
            }
        }
    }
}
