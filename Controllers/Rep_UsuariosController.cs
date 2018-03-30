using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Rep_UsuariosController
    {
        Conexion bd = new Conexion();
        public IQueryable<v_rep_usuarios> personal()
        {
            return bd.v_rep_usuarios.OrderBy(r => r.per_id);
        }
    }
}
