using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Equipo : IEntidad
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Usuario Lider { get; set; }
        public Usuario Manager { get; set; }
    }
}
