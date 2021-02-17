using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Candidato : IEntidad
    {
        public string Cuil { get; set; }
        public long DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FecNac { get; set; }
        public Contacto Contacto { get; set; }
        public Direccion Direccion { get; set; }
        public List<Experiencia> Experiencia { get; set; }
        public List<Educacion> Educacion { get; set; }
    }
}
