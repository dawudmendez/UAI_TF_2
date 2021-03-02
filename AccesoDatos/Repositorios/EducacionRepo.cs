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
    public class EducacionRepo : Repositorio<Educacion>
    {
        private CandidatoRepo CandidatoRepo = new CandidatoRepo();

        protected override string SPTraerTodo { get; set; } = "sp_educacion_traer";
        protected override string SPTraerUno { get; set; } = "sp_educacion_traeruno";
        protected override string SPActualizar { get; set; } = "sp_educacion_actualizar";
        protected override string SPInsertar { get; set; } = "sp_educacion_insertar";
        protected override string SPEliminar { get; set; } = "sp_educacion_eliminar";
        private string SPTraerPorCandidato { get; set; } = "sp_educacion_traer_por_candidato";

        public IEnumerable<Educacion> TraerPorCandidato(Candidato Candidato)
        {
            DataTable data = this.contexto.EjecutarQuery(SPTraerPorCandidato, this.PrepararParametros(EAccion.TraerPorProceso, new Educacion { Candidato = Candidato }));

            foreach (DataRow row in data.Rows)
            {
                Educacion entidad = this.MapearDataRow(row);

                yield return entidad;
            }

            yield return default;
        }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Educacion Entidad, int Elemento = 0)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Institucion = new SqlParameter();
            SqlParameter Carrera = new SqlParameter();
            SqlParameter TipoCarrera = new SqlParameter();
            SqlParameter Duracion = new SqlParameter();
            SqlParameter FechaDesde = new SqlParameter();
            SqlParameter FechaHasta = new SqlParameter();
            SqlParameter RubroCarrera = new SqlParameter();
            SqlParameter Estado = new SqlParameter();
            SqlParameter CuilCandidato = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Institucion.ParameterName = "institucion";
            Carrera.ParameterName = "carrera";
            TipoCarrera.ParameterName = "tipocarrera";
            Duracion.ParameterName = "duracion";
            FechaDesde.ParameterName = "fechadesde";
            FechaHasta.ParameterName = "fechahasta";
            RubroCarrera.ParameterName = "rubrocarrera";
            Estado.ParameterName = "estado";
            CuilCandidato.ParameterName = "cuil_candidato";

            Codigo.Value = Entidad.Codigo;
            Institucion.Value = Entidad.Institucion;
            Carrera.Value = Entidad.Carrera;
            TipoCarrera.Value = Entidad.TipoCarrera;
            Duracion.Value = Entidad.Duracion;
            FechaDesde.Value = Entidad.FechaDesde;
            FechaHasta.Value = Entidad.FechaHasta;
            RubroCarrera.Value = Entidad.RubroCarrera;
            Estado.Value = Entidad.Estado;
            CuilCandidato.Value = Entidad.Candidato.Cuil;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Institucion);
                    Parametros.Add(Carrera);
                    Parametros.Add(TipoCarrera);
                    Parametros.Add(Duracion);
                    Parametros.Add(FechaDesde);
                    Parametros.Add(FechaHasta);
                    Parametros.Add(RubroCarrera);
                    Parametros.Add(Estado);
                    Parametros.Add(CuilCandidato);
                    break;

                case EAccion.TraerPorCandidato:
                    Parametros.Add(CuilCandidato);
                    break;

                case EAccion.Traer:
                case EAccion.Eliminar:
                    Parametros.Add(Codigo);
                    break;

                default:
                    break;
            }

            return Parametros.ToArray();
        }

        internal override Educacion MapearDataRow(DataRow Row)
        {
            Educacion edu = new Educacion();

            edu.Codigo = new Guid(Row["codigo"].ToString());
            edu.Institucion = Row["institucion"].ToString();
            edu.Carrera = Row["carrera"].ToString();
            edu.TipoCarrera = (ETipoCarrera)Enum.Parse(typeof(ETipoCarrera), Row["tipocarrera"].ToString(), true);
            edu.Duracion = Row["duracion"].ToString();
            edu.FechaDesde = Convert.ToDateTime(Row["fechadesde"].ToString());
            edu.FechaHasta = Convert.ToDateTime(Row["fechahasta"].ToString());
            edu.RubroCarrera = (ERubroCarrera)Enum.Parse(typeof(ERubroCarrera), Row["rubrocarrera"].ToString(), true);
            edu.Estado = (EEstadoEducacion)Enum.Parse(typeof(EEstadoEducacion), Row["estado"].ToString(), true);
            edu.Candidato = this.CandidatoRepo.Traer(new Candidato { Cuil = Row["cuil"].ToString() });

            return edu;
        }
    }
}
