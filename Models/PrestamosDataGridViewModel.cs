using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    using System.Collections.Generic;
    using System.Data.Objects.SqlClient;

    public partial class PrestamosDataGridViewModel
    {
        public long pre_id { get; set; }
        public decimal pre_credito { get; set; }
        public int pre_cuotas { get; set; }
        public string pre_tipo { get; set; }
        public System.DateTime pre_fechasolicitud { get; set; }
        public long aso_id { get; set; }
        public string aso_nombre { get; set; }

        public List<PrestamosDataGridViewModel> prestamosDGV()
        {
            var bd = new Conexion();

            var consulta = (from p in bd.prestamos
                            join a in bd.asociados on p.pre_asociado equals a.aso_id
                            select new
                            {
                                p.pre_id,
                                prestamodado = p.pre_credito,
                                p.pre_cuotas,
                                p.pre_tipo,
                                p.pre_fechaprestamo,
                                a.aso_id,
                                nombre = a.aso_nombre + " " + a.aso_apellidos
                            });

            var listado = consulta.Select(p => new PrestamosDataGridViewModel()
            {
                pre_id = p.pre_id,
                pre_credito = p.prestamodado,
                pre_cuotas = p.pre_cuotas,
                pre_tipo = p.pre_tipo,
                pre_fechasolicitud = p.pre_fechaprestamo,
                aso_id = p.aso_id,
                aso_nombre = p.nombre
            });

            return listado.ToList();
        }
    }
}
