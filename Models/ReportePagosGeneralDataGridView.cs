using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ReportePagosGeneralDataGridView
    {
        public long aso_id { get; set; }
        public string aso_nombre { get; set; }
        public decimal pag_importe { get; set; }
        public decimal pag_interes { get; set; }
        public decimal pag_total { get; set; }
        public DateTime pag_fechapago { get; set; }
        public Nullable<System.DateTime> pag_fechapagado { get; set; }
        public decimal pag_porpagar { get; set; }

        public List<ReportePagosGeneralDataGridView> dgvPagosGeneralReporte()
        {
            using (var bd = new Conexion())
            {
                var listado = bd.v_rep_generalpagos.Select(a => new ReportePagosGeneralDataGridView()
                {
                    aso_id = a.aso_id,
                    aso_nombre = a.aso_nombrecompleto,
                    pag_porpagar = a.pag_porpagar,
                    pag_fechapago = a.pag_fechapago,
                    pag_importe = a.pag_importe,
                    pag_interes = a.pag_interes,
                    pag_total = a.pag_total,
                    pag_fechapagado = a.pag_fechapagado               
                });

                return listado.ToList();

            }
        }
    }
}
