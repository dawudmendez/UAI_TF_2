using AccesoDatos.Repositorios;
using Entidad.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.ABM
{
    public class UsuarioNegocio
    {
        private UsuarioRepo usuarioRepo;

        public UsuarioNegocio()
        {
            this.usuarioRepo = new UsuarioRepo();
        }

        public bool CrearUsuario(Usuario Usuario)
        {
            return this.usuarioRepo.Insertar(Usuario);
        }

    }
}
