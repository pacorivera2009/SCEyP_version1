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
    
    public partial class v_rep_historial_pagos_socio
    {
        public long aso_id { get; set; }
        public string aso_nombre { get; set; }
        public string aso_domicilio { get; set; }
        public string aso_telefono { get; set; }
        public string aso_movil { get; set; }
        public string aso_correoelectronico { get; set; }
        public Nullable<long> cantidad_prestamos { get; set; }
        public Nullable<long> prestamos_por_pagar { get; set; }
        public Nullable<long> prestamos_pagados { get; set; }
        public long tra_id { get; set; }
        public decimal tra_subtotal { get; set; }
        public decimal tra_interes { get; set; }
        public System.DateTime tra_fecha { get; set; }
        public long ope_id { get; set; }
        public long pre_id { get; set; }
        public System.DateTime ope_fecha { get; set; }
    }
}
