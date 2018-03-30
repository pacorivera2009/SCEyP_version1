using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.Entity.Infrastructure;

namespace Controllers
{
    public class CatalogoEMLCController
    {
        public long contarEstados()
        {
            using (var bd = new Conexion())
            {
                return bd.estados.LongCount();
            }
        }

        public long contarMunicipios()
        {
            using (var bd = new Conexion())
            {
                return bd.municipios.LongCount();
            }
        }

        public long contarLocalidades()
        {
            using (var bd = new Conexion())
            {
                return bd.localidades.LongCount();
            }
        }

        public long contarColonias()
        {
            using (var bd = new Conexion())
            {
                return bd.colonias.LongCount();
            }
        }

        public List<v_rep_estados> estadosDGV()
        {
            using (var bd = new Conexion())
            {
                return bd.v_rep_estados.OrderBy(e => e.est_nombreestado).ToList();
            }
        }

        public List<v_rep_municipios> municipiosDGV()
        {
            using (var bd = new Conexion())
            {
                return bd.v_rep_municipios.OrderBy(m => m.est_nombreestado).OrderBy(m => m.mun_nombremunicipio).ToList();
            }
        }

        public List<v_rep_localidades> localidadesDGV()
        {
            using (var bd = new Conexion())
            {
                return bd.v_rep_localidades.OrderBy(l => l.est_nombreestado).OrderBy(l => l.mun_nombremunicipio).OrderBy(l => l.loc_nombrelocalidad).ToList();
            }
        }

        public List<v_rep_colonias> coloniasDGV()
        {
            using (var bd = new Conexion())
            {
                return bd.v_rep_colonias.OrderBy(c => c.est_nombreestado).OrderBy(c => c.mun_nombremunicipio).OrderBy(c => c.loc_nombrelocalidad).OrderBy(c => c.col_nombrecolonia).ToList();
            }
        }

        //OPERACIONES DE INSERCION, ACTUALIZACION Y ELIMINACION DE ESTADOS
        public void agregar_Estado(string estado)
        {
            using (var bd = new Conexion())
            {
                estados estados = new estados
                {
                    est_nombreestado = estado
                };

                long consulta = bd.estados.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE estados AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.estados.Max(e => e.est_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE estados AUTO_INCREMENT={0}", maxVal);
                }

                bd.estados.Add(estados);
                bd.SaveChanges();
            }
        }

        public void actualizar_Estado(long id, string estado)
        {
            using (var bd = new Conexion())
            {
                var busqueda = bd.estados.Find(id);

                busqueda.est_nombreestado = estado;

                bd.SaveChanges();
            }
        }

        public void eliminar_Estado(long id)
        {
            using (var bd = new Conexion())
            {
                var eliminar = bd.estados.Where(e => e.est_id == id).First();

                bd.estados.Remove(eliminar);

                bd.SaveChanges();
            }
        }

        //OPERACIONES DE INSERCION, ACTUALIZACION y ELIMINACION DE MUNICIPIOS

        public void agregar_Municipio(string municipio, long ide)
        {
            using (var bd = new Conexion())
            {
                municipios municipios = new municipios
                {
                    mun_nombremunicipio = municipio,
                    mun_estado = ide
                };

                long consulta = bd.municipios.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE municipios AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.municipios.Max(m => m.mun_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE municipios AUTO_INCREMENT={0}", maxVal);
                }

                bd.municipios.Add(municipios);
                bd.SaveChanges();
            }
        }

        public void actualizar_Municipio(long id, string municipio, long ide)
        {
            using(var bd = new Conexion())
            {
                var consulta = bd.municipios.Find(id);

                consulta.mun_nombremunicipio = municipio;
                consulta.mun_estado = ide;

                bd.SaveChanges();
            }
        }

        public void eliminar_Municipio(long id)
        {
            using(var bd = new Conexion())
            {
                municipios mun = bd.municipios.Where(m => m.mun_id == id).First();

                bd.municipios.Remove(mun);

                bd.SaveChanges();
            }
        }

        //OPERACIONES DE INSERCION, ACTUALIZACION y ELIMINACION DE LOCALIDADES
        
        public void agregar_Localidad(string localidad, long idM)
        {
            using (var bd = new Conexion())
            {
                localidades localidades = new localidades
                {
                    loc_nombrelocalidad = localidad,
                    loc_municipio = idM
                };

                long consulta = bd.localidades.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE localidades AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.localidades.Max(l => l.loc_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE localidades AUTO_INCREMENT={0}", maxVal);
                }

                bd.localidades.Add(localidades);
                bd.SaveChanges();
            }
        }

