using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class OperacionesController
    {
        public List<v_rep_operaciones> operaciones(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.v_rep_operaciones.Where(o => o.ope_id == id).ToList();
            }
        }
    }
}
