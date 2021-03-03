using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad.Negocio;
using AccesoDatos.Repositorios;
using AccesoDatos.Contexto;

namespace Negocio.ABM
{
    public class EquiposNegocio
    {
        public List<Equipo> TraerTodo()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EquipoRepo equipoRepo = new EquipoRepo(contexto);
                return equipoRepo.TraerTodo();
            }
        }

        public Equipo Traer(string Nombre)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EquipoRepo equipoRepo = new EquipoRepo(contexto);

                Equipo equipo = new Equipo();
                equipo.Nombre = Nombre;
                return equipoRepo.Traer(equipo);
            }            
        }

        public bool Agregar(Equipo Equipo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EquipoRepo equipoRepo = new EquipoRepo(contexto);
                return equipoRepo.Insertar(Equipo);
            }
        }

        public bool Modificar(Equipo Equipo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EquipoRepo equipoRepo = new EquipoRepo(contexto);
                return equipoRepo.Actualizar(Equipo);
            }
        }

        public bool Eliminar(string Nombre)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EquipoRepo equipoRepo = new EquipoRepo(contexto);
                return equipoRepo.Eliminar(new Equipo { Nombre = Nombre });
            }
        }
    }
}
