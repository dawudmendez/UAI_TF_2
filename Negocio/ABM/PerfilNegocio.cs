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
    public class PerfilNegocio
    {
        private PerfilRepo perfilRepo = new PerfilRepo();
        private TecnologiaRepo tecnologiaRepo = new TecnologiaRepo();

        public IEnumerable<Perfil> TraerPerfiles()
        {
            return this.perfilRepo.TraerTodo();
        }

        public Perfil TraerPerfil(string Codigo)
        {
            return this.perfilRepo.Traer(new Perfil { Codigo = new Guid(Codigo) });
        }

        public bool AgregarPerfil(Perfil Perfil)
        {
            try
            {
                Perfil.Codigo = Guid.NewGuid();

                //Este método agrega todas las categorías que haya
                return this.perfilRepo.Insertar(Perfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModificarPerfil(Perfil Perfil)
        {
            try
            {
                return this.perfilRepo.Actualizar(Perfil);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarPerfil(string Codigo)
        {
            try
            {
                return this.perfilRepo.Eliminar(new Perfil { Codigo = new Guid(Codigo) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tecnologia TraerTecnologia(string Codigo)
        {
            return this.tecnologiaRepo.Traer(new Tecnologia { Codigo = new Guid(Codigo) });
        }
    }
}
