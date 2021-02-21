using AccesoDatos.Repositorios;
using Entidad.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Helpers
{
    public static class LoginHelper
    {
        public static Usuario UsuarioSistema;
        public static bool Login(Usuario Usuario)
        {
            UsuarioRepo usuarioRepo = new UsuarioRepo();
            Usuario usuario = usuarioRepo.Traer(Usuario);

            if (usuario.Password == Usuario.Password)
            {
                UsuarioSistema = usuario;
                return true;
            }

            return false;
        }

        public static void Logout()
        {
            UsuarioSistema = null;
        }

    }

    
}
