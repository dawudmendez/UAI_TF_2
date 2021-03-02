using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad.Negocio;
using AccesoDatos.Repositorios;

namespace Negocio.ABM
{
    public class OficinaNegocio
    {
        private OficinaRepo oficinaRepo = new OficinaRepo();
        private DireccionRepo direccionRepo = new DireccionRepo();
        private ContactoRepo contactoRepo = new ContactoRepo();

        public IEnumerable<Oficina> TraerTodo()
        {
            return this.oficinaRepo.TraerTodo();
        }

        public Oficina Traer(string Nombre)
        {
            Oficina oficina = new Oficina();
            oficina.Nombre = Nombre;
            return this.oficinaRepo.Traer(oficina);
        }

        public bool Agregar(Oficina Oficina)
        {
            Oficina.Direccion.Codigo = Guid.NewGuid();
            Oficina.Contacto.Codigo = Guid.NewGuid();

            try
            {
                this.direccionRepo.Insertar(Oficina.Direccion);
                this.contactoRepo.Insertar(Oficina.Contacto);
                this.oficinaRepo.Insertar(Oficina);
            }
            catch (Exception)
            {
                this.direccionRepo.Eliminar(Oficina.Direccion);
                this.contactoRepo.Eliminar(Oficina.Contacto);

                return false;
            }
            

            return true;
        }

        public bool Modificar(Oficina Oficina)
        {
            this.direccionRepo.Actualizar(Oficina.Direccion);
            this.contactoRepo.Actualizar(Oficina.Contacto);
            this.oficinaRepo.Actualizar(Oficina);

            return true;
        }

        public bool Eliminar(string Nombre)
        {
            try
            {
                this.oficinaRepo.Eliminar(new Oficina { Nombre = Nombre });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
