using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Contacto : IEntidad
    {
        public Guid Codigo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Uri SitioWeb { get; set; }
    }
}
