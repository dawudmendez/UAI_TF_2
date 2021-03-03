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
    public class PosicionNegocio
    {
        //private AnalyticsRepo analyticsRepo = new AnalyticsRepo();

        public List<Posicion> TraerPosiciones()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PosicionRepo posicionRepo = new PosicionRepo(contexto);
                return posicionRepo.TraerTodo();
            }
        }

        public Posicion TraerPosicion(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PosicionRepo posicionRepo = new PosicionRepo(contexto);
                return posicionRepo.Traer(new Posicion { Codigo = new Guid(Codigo) });
            }
        }

        public bool AgregarPosicion(Posicion Posicion)
        {
            Posicion.Codigo = Guid.NewGuid();
            using (SQLContexto contexto = new SQLContexto())
            {
                PosicionRepo posicionRepo = new PosicionRepo(contexto);
                return posicionRepo.Insertar(Posicion);
            }
        }

        public bool ModificarPosicion(Posicion Posicion)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PosicionRepo posicionRepo = new PosicionRepo(contexto);
                return posicionRepo.Actualizar(Posicion);
            }
        }

        public bool EliminarPosicion(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PosicionRepo posicionRepo = new PosicionRepo(contexto);
                return posicionRepo.Eliminar(new Posicion { Codigo = new Guid(Codigo) });
            }
        }

        //public IEnumerable<Candidato> TraerCandidatosRecomendados(string Codigo)
        //{
        //    return this.analyticsRepo.TraerCandidatosRecomendados(new Guid(Codigo));
        //}

        public List<Equipo> TraerEquipos()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EquipoRepo equipoRepo = new EquipoRepo(contexto);
                return equipoRepo.TraerTodo();
            }
        }

        public List<Perfil> TraerPerfiles()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                PerfilRepo perfilRepo = new PerfilRepo(contexto);
                return perfilRepo.TraerTodo();
            }
        }

        public List<Oficina> TraerOficinas()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                OficinaRepo oficinaRepo = new OficinaRepo(contexto);
                return oficinaRepo.TraerTodo();
            }
        }

    }
}
