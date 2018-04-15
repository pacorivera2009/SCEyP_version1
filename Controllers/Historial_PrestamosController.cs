using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Historial_PrestamosController
    {
        public List<v_rep_prestamos_historial> prestamos_historial(long id)
        {
            using(var bd = new Conexion())
            {
                return bd.v_rep_prestamos_historial.Where(a => a.aso_id == id).OrderBy(p => p.pre_id).ToList();
            }

        }

        public asociados asociados(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.asociados.Where(a => a.aso_id == id).FirstOrDefault();
            }
        }
    }
}
