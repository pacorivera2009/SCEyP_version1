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
    
    public partial class asociados
    {
        public asociados()
        {
            this.autorizacion = new HashSet<autorizacion>();
            this.avales = new HashSet<avales>();
            this.comprobante = new HashSet<comprobante>();
            this.domicilio = new HashSet<domicilio>();
            this.fotosasociados = new HashSet<fotosasociados>();
            this.identificacion = new HashSet<identificacion>();
            this.prestamos = new HashSet<prestamos>();
        }
    
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
    
        public virtual estados estados { get; set; }
        public virtual municipios municipios { get; set; }
        public virtual localidades localidades { get; set; }
        public virtual colonias colonias { get; set; }
        public virtual ICollection<autorizacion> autorizacion { get; set; }
        public virtual ICollection<avales> avales { get; set; }
        public virtual ICollection<comprobante> comprobante { get; set; }
        public virtual ICollection<domicilio> domicilio { get; set; }
        public virtual ICollection<fotosasociados> fotosasociados { get; set; }
        public virtual ICollection<identificacion> identificacion { get; set; }
        public virtual ICollection<prestamos> prestamos { get; set; }
    }
}
