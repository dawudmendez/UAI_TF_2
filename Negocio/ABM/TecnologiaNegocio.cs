using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Repositorios;
using Entidad.Enums;
using Entidad.Negocio;
using Negocio.Helpers;
using System;
using System.Collections.Generic;
using AccesoDatos.Contexto;

namespace Negocio.ABM
{
    public class TecnologiaNegocio
    {

        public List<Tecnologia> Traer()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                TecnologiaRepo tecnologiaRepo = new TecnologiaRepo(contexto);
                return tecnologiaRepo.TraerTodo();
            }
        }

        public Tecnologia Traer(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                TecnologiaRepo tecnologiaRepo = new TecnologiaRepo(contexto);
                return tecnologiaRepo.Traer(new Tecnologia { Codigo = new Guid(Codigo) });
            }
        }

        public bool Agregar(Tecnologia Tecnologia)
        {
            Tecnologia.Codigo = Guid.NewGuid();
            using (SQLContexto contexto = new SQLContexto())
            {
                TecnologiaRepo tecnologiaRepo = new TecnologiaRepo(contexto);
                return tecnologiaRepo.Insertar(Tecnologia);
            }
        }

        public bool Modificar(Tecnologia Tecnologia)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                TecnologiaRepo tecnologiaRepo = new TecnologiaRepo(contexto);
                return tecnologiaRepo.Actualizar(Tecnologia);
            }
        }

        public bool Eliminar(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                TecnologiaRepo tecnologiaRepo = new TecnologiaRepo(contexto);
                return tecnologiaRepo.Eliminar(new Tecnologia { Codigo = new Guid(Codigo) });
            }
        }
    }
}
