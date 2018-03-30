using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PersonalDataGridViewModel
    {
        public long per_id { get; set; }
        public string per_nombrecompleto { get; set; }
        public string per_sexo { get; set; }
        public DateTime per_fechanacimiento { get; set; }
        public string per_estadocivil { get; set; }
        public string per_telefono { get; set; }
        public string per_movil { get; set; }
        public string per_correoelectronico { get; set; }
        public string per_nombre { get; set; }
        public string per_apellidos { get; set; }

        public List<PersonalDataGridViewModel> dataGridPersonal()
        {
            using (var bd = new Conexion())
            {
                var listado = bd.personal.Select(p => new PersonalDataGridViewModel()
                {
                    per_id = p.per_id,
                    per_nombrecompleto = p.per_nombre + " " + p.per_apellidos,
                    per_sexo = p.per_sexo,
                    per_fechanacimiento = p.per_fechanacimiento,
                    per_estadocivil = p.per_estadocivil,
                    per_telefono = p.per_telefono,
                    per_movil = p.per_movil,
                    per_correoelectronico = p.per_correoelectronico,
                    per_nombre = p.per_nombre,
                    per_apellidos = p.per_apellidos
                });

                return listado.ToList();
            }
        }
    }
}
