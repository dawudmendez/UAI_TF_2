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
    public class DireccionRepo : Repositorio<Direccion>
    {
        protected override string SPTraerTodo { get; set; } = "sp_direccion_traer";
        protected override string SPTraerUno { get; set; } = "sp_direccion_traeruno";
        protected override string SPActualizar { get; set; } = "sp_direccion_actualizar";
        protected override string SPInsertar { get; set; } = "sp_direccion_insertar";
        protected override string SPEliminar { get; set; } = "sp_direccion_eliminar";

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Direccion Entidad)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Provincia = new SqlParameter();
            SqlParameter Localidad = new SqlParameter();
            SqlParameter Ciudad = new SqlParameter();
            SqlParameter Barrio = new SqlParameter();
            SqlParameter Calle = new SqlParameter();
            SqlParameter Numero = new SqlParameter();
            SqlParameter CodigoPostal = new SqlParameter();
            SqlParameter Torre = new SqlParameter();
            SqlParameter Piso = new SqlParameter();
            SqlParameter Departamento = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Provincia.ParameterName = "provincia";
            Localidad.ParameterName = "localidad";
            Ciudad.ParameterName = "ciudad";
            Barrio.ParameterName = "barrio";
            Calle.ParameterName = "calle";
            Numero.ParameterName = "numero";
            CodigoPostal.ParameterName = "codigopostal";
            Torre.ParameterName = "torre";
            Piso.ParameterName = "piso";
            Departamento.ParameterName = "departamento";

            Codigo.Value = Entidad.Codigo;
            Provincia.Value = Entidad.Provincia;
            Localidad.Value = Entidad.Localidad;
            Ciudad.Value = Entidad.Ciudad;
            Barrio.Value = Entidad.Barrio;
            Calle.Value = Entidad.Calle;
            Numero.Value = Entidad.Numero;
            CodigoPostal.Value = Entidad.CodigoPostal;
            Torre.Value = Entidad.Torre;
            Piso.Value = Entidad.Piso;
            Departamento.Value = Entidad.Departamento;

            List<SqlParameter> Parametros = new List<SqlParameter>();            

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Provincia);
                    Parametros.Add(Localidad);
                    Parametros.Add(Ciudad);
                    Parametros.Add(Barrio);
                    Parametros.Add(Calle);
                    Parametros.Add(Numero);
                    Parametros.Add(CodigoPostal);
                    Parametros.Add(Torre);
                    Parametros.Add(Piso);
                    Parametros.Add(Departamento);
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

        internal override Direccion MapearDataRow(DataRow Row)
        {
            Direccion dire = new Direccion();

            dire.Codigo = new Guid(Row["codigo"].ToString());
            dire.Provincia = Row["provincia"].ToString();
            dire.Localidad = Row["localidad"].ToString();
            dire.Ciudad = Row["ciudad"].ToString();
            dire.Barrio = Row["barrio"].ToString();
            dire.Calle = Row["calle"].ToString();
            dire.Numero = Convert.ToInt64(Row["numero"].ToString());
            dire.CodigoPostal = Row["codigopostal"].ToString();
            dire.Torre = Row["torre"].ToString();
            dire.Piso = Row["piso"].ToString();
            dire.Departamento = Row["departamento"].ToString();

            return dire;
        }
    }
}
