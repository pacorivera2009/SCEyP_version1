using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Rep_TarjetonController
    {
        public List<v_rep_tarjeton_pagos> pagos(long id)
        {
            List<v_rep_tarjeton_pagos> pagosV;

            using (Conexion bd = new Conexion())
            {
                pagosV = bd.v_rep_tarjeton_pagos.Where(p => p.pre_id == id).OrderBy(p => p.pag_id).ToList();
            }

            return pagosV;
        }

        public List<v_rep_tarjeton_asociados> asociados(long id)
        {
            List<v_rep_tarjeton_asociados> asociadosV;

            using (Conexion bd = new Conexion())
            {
                asociadosV = bd.v_rep_tarjeton_asociados.Where(a => a.aso_id == id).ToList();
            }

            return asociadosV;
        }

        public List<v_rep_tarjeton_prestamos> prestamos(long id)
        {
            List<v_rep_tarjeton_prestamos> prestamosV;

            using (Conexion bd = new Conexion())
            {
                prestamosV = bd.v_rep_tarjeton_prestamos.Where(p => p.pre_id == id).ToList();
            }

            return prestamosV;
        }
    }
}
