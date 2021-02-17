using Entidad.Enums;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Experiencia : IEntidad
    {
        public Guid Codigo { get; set; }
        public string Puesto { get; set; }
        public ECategoria Categoria { get; set; }
        public string Empresa { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public string Descripcion { get; set; }
        public Candidato Candidato { get; set; }
        public List<Tecnologia> Tecnologias { get; set; }
    }
}
