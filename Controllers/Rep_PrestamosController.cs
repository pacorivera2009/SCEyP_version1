using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Rep_PrestamosController
    {
        Conexion bd = new Conexion();
        public IQueryable<v_rep_prestamos> prestamos(long id)
        {
            return bd.v_rep_prestamos.Where(r => r.pre_id == id).OrderBy(r => r.pag_id);
        }
    }
}
