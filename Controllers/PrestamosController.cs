using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class PrestamosController
    {
        //SEGURIDAD (ENCRIPTACION)
        Seguridad seguridad = new Seguridad();

        //MODELOS
        PrestamosDataGridViewModel prestamosdatagridviewmodel = new PrestamosDataGridViewModel();

        ////AGREGAR PRESTAMO//
        public long agregarPrestamo(long id, decimal credito, int cuotas, DateTime fecha, int interes, int meses, DateTime primerpago, int interesfinal, string observaciones, int semanas, string tipo, decimal total, decimal totalinteres)
        {
            using (var bd = new Conexion())
            {
                prestamos prestamos = new prestamos
                {
                    pre_asociado = id,
                    pre_credito = credito,
                    pre_cuotas = cuotas,
                    pre_fechaprestamo = fecha,
                    pre_interes = interes,
                    pre_meses = meses,
                    pre_primerpago = primerpago,
                    pre_interesfinal = interesfinal,
                    pre_observaciones = observaciones,
                    pre_semenas = semanas,
                    pre_tipo = tipo,
                    pre_total = total,
                    pre_totalinteres = totalinteres
                };

                long consulta = bd.prestamos.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE prestamos AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.prestamos.Max(p => p.pre_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE prestamos AUTO_INCREMENT={0}", maxVal);
                }

                bd.prestamos.Add(prestamos);
                bd.SaveChanges();

                long idprestamo = bd.prestamos.Max(p => p.pre_id);
                return idprestamo;
            }
        }

        public autorizacion autorizacion(long id, string nip)
        {
            using (var bd = new Conexion())
            {
                string autorizacion = seguridad.Encriptar(nip);
                return bd.autorizacion.Where(a => a.aut_asociado == id && a.aut_nip == autorizacion).FirstOrDefault();
            }
        }

        //AGREGAR PAGOS DEL PRESTAMOS
        public void agregarPago(decimal credito, DateTime fechapago, long id, decimal total, decimal interes, decimal importe)
        {
            using (var bd = new Conexion())
            {
                pagos pagos = new pagos
                {
                    pag_credito = credito,
                    pag_fechapago = fechapago,
                    pag_prestamo = id,
                    pag_total = total,
                    pag_interes = interes,
                    pag_importe = importe
                };

                long consulta = bd.pagos.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE pagos AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.pagos.Max(p => p.pag_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE pagos AUTO_INCREMENT={0}", maxVal);
                }

                bd.pagos.Add(pagos);
                bd.SaveChanges();
            }
        }

        //LISTA DE PRESTAMOS//
        public List<prestamos> prestamos(long id)
        {
            var bd = new Conexion();

            return bd.prestamos.Where(p => p.pre_asociado == id).OrderBy(p => p.pre_id).ToList();
        }

        public List<PrestamosDataGridViewModel> prestamoscontrol()
        {
            return prestamosdatagridviewmodel.prestamosDGV();
        }

        public List<PrestamosDataGridViewModel> prestamosPeriodo(DateTime fechainicio, DateTime fechafin)
        {
            using (var bd = new Conexion())
            {
                return prestamosdatagridviewmodel.prestamosDGV().Where(p => p.pre_fechasolicitud >= fechainicio && p.pre_fechasolicitud <= fechafin).ToList();
            }
        }

        //ELIMINACION DE PAGOS CON RELACION AL PRESTAMO
        public void eliminarPagos(long id)
        {
            using (var bd = new Conexion())
            {
                var consulta = bd.pagos.Find(id);

                bd.pagos.Remove(consulta);

                bd.SaveChanges();
            }
        }

        //ELIMINACION DE PRESTAMO
        public void eliminarPrestamo(long id)
        {
            using (var bd = new Conexion())
            {
                var consulta = bd.prestamos.Find(id);

                bd.prestamos.Remove(consulta);

                bd.SaveChanges();
            }
        }
    }
}
