using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class LoginController
    {
        Seguridad seguridad = new Seguridad();

        //METODO PARA INICIAR SESION EN EL SISTEMA
        public usuarios inicioSesion(string usuario, string contrasena)
        {
            string password = seguridad.Encriptar(contrasena); //CONTRASEÑA ENCRIPTADA

            using (var bd = new Conexion())
            {
                return bd.usuarios.Where(u => u.usu_usuario == usuario && u.usu_contrasena == password).FirstOrDefault();
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

        public long personalRegistrado()
        {
            using (var bd = new Conexion())
            {
                return bd.personal.Count();
            }
        }
    }
}
