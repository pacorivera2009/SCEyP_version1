using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ReporteContabilidadDataGridView
    {
        public long con_id { get; set; }
        public String con_concepto { get; set; }
        public decimal con_monto { get; set; }
        public decimal con_interes { get; set; }
        public decimal con_total { get; set; }
        public DateTime con_fecha { get; set; }

        public List<ReporteContabilidadDataGridView> dgvcontabilidadreportes()
        {
            using (var bd = new Conexion())
            {
                var listado = bd.v_rep_contabilidad.Select(a => new ReporteContabilidadDataGridView()
                {
                    con_id = a.con_id,
                    con_concepto = a.con_concepto,
                    con_monto = a.con_monto,
                    con_interes = a.con_interes,
                    con_total = a.con_total,
                    con_fecha = a.con_fecha
                });
                return listado.ToList();
            }
        }
    }
}
