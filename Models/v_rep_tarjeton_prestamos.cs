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
    
    public partial class v_rep_tarjeton_prestamos
    {
        public long pre_id { get; set; }
        public decimal pre_credito { get; set; }
        public int pre_cuotas { get; set; }
        public string pre_tipo { get; set; }
        public int pre_interes { get; set; }
        public int pre_semenas { get; set; }
        public int pre_meses { get; set; }
        public int pre_interesfinal { get; set; }
        public decimal pre_totalinteres { get; set; }
        public decimal pre_total { get; set; }
        public System.DateTime pre_fechaprestamo { get; set; }
        public System.DateTime pre_primerpago { get; set; }
        public string pre_observaciones { get; set; }
        public long pre_asociado { get; set; }
    }
}
