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
    public partial class CatalogoEMLC : Form
    {
        //CONTROLADORES
        CatalogoEMLCController catalogoEMLCcontroller = new CatalogoEMLCController();

        //ACCIONES GLOBALES
        int estados, municipios, localidades, colonias;

        //ID SELECCIONADO//
        long idestado, idmunicipio, idlocalidad, idcolonia;

        //NOMBRES GUARDAR
        string nombreestado, nombremunicipio, nombrelocalidad, nombrecolonia;

        public CatalogoEMLC()
        {
            InitializeComponent();
        }

        private void CatalogoEMLC_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(this.btnActualizar, "De clíc aquí para actualizar");
                notificacion.SetToolTip(this.btnCancelar, "De clíc aquí para cancelar la operación actual");
                notificacion.SetToolTip(this.btnGuardar, "De clíc aquí para guardar el registro");
                notificacion.SetToolTip(this.btnEliminar, "De clíc aquí para eliminar el registro");
                notificacion.SetToolTip(this.btnNuevo, "De clíc aquí para agregar un nuevo registro");

                notificacion.SetToolTip(this.btnActualizar2, "De clíc aquí para actualizar");
                notificacion.SetToolTip(this.btnCancelar2, "De clíc aquí para cancelar la operación actual");
                notificacion.SetToolTip(this.btnGuardar2, "De clíc aquí para guardar el registro");
                notificacion.SetToolTip(this.btnEliminar2, "De clíc aquí para eliminar el registro");
                notificacion.SetToolTip(this.btnNuevo2, "De clíc aquí para agregar un nuevo registro");

                notificacion.SetToolTip(this.btnActualizar3, "De clíc aquí para actualizar");
                notificacion.SetToolTip(this.btnCancelar3, "De clíc aquí para cancelar la operación actual");
                notificacion.SetToolTip(this.btnGuardar3, "De clíc aquí para guardar el registro");
                notificacion.SetToolTip(this.btnEliminar3, "De clíc aquí para eliminar el registro");
                notificacion.SetToolTip(this.btnNuevo3, "De clíc aquí para agregar un nuevo registro");


                lblEstados.Text = catalogoEMLCcontroller.contarEstados().ToString();
                lblMunicipios.Text = catalogoEMLCcontroller.contarMunicipios().ToString();
                lblLocalidades.Text = catalogoEMLCcontroller.contarLocalidades().ToString();
                lblColonias.Text = catalogoEMLCcontroller.contarColonias().ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReporteEstados_Click(object sender, EventArgs e)
        {
            Reportes.Rep_Estados repestados = new Reportes.Rep_Estados();
            repestados.ShowDialog();
        }

        private void btnReporteMunicipios_Click(object sender, EventArgs e)
        {
            Reportes.Rep_Municipios repmunicipios = new Reportes.Rep_Municipios();
            repmunicipios.ShowDialog();
        }

        private void btnReporteLocalidades_Click(object sender, EventArgs e)
        {
            Reportes.Rep_Localidades replocalidades = new Reportes.Rep_Localidades();
            replocalidades.ShowDialog();
        }

        private void btnReporteColonias_Click(object sender, EventArgs e)
        {
            Reportes.Rep_Colonias repcolonias = new Reportes.Rep_Colonias();
            repcolonias.ShowDialog();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                var consulta = from c in catalogoEMLCcontroller.estadosDGV()
                               select new
                               {
                                   c.est_id,
                                   c.est_nombreestado
                               };

                dgvEstados.DataSource = consulta.ToList();
                dgvEstados.Columns[0].HeaderText = "No.";
                dgvEstados.Columns[1].HeaderText = "Nombre del estado";
            }
            else if(tabControl1.SelectedIndex == 2)
            {
                //CARGAR LOS ESTADOS EN LOS COMBOBOX
                cbxEstadoFiltro.DataSource = catalogoEMLCcontroller.estadosDGV().ToList();
                cbxEstadoFiltro.DisplayMember = "est_nombreestado";
                cbxEstadoFiltro.ValueMember = "est_id";

                cbxEstado.DataSource = catalogoEMLCcontroller.estadosDGV().ToList();
                cbxEstado.DisplayMember = "est_nombreestado";
                cbxEstado.ValueMember = "est_id";

                var consulta = from c in catalogoEMLCcontroller.municipiosDGV()
                               select new
                               {
                                   c.mun_id,
                                   c.mun_nombremunicipio,
                                   c.est_nombreestado
                               };

                dgvMunicipios.DataSource = consulta.Where(est => est.est_nombreestado == cbxEstadoFiltro.Text).ToList();
                dgvMunicipios.Columns[0].HeaderText = "No.";
                dgvMunicipios.Columns[1].HeaderText = "Nombre del municipio";
                dgvMunicipios.Columns[2].HeaderText = "Nombre del estado";

                cbxEstado.SelectedIndex = -1;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                //CARGAR LOS ESTADOS EN LOS COMBOBOX
                cbxEstadoFiltro2.DataSource = catalogoEMLCcontroller.estadosDGV().ToList();
                cbxEstadoFiltro2.DisplayMember = "est_nombreestado";
                cbxEstadoFiltro2.ValueMember = "est_id";

                cbxMunicipioFiltro2.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b=> b.est_nombreestado == cbxEstadoFiltro2.Text).ToList();
                cbxMunicipioFiltro2.DisplayMember = "mun_nombremunicipio";
                cbxMunicipioFiltro2.ValueMember = "mun_id";

                cbxEstado2.DataSource = catalogoEMLCcontroller.estadosDGV().ToList();
                cbxEstado2.DisplayMember = "est_nombreestado";
                cbxEstado2.ValueMember = "est_id";

                cbxMunicipio2.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b => b.est_nombreestado == cbxEstadoFiltro2.Text).ToList();
                cbxMunicipio2.DisplayMember = "mun_nombremunicipio";
                cbxMunicipio2.ValueMember = "mun_id";
            }
            else if(tabControl1.SelectedIndex == 4)
            {
                //CARGAR LOS ESTADOS EN LOS COMBOBOX
                cbxEstadoFiltro3.DataSource = catalogoEMLCcontroller.estadosDGV().ToList();
                cbxEstadoFiltro3.DisplayMember = "est_nombreestado";
                cbxEstadoFiltro3.ValueMember = "est_id";

                cbxMunicipioFiltro3.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b => b.est_nombreestado == cbxEstadoFiltro3.Text).ToList();
                cbxMunicipioFiltro3.DisplayMember = "mun_nombremunicipio";
                cbxMunicipioFiltro3.ValueMember = "mun_id";

                cbxLocalidadFiltro3.DataSource = catalogoEMLCcontroller.localidadesDGV().Where(b => b.mun_nombremunicipio == cbxMunicipioFiltro3.Text).ToList();
                cbxLocalidadFiltro3.DisplayMember = "loc_nombrelocalidad";
                cbxLocalidadFiltro3.ValueMember = "loc_id";
            
                cbxEstado3.DataSource = catalogoEMLCcontroller.estadosDGV().ToList();
                cbxEstado3.DisplayMember = "est_nombreestado";
                cbxEstado3.ValueMember = "est_id";

                cbxMunicipio3.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b => b.est_nombreestado == cbxEstadoFiltro3.Text).ToList();
                cbxMunicipio3.DisplayMember = "mun_nombremunicipio";
                cbxMunicipio3.ValueMember = "mun_id";

                cbxLocalidadFiltro3.DataSource = catalogoEMLCcontroller.localidadesDGV().Where(b => b.mun_nombremunicipio == cbxMunicipioFiltro3.Text).ToList();
                cbxLocalidadFiltro3.DisplayMember = "loc_nombrelocalidad";
                cbxLocalidadFiltro3.ValueMember = "loc_id";
            }
            else
            {
                dgvEstados.DataSource = null;
                dgvMunicipios.DataSource = null;
                dgvLocalidades.DataSource = null;
                dgvColonias.DataSource = null;
            }
        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtBusqueda.Text == "")
                {
                    var consulta = from c in catalogoEMLCcontroller.estadosDGV()
                                   select new
                                   {
                                       c.est_id,
                                       c.est_nombreestado
                                   };

                    dgvEstados.DataSource = consulta.ToList();
                    dgvEstados.Columns[0].HeaderText = "No.";
                    dgvEstados.Columns[1].HeaderText = "Nombre del estado";
                }
                else
                {
                    var consulta = from c in catalogoEMLCcontroller.estadosDGV()
                                   select new
                                   {
                                       c.est_id,
                                       c.est_nombreestado
                                   };

                    dgvEstados.DataSource = consulta.Where(c=> c.est_nombreestado.Contains(txtBusqueda.Text)).ToList();
                    dgvEstados.Columns[0].HeaderText = "No.";
                    dgvEstados.Columns[1].HeaderText = "Nombre del estado";
                }

                if (groupBox1.Enabled == false)
                {
                    if (dgvEstados.CurrentRow == null)
                    {
                        btnActualizar.Enabled = false;
                        btnEliminar.Enabled = false;
                    }
                    else
                    {
                        btnActualizar.Enabled = true;
                        btnEliminar.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBusqueda2_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(txtBusqueda2.Text == "")
                {
                    var consulta = from c in catalogoEMLCcontroller.municipiosDGV()
                                   select new
                                   {
                                       c.mun_id,
                                       c.mun_nombremunicipio,
                                       c.est_nombreestado
                                   };

                    dgvMunicipios.DataSource = consulta.ToList();
                    dgvMunicipios.Columns[0].HeaderText = "No.";
                    dgvMunicipios.Columns[1].HeaderText = "Nombre del municipio";
                    dgvMunicipios.Columns[2].HeaderText = "Nombre del estado";
                }
                else
                {
                    var consulta = from c in catalogoEMLCcontroller.municipiosDGV()
                                   select new
                                   {
                                       c.mun_id,
                                       c.mun_nombremunicipio,
                                       c.est_nombreestado
                                   };

                    dgvMunicipios.DataSource = consulta.Where(b => b.est_nombreestado == cbxEstadoFiltro.Text && b.mun_nombremunicipio.Contains(txtBusqueda2.Text)).ToList();
                    dgvMunicipios.Columns[0].HeaderText = "No.";
                    dgvMunicipios.Columns[1].HeaderText = "Nombre del municipio";
                    dgvMunicipios.Columns[2].HeaderText = "Nombre del estado";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxEstadoFiltro2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxMunicipioFiltro2.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b => b.est_nombreestado == cbxEstadoFiltro2.Text).ToList();
                cbxMunicipioFiltro2.DisplayMember = "mun_nombremunicipio";
                cbxMunicipioFiltro2.ValueMember = "mun_id";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBusqueda3_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(txtBusqueda3.Text == "")
                {
                    var consulta = from c in catalogoEMLCcontroller.localidadesDGV()
                                   select new
                                   {
                                       c.loc_id,
                                       c.loc_nombrelocalidad,
                                       c.mun_nombremunicipio,
                                       c.est_nombreestado
                                   };

                    dgvLocalidades.DataSource = consulta.ToList();
                    dgvLocalidades.Columns[0].HeaderText = "No.";
                    dgvLocalidades.Columns[1].HeaderText = "Nombre de la localidad";
                    dgvLocalidades.Columns[2].HeaderText = "Nombre del municipio";
                    dgvLocalidades.Columns[3].HeaderText = "Nombre del estado";
                }
                else
                {
                    var consulta = from c in catalogoEMLCcontroller.localidadesDGV()
                                   select new
                                   {
                                       c.loc_id,
                                       c.loc_nombrelocalidad,
                                       c.mun_nombremunicipio,
                                       c.est_nombreestado
                                   };

                    dgvLocalidades.DataSource = consulta.ToList().Where(b => b.est_nombreestado == cbxEstadoFiltro2.Text && b.mun_nombremunicipio == cbxMunicipioFiltro2.Text && b.loc_nombrelocalidad.Contains(txtBusqueda3.Text)).ToList();
                    dgvLocalidades.Columns[0].HeaderText = "No.";
                    dgvLocalidades.Columns[1].HeaderText = "Nombre de la localidad";
                    dgvLocalidades.Columns[2].HeaderText = "Nombre del municipio";
                    dgvLocalidades.Columns[3].HeaderText = "Nombre del estado";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea eliminar el registro: " + dgvEstados.CurrentRow.Cells[1].Value.ToString() + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(mensaje == DialogResult.Yes)
                {
                    if (catalogoEMLCcontroller.comprobar_Eliminacion_Estado(long.Parse(dgvEstados.CurrentRow.Cells[0].Value.ToString())) == false)
                    {
                        catalogoEMLCcontroller.eliminar_Estado(long.Parse(dgvEstados.CurrentRow.Cells[0].Value.ToString()));

                        MessageBox.Show("¡El registro ha sido eliminado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        var consulta = from c in catalogoEMLCcontroller.estadosDGV()
                                       select new
                                       {
                                           c.est_id,
                                           c.est_nombreestado
                                       };

                        dgvEstados.DataSource = consulta.ToList();
                        dgvEstados.Columns[0].HeaderText = "No.";
                        dgvEstados.Columns[1].HeaderText = "Nombre del estado";

                        idestado = 0;
                        nombreestado = null;
                    }
                    else
                    {
                        MessageBox.Show("¡El elemento seleccionado que desea eliminar tiene registros asociados a él por lo cual no puede completarse la acción!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo2_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea agregar un registro?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(mensaje == DialogResult.Yes)
            {
                groupBox4.Enabled = true;
                txtMunicipio.Focus();
                btnGuardar2.Enabled = true;
                btnCancelar2.Enabled = true;
                municipios = 1;
            }
            else
            {
                groupBox4.Enabled = false;
                txtMunicipio.Clear();
                btnGuardar2.Enabled = false;
                btnCancelar2.Enabled = false;
                municipios = 0;
            }
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            try
            {
                if(municipios == 1) //AGREGAR MUNICIPIO
                {
                    if(txtMunicipio.Text == "")
                    {
                        lblValidacion4.Text = "* Complete el campo";
                        lblValidacion4.Visible = true;
                    }
                    else
                    {
                        long mun_repetido = catalogoEMLCcontroller.municipiosDGV().Where(mun => mun.mun_nombremunicipio == txtMunicipio.Text && mun.est_nombreestado == cbxEstado.Text).LongCount();

                        if(mun_repetido == 0)
                        {
                            lblValidacion4.Visible = false;

                            DialogResult mensaje = MessageBox.Show("¿Desea agregar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if(mensaje == DialogResult.Yes)
                            {
                                catalogoEMLCcontroller.agregar_Municipio(txtMunicipio.Text, long.Parse(cbxEstado.SelectedValue.ToString()));

                                MessageBox.Show("¡El registro fue dado de alta exitosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                var consulta = from c in catalogoEMLCcontroller.municipiosDGV()
                                               select new
                                               {
                                                   c.mun_id,
                                                   c.mun_nombremunicipio,
                                                   c.est_nombreestado
                                               };

                                dgvMunicipios.DataSource = consulta.ToList();
                                dgvMunicipios.Columns[0].HeaderText = "No.";
                                dgvMunicipios.Columns[1].HeaderText = "Nombre del municipio";
                                dgvMunicipios.Columns[2].HeaderText = "Nombre del estado";

                                groupBox4.Enabled = false;
                                txtMunicipio.Clear();
                                btnGuardar2.Enabled = false;
                                btnCancelar2.Enabled = false;
                                cbxEstado.SelectedIndex = -1;
                                municipios = 0;

                            }
                        }
                        else
                        {
                            lblValidacion4.Text = "* El municipio ya se encuentra registrado";
                            lblValidacion4.Visible = true;
                        }
                    }
                }
                else if(municipios == 2) //PARA ACTUALIZAR MUNICIPIO
                {
                    if(txtMunicipio.Text == "")
                    {
                        lblValidacion4.Text = "* Complete el campo";
                        lblValidacion4.Visible = true;
                    }
                    else
                    {
                        bool verificar = false;
                        
                        if(txtMunicipio.Text == nombremunicipio && cbxEstado.Text == nombreestado)
                        {
                            verificar = false;
                        }
                        else
                        {
                            long mun_repetido = catalogoEMLCcontroller.municipiosDGV().Where(mun => mun.mun_nombremunicipio == txtMunicipio.Text && mun.mun_estado == long.Parse(cbxEstado.SelectedValue.ToString())).LongCount();

                            if(mun_repetido == 0)
                            {
                                verificar = false;
                            }
                            else
                            {
                                verificar = true;
                            }
                        }

                        if (verificar == false)
                        {
                            lblValidacion4.Visible = false;

                            DialogResult mensaje = MessageBox.Show("¿Desea actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if(mensaje == DialogResult.Yes)
                            {
                                catalogoEMLCcontroller.actualizar_Municipio(idmunicipio, txtMunicipio.Text, long.Parse(cbxEstado.SelectedValue.ToString()));

                                MessageBox.Show("¡El registro ha sido actualizado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                var consulta = from c in catalogoEMLCcontroller.municipiosDGV()
                                               select new
                                               {
                                                   c.mun_id,
                                                   c.mun_nombremunicipio,
                                                   c.est_nombreestado
                                               };

                                dgvMunicipios.DataSource = consulta.ToList();
                                dgvMunicipios.Columns[0].HeaderText = "No.";
                                dgvMunicipios.Columns[1].HeaderText = "Nombre del municipio";
                                dgvMunicipios.Columns[2].HeaderText = "Nombre del estado";

                                groupBox4.Enabled = false;
                                txtMunicipio.Clear();
                                btnGuardar2.Enabled = false;
                                btnCancelar2.Enabled = false;
                                cbxEstado.SelectedIndex = -1;
                                municipios = 0;
                            }
                        }
                        else
                        {
                            lblValidacion4.Text = "* El municipio ya se encuentra registrado";
                            lblValidacion4.Visible = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("¡No se ejecutan acciones!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea actualizar el registro: " + dgvMunicipios.CurrentRow.Cells[1].Value.ToString() + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(mensaje == DialogResult.Yes)
                {
                    municipios = 2;
                    nombremunicipio = dgvMunicipios.CurrentRow.Cells[1].Value.ToString();
                    idmunicipio = long.Parse(dgvMunicipios.CurrentRow.Cells[0].Value.ToString());
                    txtMunicipio.Text = dgvMunicipios.CurrentRow.Cells[1].Value.ToString();
                    cbxEstado.Text = dgvMunicipios.CurrentRow.Cells[2].Value.ToString();
                    groupBox4.Enabled = true;
                    txtMunicipio.Focus();
                    btnGuardar2.Enabled = true;
                    btnCancelar2.Enabled = true;
                }
                else
                {
                    groupBox4.Enabled = false;
                    txtMunicipio.Clear();
                    btnGuardar2.Enabled = false;
                    btnCancelar2.Enabled = false;
                    cbxEstado.SelectedIndex = 0;
                    municipios = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea eliminar el registro: " + dgvMunicipios.CurrentRow.Cells[1].Value.ToString() + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (mensaje == DialogResult.Yes)
                {
                    if(catalogoEMLCcontroller.comprobar_Eliminacion_Municipio(long.Parse(dgvMunicipios.CurrentRow.Cells[0].Value.ToString())) == true)
                    {
                        MessageBox.Show("¡El elemento seleccionado que desea eliminar tiene registros asociados a él por lo cual no puede completarse la acción!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        long id_mun = long.Parse(dgvMunicipios.CurrentRow.Cells[0].Value.ToString());
                        catalogoEMLCcontroller.eliminar_Municipio(id_mun);

                        MessageBox.Show("¡El registro ha sido eliminado exitosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        var consulta = from c in catalogoEMLCcontroller.municipiosDGV()
                                       select new
                                       {
                                           c.mun_id,
                                           c.mun_nombremunicipio,
                                           c.est_nombreestado
                                       };

                        dgvMunicipios.DataSource = consulta.Where(mun => mun.est_nombreestado == cbxEstadoFiltro.Text).ToList();
                        dgvMunicipios.Columns[0].HeaderText = "No.";
                        dgvMunicipios.Columns[1].HeaderText = "Nombre del municipio";
                        dgvMunicipios.Columns[2].HeaderText = "Nombre del estado";

                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea eliminar el registro: " + dgvLocalidades.CurrentRow.Cells[1].Value.ToString() + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(mensaje == DialogResult.Yes)
                {
                    if (catalogoEMLCcontroller.comprobar_Eliminacion_Localidades(long.Parse(dgvLocalidades.CurrentRow.Cells[0].Value.ToString())) == true)
                    {
                        MessageBox.Show("¡El elemento seleccionado que desea eliminar tiene registros asociados a él por lo cual no puede completarse la acción!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        catalogoEMLCcontroller.eliminar_Localidad(long.Parse(dgvLocalidades.CurrentRow.Cells[0].Value.ToString()));

                        MessageBox.Show("¡El registro ha sido eliminado exitosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var consulta = from c in catalogoEMLCcontroller.localidadesDGV()
                                       select new
                                       {
                                           c.loc_id,
                                           c.loc_municipio,
                                           c.loc_nombrelocalidad
                                       };
                        dgvLocalidades.DataSource = consulta.ToList();
                        dgvLocalidades.Columns[0].HeaderText = "No.";
                        dgvLocalidades.Columns[1].HeaderText = "Nombre del municipio";
                        dgvLocalidades.Columns[2].HeaderText = "Nombre de la localidad";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea eliminar el registro: " + dgvColonias.CurrentRow.Cells[1].Value.ToString() + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (mensaje == DialogResult.Yes)
                {
                    if (catalogoEMLCcontroller.comprobar_Eliminacion_Colonias(long.Parse(dgvColonias.CurrentRow.Cells[0].Value.ToString())) == true)
                    {
                        MessageBox.Show("¡El elemento seleccionado que desea eliminar tiene registros asociados a él por lo cual no puede completarse la acción!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //long valor = long.Parse(dgvColonias.CurrentRow.Cells[0].Value.ToString());
                        catalogoEMLCcontroller.eliminar_Colonia(long.Parse(dgvColonias.CurrentRow.Cells[0].Value.ToString()));
                        MessageBox.Show("¡El registro ha sido eliminado exitosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var consulta = from c in catalogoEMLCcontroller.coloniasDGV()
                                       select new
                                       {
                                           c.col_id,
                                           c.col_nombrecolonia,
                                           c.loc_nombrelocalidad,
                                           c.mun_nombremunicipio,
                                           c.est_nombreestado

                                       };
                        dgvColonias.DataSource = consulta.ToList();
                        dgvColonias.Columns[0].HeaderText = "No.";
                        dgvColonias.Columns[1].HeaderText = "Nombre de la colonia";
                        dgvColonias.Columns[2].HeaderText = "Nombre de la localidad";
                        dgvColonias.Columns[3].HeaderText = "Nombre del municipio";
                        dgvColonias.Columns[4].HeaderText = "Nombre del estado";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea agregar un registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(mensaje == DialogResult.Yes)
                {
                    groupBox6.Enabled = true;
                    btnGuardar3.Enabled = true;
                    btnCancelar3.Enabled = true;
                    txtLocalidad.Focus();
                    localidades = 1;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar3_Click(object sender, EventArgs e)
        {
            try
            {
                if(localidades == 1) //EJECUTARA LAS ACCIONES DE INSERCION DE LOCALIDADES
                {
                    if(txtLocalidad.Text == "")
                    {
                        lblValidacion4.Text = "* Complete el campo";
                        lblValidacion4.Visible = true;
                    }
                    else
                    {
                        lblValidacion4.Visible = false;
                    }
                    
                    if(cbxEstado2.Text == "")
                    {
                        lblValidacion5.Text = "* Complete el campo";
                        lblValidacion5.Visible = true;
                    }
                    else
                    {
                        lblValidacion5.Visible = false;
                    }

                    if (cbxMunicipio2.Text == "")
                    {
                        lblValidacion6.Text = "* Complete el campo";
                        lblValidacion6.Visible = true;
                    }
                    else
                    {
                        lblValidacion6.Visible = false;
                    }


                    if(txtLocalidad.Text != "" && cbxMunicipio2.Text != "" && cbxEstado2.Text != "")
                    {
                        if(catalogoEMLCcontroller.localidadesDGV().Where(loc => loc.loc_nombrelocalidad == txtLocalidad.Text && loc.loc_municipio == long.Parse(cbxMunicipio2.SelectedValue.ToString())).LongCount() == 0)
                        {
                            DialogResult mensaje = MessageBox.Show("¿Desea agregar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if(mensaje == DialogResult.Yes)
                            {
                                catalogoEMLCcontroller.agregar_Localidad(txtLocalidad.Text, long.Parse(cbxMunicipio2.SelectedValue.ToString()));

                                MessageBox.Show("¡El registro fue dado de alta exitosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                var consulta = from c in catalogoEMLCcontroller.localidadesDGV()
                                               select new
                                               {
                                                   c.loc_id,
                                                   c.loc_nombrelocalidad,
                                                   c.mun_nombremunicipio,
                                                   c.est_nombreestado
                                               };

                                dgvLocalidades.DataSource = consulta.ToList();
                                dgvLocalidades.Columns[0].HeaderText = "No.";
                                dgvLocalidades.Columns[1].HeaderText = "Nombre de la localidad";
                                dgvLocalidades.Columns[2].HeaderText = "Nombre del municipio";
                                dgvLocalidades.Columns[3].HeaderText = "Nombre del estado";

                                groupBox6.Enabled = false;
                                txtLocalidad.Clear();
                                btnGuardar3.Enabled = false;
                                cbxEstado2.SelectedIndex = -1;
                                cbxMunicipio2.SelectedIndex = -1;
                                btnCancelar3.Enabled = false;

                                localidades = 0;
                            }
                        }
                        else
                        {
                            lblValidacion4.Text = "* La localidad ya se encuentra registrada";
                            lblValidacion4.Visible = true;
                        }
                    }
                }
                else if(localidades == 2) //EJECUTARA LAS ACCIONES DE ACTUALIZACION DE LOCALIDADES
                {
                    if (txtLocalidad.Text == "")
                    {
                        lblValidacion4.Text = "* Complete el campo";
                        lblValidacion4.Visible = true;
                    }
                    else
                    {
                        lblValidacion4.Visible = false;
                    }

                    if (cbxEstado2.Text == "")
                    {
                        lblValidacion5.Text = "* Complete el campo";
                        lblValidacion5.Visible = true;
                    }
                    else
                    {
                        lblValidacion5.Visible = false;
                    }

                    if (cbxMunicipio2.Text == "")
                    {
                        lblValidacion6.Text = "* Complete el campo";
                        lblValidacion6.Visible = true;
                    }
                    else
                    {
                        lblValidacion6.Visible = false;
                    }


                    if (txtLocalidad.Text != "" && cbxMunicipio2.Text != "" && cbxEstado2.Text != "")
                    {
                        DialogResult mensaje = MessageBox.Show("¿Desea actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (mensaje == DialogResult.Yes)
                        {
                            bool valor = true;

                            if(txtLocalidad.Text == nombrelocalidad && cbxMunicipio2.Text == nombremunicipio)
                            {
                                valor = false;
                                lblValidacion4.Visible = false;
                            }
                            else
                            {
                                if (catalogoEMLCcontroller.localidadesDGV().Where(loc => loc.loc_nombrelocalidad == txtLocalidad.Text && loc.loc_municipio == long.Parse(cbxMunicipio2.SelectedValue.ToString())).LongCount() > 0)
                                {
                                    lblValidacion4.Text = "* La localidad ya se encuentra registrada";
                                    lblValidacion4.Visible = true;

                                    valor = true;
                                }
                                else
                                {
                                    lblValidacion4.Visible = false;
                                    valor = false;
                                }
                            }

                            if(valor == false)
                            {
                                catalogoEMLCcontroller.actualizar_Localidad(idlocalidad, txtLocalidad.Text, long.Parse(cbxMunicipio2.SelectedValue.ToString()));

                                MessageBox.Show("¡El registro ha sido actualizado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                var consulta = from c in catalogoEMLCcontroller.localidadesDGV()
                                               select new
                                               {
                                                   c.loc_id,
                                                   c.loc_nombrelocalidad,
                                                   c.mun_nombremunicipio,
                                                   c.est_nombreestado
                                               };

                                dgvLocalidades.DataSource = consulta.ToList();
                                dgvLocalidades.Columns[0].HeaderText = "No.";
                                dgvLocalidades.Columns[1].HeaderText = "Nombre de la localidad";
                                dgvLocalidades.Columns[2].HeaderText = "Nombre del municipio";
                                dgvLocalidades.Columns[3].HeaderText = "Nombre del estado";

                                groupBox6.Enabled = false;
                                txtLocalidad.Clear();
                                btnGuardar3.Enabled = false;
                                btnCancelar3.Enabled = false;
                                cbxEstado2.SelectedIndex = -1;
                                cbxMunicipio2.SelectedIndex = -1;

                                localidades = 0;
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

        private void btnActualizar3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea actualizar el registro: " + dgvLocalidades.CurrentRow.Cells[1].Value.ToString() +"?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(mensaje == DialogResult.Yes)
                {
                    idlocalidad = long.Parse(dgvLocalidades.CurrentRow.Cells[0].Value.ToString());
                    nombrelocalidad = dgvLocalidades.CurrentRow.Cells[1].Value.ToString();
                    nombremunicipio = dgvLocalidades.CurrentRow.Cells[2].Value.ToString();

                    txtLocalidad.Text = dgvLocalidades.CurrentRow.Cells[1].Value.ToString();
                    cbxEstado2.Text = dgvLocalidades.CurrentRow.Cells[3].Value.ToString();
                    cbxMunicipio2.Text = dgvLocalidades.CurrentRow.Cells[2].Value.ToString();
                   
                    groupBox6.Enabled = true;
                    btnGuardar3.Enabled = true;
                    btnCancelar3.Enabled = true;
                    txtLocalidad.Focus();

                    localidades = 2;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea agregar un registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (mensaje == DialogResult.Yes)
                {
                    groupBox8.Enabled = true;
                    btnGuardar4.Enabled = true;
                    btnCancelar4.Enabled = true;
                    txtColonia.Focus();
                    colonias = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar4_Click(object sender, EventArgs e)
        {
            try
            {
                if (colonias == 1)//EJECUTARA LAS ACCIONES DE INSERCCION DE COLONIAS
                {
                    if (txtColonia.Text == "")
                    {
                        lblValidacion7.Text = "* Complete el campo";
                        lblValidacion7.Visible = true;
                    }
                    else
                    {
                        lblValidacion7.Visible = false;
                    }

                    if (cbxEstado3.Text == "")
                    {
                        lblValidacion8.Text = "* Complete el campo";
                        lblValidacion8.Visible = true;
                    }
                    else
                    {
                        lblValidacion8.Visible = false;
                    }
                    if (cbxMunicipio3.Text == "")
                    {
                        lblValidacion9.Text = "* Complete el campo";
                        lblValidacion9.Visible = true;
                    }
                    else
                    {
                        lblValidacion9.Visible = false;
                    }

                    if (cbxLocalidad3.Text == "")
                    {
                        lblValidacion10.Text = "* Complete el campo";
                        lblValidacion10.Visible = true;
                    }
                    else
                    {
                        lblValidacion10.Visible = false;
                    }

                    if (txtColonia.Text != "" && cbxEstado3.Text != "" && cbxMunicipio3.Text != "" && cbxLocalidad3.Text != "")
                    {
                        if (catalogoEMLCcontroller.coloniasDGV().Where(col => col.col_nombrecolonia == txtColonia.Text && col.col_localidad == long.Parse(cbxLocalidad3.SelectedValue.ToString())).LongCount() == 0)
                        {
                            DialogResult mensaje = MessageBox.Show("¿Desea agregar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (mensaje == DialogResult.Yes)
                            {
                                catalogoEMLCcontroller.agregar_Colonia(txtColonia.Text, long.Parse(cbxLocalidad3.SelectedValue.ToString()));

                                MessageBox.Show("¡El registro fue dado de alta exitosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                var consulta = from c in catalogoEMLCcontroller.coloniasDGV()
                                               select new
                                               {
                                                   //c.col_id,
                                                   //c.col_localidad,
                                                   //c.col_nombrecolonia,
                                                   c.col_id,
                                                   c.col_nombrecolonia,
                                                   c.loc_nombrelocalidad,
                                                   c.mun_nombremunicipio,
                                                   c.est_nombreestado
                                               };

                                dgvColonias.DataSource = consulta.ToList();
                                dgvColonias.Columns[0].HeaderText = "No.";
                                dgvColonias.Columns[1].HeaderText = "Nombre de la colonia";
                                dgvColonias.Columns[2].HeaderText = "Nombre de la localidad";
                                dgvColonias.Columns[3].HeaderText = "Nombre del municipio";
                                dgvColonias.Columns[4].HeaderText = "Nombre del estado";

                                groupBox8.Enabled = false;
                                txtColonia.Clear();
                                btnGuardar4.Enabled = false;
                                btnCancelar4.Enabled = false;
                                cbxEstado3.SelectedIndex = -1;
                                cbxMunicipio3.SelectedIndex = -1;
                                cbxLocalidad3.SelectedIndex = -1;
                                colonias = 0;
                            }
                            else
                            {
                                lblValidacion7.Text = "* La colonia ya se encuentra registrada";
                                lblValidacion7.Visible = true;
                            }
                        }        
                    }
                }
                else if (colonias == 2) //EJECUTARA LAS ACCIONES DE ACTUALIZACION DE COLONIAS
                {
                    if (txtColonia.Text == "")
                    {
                        lblValidacion7.Text = "* Complete el campo";
                        lblValidacion7.Visible = true;
                    }
                    else
                    {
                        lblValidacion7.Visible = false;
                    }

                    if (cbxEstado3.Text == "")
                    {
                        lblValidacion8.Text = "* Complete el campo";
                        lblValidacion8.Visible = true;
                    }
                    else
                    {
                        lblValidacion8.Visible = false;
                    }

                    if (cbxMunicipio3.Text == "")
                    {
                        lblValidacion9.Text = "* Complete el campo";
                        lblValidacion9.Visible = true;
                    }
                    else
                    {
                        lblValidacion9.Visible = false;
                    }

                    if (cbxLocalidad3.Text == "")
                    {
                        lblValidacion10.Text = "* Complete el campo";
                        lblValidacion10.Visible = true;
                    }
                    else
                    {
                        lblValidacion10.Visible = false;
                    }


                    if (txtColonia.Text != "" && cbxMunicipio3.Text != "" && cbxEstado3.Text != "" && cbxLocalidad3.Text != "")
                    {
                        DialogResult mensaje = MessageBox.Show("¿Desea actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (mensaje == DialogResult.Yes)
                        {
                            bool valor = true;

                            if (txtColonia.Text == nombrecolonia && cbxLocalidad3.Text == nombrelocalidad)
                            {
                                lblValidacion7.Visible = false;

                                valor = false;
                            }
                            else
                            {
                                if (catalogoEMLCcontroller.coloniasDGV().Where(col => col.col_nombrecolonia == txtColonia.Text && col.col_localidad == long.Parse(cbxLocalidad3.SelectedValue.ToString())).LongCount() > 0)
                                {
                                    lblValidacion4.Text = "* La colonia ya se encuentra registrada";
                                    lblValidacion4.Visible = true;

                                    valor = true;
                                }
                                else
                                {
                                    valor = false;
                                    lblValidacion7.Visible = false;
                                }
                            }

                            if (valor == false)
                            {
                                catalogoEMLCcontroller.actualizar_Colonia(idcolonia, txtColonia.Text, long.Parse(cbxLocalidad3.SelectedValue.ToString()));

                                MessageBox.Show("¡El registro ha sido actualizado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                var consulta = from c in catalogoEMLCcontroller.coloniasDGV()
                                               select new
                                               {
                                                   c.col_id,
                                                   c.col_nombrecolonia,
                                                   c.loc_nombrelocalidad,
                                                   c.mun_nombremunicipio,
                                                   c.est_nombreestado
                                               };

                                dgvColonias.DataSource = consulta.ToList();
                                dgvColonias.Columns[0].HeaderText = "No.";
                                dgvColonias.Columns[1].HeaderText = "Nombre de la colonia";
                                dgvColonias.Columns[2].HeaderText = "Nombre de la localidad";
                                dgvColonias.Columns[3].HeaderText = "Nombre del municipio";
                                dgvColonias.Columns[4].HeaderText = "Nombre del estado";


                                groupBox8.Enabled = false;
                                txtColonia.Clear();
                                btnGuardar4.Enabled = false;
                                btnCancelar4.Enabled = false;
                                cbxEstado3.SelectedIndex = -1;
                                cbxMunicipio3.SelectedIndex = -1;
                                cbxLocalidad3.SelectedIndex = -1;
                                colonias = 0;
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

        private void btnActualizar4_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea actualizar el registro: " + dgvColonias.CurrentRow.Cells[1].Value.ToString() + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (mensaje == DialogResult.Yes)
                {
                    idcolonia = long.Parse(dgvColonias.CurrentRow.Cells[0].Value.ToString());
                    nombrecolonia = dgvColonias.CurrentRow.Cells[1].Value.ToString();
                    nombrelocalidad = dgvColonias.CurrentRow.Cells[2].Value.ToString();
                    nombremunicipio = dgvColonias.CurrentRow.Cells[3].Value.ToString();
                    nombreestado = dgvColonias.CurrentRow.Cells[4].Value.ToString();

                    txtColonia.Text = dgvColonias.CurrentRow.Cells[1].Value.ToString();
                    cbxEstado3.Text = dgvColonias.CurrentRow.Cells[4].Value.ToString();
                    cbxMunicipio3.Text = dgvColonias.CurrentRow.Cells[3].Value.ToString();
                    cbxLocalidad3.Text = dgvColonias.CurrentRow.Cells[2].Value.ToString();
                    
                    groupBox8.Enabled = true;
                    btnGuardar4.Enabled = true;
                    btnCancelar4.Enabled = true;
                    txtColonia.Focus();

                    colonias = 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar3_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (mensaje == DialogResult.Yes)
            {
                groupBox6.Enabled = false;
                txtLocalidad.Clear();
                btnGuardar3.Enabled = false;
                btnCancelar3.Enabled = false;
                cbxEstado2.SelectedIndex = -1;
                cbxMunicipio2.SelectedIndex = -1;
                localidades = 0;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(mensaje == DialogResult.Yes)
            {
                groupBox1.Enabled = false;
                txtEstado.Clear();
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                estados = 0;
            }
        }

        private void btnCancelar2_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (mensaje == DialogResult.Yes)
            {
                groupBox4.Enabled = false;
                txtMunicipio.Clear();
                btnGuardar2.Enabled = false;
                btnCancelar2.Enabled = false;
                cbxEstado.SelectedIndex = -1;
                municipios = 0;
            }
        }

        private void btnCancelar4_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea cancelar la operación?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (mensaje == DialogResult.Yes)
            {
                groupBox8.Enabled = false;
                txtColonia.Clear();
                btnGuardar4.Enabled = false;
                btnCancelar4.Enabled = false;
                cbxEstado3.SelectedIndex = -1;
                cbxMunicipio3.SelectedIndex = -1;
                cbxLocalidad3.SelectedIndex = -1;
                colonias = 0;
            }
        }

        private void cbxEstado2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxMunicipio2.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b => b.est_nombreestado == cbxEstado2.Text).ToList();
                cbxMunicipio2.DisplayMember = "mun_nombremunicipio";
                cbxMunicipio2.ValueMember = "mun_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxEstado3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbxMunicipio3.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b => b.est_nombreestado == cbxEstado3.Text).ToList();
                cbxMunicipio3.DisplayMember = "mun_nombremunicipio";
                cbxMunicipio3.ValueMember = "mun_id";

                cbxLocalidad3.DataSource = catalogoEMLCcontroller.localidadesDGV().Where(b => b.mun_nombremunicipio == cbxMunicipio3.Text).ToList();
                cbxLocalidad3.DisplayMember = "loc_nombrelocalidad";
                cbxLocalidad3.ValueMember = "loc_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxMunicipio3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbxLocalidad3.DataSource = catalogoEMLCcontroller.localidadesDGV().Where(b => b.mun_nombremunicipio == cbxMunicipio3.Text).ToList();
                cbxLocalidad3.DisplayMember = "loc_nombrelocalidad";
                cbxLocalidad3.ValueMember = "loc_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxEstado2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                cbxMunicipio2.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b => b.est_nombreestado == cbxEstado2.Text).ToList();
                cbxMunicipio2.DisplayMember = "mun_nombremunicipio";
                cbxMunicipio2.ValueMember = "mun_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxEstadoFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var consulta = from c in catalogoEMLCcontroller.municipiosDGV()
                               select new
                               {
                                   c.mun_id,
                                   c.mun_nombremunicipio,
                                   c.est_nombreestado
                               };

                dgvMunicipios.DataSource = consulta.Where(mun => mun.est_nombreestado == cbxEstadoFiltro.Text).ToList();
                dgvMunicipios.Columns[0].HeaderText = "No.";
                dgvMunicipios.Columns[1].HeaderText = "Nombre del municipio";
                dgvMunicipios.Columns[2].HeaderText = "Nombre del estado";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxEstadoFiltro2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxMunicipioFiltro2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var consulta = from c in catalogoEMLCcontroller.localidadesDGV()
                               select new
                               {
                                   c.loc_id,
                                   c.loc_nombrelocalidad,
                                   c.mun_nombremunicipio,
                                   c.est_nombreestado
                               };

                dgvLocalidades.DataSource = consulta.Where(loc => loc.mun_nombremunicipio == cbxMunicipioFiltro2.Text).ToList();
                dgvLocalidades.Columns[0].HeaderText = "No.";
                dgvLocalidades.Columns[1].HeaderText = "Nombre de la localidad";
                dgvLocalidades.Columns[2].HeaderText = "Nombre del municipio";
                dgvLocalidades.Columns[3].HeaderText = "Nombre del estado";

                cbxEstado2.SelectedIndex = -1;
                cbxMunicipio2.SelectedIndex = -1;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxLocalidadFiltro3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var consulta = from c in catalogoEMLCcontroller.coloniasDGV()
                               select new
                               {
                                   c.col_id,
                                   c.col_nombrecolonia,
                                   c.loc_nombrelocalidad,
                                   c.mun_nombremunicipio,
                                   c.est_nombreestado
                               };

                dgvColonias.DataSource = consulta.ToList();
                dgvColonias.Columns[0].HeaderText = "No.";
                dgvColonias.Columns[1].HeaderText = "Nombre de la colonia";
                dgvColonias.Columns[2].HeaderText = "Nombre de la localidad";
                dgvColonias.Columns[3].HeaderText = "Nombre del municipio";
                dgvColonias.Columns[4].HeaderText = "Nombre del estado";

                cbxEstado3.SelectedIndex = -1;
                cbxMunicipio3.SelectedIndex = -1;
                cbxLocalidad3.SelectedIndex = -1;
            }
            catch(Exception ex)
            {
                 MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxEstado2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cbxEstadoFiltro3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //CARGAR LOS ESTADOS EN LOS COMBOBOX
                cbxMunicipioFiltro3.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b => b.est_nombreestado == cbxEstadoFiltro3.Text).ToList();
                cbxMunicipioFiltro3.DisplayMember = "mun_nombremunicipio";
                cbxMunicipioFiltro3.ValueMember = "mun_id";

                cbxLocalidadFiltro3.DataSource = catalogoEMLCcontroller.localidadesDGV().Where(b => b.mun_nombremunicipio == cbxMunicipioFiltro3.Text).ToList();
                cbxLocalidadFiltro3.DisplayMember = "loc_nombrelocalidad";
                cbxLocalidadFiltro3.ValueMember = "loc_id";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxMunicipioFiltro3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxLocalidadFiltro3.DataSource = catalogoEMLCcontroller.localidadesDGV().Where(b => b.mun_nombremunicipio == cbxMunicipioFiltro3.Text).ToList();
                cbxLocalidadFiltro3.DisplayMember = "loc_nombrelocalidad";
                cbxLocalidadFiltro3.ValueMember = "loc_id";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxMunicipio3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxLocalidad3.DataSource = catalogoEMLCcontroller.localidadesDGV().Where(b => b.mun_nombremunicipio == cbxMunicipio3.Text).ToList();
                cbxLocalidad3.DisplayMember = "loc_nombrelocalidad";
                cbxLocalidad3.ValueMember = "loc_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if(estados == 1) //PARA QUE EL BOTON EJECUTE LAS ACCIONES DE INSERCION
                {
                    if (txtEstado.Text == "")
                    {
                        lblValidacion1.Text = "* Complete el campo";
                        lblValidacion1.Visible = true;
                    }
                    else
                    {
                        long est_repetido = catalogoEMLCcontroller.estadosDGV().Where(es => es.est_nombreestado == txtEstado.Text).LongCount();

                        if (est_repetido == 0)
                        {
                            lblValidacion1.Visible = false;

                            DialogResult mensaje = MessageBox.Show("¿Desea agregar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if(mensaje == DialogResult.Yes)
                            {
                                catalogoEMLCcontroller.agregar_Estado(txtEstado.Text);

                                MessageBox.Show("¡El registro fue dado de alta exitosamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                groupBox1.Enabled = false;
                                txtEstado.Clear();
                                btnGuardar.Enabled = false;
                                btnCancelar.Enabled = false;
                                estados = 0;

                                var consulta = from c in catalogoEMLCcontroller.estadosDGV()
                                               select new
                                               {
                                                   c.est_id,
                                                   c.est_nombreestado
                                               };

                                dgvEstados.DataSource = consulta.ToList();
                                dgvEstados.Columns[0].HeaderText = "No.";
                                dgvEstados.Columns[1].HeaderText = "Nombre del estado";
                            }
                        }
                        else
                        {
                            lblValidacion1.Text = "* El estado ya se encuentra registrado";
                            lblValidacion1.Visible = true;
                        }
                    }
                }
                else if(estados == 2)
                {
                    if(txtEstado.Text == "")
                    {
                        lblValidacion1.Text = "* Complete el campo";
                        lblValidacion1.Visible = true;
                    }
                    else
                    {
                        bool verificar = false;

                        if(nombreestado == txtEstado.Text)
                        {
                            verificar = true;
                        }
                        else
                        {
                            long est_repetido = catalogoEMLCcontroller.estadosDGV().Where(es => es.est_nombreestado == txtEstado.Text).LongCount();

                            if(est_repetido == 0)
                            {
                                verificar = false;
                            }
                            else
                            {
                                verificar = true;
                            }
                        }

                        if(verificar == false)
                        {
                            DialogResult mensaje = MessageBox.Show("¿Desea actualizar el registro?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if(mensaje == DialogResult.Yes)
                            {
                                catalogoEMLCcontroller.actualizar_Estado(idestado, txtEstado.Text);
                                MessageBox.Show("¡El registro ha sido actualizado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                groupBox1.Enabled = false;
                                txtEstado.Clear();
                                btnGuardar.Enabled = false;
                                btnCancelar.Enabled = false;
                                estados = 0;
                                idestado = 0;
                                nombreestado = "";

                                var consulta = from c in catalogoEMLCcontroller.estadosDGV()
                                               select new
                                               {
                                                   c.est_id,
                                                   c.est_nombreestado
                                               };

                                dgvEstados.DataSource = consulta.ToList();
                                dgvEstados.Columns[0].HeaderText = "No.";
                                dgvEstados.Columns[1].HeaderText = "Nombre del estado";
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("¡No se ejecuta ninguna acción!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult mensaje = MessageBox.Show("¿Desea actualizar el registro: " + dgvEstados.CurrentRow.Cells[1].Value.ToString() + "?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(mensaje == DialogResult.Yes)
                {
                    idestado = long.Parse(dgvEstados.CurrentRow.Cells[0].Value.ToString());
                    nombreestado = dgvEstados.CurrentRow.Cells[1].Value.ToString();
                    txtEstado.Text = dgvEstados.CurrentRow.Cells[1].Value.ToString();
                    groupBox1.Enabled = true;
                    txtEstado.Focus();
                    btnGuardar.Enabled = true;
                    btnCancelar.Enabled = true;
                    estados = 2;
                }
                else
                {
                    idestado = 0;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxEstado3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxMunicipio3.DataSource = catalogoEMLCcontroller.municipiosDGV().Where(b => b.est_nombreestado == cbxEstado3.Text).ToList();
                cbxMunicipio3.DisplayMember = "mun_nombremunicipio";
                cbxMunicipio3.ValueMember = "mun_id";

                cbxLocalidad3.DataSource = catalogoEMLCcontroller.localidadesDGV().Where(b => b.mun_nombremunicipio == cbxMunicipio3.Text).ToList();
                cbxLocalidad3.DisplayMember = "loc_nombrelocalidad";
                cbxLocalidad3.ValueMember = "loc_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBusqueda4_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtBusqueda4.Text == "")
                {
                    var consulta = from c in catalogoEMLCcontroller.coloniasDGV()
                                   select new
                                   {
                                       c.col_id,
                                       c.col_nombrecolonia,
                                       c.loc_nombrelocalidad,
                                       c.mun_nombremunicipio,
                                       c.est_nombreestado
                                   };

                    dgvColonias.DataSource = consulta.ToList();
                    dgvColonias.Columns[0].HeaderText = "No.";
                    dgvColonias.Columns[1].HeaderText = "Nombre de la colonia";
                    dgvColonias.Columns[2].HeaderText = "Nombre de la localidad";
                    dgvColonias.Columns[3].HeaderText = "Nombre del municipio";
                    dgvColonias.Columns[4].HeaderText = "Nombre del estado";
                }
                else
                {
                    var consulta = from c in catalogoEMLCcontroller.coloniasDGV()
                                   select new
                                   {
                                       c.col_id,
                                       c.col_nombrecolonia,
                                       c.loc_nombrelocalidad,
                                       c.mun_nombremunicipio,
                                       c.est_nombreestado
                                   };

                    dgvColonias.DataSource = consulta.Where(b => b.est_nombreestado == cbxEstadoFiltro3.Text && b.mun_nombremunicipio == cbxMunicipioFiltro3.Text && b.loc_nombrelocalidad == cbxLocalidadFiltro3.Text && b.col_nombrecolonia.Contains(txtBusqueda4.Text)).ToList();
                    dgvColonias.Columns[0].HeaderText = "No.";
                    dgvColonias.Columns[1].HeaderText = "Nombre de la colonia";
                    dgvColonias.Columns[2].HeaderText = "Nombre de la localidad";
                    dgvColonias.Columns[3].HeaderText = "Nombre del municipio";
                    dgvColonias.Columns[4].HeaderText = "Nombre del estado";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            DialogResult mensaje = MessageBox.Show("¿Desea agregar un nuevo municipio?", "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(mensaje == DialogResult.Yes)
            {
                groupBox1.Enabled = true;
                txtEstado.Focus();
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                estados = 1;
            }
            else
            {
                groupBox1.Enabled = false;
                txtEstado.Clear();
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                estados = 0;
            }
        }
    }
}
