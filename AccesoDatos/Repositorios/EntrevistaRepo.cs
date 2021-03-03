using AccesoDatos.Contexto;
using AccesoDatos.Enums;
using AccesoDatos.Helpers;
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
    public class EntrevistaRepo : Repositorio<Entrevista>
    {
        protected override string SPTraerTodo { get; set; } = "sp_entrevista_traer";
        protected override string SPTraerUno { get; set; } = "sp_entrevista_traeruno";
        protected override string SPActualizar { get; set; } = "sp_entrevista_actualizar";
        protected override string SPInsertar { get; set; } = "sp_entrevista_insertar";
        protected override string SPEliminar { get; set; } = "sp_entrevista_eliminar";
        private string SPTraerPorProceso { get; set; } = "sp_entrevista_traer_por_proceso";

        public EntrevistaRepo(IDBContexto contexto) : base(contexto)
        {

        }

        public List<Entrevista> TraerPorProceso(ProcesoSeleccion Proceso)
        {
            DataTable data = this.contexto.EjecutarQuery(SPTraerPorProceso, this.PrepararParametros(EAccion.TraerPorProceso, new Entrevista { ProcesoSeleccion = Proceso }));

            List<Entrevista> lista = new List<Entrevista>();

            foreach (DataRow row in data.Rows)
            {
                Entrevista entidad = this.MapearDataRow(row);
                lista.Add(entidad);
            }

            return lista;
        }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Entrevista Entidad, int Elemento = 0)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Descripcion = new SqlParameter();
            SqlParameter TipoEntrevista = new SqlParameter();
            SqlParameter CodigoProcesoSeleccion = new SqlParameter();
            SqlParameter LegajoEntrevistador = new SqlParameter();
            SqlParameter Orden = new SqlParameter();
            SqlParameter Estado = new SqlParameter();
            SqlParameter Comentarios = new SqlParameter();
            SqlParameter Puntaje = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Descripcion.ParameterName = "descripcion";
            TipoEntrevista.ParameterName = "tipoentrevista";
            CodigoProcesoSeleccion.ParameterName = "codigo_procesoseleccion";
            LegajoEntrevistador.ParameterName = "legajo_entrevistador";
            Orden.ParameterName = "orden";
            Estado.ParameterName = "estado";
            Comentarios.ParameterName = "comentarios";
            Puntaje.ParameterName = "puntaje";

            Codigo.Value = Entidad.Codigo;
            Descripcion.Value = Entidad.Descripcion;
            TipoEntrevista.Value = Entidad.TipoEntrevista;
            CodigoProcesoSeleccion.Value = Entidad.ProcesoSeleccion.Codigo;
            LegajoEntrevistador.Value = Entidad.Entrevistador.Legajo;
            Orden.Value = Entidad.Orden;
            Estado.Value = Entidad.Estado;
            Comentarios.Value = Entidad.Comentarios;
            Puntaje.Value = Entidad.Puntaje;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Descripcion);
                    Parametros.Add(TipoEntrevista);
                    Parametros.Add(CodigoProcesoSeleccion);
                    Parametros.Add(LegajoEntrevistador);
                    Parametros.Add(Orden);
                    Parametros.Add(Estado);
                    Parametros.Add(Comentarios);
                    Parametros.Add(Puntaje);
                    break;

                case EAccion.TraerPorProceso:
                    Parametros.Add(CodigoProcesoSeleccion);
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

        internal override Entrevista MapearDataRow(DataRow Row)
        {
            Entrevista entre = new Entrevista();

            entre.Codigo = new Guid(Row["id"].ToString());
            entre.Descripcion = Row["descripcion"].ToString();
            entre.TipoEntrevista = (ETipoEntrevista)Enum.Parse(typeof(ETipoEntrevista), Row["tipoentrevista"].ToString(), true);
            entre.ProcesoSeleccion = new ProcesoSeleccion { Codigo = new Guid(Row["id_proceso_seleccion"].ToString()) };
            entre.Entrevistador = new Usuario { Legajo = Row["legajo_entrevistador"].ToString() };
            entre.Orden = Convert.ToInt32(Row["orden"].ToString());
            entre.Estado = (EEstadoEntrevista)Enum.Parse(typeof(EEstadoEntrevista), Row["estado"].ToString(), true);
            entre.Comentarios = Row["comentarios"].ToString();
            entre.Puntaje = Convert.ToInt32(Row["puntaje"].ToString());

            return entre;
        }
    }
}
