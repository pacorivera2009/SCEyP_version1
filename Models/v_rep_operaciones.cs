//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class v_rep_operaciones
    {
        public long tra_id { get; set; }
        public System.DateTime tra_fecha { get; set; }
        public decimal tra_subtotal { get; set; }
        public decimal tra_interes { get; set; }
        public decimal tra_total { get; set; }
        public long tra_pago { get; set; }
        public long tra_operacion { get; set; }
        public long ope_id { get; set; }
        public System.DateTime ope_fecha { get; set; }
        public System.DateTime ope_hora { get; set; }
    }
}
