using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad.Negocio;
using AccesoDatos.Repositorios;

namespace Negocio.ABM
{
    public class EquiposNegocio
    {
        private EquipoRepo equipoRepo = new EquipoRepo();

        public IEnumerable<Equipo> TraerTodo()
        {
            return this.equipoRepo.TraerTodo();
        }

        public Equipo Traer(string Nombre)
        {
            Equipo equipo = new Equipo();
            equipo.Nombre = Nombre;
            return this.equipoRepo.Traer(equipo);
        }

        public bool Agregar(Equipo Equipo)
        {
            try
            {
                this.equipoRepo.Insertar(Equipo);
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }

        public bool Modificar(Equipo Equipo)
        {
            try
            {
                this.equipoRepo.Actualizar(Equipo);
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }
    }
}
