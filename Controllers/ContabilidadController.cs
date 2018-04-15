using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;

namespace Controllers
{
    public class ContabilidadController
    {

        //MODELO GENERICO
        ReporteContabilidadDataGridView reportecontabilidadDGV = new ReporteContabilidadDataGridView();
        public List<ReporteContabilidadDataGridView> contabilidad()
        {
            return reportecontabilidadDGV.dgvcontabilidadreportes();
        }
        public List<ReporteContabilidadDataGridView> contabilidadPeriodo(DateTime fechainicio, DateTime fechafin)
        {
            return reportecontabilidadDGV.dgvcontabilidadreportes().Where(c => c.con_fecha >= fechainicio && c.con_fecha <= fechafin).ToList();
        }

        public List<ContabilidadTotalesModel> contabilidadTotales(string fecha_inicio, string fecha_fin)
        {
            List<ContabilidadTotalesModel> lista = new List<ContabilidadTotalesModel>();

            using (var bd = new Conexion())
            {
                decimal entradasT = 0, salidasT = 0, totalT = 0;

                if (fecha_inicio == null || fecha_fin == null || fecha_inicio == "" || fecha_fin == "")
                {
                    long totalEntradas = bd.contabilidad.Where(c => c.con_operacion == "E" || c.con_operacion == "C").LongCount();
                    long totalSalidas = bd.contabilidad.Where(c => c.con_operacion == "S").LongCount();

                    if (totalEntradas == 0)
                    {
                        entradasT = 0;
                    }
                    else
                    {
                        entradasT = bd.contabilidad.Where(c => c.con_operacion == "E" || c.con_operacion == "C").Sum(c => c.con_total);
                    }

                    if (totalSalidas == 0)
                    {
                        salidasT = 0;
                    }
                    else
                    {
                        salidasT = bd.contabilidad.Where(c => c.con_operacion == "S").Sum(c => c.con_total);
                    }

                    totalT = entradasT + salidasT;

                    ContabilidadTotalesModel conta;
                    conta = new ContabilidadTotalesModel(entradasT, salidasT, totalT)
                    {
                        totalentradas = entradasT,
                        totalsalidas = salidasT,
                        total = totalT
                    };

                    lista.Add(conta);
                }
                else
                {
                    DateTime fecha_i = Convert.ToDateTime(fecha_inicio);
                    DateTime fecha_f = Convert.ToDateTime(fecha_fin);

                    long totalEntradas = bd.contabilidad.Where(c => (c.con_operacion == "E" || c.con_operacion == "C") && (c.con_fecha >= fecha_i && c.con_fecha <= fecha_f)).LongCount();
                    long totalSalidas = bd.contabilidad.Where(c =>( c.con_operacion == "S") && (c.con_fecha >= fecha_i && c.con_fecha <= fecha_f)).LongCount();

                    if (totalEntradas == 0)
                    {
                        entradasT = 0;
                    }
                    else
                    {
                        entradasT = bd.contabilidad.Where(c => (c.con_operacion == "E" || c.con_operacion == "C") && (c.con_fecha >= fecha_i && c.con_fecha <= fecha_f)).Sum(c => c.con_total);
                    }

                    if (totalSalidas == 0)
                    {
                        salidasT = 0;
                    }
                    else
                    {
                        salidasT = bd.contabilidad.Where(c => (c.con_operacion == "S")&& (c.con_fecha >= fecha_i && c.con_fecha <= fecha_f)).Sum(c => c.con_total);
                    }

                    totalT = entradasT + salidasT;

                    ContabilidadTotalesModel conta;
                    conta = new ContabilidadTotalesModel(entradasT, salidasT, totalT)
                    {
                        totalentradas = entradasT,
                        totalsalidas = salidasT,
                        total = totalT
                    };

                    lista.Add(conta);
                }

                return lista;
            }
        }

