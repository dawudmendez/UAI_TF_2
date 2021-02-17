using Entidad.Enums;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Educacion : IEntidad
    {
        public Guid Codigo { get; set; }
        public string Institucion { get; set; }
        public string Carrera { get; set; }
        public ETipoCarrera TipoCarrera { get; set; }
        public string Duracion { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public ERubroCarrera RubroCarrera { get; set; }
        public EEstadoEducacion Estado { get; set; }
        public Candidato Candidato { get; set; }

    }
}
