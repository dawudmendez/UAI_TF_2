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

        private string SPTraerCandidatosRecomendados { get; set; } = "sp_analytics_traer_candidatos_recomendados";



        //public List<Candidato> TraerCandidatosRecomendados(Guid CodigoPosicion)
        //{
        //    SqlParameter guid = new SqlParameter();
        //    guid.ParameterName = "codigo_posicion";
        //    guid.Value = CodigoPosicion;

        //    DataTable data = SQLContexto.EjecutarQuery(SPTraerCandidatosRecomendados, guid);

        //    List<Candidato> lista = new List<Candidato>();

        //    foreach (DataRow row in data.Rows)
        //    {
        //        Candidato entidad = candidatoRepo.MapearDataRow(row);

        //        lista.Add(entidad);
        //    }

        //    return lista;
        //}
    }
}
