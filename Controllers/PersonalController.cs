using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Windows.Forms;

namespace Controllers
{
    public class PersonalController
    {
        //SEGURIDAD
        Seguridad seguridad = new Seguridad();

        //ENTIDAD GENERADA
        PersonalDataGridViewModel personalDGV = new PersonalDataGridViewModel();

        //AGREGAR PERSONAL A LA BD
        public long agregarPersonal(string nombre, string apellidos, string sexo, DateTime fechanacimiento, string estadocivil, string domicilio, int codigopostal, long estado, long municipio, long localidad, long colonia, string telefono, string movil, string correo)
        {
            using (var bd = new Conexion())
            {
                long id = 1;

                personal personal = new personal
                {
                    per_nombre = nombre,
                    per_apellidos = apellidos,
                    per_sexo = sexo,
                    per_fechanacimiento = fechanacimiento,
                    per_estadocivil = estadocivil,
                    per_domicilio = domicilio,
                    per_codigopostal = codigopostal,
                    per_estado = estado,
                    per_municipio = municipio,
                    per_localidad = localidad,
                    per_colonia = colonia,
                    per_movil = movil,
                    per_telefono = telefono,
                    per_correoelectronico = correo
                };

                long consulta = bd.personal.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE personal AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.personal.Max(p => p.per_id) + 1;

                    id = maxVal;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE personal AUTO_INCREMENT={0}", maxVal);
                }

                bd.personal.Add(personal);
                bd.SaveChanges();

                return id;
            }
        }

        //AGREGAR FOTO DE PERFIL
        public void agregarFoto(long id, byte[] fotografia)
        {
            using (var bd = new Conexion())
            {
                fotospersonal fotospersonal = new fotospersonal
                {
                    fot_personal = id,
                    fot_fotoperfil = fotografia
                };

                long consulta = bd.fotospersonal.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE fotospersonal AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.fotospersonal.Max(f => f.fot_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE fotospersonal AUTO_INCREMENT={0}", maxVal);
                }

                bd.fotospersonal.Add(fotospersonal);
                bd.SaveChanges();
            }
        }

        //COMBOBOX ESTADOS
        public List<estados> comboBoxEstados()
        {
            using (var bd = new Conexion())
            {
                return bd.estados.OrderBy(e => e.est_nombreestado).ToList();
            }
        }

        //COMBOBOX MUNCIPIOS
        public List<municipios> comboBoxMunicipios(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.municipios.Where(m => m.mun_estado == id).OrderBy(m => m.mun_nombremunicipio).ToList();
            }
        }

        //COMBOBOX LOCALIDADES
        public List<localidades> comboBoxLocalidades(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.localidades.Where(l => l.loc_municipio == id).OrderBy(l => l.loc_nombrelocalidad).ToList();
            }
        }

        //COMBOBOX COLONIAS
        public List<colonias> comboBoxColonias(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.colonias.Where(c => c.col_localidad == id).OrderBy(c => c.col_nombrecolonia).ToList();
            }
        }

        //LLENAR DATAGRIDVIEW CON EL PERSONAL REGISTRADO
        public List<PersonalDataGridViewModel> dataGridViewPersonal()
        {
            using (var bd = new Conexion())
            {
                return personalDGV.dataGridPersonal().OrderBy(p => p.per_id).ToList();
            }
        }

        //LLENAR DATAGRIDVIEW CON LA BUSQUEDA DADA
        public List<PersonalDataGridViewModel> dataGridViewbuscarPersonal(string nombre, string apellidos, DateTime fechanacimiento)
        {
            using (var bd = new Conexion())
            {
                return personalDGV.dataGridPersonal().OrderBy(p => p.per_id).Where(p => p.per_nombre == nombre && p.per_apellidos == apellidos && p.per_fechanacimiento == fechanacimiento).ToList();
            }
        }

        public fotospersonal fotoPersonal(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.fotospersonal.Where(f => f.fot_personal == id).FirstOrDefault();
            }
        }

        //BUSCAR USUARIO//
        public usuarios usuarios(string usuario)
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

        //BUSCAR PERSONAL//
        public personal personal(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.personal.Where(p => p.per_id == id).FirstOrDefault();
            }
        }

        //MODIFICAR PERSONAL//
        public void actualizarPersonal(long id, string nombre, string apellidos, string sexo, DateTime fechanacimiento, string estadocivil, string domicilio, int codigopostal, long estado, long municipio, long localidad, long colonia, string telefono, string movil, string correo)
        {
            using (var bd = new Conexion())
            {
                var personaeditar = bd.personal.FirstOrDefault(p => p.per_id == id);

                personaeditar.per_nombre = nombre;
                personaeditar.per_apellidos = apellidos;
                personaeditar.per_sexo = sexo;
                personaeditar.per_fechanacimiento = fechanacimiento;
                personaeditar.per_estadocivil = estadocivil;
                personaeditar.per_domicilio = domicilio;
                personaeditar.per_codigopostal = codigopostal;
                personaeditar.per_estado = estado;
                personaeditar.per_municipio = municipio;
                personaeditar.per_localidad = localidad;
                personaeditar.per_colonia = colonia;
                personaeditar.per_telefono = telefono;
                personaeditar.per_movil = movil;
                personaeditar.per_correoelectronico = correo;

                bd.SaveChanges();
            }
        }
        
        //ACTUALIZAR FOTOGRAFIA
        public void actualizarFoto(long id, byte[] fotografia)
        {
            using (var bd = new Conexion())
            {
                var consulta = bd.fotospersonal.FirstOrDefault(f => f.fot_personal == id);

                consulta.fot_fotoperfil = fotografia;

                bd.SaveChanges();
            }
        }

        //ELIMINAR USUARIO
        public void eliminarUsuario(long id)
        {
            using (var bd = new Conexion())
            {
                var consulta = bd.usuarios.FirstOrDefault(u => u.usu_personal == id);

                if(consulta != null)
                {
                    bd.usuarios.Remove(consulta);

                    bd.SaveChanges();
                }
            }
        }

        //ELIMINAR FOTO DE PERFIL
        public void eliminarFoto(long id)
        {
            using (var bd = new Conexion())
            {
                var consulta = bd.fotospersonal.FirstOrDefault(f => f.fot_personal == id);

                if (consulta != null)
                {
                    bd.fotospersonal.Remove(consulta);

                    bd.SaveChanges();
                }
            }
        }

        //ELIMINAR PERSONAL
        public void eliminarPersonal(long id)
        {
            using (var bd = new Conexion())
            {
                var consulta = bd.personal.FirstOrDefault(p => p.per_id == id);

                if (consulta != null)
                {
                    bd.personal.Remove(consulta);

                    bd.SaveChanges();
                }
            }
        }
    }
}
