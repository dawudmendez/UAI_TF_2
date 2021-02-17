using Entidad.Enums;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class ProcesoSeleccion : IEntidad
    {
        public Guid Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Posicion Posicion { get; set; }
        public Candidato Candidato { get; set; }
        public Usuario Reclutador { get; set; }
        public List<Entrevista> Entrevistas { get; set; }
        public EEstadoProcesoSeleccion Estado { get; set; }
        public string Comentarios { get; set; }
    }
}
