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
    
    public partial class pagos
    {
        public pagos()
        {
            this.transacciones = new HashSet<transacciones>();
        }
    
        public long pag_id { get; set; }
        public decimal pag_credito { get; set; }
        public decimal pag_importe { get; set; }
        public decimal pag_interes { get; set; }
        public decimal pag_total { get; set; }
        public System.DateTime pag_fechapago { get; set; }
        public decimal pag_pagado { get; set; }
        public Nullable<System.DateTime> pag_fechapagado { get; set; }
        public long pag_prestamo { get; set; }
    
        public virtual prestamos prestamos { get; set; }
        public virtual ICollection<transacciones> transacciones { get; set; }
    }
}