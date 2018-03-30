using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class UsuariosController
    {
        //SEGURIDAD
        Seguridad seguridad = new Seguridad();

        //OBTENER DATOS DEL PERSONAL
        public personal personal(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.personal.Where(p => p.per_id == id).FirstOrDefault();
            }
        }

        //OBTENER FOTO DE PERFIL
        public fotospersonal fotospersonal(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.fotospersonal.Where(f => f.fot_personal == id).FirstOrDefault();
            }
        }

        //OBTENER EL USUARIO
        public usuarios usuarios(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.usuarios.Where(u => u.usu_personal == id).FirstOrDefault();
            }
        }

        //OBTENER EL USUARIO SI EXISTE
        public usuarios usuarios2(string usuario)
        {
            using (var bd = new Conexion())
            {
                return bd.usuarios.Where(u => u.usu_usuario == usuario).FirstOrDefault();
            }
        }

        //AGREGAR USUARIO
        public void agregarUsuario(long id, string usuario, string contrasena, string cargo, string estadocuenta)
        {
            using (var bd = new Conexion())
            {
                contrasena = seguridad.Encriptar(contrasena);

                usuarios usuarios = new usuarios
                {
                    usu_usuario = usuario,
                    usu_contrasena = contrasena,
                    usu_cargo = cargo,
                    usu_estadocuenta = estadocuenta,
                    usu_personal = id
                };

                long consulta = bd.usuarios.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE usuarios AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.usuarios.Max(u => u.usu_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE usuarios AUTO_INCREMENT={0}", maxVal);
                }

                bd.usuarios.Add(usuarios);
                bd.SaveChanges();
            }
        }

        //ACTUALIZAR USUARIO
        public void actualizarUsuario(long id, string contrasena, string cargo, string estadocuenta)
        {
            using (var bd = new Conexion())
            {
                contrasena = seguridad.Encriptar(contrasena);

                var consulta = bd.usuarios.FirstOrDefault(u => u.usu_personal == id);

                consulta.usu_contrasena = contrasena;
                consulta.usu_cargo = cargo;
                consulta.usu_estadocuenta = estadocuenta;
                consulta.usu_personal = id;

                bd.SaveChanges();
            }
        }

        //ELIMINAR USUARIO//
        public void eliminarUsuario(long id)
        {
            using (var bd = new Conexion())
            {
                var consulta = bd.usuarios.FirstOrDefault(u => u.usu_personal == id);

                bd.usuarios.Remove(consulta);

                bd.SaveChanges();
            }
        }
    }
}
