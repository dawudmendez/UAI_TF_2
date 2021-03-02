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
    public class CandidatoNegocio
    {
        private CandidatoRepo candidatoRepo = new CandidatoRepo();
        private EducacionRepo educacionRepo = new EducacionRepo();
        private ExperienciaRepo experienciaRepo = new ExperienciaRepo();
        private TecnologiaRepo tecnologiaRepo = new TecnologiaRepo();

        public IEnumerable<Candidato> TraerCandidatos()
        {
            return this.candidatoRepo.TraerTodo();
        }

        public Candidato TraerCandidato(string Cuil)
        {
            return this.candidatoRepo.Traer(new Candidato { Cuil = Cuil });
        }

        public bool AgregarCandidato(Candidato Candidato)
        {
            try
            {
                this.candidatoRepo.Insertar(Candidato);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool ModificarCandidato(Candidato Candidato)
        {
            try
            {
                this.candidatoRepo.Actualizar(Candidato);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool EliminarCandidato(string Cuil)
        {
            try
            {
                return this.candidatoRepo.Eliminar(new Candidato { Cuil = Cuil });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Educacion> TraerEducaciones(string Cuil)
        {
            return this.educacionRepo.TraerPorCandidato(new Candidato { Cuil = Cuil });
        }

        public Educacion TraerEducacion(string Codigo)
        {
            return this.educacionRepo.Traer(new Educacion { Codigo = new Guid(Codigo) });
        }

        public bool AgregarEducacion(Educacion Educacion)
        {
            try
            {
                this.educacionRepo.Insertar(Educacion);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool ModificarEducacion(Educacion Educacion)
        {
            try
            {
                this.educacionRepo.Actualizar(Educacion);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool EliminarEducacion(string Codigo)
        {
            try
            {
                return this.educacionRepo.Eliminar(new Educacion { Codigo = new Guid(Codigo) });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Experiencia> TraerExperiencias(string Cuil)
        {
            return this.experienciaRepo.TraerPorCandidato(new Candidato { Cuil = Cuil });
        }

        public Experiencia TraerExperiencia(string Codigo)
        {
            return this.experienciaRepo.Traer(new Experiencia { Codigo = new Guid(Codigo) });
        }

        public bool AgregarExperiencia(Experiencia Experiencia)
        {
            try
            {
                this.experienciaRepo.Insertar(Experiencia);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool ModificarExperiencia(Experiencia Experiencia)
        {
            try
            {
                this.experienciaRepo.Actualizar(Experiencia);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool EliminarExperiencia(string Codigo)
        {
            try
            {
                return this.experienciaRepo.Eliminar(new Experiencia { Codigo = new Guid(Codigo) });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Tecnologia> TraerTecnologias()
        {
            return this.tecnologiaRepo.TraerTodo();
        }

        public bool AgregarTecnologia(Tecnologia Tecnologia)
        {
            try
            {
                this.tecnologiaRepo.Insertar(Tecnologia);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}
