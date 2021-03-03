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
    public class EntrevistaNegocio
    {
        public List<Entrevista> TraerEntrevistas()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EntrevistaRepo entrevistaRepo = new EntrevistaRepo(contexto);
                return entrevistaRepo.TraerTodo();
            }
            
        }

        public Entrevista TraerEntrevista(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EntrevistaRepo entrevistaRepo = new EntrevistaRepo(contexto);
                Entrevista entrevista = entrevistaRepo.Traer(new Entrevista { Codigo = new Guid(Codigo) });

                entrevista.ProcesoSeleccion = this.HidratarProcesoSeleccion(entrevista);
                entrevista.Entrevistador = this.HidratarUsuario(entrevista);

                return entrevista;
            }
        }

        public bool AgregarEntrevista(Entrevista Entrevista)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EntrevistaRepo entrevistaRepo = new EntrevistaRepo(contexto);
                return entrevistaRepo.Insertar(Entrevista);
            }
        }

        public bool ModificarEntrevista(Entrevista Entrevista)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EntrevistaRepo entrevistaRepo = new EntrevistaRepo(contexto);
                return entrevistaRepo.Actualizar(Entrevista);
            }
        }

        public bool EliminarEntrevista(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EntrevistaRepo entrevistaRepo = new EntrevistaRepo(contexto);
                return entrevistaRepo.Eliminar(new Entrevista { Codigo = new Guid(Codigo) });
            }
        }

        private Usuario HidratarUsuario(Entrevista entrevista)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                return usuarioRepo.Traer(entrevista.Entrevistador);
            }
        }

        private ProcesoSeleccion HidratarProcesoSeleccion(Entrevista entrevista)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ProcesoSeleccionRepo procesoSeleccionRepo = new ProcesoSeleccionRepo(contexto);
                return procesoSeleccionRepo.Traer(entrevista.ProcesoSeleccion);
            }
        }

    }
}
