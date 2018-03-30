using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class Rep_DeudoresController
    {
        
        public List<v_rep_pagos_pendientes_por_pagar> pagos()
        {
            List<v_rep_pagos_pendientes_por_pagar> pagos;
            using(Conexion bd = new Conexion())
            {
                pagos = bd.v_rep_pagos_pendientes_por_pagar.ToList(); 
                return pagos;
            }
        }

        public List<v_rep_prestamos_pendientes_por_pagar> prestamos()
        {
            List<v_rep_prestamos_pendientes_por_pagar> prestamos;

            using (Conexion bd = new Conexion())
            {
                prestamos = bd.v_rep_prestamos_pendientes_por_pagar.ToList();
                return prestamos;
            }
        }

        public List<v_rep_socios_con_pagos_pendientes> socios()
        {
            List<v_rep_socios_con_pagos_pendientes> socios;

            using (Conexion bd = new Conexion())
            {
                socios = bd.v_rep_socios_con_pagos_pendientes.ToList();
                return socios;
            }
        }
    }
}
