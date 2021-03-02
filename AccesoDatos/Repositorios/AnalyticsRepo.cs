using AccesoDatos.Contexto;
using AccesoDatos.Enums;
using AccesoDatos.Interfaces;
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
    public class AnalyticsRepo
    {
        private SQLContexto contexto = new SQLContexto();
        private CandidatoRepo candidatoRepo = new CandidatoRepo();

        private string SPTraerCandidatosRecomendados { get; set; } = "sp_analytics_traer_candidatos_recomendados";

        public IEnumerable<Candidato> TraerCandidatosRecomendados(Guid CodigoPosicion)
        {
            SqlParameter guid = new SqlParameter();
            guid.ParameterName = "codigo_posicion";
            guid.Value = CodigoPosicion;

            DataTable data = this.contexto.EjecutarQuery(SPTraerCandidatosRecomendados, guid);

            foreach (DataRow row in data.Rows)
            {
                Candidato entidad = candidatoRepo.MapearDataRow(row);

                yield return entidad;
            }
        }
    }
}
