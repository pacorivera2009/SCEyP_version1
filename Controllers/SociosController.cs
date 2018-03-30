using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.Entity;
using System.Data;

namespace Controllers
{
    public class SociosController
    {
        //SEGURIDAD
        Seguridad seguridad = new Seguridad();

        //ENTIDAD//
        SociosDataGridViewModel sociosDGV = new SociosDataGridViewModel();

        //METODO PARA RELLENAR DATAGRIDVIEW DE SOCIOS
        public List<SociosDataGridViewModel> dataGridViewSocios()
        {
            using (var bd = new Conexion())
            {
                return sociosDGV.dataGridSocios().OrderBy(a => a.aso_id).ToList();
            }
        }

        //METODO PARA RELLENAR DATAGRIDVIEW DE SOCIOS MEDIANTE UNA BUSQUEDA
        public List<SociosDataGridViewModel> dataGridViewbuscarSocios(string nombre, string apellidos, DateTime fechanacimiento)
        {
            using (var bd = new Conexion())
            {
                return sociosDGV.dataGridSocios().OrderBy(a => a.aso_id).Where(a => a.aso_nombre == nombre && a.aso_apellidos == apellidos && a.aso_fechanacimiento == fechanacimiento).ToList();
            }
        }

        //DATOS DEL SOCIO
        public asociados asociados(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.asociados.Where(a => a.aso_id == id).FirstOrDefault();
            }
        }

        //FOTO DE PERFIL DEL SOCIO
        public fotosasociados fotosasociados(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.fotosasociados.Where(f => f.fot_asociado == id).FirstOrDefault();
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

        //DATOS DEL ESTADO
        public estados estados(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.estados.Where(e => e.est_id == id).FirstOrDefault();
            }
        }

