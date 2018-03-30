using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class MenuController
    {
        //METODO PARA INICIAR SESION EN EL SISTEMA
        public usuarios datosUsuario(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.usuarios.Where(u => u.usu_personal == id).FirstOrDefault();
            }
        }

        //METODO PARA OBTENER LA FOTO DE PERFIL DEL PERSONAL
        public fotospersonal obtenerFoto(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.fotospersonal.Where(f => f.fot_personal == id).FirstOrDefault();
            }

        }

        //METODO PARA OBTENER LA INFORMACION DEL USUARIO
        public personal obtenerDatos(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.personal.Where(p => p.per_id == id).FirstOrDefault();
            }
        }
    }
}
