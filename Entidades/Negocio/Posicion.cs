using Entidad.Enums;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Posicion : IEntidad
    {
        public Guid Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Perfil Perfil { get; set; }
        public Equipo Equipo { get; set; }
        public EEstadoPosicion Estado { get; set; }
        public Oficina Oficina { get; set; }
    }
}
