using Microsoft.Reporting.WinForms;
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

namespace Views.Reportes
{
    public partial class Rep_Prestamos : Form
    {
        Rep_PrestamosController repprestamoscontroller = new Rep_PrestamosController();
        public long idprestamo { get; set; }
        public Rep_Prestamos()
        {
            InitializeComponent();
        }

        private void Rep_Prestamos_Load(object sender, EventArgs e)
        {
            try
            {
                IQueryable datos = repprestamoscontroller.prestamos(idprestamo);

                reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSetBD", datos));

                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
                reportViewer1.ZoomPercent = 100;


                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
