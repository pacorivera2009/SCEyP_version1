using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ReporteTransaccionesDataGridView
    {
        public long aso_id { get; set; }
        public string aso_nombre { get; set; }
        public decimal his_subtotal { get; set; }
        public decimal his_interes { get; set; }
        public decimal his_total { get; set; }
        public DateTime his_fecha { get; set; }

        public List<ReporteTransaccionesDataGridView> dgvTransaccionesdGeneralReporte()
        {
            using (var bd = new Conexion())
            {
                var listado = bd.v_rep_generaltransacciones.Select(a => new ReporteTransaccionesDataGridView()
                {
                    aso_id = a.aso_id,
                    aso_nombre = a.aso_nombrecompleto,
                    his_subtotal = a.tra_subtotal,
                    his_interes = a.tra_interes,
                    his_total = a.tra_total,
                    his_fecha = a.tra_fecha
                });

                return listado.ToList();
            }
        }

    }
}
