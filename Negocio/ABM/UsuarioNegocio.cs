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
        private UsuarioRepo usuarioRepo = new UsuarioRepo();

        public IEnumerable<Usuario> TraerPorPuesto(EPuesto Puesto)
        {
            return this.usuarioRepo.TraerPorPuesto(Puesto);
        }

        public IEnumerable<Usuario> TraerTodo()
        {
            return this.usuarioRepo.TraerTodo();
        }

        public Usuario Traer(string Legajo)
        {
            return this.usuarioRepo.Traer(new Usuario { Legajo = Legajo });
        }

        public bool Agregar(Usuario Usuario)
        {
            try
            {
                this.usuarioRepo.Insertar(Usuario);
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }

        public bool Modificar(Usuario Usuario)
        {
            try
            {
                this.usuarioRepo.Actualizar(Usuario);
            }
            catch (Exception)
            {
                return false;
            }
            

            return true;
        }
        
        public bool Eliminar(Usuario Usuario)
        {

            if (Usuario.Legajo == UserHelper.UsuarioSistema.Legajo)
                throw new Exception("No se puede eliminar el usuario en uso");

            try
            {
                return this.usuarioRepo.Eliminar(Usuario);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CambiarContrasena(string Actual, string Nueva)
        {
            if (Actual != UserHelper.UsuarioSistema.Password)
                return false;

            try
            {
                return this.usuarioRepo.CambiarPassword(new Usuario { Legajo = UserHelper.UsuarioSistema.Legajo, Password = Nueva });
            }
            catch (Exception)
            {

                return false;
            }
            
        }

    }
}
