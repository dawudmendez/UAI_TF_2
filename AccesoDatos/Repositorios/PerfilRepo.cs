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
    class PerfilRepo : Repositorio<Perfil>
    {
        private TecnologiaRepo TecnologiaRepo = new TecnologiaRepo();

        protected override string SPTraerTodo { get; set; } = "sp_perfil_traer";
        protected override string SPTraerUno { get; set; } = "sp_perfil_traeruno";
        protected override string SPActualizar { get; set; } = "sp_perfil_actualizar";
        protected override string SPInsertar { get; set; } = "sp_perfil_insertar";
        protected override string SPEliminar { get; set; } = "sp_perfil_eliminar";
        private string SPTraerTecnologias { get; set; } = "sp_perfil_traer_tecnologias";

        private IEnumerable<Tecnologia> TraerTecnologias(Perfil Perfil)
        {
            DataTable data = this.Contexto.EjecutarQuery(SPTraerTecnologias, this.PrepararParametros(EAccion.TraerTecnologias, Perfil));

            foreach (DataRow row in data.Rows)
            {
                Tecnologia entidad = this.TecnologiaRepo.MapearDataRow(row);

                yield return entidad;
            }

            yield return default;
        }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Perfil Entidad)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Nombre = new SqlParameter();
            SqlParameter Categoria = new SqlParameter();
            SqlParameter AniosExperiencia = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Nombre.ParameterName = "nombre";
            Categoria.ParameterName = "categoria";
            AniosExperiencia.ParameterName = "aniosexperiencia";

            Codigo.Value = Entidad.Codigo;
            Nombre.Value = Entidad.Nombre;
            Categoria.Value = Entidad.Categoria;
            AniosExperiencia.Value = Entidad.AniosExperiencia;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Nombre);
                    Parametros.Add(Categoria);
                    Parametros.Add(AniosExperiencia);
                    break;

                case EAccion.Insertar:
                    Parametros.Add(Nombre);
                    Parametros.Add(Categoria);
                    Parametros.Add(AniosExperiencia);
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

        internal override Perfil MapearDataRow(DataRow Row)
        {
            Perfil perf = new Perfil();

            perf.Codigo = new Guid(Row["codigo"].ToString());
            perf.Nombre = Row["nombre"].ToString();
            perf.Categoria = (ECategoria)Enum.Parse(typeof(ECategoria), Row["categoria"].ToString(), true);
            perf.AniosExperiencia = Convert.ToInt32(Row["aniosexperiencia"].ToString());
            perf.Tecnologias = this.TraerTecnologias(perf).ToList();

            return perf;
        }
    }
}
