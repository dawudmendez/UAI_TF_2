using Entidad.Enums;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Entrevista : IEntidad
    {
        public Guid Codigo { get; set; }
        public string Descripcion { get; set; }
        public ETipoEntrevista TipoEntrevista { get; set; }
        public ProcesoSeleccion ProcesoSeleccion { get; set; }
        public Usuario Entrevistador { get; set; }
        public int Orden { get; set; }
        public DateTime Fecha { get; set; }
        public EEstadoEntrevista Estado { get; set; }
        public string Comentarios { get; set; }
        public int Puntaje { get; set; }
    }
}
