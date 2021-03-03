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
    public class CandidatoNegocio
    {
        public List<Candidato> TraerCandidatos()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                CandidatoRepo candidatoRepo = new CandidatoRepo(contexto);
                List<Candidato> candidatos = candidatoRepo.TraerTodo();

                foreach (Candidato item in candidatos)
                {
                    item.Contacto = this.HidratarContacto(item.Contacto, contexto);
                    item.Direccion = this.HidratarDireccion(item.Direccion, contexto);
                }

                return candidatos;
            }
        }

        public Candidato TraerCandidato(string Cuil)
        {
            try
            {
                using (SQLContexto contexto = new SQLContexto())
                {
                    CandidatoRepo candidatoRepo = new CandidatoRepo(contexto);
                    Candidato candidato = candidatoRepo.Traer(new Candidato { Cuil = Cuil });
                    candidato.Contacto = this.HidratarContacto(candidato.Contacto, contexto);
                    candidato.Direccion = this.HidratarDireccion(candidato.Direccion, contexto);

                    return candidato;
                }                
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public bool AgregarCandidato(Candidato Candidato)
        {
            Candidato.Direccion.Codigo = Guid.NewGuid();
            Candidato.Contacto.Codigo = Guid.NewGuid();

            try
            {
                //Si una de las tres operaciones sale mal, se hace rollback de todas las del bloque
                using (SQLContexto contexto = new SQLContexto())
                {
                    CandidatoRepo candidatoRepo = new CandidatoRepo(contexto);
                    DireccionRepo direccionRepo = new DireccionRepo(contexto);
                    ContactoRepo contactoRepo = new ContactoRepo(contexto);

                    direccionRepo.Insertar(Candidato.Direccion);
                    contactoRepo.Insertar(Candidato.Contacto);
                    candidatoRepo.Insertar(Candidato);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public bool ModificarCandidato(Candidato Candidato)
        {
            try
            {
                //Si una de las tres operaciones sale mal, se hace rollback de todas las del bloque
                using (SQLContexto contexto = new SQLContexto())
                {
                    CandidatoRepo candidatoRepo = new CandidatoRepo(contexto);
                    DireccionRepo direccionRepo = new DireccionRepo(contexto);
                    ContactoRepo contactoRepo = new ContactoRepo(contexto);

                    direccionRepo.Actualizar(Candidato.Direccion);
                    contactoRepo.Actualizar(Candidato.Contacto);
                    candidatoRepo.Actualizar(Candidato);
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool EliminarCandidato(string Cuil)
        {
            try
            {
                using (SQLContexto contexto = new SQLContexto())
                {
                    CandidatoRepo candidatoRepo = new CandidatoRepo(contexto);
                    DireccionRepo direccionRepo = new DireccionRepo(contexto);
                    ContactoRepo contactoRepo = new ContactoRepo(contexto);

                    Candidato candidato = candidatoRepo.Traer(new Candidato { Cuil = Cuil });

                    candidatoRepo.Eliminar(candidato);
                    direccionRepo.Eliminar(candidato.Direccion);
                    return contactoRepo.Eliminar(candidato.Contacto);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Educacion> TraerEducaciones(string Cuil)
        {
            try
            {
                using (SQLContexto contexto = new SQLContexto())
                {
                    EducacionRepo educacionRepo = new EducacionRepo(contexto);
                    return educacionRepo.TraerPorCandidato(new Candidato { Cuil = Cuil });
                }
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public Educacion TraerEducacion(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EducacionRepo educacionRepo = new EducacionRepo(contexto);
                return educacionRepo.Traer(new Educacion { Codigo = new Guid(Codigo) });
            }
        }

        public bool AgregarEducacion(Educacion Educacion)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EducacionRepo educacionRepo = new EducacionRepo(contexto);
                return educacionRepo.Insertar(Educacion);
            }
        }

        public bool ModificarEducacion(Educacion Educacion)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EducacionRepo educacionRepo = new EducacionRepo(contexto);
                return educacionRepo.Actualizar(Educacion);
            }
        }

        public bool EliminarEducacion(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                EducacionRepo educacionRepo = new EducacionRepo(contexto);
                return educacionRepo.Eliminar(new Educacion { Codigo = new Guid(Codigo) });
            }
        }

        public List<Experiencia> TraerExperiencias(string Cuil)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ExperienciaRepo experienciaRepo = new ExperienciaRepo(contexto);
                return experienciaRepo.TraerPorCandidato(new Candidato { Cuil = Cuil });
            }
        }

        public Experiencia TraerExperiencia(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ExperienciaRepo experienciaRepo = new ExperienciaRepo(contexto);
                return experienciaRepo.Traer(new Experiencia { Codigo = new Guid(Codigo) });
            }
        }

        public bool AgregarExperiencia(Experiencia Experiencia)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ExperienciaRepo experienciaRepo = new ExperienciaRepo(contexto);
                return experienciaRepo.Insertar(Experiencia);
            }
        }

        public bool ModificarExperiencia(Experiencia Experiencia)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ExperienciaRepo experienciaRepo = new ExperienciaRepo(contexto);
                return experienciaRepo.Actualizar(Experiencia);
            }
        }

        public bool EliminarExperiencia(string Codigo)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                ExperienciaRepo experienciaRepo = new ExperienciaRepo(contexto);
                return experienciaRepo.Eliminar(new Experiencia { Codigo = new Guid(Codigo) });
            }
        }

        public List<Tecnologia> TraerTecnologias()
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                TecnologiaRepo tecnologiaRepo = new TecnologiaRepo(contexto);
                return tecnologiaRepo.TraerTodo();
            }
        }

        public bool AgregarTecnologia(Tecnologia Tecnologia)
        {
            using (SQLContexto contexto = new SQLContexto())
            {
                TecnologiaRepo tecnologiaRepo = new TecnologiaRepo(contexto);
                return tecnologiaRepo.Insertar(Tecnologia);
            }
        }

        private Contacto HidratarContacto(Contacto contacto, IDBContexto contexto)
        {
            ContactoRepo contactoRepo = new ContactoRepo(contexto);
            return contactoRepo.Traer(contacto);
        }

        private Direccion HidratarDireccion(Direccion direccion, IDBContexto contexto)
        {
            DireccionRepo direccionRepo = new DireccionRepo(contexto);
            return direccionRepo.Traer(direccion);
        }

    }
}
