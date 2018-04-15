using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Historial_Pagos_Controller
    {
        public List<v_rep_consulta_operaciones_realizadas> relacion_pagos_socio(long id)
        {
            using (var bd = new Conexion())
            {
                return bd.v_rep_consulta_operaciones_realizadas.Where(a => a.aso_id == id).ToList();
            }
        }
        public asociados asociados (long id)
        {
            using (var bd = new Conexion())
            {
                return bd.asociados.Where(a => a.aso_id == id).FirstOrDefault();
            }
        }
    }
}
