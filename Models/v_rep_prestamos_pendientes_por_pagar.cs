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
    
    public partial class v_rep_prestamos_pendientes_por_pagar
    {
        public long aso_id { get; set; }
        public long pre_id { get; set; }
        public decimal pre_credito { get; set; }
        public int pre_cuotas { get; set; }
        public string pre_tipo { get; set; }
        public int pre_interes { get; set; }
        public System.DateTime pre_fechaprestamo { get; set; }
    }
}
