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
    
    public partial class v_rep_ticket
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
        public long pag_id { get; set; }
        public decimal pag_credito { get; set; }
        public decimal pag_importe { get; set; }
        public decimal pag_interes { get; set; }
        public decimal pag_total { get; set; }
        public System.DateTime pag_fechapago { get; set; }
        public decimal pag_pagado { get; set; }
        public Nullable<System.DateTime> pag_fechapagado { get; set; }
        public long pag_prestamo { get; set; }
        public long aso_id { get; set; }
        public string aso_nombre { get; set; }
        public string aso_apellidos { get; set; }
        public string aso_sexo { get; set; }
        public System.DateTime aso_fechanacimiento { get; set; }
        public string aso_estadocivil { get; set; }
        public string aso_domicilio { get; set; }
        public string aso_referenciasdomicilio { get; set; }
        public string aso_referenciascalles { get; set; }
        public int aso_codigopostal { get; set; }
        public long aso_colonia { get; set; }
        public long aso_localidad { get; set; }
        public long aso_municipio { get; set; }
        public long aso_estado { get; set; }
        public string aso_tipodeingreso { get; set; }
        public string aso_telefono { get; set; }
        public string aso_movil { get; set; }
        public string aso_correoelectronico { get; set; }
        public string aso_nombretrabajo { get; set; }
        public string aso_ocupacion { get; set; }
        public string aso_domiciliotrabajo { get; set; }
        public string aso_telefonotrabajo { get; set; }
        public string aso_extension { get; set; }
        public string aso_nombrefamiliar { get; set; }
        public string aso_parentesco { get; set; }
        public string aso_telefonofamiliar { get; set; }
        public string aso_movilfamiliar { get; set; }
        public System.DateTime aso_fechaingreso { get; set; }
    }
}