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
    public class PosicionNegocio
    {
        private PosicionRepo posicionRepo = new PosicionRepo();
        private EquipoRepo equipoRepo = new EquipoRepo();
        private PerfilRepo perfilRepo = new PerfilRepo();
        private OficinaRepo oficinaRepo = new OficinaRepo();
        //private AnalyticsRepo analyticsRepo = new AnalyticsRepo();

        public IEnumerable<Posicion> TraerPosiciones()
        {
            return this.posicionRepo.TraerTodo();
        }

        public Posicion TraerPosicion(string Codigo)
        {
            return this.posicionRepo.Traer(new Posicion { Codigo = new Guid(Codigo) });
        }

        public bool AgregarPosicion(Posicion Posicion)
        {
            try
            {
                Posicion.Codigo = Guid.NewGuid();
                return this.posicionRepo.Insertar(Posicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ModificarPosicion(Posicion Posicion)
        {
            try
            {
                return this.posicionRepo.Actualizar(Posicion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarPosicion(string Codigo)
        {
            try
            {
                return this.posicionRepo.Eliminar(new Posicion { Codigo = new Guid(Codigo) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public IEnumerable<Candidato> TraerCandidatosRecomendados(string Codigo)
        //{
        //    return this.analyticsRepo.TraerCandidatosRecomendados(new Guid(Codigo));
        //}

        public IEnumerable<Equipo> TraerEquipos()
        {
            return this.equipoRepo.TraerTodo();
        }

        public IEnumerable<Perfil> TraerPerfiles()
        {
            return this.perfilRepo.TraerTodo();
        }

        public IEnumerable<Oficina> TraerOficinas()
        {
            return this.oficinaRepo.TraerTodo();
        }

    }
}
