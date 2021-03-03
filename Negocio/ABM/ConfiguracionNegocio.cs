using AccesoDatos.Contexto;
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
        public Configuracion TraerConfiguracion()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ConfiguracionRepo configuracionRepo = new ConfiguracionRepo(contexto);
                List<Configuracion> conf = configuracionRepo.TraerTodo();

                foreach (var item in conf)
                {
                    return item;
                }

                return default;
            }
            
        }

        public bool ActualizarConfiguracion(Configuracion Configuracion)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ConfiguracionRepo configuracionRepo = new ConfiguracionRepo(contexto);
                return configuracionRepo.Actualizar(Configuracion);
            }
        }
    }
}
