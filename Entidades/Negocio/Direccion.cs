
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.Negocio
{
    public class Direccion : IEntidad
    {
        public Guid Codigo { get; set; }
        public string Provincia { get; set; }
        public string Localidad { get; set; }
        public string Ciudad { get; set; }
        public string Barrio { get; set; }
        public string Calle { get; set; }
        public long Numero { get; set; }
        public string CodigoPostal { get; set; }
        public string Torre { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
    }
}