        public void agregarContabilidad(string concepto, decimal monto, decimal interes, decimal total, DateTime fecha, string operacion)
        {
            using (var bd = new Conexion())
            {
                contabilidad contabilidad = new contabilidad()
                {
                    con_concepto = concepto,
                    con_monto = monto,
                    con_interes = interes,
                    con_total = total,
                    con_fecha = fecha,
                    con_operacion = operacion
                };

                long consulta = bd.contabilidad.LongCount();

                if (consulta == 0)
                {
                    bd.Database.ExecuteSqlCommand("ALTER TABLE contabilidad AUTO_INCREMENT=1");
                }
                else
                {
                    long maxVal = bd.contabilidad.Max(c => c.con_id) + 1;

                    bd.Database.ExecuteSqlCommand("ALTER TABLE contabilidad AUTO_INCREMENT={0}", maxVal);
                }

                bd.contabilidad.Add(contabilidad);
                bd.SaveChanges();
            }
        }

        public void CorteMensual()
        {
            using (var bd = new Conexion())
            {
                long contabilidadc = bd.contabilidad.Count();

                if(contabilidadc != 0)
                {
                    //OBTENEMOS VALORES//
                    string concepto = "Corte mensual anterior " + DateTime.Now.ToShortDateString();
                    decimal monto = bd.contabilidad.Sum(c => c.con_monto);
                    decimal interes = bd.contabilidad.Sum(c => c.con_interes);
                    decimal total = bd.contabilidad.Sum(c => c.con_total);
                    DateTime fecha = DateTime.Now;

                    //LLENAMOS EL OBJETO//
                    var contabilidad = new contabilidad
                    {
                        con_concepto = concepto,
                        con_interes = interes,
                        con_monto = monto,
                        con_operacion = "C",
                        con_total = total,
                        con_fecha = fecha
                    };

                    //CONTADOR PARA VERIFICAR EL NUMERO DE REGISTROS -- PARA EVITAR QUE SE INSERTE EL VALOR DE CORTE MENSUAL EN EL HISTORICO EN CASO QUE YA HAYA REGISTROS
                    long contarcontabilidad_historico = bd.contabilidad_historico.Count();

                    if (contarcontabilidad_historico == 0)
                    {
                        //REALIZAMOS LA INSERCION DESDE CONTABILIDAD A CONTABILIDAD_HISTORICO PARA RESPALDO
                        bd.Database.ExecuteSqlCommand("INSERT INTO contabilidad_historico(con_concepto, con_monto, con_interes, con_total, con_fecha, con_operacion, con_fecharespaldo) SELECT con_concepto, con_monto, con_interes, con_total, con_fecha, con_operacion, now() FROM contabilidad");
                    }
                    else
                    {
                        //REALIZAMOS LA INSERCION DESDE CONTABILIDAD A CONTABILIDAD_HISTORICO PARA RESPALDO
                        bd.Database.ExecuteSqlCommand("INSERT INTO contabilidad_historico(con_concepto, con_monto, con_interes, con_total, con_fecha, con_operacion, con_fecharespaldo) SELECT con_concepto, con_monto, con_interes, con_total, con_fecha, con_operacion, now() FROM contabilidad");
                    }

                    //ELIMINAMOS LOS REGISTROS DE CONTABILIDAD
                    bd.Database.ExecuteSqlCommand("DELETE FROM contabilidad");

                    //VERIFICACION PARA MODIFICAR EL AUTO_INCREMENT A 1 DE NUEVO o AL ULTIMO REGISTRADO
                    long consulta = bd.contabilidad.LongCount();

                    if (consulta == 0)
                    {
                        bd.Database.ExecuteSqlCommand("ALTER TABLE contabilidad AUTO_INCREMENT=1");
                    }
                    else
                    {
                        long maxVal = bd.contabilidad.Max(c => c.con_id) + 1;

                        bd.Database.ExecuteSqlCommand("ALTER TABLE contabilidad AUTO_INCREMENT={0}", maxVal);
                    }

                    bd.contabilidad.Add(contabilidad);
                    bd.SaveChanges();
                }
            }
        }
    }
}
