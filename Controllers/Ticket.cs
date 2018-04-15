using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Controllers
{
    public class Ticket
    {
        public IQueryable<v_rep_ticket> ticket_pago(long id)
        {
            var bd = new Conexion();

            return bd.v_rep_ticket.Where(t => t.ope_id == id);
        }
    }
}
