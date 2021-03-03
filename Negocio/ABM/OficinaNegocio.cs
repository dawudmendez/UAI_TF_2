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
    public class OficinaNegocio
    {
        public List<Oficina> TraerTodo()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                OficinaRepo oficinaRepo = new OficinaRepo(contexto);
                return oficinaRepo.TraerTodo();
            }
        }

        public Oficina Traer(string Nombre)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                OficinaRepo oficinaRepo = new OficinaRepo(contexto);
                Oficina oficina = new Oficina();
                oficina.Nombre = Nombre;
                return oficinaRepo.Traer(oficina);
            }            
        }

        public bool Agregar(Oficina Oficina)
        {
            Oficina.Direccion.Codigo = Guid.NewGuid();
            Oficina.Contacto.Codigo = Guid.NewGuid();

            using (SQLContexto contexto = new SQLContexto())
            {
                OficinaRepo oficinaRepo = new OficinaRepo(contexto);
                DireccionRepo direccionRepo = new DireccionRepo(contexto);
                ContactoRepo contactoRepo = new ContactoRepo(contexto);

                direccionRepo.Insertar(Oficina.Direccion);
                contactoRepo.Insertar(Oficina.Contacto);
                return oficinaRepo.Insertar(Oficina);
            }
        }

        public bool Modificar(Oficina Oficina)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                OficinaRepo oficinaRepo = new OficinaRepo(contexto);
                DireccionRepo direccionRepo = new DireccionRepo(contexto);
                ContactoRepo contactoRepo = new ContactoRepo(contexto);

                direccionRepo.Actualizar(Oficina.Direccion);
                contactoRepo.Actualizar(Oficina.Contacto);
                return oficinaRepo.Actualizar(Oficina);
            }
        }

        public bool Eliminar(string Nombre)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                OficinaRepo oficinaRepo = new OficinaRepo(contexto);
                return oficinaRepo.Eliminar(new Oficina { Nombre = Nombre });
            }
        }
    }
}
