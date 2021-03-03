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
    public class CandidatoRepo : Repositorio<Candidato>
    {
        protected override string SPTraerTodo { get ; set ; } = "sp_candidato_traer";
        protected override string SPTraerUno { get ; set ; } = "sp_candidato_traeruno";
        protected override string SPActualizar { get ; set ; } = "sp_candidato_actualizar";
        protected override string SPInsertar { get ; set ; } = "sp_candidato_insertar";
        protected override string SPEliminar { get ; set ; } = "sp_candidato_eliminar";

        public CandidatoRepo(IDBContexto contexto) : base(contexto)
        {

        }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Candidato Entidad, int Elemento = 0)
        {
            SqlParameter Cuil = new SqlParameter();
            SqlParameter DNI = new SqlParameter();
            SqlParameter Nombre = new SqlParameter();
            SqlParameter Apellido = new SqlParameter();
            SqlParameter FecNac = new SqlParameter();
            SqlParameter CodigoContacto = new SqlParameter();
            SqlParameter CodigoDireccion = new SqlParameter();

            Cuil.ParameterName = "cuil";
            DNI.ParameterName = "dni";
            Nombre.ParameterName = "nombre";
            Apellido.ParameterName = "apellido";
            FecNac.ParameterName = "fecnac";
            CodigoContacto.ParameterName = "codigo_contacto";
            CodigoDireccion.ParameterName = "codigo_direccion";

            Cuil.Value = Entidad.Cuil;
            DNI.Value = Entidad.DNI;
            Nombre.Value = Entidad.Nombre;
            Apellido.Value = Entidad.Apellido;
            FecNac.Value = Entidad.FecNac;
            CodigoContacto.Value = Entidad.Contacto?.Codigo.ToString();
            CodigoDireccion.Value = Entidad.Direccion?.Codigo.ToString();

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Cuil);
                    Parametros.Add(DNI);
                    Parametros.Add(Nombre);
                    Parametros.Add(Apellido);
                    Parametros.Add(FecNac);
                    Parametros.Add(CodigoContacto);
                    Parametros.Add(CodigoDireccion);
                    break;

                case EAccion.Traer:
                case EAccion.Eliminar:
                    Parametros.Add(Cuil);
                    break;

                default:
                    break;
            }

            return Parametros.ToArray();
        }

        internal override Candidato MapearDataRow(DataRow Row)
        {
            Candidato cand = new Candidato();

            cand.Cuil = Row["cuil"].ToString();
            cand.DNI = Convert.ToInt64(Row["dni"].ToString());
            cand.Nombre = Row["nombre"].ToString();
            cand.Apellido = Row["apellido"].ToString();
            cand.FecNac = Convert.ToDateTime(Row["fecnac"].ToString());
            cand.Contacto = new Contacto { Codigo = new Guid(Row["codigo_contacto"].ToString()) };
            cand.Direccion = new Direccion { Codigo = new Guid(Row["codigo_direccion"].ToString()) };

            return cand;
        }
    }
}
