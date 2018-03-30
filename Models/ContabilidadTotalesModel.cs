using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ContabilidadTotalesModel
    {
        public decimal totalentradas { get; set; }
        public decimal totalsalidas { get; set; }
        public decimal total { get; set; }

        public ContabilidadTotalesModel(decimal entradas, decimal salidas, decimal tot)
        {
            totalentradas = entradas;
            totalsalidas = salidas;
            total = tot;
        }
    }
}
