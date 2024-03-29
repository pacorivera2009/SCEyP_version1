﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class Conexion : DbContext
    {
        public Conexion()
            : base("name=Conexion")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<accesos> accesos { get; set; }
        public DbSet<asociados> asociados { get; set; }
        public DbSet<autorizacion> autorizacion { get; set; }
        public DbSet<avales> avales { get; set; }
        public DbSet<colonias> colonias { get; set; }
        public DbSet<comprobante> comprobante { get; set; }
        public DbSet<contabilidad> contabilidad { get; set; }
        public DbSet<contabilidad_concepto> contabilidad_concepto { get; set; }
        public DbSet<contabilidad_historico> contabilidad_historico { get; set; }
        public DbSet<control_sesiones> control_sesiones { get; set; }
        public DbSet<domicilio> domicilio { get; set; }
        public DbSet<estados> estados { get; set; }
        public DbSet<fotosasociados> fotosasociados { get; set; }
        public DbSet<fotospersonal> fotospersonal { get; set; }
        public DbSet<identificacion> identificacion { get; set; }
        public DbSet<localidades> localidades { get; set; }
        public DbSet<log_sesiones> log_sesiones { get; set; }
        public DbSet<menu_sistema> menu_sistema { get; set; }
        public DbSet<municipios> municipios { get; set; }
        public DbSet<operaciones> operaciones { get; set; }
        public DbSet<pagos> pagos { get; set; }
        public DbSet<personal> personal { get; set; }
        public DbSet<prestamos> prestamos { get; set; }
        public DbSet<privilegios_usuario> privilegios_usuario { get; set; }
        public DbSet<submenu_sistema> submenu_sistema { get; set; }
        public DbSet<transacciones> transacciones { get; set; }
        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<v_contabilidad_entrada> v_contabilidad_entrada { get; set; }
        public DbSet<v_contabilidad_salida> v_contabilidad_salida { get; set; }
        public DbSet<v_rep_colonias> v_rep_colonias { get; set; }
        public DbSet<v_rep_consulta_operaciones_realizadas> v_rep_consulta_operaciones_realizadas { get; set; }
        public DbSet<v_rep_contabilidad> v_rep_contabilidad { get; set; }
        public DbSet<v_rep_estados> v_rep_estados { get; set; }
        public DbSet<v_rep_gastos_operacion> v_rep_gastos_operacion { get; set; }
        public DbSet<v_rep_generalpagos> v_rep_generalpagos { get; set; }
        public DbSet<v_rep_generaltransacciones> v_rep_generaltransacciones { get; set; }
        public DbSet<v_rep_historial_pagos_socio> v_rep_historial_pagos_socio { get; set; }
        public DbSet<v_rep_localidades> v_rep_localidades { get; set; }
        public DbSet<v_rep_municipios> v_rep_municipios { get; set; }
        public DbSet<v_rep_operaciones> v_rep_operaciones { get; set; }
        public DbSet<v_rep_pagos_atrasados_socio> v_rep_pagos_atrasados_socio { get; set; }
        public DbSet<v_rep_pagos_pendientes_por_pagar> v_rep_pagos_pendientes_por_pagar { get; set; }
        public DbSet<v_rep_pagos_socio> v_rep_pagos_socio { get; set; }
        public DbSet<v_rep_pagospendientes> v_rep_pagospendientes { get; set; }
        public DbSet<v_rep_personal> v_rep_personal { get; set; }
        public DbSet<v_rep_prestamos> v_rep_prestamos { get; set; }
        public DbSet<v_rep_prestamos_historial> v_rep_prestamos_historial { get; set; }
        public DbSet<v_rep_prestamos_pendientes_por_pagar> v_rep_prestamos_pendientes_por_pagar { get; set; }
        public DbSet<v_rep_socios> v_rep_socios { get; set; }
        public DbSet<v_rep_socios_adeudo> v_rep_socios_adeudo { get; set; }
        public DbSet<v_rep_socios_adeudo_pagos_pendientes> v_rep_socios_adeudo_pagos_pendientes { get; set; }
        public DbSet<v_rep_socios_buen_aval> v_rep_socios_buen_aval { get; set; }
        public DbSet<v_rep_socios_buen_historial> v_rep_socios_buen_historial { get; set; }
        public DbSet<v_rep_socios_con_pagos_pendientes> v_rep_socios_con_pagos_pendientes { get; set; }
        public DbSet<v_rep_tarjeton_asociados> v_rep_tarjeton_asociados { get; set; }
        public DbSet<v_rep_tarjeton_pagos> v_rep_tarjeton_pagos { get; set; }
        public DbSet<v_rep_tarjeton_prestamos> v_rep_tarjeton_prestamos { get; set; }
        public DbSet<v_rep_ticket> v_rep_ticket { get; set; }
        public DbSet<v_rep_usuarios> v_rep_usuarios { get; set; }
    
        public virtual ObjectResult<agregar_Result> agregar(Nullable<long> vn_id_socio)
        {
            var vn_id_socioParameter = vn_id_socio.HasValue ?
                new ObjectParameter("vn_id_socio", vn_id_socio) :
                new ObjectParameter("vn_id_socio", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<agregar_Result>("agregar", vn_id_socioParameter);
        }
    }
}
