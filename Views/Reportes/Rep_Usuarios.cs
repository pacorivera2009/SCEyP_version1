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
using Microsoft.Reporting.WinForms;

namespace Views.Reportes
{
    public partial class Rep_Usuarios : Form
    {
        Rep_UsuariosController repusuarioscontroller = new Rep_UsuariosController();
        public Rep_Usuarios()
        {
            InitializeComponent();
        }

        private void Rep_Usuarios_Load(object sender, EventArgs e)
        {
            IQueryable datos = repusuarioscontroller.personal();

            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSetBD", datos));

            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            //Seleccionamos el zoom que deseamos utilizar. En este caso un 100%
            reportViewer1.ZoomPercent = 100;

            this.reportViewer1.RefreshReport();
        }
    }
}