        //DATOS DEL MUNICIPIO
        public municipios municipios(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.municipios.Where(m => m.mun_id == id).FirstOrDefault();
            }
        }

        //DATOS DE LA LOCALIDAD
        public localidades localidades(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.localidades.Where(l => l.loc_id == id).FirstOrDefault();
            }
        }

        //DATOS DE LA COLONIA
        public colonias colonias(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.colonias.Where(c => c.col_id == id).FirstOrDefault();
            }
        }

        //DATOS DE AVAL//
        public avales aval(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.avales.Where(a => a.ava_asociado == id).FirstOrDefault();
            }
        }

        //AGREGAR SOCIOS//
        public long agregarSocios(string nombre, string apellidos, DateTime fechanacimiento, string sexo, string estadocivil, string domicilio, string referenciasdomicilio, string entrecalles, int codigopostal, long colonia, long localidad, long municipio, long estado, string tipoingreso, string telefono, string movil, string correo, string nombretrabajo, string ocupacion, string domiciliotrabajo, string telefonotrabajo, string extension, string nombrefamiliar, string parentesco, string telefonofamiliar, string movilfamiliar, DateTime fechaingreso)
        {
            using (var bd = new Conexion())
            {
                asociados asociados = new asociados
                {
                    aso_nombre = nombre,
                    aso_apellidos = apellidos,
                    aso_fechanacimiento = fechanacimiento,
                    aso_sexo = sexo,
                    aso_estadocivil = estadocivil,
                    aso_domicilio = domicilio,
                    aso_codigopostal = codigopostal,
                    aso_referenciascalles = entrecalles,
                    aso_referenciasdomicilio = referenciasdomicilio,
                    aso_estado = estado,
                    aso_municipio = municipio,
                    aso_localidad = localidad,
                    aso_colonia = colonia,
                    aso_telefono = telefono,
                    aso_movil = movil,
                    aso_correoelectronico = correo,
                    aso_tipodeingreso = tipoingreso,
                    aso_fechaingreso = fechaingreso,
                    aso_nombretrabajo = nombretrabajo,
                    aso_ocupacion = ocupacion,
                    aso_domiciliotrabajo = domiciliotrabajo,
                    aso_telefonotrabajo = telefonotrabajo,
                    aso_extension = extension,
                    aso_nombrefamiliar = nombrefamiliar,
                    aso_parentesco = parentesco,
                    aso_telefonofamiliar = telefonofamiliar,
                    aso_movilfamiliar = movilfamiliar
                };

                long consulta = bd.asociados.LongCount();
                long id = 0;

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE asociados AUTO_INCREMENT=1");
                    id = 1;
                }
                else
                {
                    long maxVal = bd.asociados.Max(a => a.aso_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE asociados AUTO_INCREMENT={0}", maxVal);
                    id = maxVal;
                }

                bd.asociados.Add(asociados);
                bd.SaveChanges();

                return id;
            }
        }

        //AGREGAR FOTO DEL SOCIO
        public void agregarFoto(long id, byte[] foto)
        {
            using (var bd = new Conexion())
            {
                fotosasociados fotosasociados = new fotosasociados
                {
                    fot_asociado = id,
                    fot_fotoperfil = foto
                };

                long consulta = bd.fotosasociados.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE fotosasociados AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.asociados.Max(a => a.aso_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE fotosasociados AUTO_INCREMENT={0}", maxVal);
                }

                bd.fotosasociados.Add(fotosasociados);
                bd.SaveChanges();
            }
        }

        //AGREGAR IDENTIFICACION DEL SOCIO
        public void agregarIdentificacion(long id, byte[]foto, string lado)
        {
            using (var bd = new Conexion())
            {
                identificacion identificacion = new identificacion
                {
                    ide_asociado =id,
                    ide_identificacion = foto, 
                    ide_lado = lado
                };

                long consulta = bd.identificacion.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE identificacion AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.identificacion.Max(i => i.ide_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE identificacion AUTO_INCREMENT={0}", maxVal);
                }

                bd.identificacion.Add(identificacion);
                bd.SaveChanges();
            }
        }

        //AGREGAR COMPROBANTE DE DOMICILIO DEL SOCIO
        public void agregarComprobante(long id, byte[] foto)
        {
            using (var bd = new Conexion())
            {
                comprobante comprobante = new comprobante
                {
                    com_asociado = id,
                    com_comprobante = foto
                };

                long consulta = bd.comprobante.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE comprobante AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.comprobante.Max(c => c.com_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE comprobante AUTO_INCREMENT={0}", maxVal);
                }

                bd.comprobante.Add(comprobante);
                bd.SaveChanges();
            }
        }

        //AGREGAR AUTORIZACION DEL SOCIO (CREDENCIALES)
        public void agregarAutorizacion(long id, string nip)
        {
            using (var bd = new Conexion())
            {
                string nipautorizacion = seguridad.Encriptar(nip);

                autorizacion autorizacion = new autorizacion
                {
                    aut_asociado = id,
                    aut_nip = nipautorizacion
                };

                long consulta = bd.autorizacion.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE autorizacion AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.autorizacion.Max(a => a.aut_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE autorizacion AUTO_INCREMENT={0}", maxVal);
                }

                bd.autorizacion.Add(autorizacion);
                bd.SaveChanges();
            }
        }

        //AGREGAR AVAL DEL SOCIO
        public void agregarAVAL(long id, string nombre, string apellidos, string domicilio, string telefono, string movil, bool es, long aval)
        {
            using (var bd = new Conexion())
            {
                avales avalador = new avales
                {
                    ava_nombre = nombre,
                    ava_apellidos = apellidos,
                    ava_domicilio = domicilio,
                    ava_telefono = telefono,
                    ava_movil = movil,
                    ava_socio = es,
                    ava_idsocio = aval,
                    ava_asociado = id
                };

                long consulta = bd.avales.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE avales AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.avales.Max(a => a.ava_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE avales AUTO_INCREMENT={0}", maxVal);
                }

                bd.avales.Add(avalador);
                bd.SaveChanges();
            }
        }

        public void agregarDomicilio(long id, byte[] foto)
        {
            using (var bd = new Conexion())
            {
                domicilio domicilio = new domicilio
                {
                    dom_asociado = id,
                    dom_foto = foto
                };

                long consulta = bd.domicilio.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE domicilio AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.domicilio.Max(d => d.dom_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE domicilio AUTO_INCREMENT={0}", maxVal);
                }

                bd.domicilio.Add(domicilio);
                bd.SaveChanges();
            }
        }

        public autorizacion autorizacionAVAL(long id, string nip)
        {
            string nipautorizacion = seguridad.Encriptar(nip);

            using (var bd = new Conexion())
            {
                return bd.autorizacion.Where(a => a.aut_asociado == id && a.aut_nip == nipautorizacion).FirstOrDefault();
            }
        }

        //BUSQUEDA DE DOMICILIO (comprobante)
        public comprobante buscarDomicilio(long id)
        {
            using (var bd = new Conexion())
            {
                DataTable dt = new DataTable();
                return bd.comprobante.Where(c => c.com_asociado == id).FirstOrDefault();
            }
        }

        //BUSQUEDA DE IDENTIFICACION
        public identificacion buscarIdentificacion(long id, string del_tra)
        {
            using (var bd = new Conexion())
            {
                return bd.identificacion.Where(i => i.ide_asociado == id && i.ide_lado == del_tra).FirstOrDefault();
            }
        }

        //BUSQUEDA DE CAPTURADO (domicilio)
        public domicilio buscarMapsCapturadoDom(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.domicilio.Where(b => b.dom_asociado == id).FirstOrDefault();
            }
        }

        //BAJA DEL SOCIO DEL SISTEMA//
        public void eliminarSocio(long id)
        {
            using (var bd = new Conexion())
            {
                long contar_prestamos = bd.prestamos.Where(p => p.pre_asociado == id).LongCount();

                //VERIFICA SI HAY PRESTAMOS REGISTRADOS
                if (contar_prestamos > 0)
                {
                    //BUSQUEDA DE LOS PRESTAMOS DEL SOCIO
                    var prestamos_socio = bd.prestamos.Where(p => p.pre_asociado == id).ToList();

                    //RECORREMOS EL ARREGLO DE VALORES PARA OBTENER LOS PAGOS RELACIONADOS AL PRESTAMO
                    foreach (var prestamos in prestamos_socio)
                    {
                        //ELIMINAMOS LOS PAGOS
                        bd.Database.ExecuteSqlCommand("DELETE FROM pagos WHERE pag_prestamo = {0}", prestamos.pre_id);
                    }

                    //ELIMINAMOS LOS PRESTAMOS
                    bd.Database.ExecuteSqlCommand("DELETE FROM prestamos WHERE pre_asociado = {0}", id);
                }

                long contar_avales = bd.avales.Where(av => av.ava_asociado == id).LongCount();

                //VERIFICAMOS QUE ESTE REGISTRADO EN LA TABLA AVAL
                if (contar_avales > 0)
                {
                    //ELIMINAMOS LOS AVALES
                    bd.Database.ExecuteSqlCommand("DELETE FROM avales WHERE ava_asociado = {0}", id);
                }

                long contar_domicilios = bd.domicilio.Where(dom => dom.dom_asociado == id).LongCount();

                //VERIFICAMOS QUE TENGA UN DOMICILIO CAPTURADO DESDE MAPS
                if(contar_domicilios > 0)
                {
                    //ELIMINAMOS EL DOMICILIO
                    bd.Database.ExecuteSqlCommand("DELETE FROM domicilio WHERE dom_asociado = {0}", id);
                }

                //ACTUALIZAR LA INFORMACION DONDE EL SOCIO FUNGE COMO AVAL//
                bd.Database.ExecuteSqlCommand("UPDATE avales SET ava_idsocio = null, ava_socio = false WHERE ava_idsocio = {0}", id);

                //ELIMINAMOS LA IDENTIFICACION OFICIAL
                bd.Database.ExecuteSqlCommand("DELETE FROM identificacion WHERE ide_asociado = {0}", id);

                //ELIMINAMOS COMPROBANTE DE DOMICILIO
                bd.Database.ExecuteSqlCommand("DELETE FROM comprobante WHERE com_asociado = {0}", id);

                //ELIMINAMOS LAS FOTOS
                bd.Database.ExecuteSqlCommand("DELETE FROM fotosasociados WHERE fot_asociado = {0}", id);

                //ELIMINAMOS AUTORIZACION
                bd.Database.ExecuteSqlCommand("DELETE FROM autorizacion WHERE aut_asociado = {0}", id);

                //ELIMINAMOS LOS  SOCIOS
                bd.Database.ExecuteSqlCommand("DELETE FROM asociados WHERE aso_id = {0}", id);

            }
        }

        //ACTUALIZACION DEL SOCIO
        public void actualizaSocio(string nombre, string apellidos, DateTime fechanacimiento, string sexo, string estadocivil, string domicilio, string referenciasdomicilio, string entrecalles, int codigopostal, long colonia, long localidad, long municipio, long estado, string tipoingreso, string telefono, string movil, string correo, string nombretrabajo, string ocupacion, string domiciliotrabajo, string telefonotrabajo, string extension, string nombrefamiliar, string parentesco, string telefonofamiliar, string movilfamiliar, DateTime fechaingreso, long id, byte[] geolocalizacion, byte[] fotoperfil, byte[] identificaciondel, byte[] identificaciontra, byte[] comprobante)
        {
            using (var bd = new Conexion())
            {
                //VERIFICAR SI HAY:
                //GEOLOCALIZACION
                long contar_geolocalizacion = bd.domicilio.Where(dom => dom.dom_asociado == id).LongCount();

                if(contar_geolocalizacion > 0)
                {
                    bd.Database.ExecuteSqlCommand("UPDATE domicilio SET dom_foto = {0} WHERE dom_asociado = {1}", geolocalizacion, id);
                }
                else
                {
                    bd.Database.ExecuteSqlCommand("INSERT INTO domicilio(dom_foto, dom_asociado) VALUES({0}, {1})", geolocalizacion, id);
                }

                //ACTUALIZAR FOTO DE PERFIL
                bd.Database.ExecuteSqlCommand("UPDATE fotosasociados SET fot_fotoperfil = {0} WHERE fot_asociado = {1}", fotoperfil, id);

                //ACTUALIZAR COMPROBANTE DE DOMICILIO
                bd.Database.ExecuteSqlCommand("UPDATE comprobante SET com_comprobante = {0} WHERE com_asociado = {1}", comprobante, id);
                
                //ACTUALIZAR IDENTIFICACION (delantera)
                bd.Database.ExecuteSqlCommand("UPDATE identificacion SET ide_identificacion = {0} WHERE ide_asociado = {1} AND ide_lado = 'DELANTERA'", identificaciondel, id);

                //ACTUALIZAR IDENTIFICACION (trasera)
                bd.Database.ExecuteSqlCommand("UPDATE identificacion SET ide_identificacion = {0} WHERE ide_asociado = {1} AND ide_lado = 'TRASERA'", identificaciontra, id);

                //ACTUALIZACION DE SOCIOS//
                var socioeditar = bd.asociados.FirstOrDefault(a => a.aso_id == id);

                socioeditar.aso_nombre = nombre;
                socioeditar.aso_apellidos = apellidos;
                socioeditar.aso_fechanacimiento = fechanacimiento;
                socioeditar.aso_sexo = sexo;
                socioeditar.aso_estadocivil = estadocivil;
                socioeditar.aso_domicilio = domicilio;
                socioeditar.aso_codigopostal = codigopostal;
                socioeditar.aso_referenciascalles = entrecalles;
                socioeditar.aso_referenciasdomicilio = referenciasdomicilio;
                socioeditar.aso_estado = estado;
                socioeditar.aso_municipio = municipio;
                socioeditar.aso_localidad = localidad;
                socioeditar.aso_colonia = colonia;
                socioeditar.aso_telefono = telefono;
                socioeditar.aso_movil = movil;
                socioeditar.aso_correoelectronico = correo;
                socioeditar.aso_tipodeingreso = tipoingreso;
                socioeditar.aso_fechaingreso = fechaingreso;
                socioeditar.aso_nombretrabajo = nombretrabajo;
                socioeditar.aso_ocupacion = ocupacion;
                socioeditar.aso_domiciliotrabajo = domiciliotrabajo;
                socioeditar.aso_telefonotrabajo = telefonotrabajo;
                socioeditar.aso_extension = extension;
                socioeditar.aso_nombrefamiliar = nombrefamiliar;
                socioeditar.aso_parentesco = parentesco;
                socioeditar.aso_telefonofamiliar = telefonofamiliar;
                socioeditar.aso_movilfamiliar = movilfamiliar;

                bd.SaveChanges();
            }  
        }

        //SACAR LOS DATOS DE DOMICILIO DEL SOCIO
        public string DatosDomicilio(long id)
        {
            using (var bd = new Conexion())
            {
                asociados asociado;
                estados estado;
                municipios muncipio;
                localidades localidad;
                colonias colonia;

                string domicilio;

                asociado = bd.asociados.Where(a => a.aso_id == id).FirstOrDefault();

                estado = bd.estados.Where(e => e.est_id == asociado.aso_estado).FirstOrDefault();

                muncipio = bd.municipios.Where(m => m.mun_id == asociado.aso_municipio).FirstOrDefault();

                localidad = bd.localidades.Where(l => l.loc_id == asociado.aso_localidad).FirstOrDefault();

                colonia = bd.colonias.Where(c => c.col_id == asociado.aso_colonia).FirstOrDefault();

                domicilio = ", C.P: " + asociado.aso_codigopostal.ToString() + ", Colonia: " + colonia.col_nombrecolonia + ", Localidad: " + localidad.loc_nombrelocalidad +
                            ", Municipio: " + muncipio.mun_nombremunicipio + ", Estado: " + estado.est_nombreestado;

                return domicilio;
            }
        }

        //CAMBIAR NIP DE AUTORIZACION
        public void cambiarNIP(long id, string NIP)
        {
            using (var bd = new Conexion())
            {
                string nipautorizacion = seguridad.Encriptar(NIP);

                bd.Database.ExecuteSqlCommand("UPDATE autorizacion SET aut_nip = {0} WHERE aut_asociado = {1}", nipautorizacion, id);

            }
        }
    }
}
