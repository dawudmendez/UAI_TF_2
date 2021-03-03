using AccesoDatos.Contexto;
using AccesoDatos.Repositorios;
using Entidad.Enums;
using Entidad.Negocio;
using Negocio.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.ABM
{
    public class UsuarioNegocio
    {

        public List<Usuario> TraerPorPuesto(EPuesto Puesto)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                return usuarioRepo.TraerPorPuesto(Puesto);
            }
        }

        public List<Usuario> TraerTodo()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                return usuarioRepo.TraerTodo();
            }
        }

        public Usuario Traer(string Legajo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                return usuarioRepo.Traer(new Usuario { Legajo = Legajo });
            }
        }

        public bool Agregar(Usuario Usuario)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                return usuarioRepo.Insertar(Usuario);
            }
        }

        public bool Modificar(Usuario Usuario)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                return usuarioRepo.Actualizar(Usuario);
            }
        }
        
        public bool Eliminar(Usuario Usuario)
        {
            if (Usuario.Legajo == UserHelper.UsuarioSistema.Legajo)
                throw new Exception("No se puede eliminar el usuario en uso");

            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                return usuarioRepo.Eliminar(Usuario);
            }
        }

        public bool CambiarContrasena(string Actual, string Nueva)
        {
            if (Actual != UserHelper.UsuarioSistema.Password)
                return false;

            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                return usuarioRepo.CambiarPassword(new Usuario { Legajo = UserHelper.UsuarioSistema.Legajo, Password = Nueva });
            }            
        }

    }
}
