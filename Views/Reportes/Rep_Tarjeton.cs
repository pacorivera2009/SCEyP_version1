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
using Microsoft.Reporting.WinForms;

namespace Views.Reportes
{
    public partial class Rep_Tarjeton : Form
    {
        Rep_TarjetonController tarjeton = new Rep_TarjetonController();

        public long idasociado { get; set; }
        public long idprestamo { get; set; }
        public Rep_Tarjeton()
        {
            InitializeComponent();
        }

        private void Rep_Tarjeton_Load(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetBD2", tarjeton.pagos(idprestamo)));
                reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSetBD1", tarjeton.asociados(idasociado)));
                reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSetBD3", tarjeton.prestamos(idprestamo)));

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
