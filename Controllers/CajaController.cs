using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class CajaController
    {
        //MOSTRANDO PAGOS PENDIENTES
        public List<v_rep_pagospendientes> pagosDGV(long id)
        {
            using(var bd = new Conexion())
            {
                return bd.v_rep_pagospendientes.Where(p => p.aso_id == id).OrderBy(p => p.pag_fechapago).ToList();
            }
        }

        //AGREGAR PAGO
        public void agregarpago(long id, decimal pagado)
        {
            using (var bd = new Conexion())
            {
                var pago = bd.pagos.Where(p => p.pag_id == id).FirstOrDefault();

                pago.pag_pagado = pago.pag_pagado + pagado;
                pago.pag_fechapagado = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                bd.SaveChanges();
            }
        }

        //AGREGAR CONTABILIDAD
        public void agregarContabilidad(decimal monto, decimal interes, string concepto)
        {
            using (var bd = new Conexion())
            {
                contabilidad contabilidad = new contabilidad()
                {
                    con_fecha = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    con_interes = interes,
                    con_monto = monto,
                    con_concepto = concepto,
                    con_operacion = "E",
                    con_total = monto + interes
                };

                bd.contabilidad.Add(contabilidad);
                bd.SaveChanges();
            }
        }

        public long agregarOperacion(DateTime fecha, DateTime hora)
        {
            long id_operacion = 0;

            using (var bd = new Conexion())
            {
                operaciones operaciones = new operaciones()
                {
                    ope_fecha = fecha,
                    ope_hora = hora
                };

                bd.operaciones.Add(operaciones);
                bd.SaveChanges();

                id_operacion = bd.operaciones.Max(o => o.ope_id);
            }

            return id_operacion;
        }

        public void agregarTransaccion(DateTime fecha, decimal total, long idoperacion, long idpago)
        {
            using(var bd = new Conexion())
            {
                transacciones transacciones = new transacciones()
                {
                    tra_fecha = fecha,
                    tra_operacion = idoperacion,
                    tra_interes = 0,
                    tra_subtotal = total,
                    tra_pago = idpago,
                    tra_total = total
                };

                bd.transacciones.Add(transacciones);
                bd.SaveChanges();
            }
        }
    }
}
