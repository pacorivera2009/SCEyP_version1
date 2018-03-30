using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class CajaCobro_Controller
    {

        public List<v_rep_pagos_atrasados_socio> pagos_atrasados(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.v_rep_pagos_atrasados_socio.Where(p => p.pre_asociado == id).OrderBy(o => o.pag_fechapago).ToList();
            }
        }

        public List<v_rep_pagos_socio> pagos_socios(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.v_rep_pagos_socio.Where(p => p.pre_asociado == id).OrderBy(o => o.pag_fechapago).ToList();
            }
        }

        //public List<v_hora_actual> fecha_actual()
        //{
        //    using (var bd = new Conexion())
        //    {
        //        var fecha_actual = from fecha in bd.v_hora_actual;
        //    }
        //}
    }
}
