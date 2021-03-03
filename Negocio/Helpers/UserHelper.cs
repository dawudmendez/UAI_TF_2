using AccesoDatos.Contexto;
using AccesoDatos.Repositorios;
using Entidad.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Helpers
{
    public static class UserHelper
    {
        public static Usuario UsuarioSistema;

        public static bool Login(string Legajo, string Password)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                Usuario usuario = usuarioRepo.Traer(new Usuario { Legajo = Legajo });

                if (usuario == null)
                    return false;

                if (usuario.Password != Password)
                    return false;

                UsuarioSistema = usuario;
            }
            
            return true;
        }

        public static void Logout()
        {
            UsuarioSistema = null;
        }

    }

    
}
