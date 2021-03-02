using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Repositorios;
using Entidad.Enums;
using Entidad.Negocio;
using Negocio.Helpers;
using System;
using System.Collections.Generic;

namespace Negocio.ABM
{
    public class TecnologiaNegocio
    {
        private TecnologiaRepo tecnologiaRepo = new TecnologiaRepo();

        public IEnumerable<Tecnologia> Traer()
        {
            return this.tecnologiaRepo.TraerTodo();
        }

        public Tecnologia Traer(string Codigo)
        {
            return this.tecnologiaRepo.Traer(new Tecnologia { Codigo = new Guid(Codigo) });
        }

        public bool Agregar(Tecnologia Tecnologia)
        {
            try
            {
                Tecnologia.Codigo = Guid.NewGuid();
                return this.tecnologiaRepo.Insertar(Tecnologia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Modificar(Tecnologia Tecnologia)
        {
            try
            {
                return this.tecnologiaRepo.Actualizar(Tecnologia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Eliminar(string Codigo)
        {
            try
            {
                return this.tecnologiaRepo.Eliminar(new Tecnologia { Codigo = new Guid(Codigo) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
