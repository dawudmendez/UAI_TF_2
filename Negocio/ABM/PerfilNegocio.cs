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
    public class PerfilNegocio
    {
        public List<Perfil> TraerPerfiles()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PerfilRepo perfilRepo = new PerfilRepo(contexto);
                return perfilRepo.TraerTodo();
            }            
        }

        public Perfil TraerPerfil(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PerfilRepo perfilRepo = new PerfilRepo(contexto);
                return perfilRepo.Traer(new Perfil { Codigo = new Guid(Codigo) });
            }            
        }

        public bool AgregarPerfil(Perfil Perfil)
        {
            Perfil.Codigo = Guid.NewGuid();

            using (SQLContexto contexto = new SQLContexto())
            {
                PerfilRepo perfilRepo = new PerfilRepo(contexto);
                return perfilRepo.Insertar(Perfil);
            }
        }

        public bool ModificarPerfil(Perfil Perfil)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PerfilRepo perfilRepo = new PerfilRepo(contexto);
                return perfilRepo.Actualizar(Perfil);
            }
        }

        public bool EliminarPerfil(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PerfilRepo perfilRepo = new PerfilRepo(contexto);
                return perfilRepo.Eliminar(new Perfil { Codigo = new Guid(Codigo) });
            }
        }

        public Tecnologia TraerTecnologia(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                TecnologiaRepo tecnologiaRepo = new TecnologiaRepo(contexto);
                return tecnologiaRepo.Traer(new Tecnologia { Codigo = new Guid(Codigo) });
            }
        }
    }
}
