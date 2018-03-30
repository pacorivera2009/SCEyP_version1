using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class Rep_ContabilidadController
    {
        Conexion bd = new Conexion();
        public IQueryable<v_rep_contabilidad> contabilidad()
        {
            return bd.v_rep_contabilidad.OrderBy(c => c.con_id);
        }
    }
}
