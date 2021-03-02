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
    public class EntrevistaNegocio
    {
        private EntrevistaRepo entrevistaRepo = new EntrevistaRepo();
        private ProcesoSeleccionRepo procesoSeleccionRepo = new ProcesoSeleccionRepo();
        private UsuarioRepo usuarioRepo = new UsuarioRepo();

        public IEnumerable<Entrevista> TraerEntrevistas()
        {
            return this.entrevistaRepo.TraerTodo();
        }

        public Entrevista TraerEntrevista(string Codigo)
        {
            Entrevista entrevista = this.entrevistaRepo.Traer(new Entrevista { Codigo = new Guid(Codigo) });

            entrevista.ProcesoSeleccion = this.HidratarProcesoSeleccion(entrevista);
            entrevista.Entrevistador = this.HidratarUsuario(entrevista);

            return entrevista;
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

        private Usuario HidratarUsuario(Entrevista entrevista)
        {
            try
            {
                return this.usuarioRepo.Traer(entrevista.Entrevistador);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private ProcesoSeleccion HidratarProcesoSeleccion(Entrevista entrevista)
        {
            try
            {
                return this.procesoSeleccionRepo.Traer(entrevista.ProcesoSeleccion);
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
