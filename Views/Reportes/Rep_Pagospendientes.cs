using Microsoft.Reporting.WinForms;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Views.Reportes
{
    public partial class Rep_Pagospendientes : Form
    {
        public Rep_Pagospendientes()
        {
            InitializeComponent();
        }

        private void Rep_Pagospendientes_Load(object sender, EventArgs e)
        {
            try
            {
                var bd = new Conexion();

                DateTime fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString()).AddMonths(1);

                IQueryable datos = bd.v_rep_pagospendientes.Where(p => p.pag_fechapago <= fecha && p.C_p_pag_total___p_pag_pagado_ > 0).OrderBy(p => p.pag_fechapago);

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
