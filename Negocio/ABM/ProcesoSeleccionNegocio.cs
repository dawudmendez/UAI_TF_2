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
    public class ProcesoSeleccionNegocio
    {
        private ProcesoSeleccionRepo procesoSeleccionRepo = new ProcesoSeleccionRepo();
        private UsuarioRepo usuarioRepo = new UsuarioRepo();
        private PosicionRepo posicionRepo = new PosicionRepo();
        private EntrevistaRepo entrevistaRepo = new EntrevistaRepo();
        private CandidatoRepo candidatoRepo = new CandidatoRepo();

        public IEnumerable<ProcesoSeleccion> TraerProcesosSeleccion()
        {
            return this.procesoSeleccionRepo.TraerTodo();
        }

        public ProcesoSeleccion TraerProcesoSeleccion(string Codigo)
        {
            ProcesoSeleccion proceso;

            try
            {
                proceso = this.procesoSeleccionRepo.Traer(new ProcesoSeleccion { Codigo = new Guid(Codigo) });
            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                proceso.Reclutador = this.HidratarReclutador(proceso);
                proceso.Candidato = this.HidratarCandidato(proceso);
                proceso.Posicion = this.HidratarPosicion(proceso);
            }
            catch (Exception)
            {

                throw;
            }
            

            return proceso;
        }

        public bool AgregarProcesoSeleccion(ProcesoSeleccion ProcesoSeleccion)
        {
            try
            {
                return this.procesoSeleccionRepo.Insertar(ProcesoSeleccion);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ModificarProcesoSeleccion(ProcesoSeleccion ProcesoSeleccion)
        {
            try
            {
                return this.procesoSeleccionRepo.Actualizar(ProcesoSeleccion);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EliminarProcesoSeleccion(string Codigo)
        {
            try
            {
                return this.procesoSeleccionRepo.Eliminar(new ProcesoSeleccion { Codigo = new Guid(Codigo) });
            }
            catch (Exception)
            {
                return false;
            }
        }

        private Candidato HidratarCandidato(ProcesoSeleccion procesoSeleccion)
        {
            try
            {
                return this.candidatoRepo.Traer(procesoSeleccion.Candidato);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Usuario HidratarReclutador(ProcesoSeleccion procesoSeleccion)
        {
            try
            {
                return this.usuarioRepo.Traer(procesoSeleccion.Reclutador);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private Posicion HidratarPosicion(ProcesoSeleccion procesoSeleccion)
        {
            try
            {
                return this.posicionRepo.Traer(procesoSeleccion.Posicion);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AgregarEntrevista(Entrevista Entrevista)
        {
            try
            {
                this.entrevistaRepo.Insertar(Entrevista);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool ModificarEntrevista(Entrevista Entrevista)
        {
            try
            {
                this.entrevistaRepo.Actualizar(Entrevista);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool EliminarEntrevista(string Codigo)
        {
            try
            {
                return this.entrevistaRepo.Eliminar(new Entrevista { Codigo = new Guid(Codigo) });
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
