using AccesoDatos.Contexto;
using AccesoDatos.Enums;
using AccesoDatos.Interfaces;
using Entidad.Enums;
using Entidad.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AccesoDatos.Repositorios
{
    public class ExperienciaRepo : Repositorio<Experiencia>
    {
        private CandidatoRepo CandidatoRepo = new CandidatoRepo();
        private TecnologiaRepo TecnologiaRepo = new TecnologiaRepo();

        protected override string SPTraerTodo { get; set; } = "sp_experiencia_traer";
        protected override string SPTraerUno { get; set; } = "sp_experiencia_traeruno";
        protected override string SPActualizar { get; set; } = "sp_experiencia_actualizar";
        protected override string SPInsertar { get; set; } = "sp_experiencia_insertar";
        protected override string SPEliminar { get; set; } = "sp_experiencia_eliminar";
        private string SPTraerPorCandidato { get; set; } = "sp_experiencia_traer_por_candidato";
        private string SPTraerTecnologias { get; set; } = "sp_experiencia_traer_tecnologias";
        private string SPEliminarTecnologias { get; set; } = "sp_experiencia_eliminar_tecnologias";
        private string SPAgregarTecnologia { get; set; } = "sp_perfil_insertar_tecnologia";

        public IEnumerable<Experiencia> TraerPorCandidato(Candidato Candidato)
        {

            DataTable data = this.contexto.EjecutarQuery(SPTraerPorCandidato, this.PrepararParametros(EAccion.TraerPorCandidato, new Experiencia { Candidato = Candidato }));

            foreach (DataRow row in data.Rows)
            {
                Experiencia entidad = this.MapearDataRow(row);

                yield return entidad;
            }

            yield return default;
        }

        private IEnumerable<Tecnologia> TraerTecnologias(Experiencia Experiencia)
        {
            DataTable data = this.contexto.EjecutarQuery(SPTraerTecnologias, this.PrepararParametros(EAccion.TraerTecnologias, Experiencia));

            foreach (DataRow row in data.Rows)
            {
                Tecnologia entidad = this.TecnologiaRepo.MapearDataRow(row);

                yield return entidad;
            }

            yield return default;
        }

        public new bool Insertar(Experiencia Entidad)
        {
            this.contexto.EjecutarNoQuery(SPInsertar, this.PrepararParametros(EAccion.Insertar, Entidad));

            this.AgregarTecnologias(Entidad);

            return true;
        }

        public new bool Actualizar(Experiencia Entidad)
        {
            this.contexto.EjecutarNoQuery(SPActualizar, this.PrepararParametros(EAccion.Actualizar, Entidad));

            this.AgregarTecnologias(Entidad);

            return true;
        }

        private void AgregarTecnologias(Experiencia Entidad)
        {
            this.contexto.EjecutarNoQuery(SPEliminarTecnologias, this.PrepararParametros(EAccion.EliminarTecnologias, Entidad));

            int i = 0;
            foreach (var tecnologia in Entidad.Tecnologias)
            {
                this.contexto.EjecutarNoQuery(SPAgregarTecnologia, this.PrepararParametros(EAccion.AgregarTecnologia, Entidad, i));
                i++;
            }
        }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Experiencia Entidad, int Elemento = 0)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Puesto = new SqlParameter();
            SqlParameter Categoria = new SqlParameter();
            SqlParameter Empresa = new SqlParameter();
            SqlParameter FechaDesde = new SqlParameter();
            SqlParameter FechaHasta = new SqlParameter();
            SqlParameter Descripcion = new SqlParameter();
            SqlParameter CuilCandidato = new SqlParameter();
            SqlParameter CodigoTecnologia = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Puesto.ParameterName = "puesto";
            Categoria.ParameterName = "categoria";
            Empresa.ParameterName = "empresa";
            FechaDesde.ParameterName = "fechadesde";
            FechaHasta.ParameterName = "fechahasta";
            Descripcion.ParameterName = "descripcion";
            CuilCandidato.ParameterName = "cuil_candidato";
            CodigoTecnologia.ParameterName = "codigo_tecnologia";

            Codigo.Value = Entidad.Codigo;
            Puesto.Value = Entidad.Puesto;
            Categoria.Value = Entidad.Categoria;
            Empresa.Value = Entidad.Empresa;
            FechaDesde.Value = Entidad.FechaDesde;
            FechaHasta.Value = Entidad.FechaHasta;
            Descripcion.Value = Entidad.Descripcion;
            CuilCandidato.Value = Entidad.Candidato.Cuil;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Puesto);
                    Parametros.Add(Categoria);
                    Parametros.Add(Empresa);
                    Parametros.Add(FechaDesde);
                    Parametros.Add(FechaHasta);
                    Parametros.Add(Descripcion);
                    Parametros.Add(CuilCandidato);
                    break;

                case EAccion.TraerPorCandidato:
                    Parametros.Add(CuilCandidato);
                    break;

                case EAccion.AgregarTecnologia:
                    CodigoTecnologia.Value = Entidad.Tecnologias[Elemento].Codigo;
                    Parametros.Add(Codigo);
                    Parametros.Add(CodigoTecnologia);
                    break;

                case EAccion.TraerTecnologias:
                case EAccion.Traer:
                case EAccion.Eliminar:
                    Parametros.Add(Codigo);
                    break;

                default:
                    break;
            }

            return Parametros.ToArray();
        }

        internal override Experiencia MapearDataRow(DataRow Row)
        {
            Experiencia expe = new Experiencia();

            expe.Codigo = new Guid(Row["id"].ToString());
            expe.Puesto = Row["puesto"].ToString();
            expe.Categoria = (ECategoria)Enum.Parse(typeof(ECategoria), Row["categoria"].ToString(), true);
            expe.Empresa = Row["empresa"].ToString();
            expe.FechaDesde = Convert.ToDateTime(Row["fechadesde"].ToString());
            expe.FechaHasta = Convert.ToDateTime(Row["fechahasta"].ToString());
            expe.Descripcion = Row["descripcion"].ToString();
            expe.Candidato = this.CandidatoRepo.Traer(new Candidato { Cuil = Row["cuil"].ToString() });
            expe.Tecnologias = this.TraerTecnologias(expe).ToList();

            return expe;
        }
    }
}
