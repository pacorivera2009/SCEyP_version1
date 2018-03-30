using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SociosDataGridViewModel
    {
        public long aso_id { get; set; }
        public string aso_nombrecompleto { get; set; }
        public string aso_sexo { get; set; }
        public DateTime aso_fechanacimiento { get; set; }
        public string aso_estadocivil { get; set; }
        public string aso_telefono { get; set; }
        public string aso_movil { get; set; }
        public string aso_correoelectronico { get; set; }
        public string aso_nombre { get; set; }
        public string aso_apellidos { get; set; }
        //public string aso_estado { get; set; }
        //public string aso_municipio { get; set; }

        public List<SociosDataGridViewModel> dataGridSocios()
        {
            using (var bd = new Conexion())
            {
                //TABLAS UNIDAS//
                //var listado = bd.asociados.Include("asociados")
                //        .Select(p => new SociosDataGridViewModel()
                //        {
                //            aso_id = p.aso_id,
                //            aso_estado = p.estados.est_nombreestado,
                //            aso_municipio = p.municipios.mun_nombremunicipio
                //        });

                var listado = bd.asociados.Select(a => new SociosDataGridViewModel()
                {
                    aso_id = a.aso_id,
                    aso_nombrecompleto = a.aso_nombre + " " + a.aso_apellidos,
                    aso_sexo = a.aso_sexo,
                    aso_fechanacimiento = a.aso_fechanacimiento,
                    aso_estadocivil = a.aso_estadocivil,
                    aso_telefono = a.aso_telefono,
                    aso_movil = a.aso_movil,
                    aso_correoelectronico = a.aso_correoelectronico,
                    aso_nombre = a.aso_nombre,
                    aso_apellidos = a.aso_apellidos
                });

                return listado.ToList();
            }
        }
    }
}
