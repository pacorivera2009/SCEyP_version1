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
    
    public partial class v_rep_generalpagos
    {
        public long aso_id { get; set; }
        public string aso_nombrecompleto { get; set; }
        public decimal pag_importe { get; set; }
        public decimal pag_interes { get; set; }
        public decimal pag_total { get; set; }
        public System.DateTime pag_fechapago { get; set; }
        public Nullable<System.DateTime> pag_fechapagado { get; set; }
        public decimal pag_porpagar { get; set; }
    }
}