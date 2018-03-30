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
    public partial class Prestamos_contratos : Form
    {
        //CONTROLADORES
        PrestamosController prestamoscontroller = new PrestamosController();
        public long id { get; set; }
        public Prestamos_contratos()
        {
            InitializeComponent();
        }

        private void Prestamos_contratos_Load(object sender, EventArgs e)
        {
            try
            {
                ToolTip notificacion = new ToolTip();
                notificacion.AutoPopDelay = 8000;
                notificacion.InitialDelay = 1000;
                notificacion.ReshowDelay = 500;
                notificacion.ShowAlways = true;
                notificacion.SetToolTip(this.btnBuscar, "De clíc aquí para mostrar el contrato del prestamo seleccionado");
                notificacion.SetToolTip(this.btnTarjeton, "De clíc aquí mostrar el tarjetón de pago del prestamo seleccionado");

                var prestamossocio = prestamoscontroller.prestamos(id);

                var resultado = from p in prestamossocio
                                select new
                                {
                                    p.pre_id,
                                    prestamopedido = "$ " + p.pre_credito,
                                    p.pre_cuotas,
                                    p.pre_tipo,
                                    p.pre_fechaprestamo
                                };

                dgvPersonal.DataSource = resultado.ToList();

                dgvPersonal.Columns[0].HeaderText = "Contrato";
                dgvPersonal.Columns[1].HeaderText = "Crédito solicitado";
                dgvPersonal.Columns[2].HeaderText = "Cuotas";
                dgvPersonal.Columns[3].HeaderText = "Tipo";
                dgvPersonal.Columns[4].HeaderText = "Fecha de solicitud";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvPersonal.CurrentRow == null)
                {
                    MessageBox.Show("¡No hay prestamos solicitados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    long idprestamo = Convert.ToInt64(dgvPersonal.CurrentRow.Cells[0].Value.ToString());

                    Reportes.Rep_Prestamos rep_prestamos = new Reportes.Rep_Prestamos();
                    rep_prestamos.idprestamo = idprestamo;
                    rep_prestamos.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void contratosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnBuscar_Click(sender, e);
        }

        private void btnTarjeton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPersonal.CurrentRow == null)
                {
                    MessageBox.Show("¡No hay prestamos solicitados!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    long idprestamo = Convert.ToInt64(dgvPersonal.CurrentRow.Cells[0].Value.ToString());

                    Reportes.Rep_Tarjeton tarjeton = new Reportes.Rep_Tarjeton();
                    tarjeton.idprestamo = idprestamo;
                    tarjeton.idasociado = id;
                    tarjeton.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tarjetonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnTarjeton_Click(sender, e);
        }
    }
}
