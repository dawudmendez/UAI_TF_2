using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Contexto;
using AccesoDatos.Enums;
using AccesoDatos.Interfaces;
using Entidad.Negocio;

namespace AccesoDatos.Repositorios
{
    public class ContactoRepo : Repositorio<Contacto>
    {
        protected override string SPTraerTodo { get ; set ; } = "sp_contacto_traer";
        protected override string SPTraerUno { get ; set ; } = "sp_contacto_traeruno";
        protected override string SPActualizar { get ; set ; } = "sp_contacto_actualizar";
        protected override string SPInsertar { get ; set ; } = "sp_contacto_insertar";
        protected override string SPEliminar { get ; set ; } = "sp_contacto_eliminar";

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Contacto Entidad)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Email = new SqlParameter();
            SqlParameter Telefono = new SqlParameter();
            SqlParameter SitioWeb = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Email.ParameterName = "email";
            Telefono.ParameterName = "telefono";
            SitioWeb.ParameterName = "sitioweb";

            Codigo.Value = Entidad.Codigo;
            Email.Value = Entidad.Email;
            Telefono.Value = Entidad.Telefono;
            SitioWeb.Value = Entidad.SitioWeb?.ToString();

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Email);
                    Parametros.Add(Telefono);
                    Parametros.Add(SitioWeb);
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

        internal override Contacto MapearDataRow(DataRow Row)
        {
            Contacto cont = new Contacto();
            cont.Codigo = new Guid(Row["codigo"].ToString());
            cont.Email = Row["email"].ToString();
            cont.Telefono = Row["telefono"].ToString();
            cont.SitioWeb = new Uri(Row["sitioweb"]?.ToString());

            return cont;
        }
    }
}
