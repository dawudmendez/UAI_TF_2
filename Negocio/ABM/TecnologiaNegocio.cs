using Entidad.Negocio;
using AccesoDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.ABM
{
    public class TecnologiaNegocio
    {
        private TecnologiaRepo repo;
        public TecnologiaNegocio()
        {
            this.repo = new TecnologiaRepo();
        }
        public void TraerTodo()
        {
            //Tecnologia tec = new Tecnologia();
            //tec.Id = 1;

            //tec = repo.Traer(tec);

            //return tec;

        }
    }
}