        public void actualizar_Localidad(long id, string localidad, long idM)
        {
            using (var bd = new Conexion())
            {
                var consulta = bd.localidades.Find(id);

                consulta.loc_nombrelocalidad = localidad;
                consulta.loc_municipio = idM;

                bd.SaveChanges();
            }
        }

        public void eliminar_Localidad(long id)
        {
            using (var bd = new Conexion())
            {
                var eliminar = bd.localidades.Where(l => l.loc_id == id).First();

                bd.localidades.Remove(eliminar);

                bd.SaveChanges();
            }
        }

        //OPERACIONES DE INSERCION, ACTUALIZACION Y ELIMINACION DE COLONIAS

        public void agregar_Colonia(string colonia, long idl)
        {
            using (var bd = new Conexion())
            {
                colonias colonias = new colonias
                {
                    col_nombrecolonia = colonia,
                    col_localidad = idl
                };

                long consulta = bd.colonias.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE colonias AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.colonias.Max(c => c.col_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE colonias AUTO_INCREMENT={0}", maxVal);
                }

                bd.colonias.Add(colonias);
                bd.SaveChanges();
            }
        }

        public void actualizar_Colonia(long id, string colonia, long idl)
        {
            using(var bd = new Conexion())
            {
                var busqueda = bd.colonias.Find(id);

                busqueda.col_nombrecolonia = colonia;
                busqueda.col_localidad = idl;

                bd.SaveChanges();
            }
        }

        public void eliminar_Colonia(long id)
        {
            using (var bd = new Conexion())
            {
                var eliminar = bd.colonias.Where(c => c.col_id == id).First();

                bd.colonias.Remove(eliminar);

                bd.SaveChanges();
            }
        }

        //COMPROBACION DE POSIBILIDAD DE ELIMINACION DE MUNCIPIO, COLONIA, ESTADO, LOCALIDAD
        public bool comprobar_Eliminacion_Estado(long id)
        {
            bool comprobacion = true;

            using (var bd = new Conexion())
            {
                long consultaMunicipio = bd.municipios.Where(m => m.mun_estado == id).LongCount();
                long consultaSocios = bd.asociados.Where(a => a.aso_estado == id).LongCount();
                long consultaPersonal = bd.personal.Where(p => p.per_estado == id).LongCount();

                if(consultaMunicipio == 0 && consultaSocios == 0 && consultaPersonal == 0)
                {
                    comprobacion = false;
                }
                else
                {
                    comprobacion = true;
                }
            }

                return comprobacion;
        }

        public bool comprobar_Eliminacion_Municipio(long id)
        {
            bool comprobacion = true;

            using (var bd = new Conexion())
            {
                long consultaLocalidades = bd.localidades.Where(l => l.loc_municipio == id).LongCount();
                long consultaSocios = bd.asociados.Where(a => a.aso_municipio == id).LongCount();
                long consultaPersonal = bd.personal.Where(p => p.per_municipio == id).LongCount();

                if (consultaLocalidades == 0 && consultaSocios == 0 && consultaPersonal == 0)
                {
                    comprobacion = false;
                }
                else
                {
                    comprobacion = true;
                }
            }

            return comprobacion;
        }

        public bool comprobar_Eliminacion_Localidades(long id)
        {
            bool comprobacion = true;

            using (var bd = new Conexion())
            {
                long consultaColonias = bd.colonias.Where(c => c.col_localidad == id).LongCount();
                long consultaSocios = bd.asociados.Where(a => a.aso_localidad == id).LongCount();
                long consultaPersonal = bd.personal.Where(p => p.per_localidad == id).LongCount();

                if (consultaColonias == 0 && consultaSocios == 0 && consultaPersonal == 0)
                {
                    comprobacion = false;
                }
                else
                {
                    comprobacion = true;
                }
            }

            return comprobacion;
        }

        public bool comprobar_Eliminacion_Colonias(long id)
        {
            bool comprobacion = true;

            using (var bd = new Conexion())
            {
                long consultaSocios = bd.asociados.Where(a => a.aso_colonia == id).LongCount();
                long consultaPersonal = bd.personal.Where(p => p.per_colonia == id).LongCount();

                if (consultaSocios == 0 && consultaPersonal == 0)
                {
                    comprobacion = false;
                }
                else
                {
                    comprobacion = true;
                }
            }

            return comprobacion;
        }
    }
}
