using Entidad.Enums;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Perfil : IEntidad
    {
        public Guid Codigo { get; set; }
        public string Nombre { get; set; }
        public ECategoria Categoria { get; set; }
        public int AniosExperiencia { get; set; }
        public List<Tecnologia> Tecnologias { get; set; }
    }
}
