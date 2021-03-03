using AccesoDatos.Contexto;
using AccesoDatos.Enums;
using Entidad.Enums;
using Entidad.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AccesoDatos.Repositorios
{
    public class ProcesoSeleccionRepo : Repositorio<ProcesoSeleccion>
    {
        protected override string SPTraerTodo { get; set; } = "sp_proceso_traer";
        protected override string SPTraerUno { get; set; } = "sp_proceso_traeruno";
        protected override string SPActualizar { get; set; } = "sp_proceso_actualizar";
        protected override string SPInsertar { get; set; } = "sp_proceso_insertar";
        protected override string SPEliminar { get; set; } = "sp_proceso_eliminar";

        public ProcesoSeleccionRepo(IDBContexto contexto) : base(contexto)
        {

        }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, ProcesoSeleccion Entidad, int Elemento = 0)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Nombre = new SqlParameter();
            SqlParameter Descripcion = new SqlParameter();
            SqlParameter IdPosicion = new SqlParameter();
            SqlParameter CuilCandidato = new SqlParameter();
            SqlParameter LegajoReclutador = new SqlParameter();
            SqlParameter Estado = new SqlParameter();
            SqlParameter Comentarios = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Nombre.ParameterName = "nombre";
            Descripcion.ParameterName = "descripcion";
            IdPosicion.ParameterName = "codigo_posicion";
            CuilCandidato.ParameterName = "cuil_candidato";
            LegajoReclutador.ParameterName = "legajo_reclutador";
            Estado.ParameterName = "estado";
            Comentarios.ParameterName = "comentarios";

            Codigo.Value = Entidad.Codigo;
            Nombre.Value = Entidad.Nombre;
            Descripcion.Value = Entidad.Descripcion;
            IdPosicion.Value = Entidad.Posicion.Codigo;
            CuilCandidato.Value = Entidad.Candidato.Cuil;
            LegajoReclutador.Value = Entidad.Reclutador.Legajo;
            Estado.Value = Entidad.Estado;
            Comentarios.Value = Entidad.Comentarios;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Nombre);
                    Parametros.Add(Descripcion);
                    Parametros.Add(IdPosicion);
                    Parametros.Add(CuilCandidato);
                    Parametros.Add(LegajoReclutador);
                    Parametros.Add(Estado);
                    Parametros.Add(Comentarios);
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

        internal override ProcesoSeleccion MapearDataRow(DataRow Row)
        {
            ProcesoSeleccion proc = new ProcesoSeleccion();

            proc.Codigo = new Guid(Row["id"].ToString());
            proc.Nombre = Row["nombre"].ToString();
            proc.Descripcion = Row["descripcion"].ToString();
            proc.Posicion = new Posicion { Codigo = new Guid(Row["id"].ToString()) };
            proc.Candidato = new Candidato { Cuil = Row["cuil"].ToString() };
            proc.Reclutador = new Usuario { Legajo = Row["legajo"].ToString() };
            proc.Estado = (EEstadoProcesoSeleccion)Enum.Parse(typeof(EEstadoProcesoSeleccion), Row["estado"].ToString(), true);
            proc.Comentarios = Row["comentarios"].ToString();

            return proc;
        }
    }
}
