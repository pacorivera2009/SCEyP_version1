using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Rep_SociosController
    {
        public List<v_rep_socios> socios()
        {
            List<v_rep_socios> sociosV;

            using(Conexion bd = new Conexion())
            {
                sociosV = bd.v_rep_socios.ToList();
            }

            return sociosV;
        }

        public List<v_rep_socios_buen_historial> sociosH(long id)
        {
            List<v_rep_socios_buen_historial> sociosV;

            using (Conexion bd = new Conexion())
            {
                sociosV = bd.v_rep_socios_buen_historial.Where(s => s.aso_id == id).ToList();
            }

            return sociosV;
        }

        public List<v_rep_socios_buen_aval> sociosA(long id)
        {
            List<v_rep_socios_buen_aval> sociosV;

            using (Conexion bd = new Conexion())
            {
                sociosV = bd.v_rep_socios_buen_aval.Where(s => s.aso_id == id).ToList();
            }

            return sociosV;
        }

        public string tipo_ingreso(long id)
        {
            string tipo = null;

            using (var bd = new Conexion())
            {
                var resultado = bd.asociados.Where(a => a.aso_id == id).FirstOrDefault();
                tipo = resultado.aso_tipodeingreso;
            }

            return tipo;
        }
    }
}
