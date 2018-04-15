using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Relacion_Socios_AdeudanController
    {
        public List<v_rep_socios_adeudo> relacion_socios()
        {
            using (var bd = new Conexion())
            {
                return bd.v_rep_socios_adeudo.ToList();
            }
        }
    }
}
