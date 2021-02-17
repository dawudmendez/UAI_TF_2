using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Oficina : IEntidad
    {
        public string Nombre { get; set; }
        public Direccion Direccion { get; set; }
        public Contacto Contacto { get; set; }
    }
}
