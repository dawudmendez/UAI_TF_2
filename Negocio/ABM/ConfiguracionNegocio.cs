using AccesoDatos.Repositorios;
using Entidad.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.ABM
{
    public class ConfiguracionNegocio
    {
        private ConfiguracionRepo configuracionRepo = new ConfiguracionRepo();

        public Configuracion TraerConfiguracion()
        {
            List<Configuracion> conf = this.configuracionRepo.TraerTodo().ToList();

            foreach (var item in conf)
            {
                return item;
            }

            return default;
        }
    }
}
