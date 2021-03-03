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
    public class ProcesoSeleccionNegocio
    {
        public List<ProcesoSeleccion> TraerProcesosSeleccion()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ProcesoSeleccionRepo procesoSeleccionRepo = new ProcesoSeleccionRepo(contexto);
                return procesoSeleccionRepo.TraerTodo();
            }
        }

        public ProcesoSeleccion TraerProcesoSeleccion(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ProcesoSeleccionRepo procesoSeleccionRepo = new ProcesoSeleccionRepo(contexto);

                ProcesoSeleccion proceso = procesoSeleccionRepo.Traer(new ProcesoSeleccion { Codigo = new Guid(Codigo) });

                proceso.Reclutador = this.HidratarReclutador(proceso);
                proceso.Candidato = this.HidratarCandidato(proceso);
                proceso.Posicion = this.HidratarPosicion(proceso);

                return proceso;
            }
        }

        public bool AgregarProcesoSeleccion(ProcesoSeleccion ProcesoSeleccion)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ProcesoSeleccionRepo procesoSeleccionRepo = new ProcesoSeleccionRepo(contexto);
                return procesoSeleccionRepo.Insertar(ProcesoSeleccion);
            }
        }

        public bool ModificarProcesoSeleccion(ProcesoSeleccion ProcesoSeleccion)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ProcesoSeleccionRepo procesoSeleccionRepo = new ProcesoSeleccionRepo(contexto);
                return procesoSeleccionRepo.Actualizar(ProcesoSeleccion);
            }
        }

        public bool EliminarProcesoSeleccion(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ProcesoSeleccionRepo procesoSeleccionRepo = new ProcesoSeleccionRepo(contexto);
                return procesoSeleccionRepo.Eliminar(new ProcesoSeleccion { Codigo = new Guid(Codigo) });
            }
        }

        private Candidato HidratarCandidato(ProcesoSeleccion procesoSeleccion)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                CandidatoRepo candidatoRepo = new CandidatoRepo(contexto);
                return candidatoRepo.Traer(procesoSeleccion.Candidato);
            }
        }

        private Usuario HidratarReclutador(ProcesoSeleccion procesoSeleccion)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                UsuarioRepo usuarioRepo = new UsuarioRepo(contexto);
                return usuarioRepo.Traer(procesoSeleccion.Reclutador);
            }
        }

        private Posicion HidratarPosicion(ProcesoSeleccion procesoSeleccion)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PosicionRepo posicionRepo = new PosicionRepo(contexto);
                return posicionRepo.Traer(procesoSeleccion.Posicion);
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

    }
}
