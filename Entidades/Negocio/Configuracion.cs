using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Configuracion : IEntidad
    {
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
        public Oficina OficinaPrincipal { get; set; }
    }
}
