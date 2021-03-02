using AccesoDatos.Enums;
using Entidad.Enums;
using Entidad.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Repositorios
{
    public class UsuarioRepo : Repositorio<Usuario>
    {

        protected override string SPTraerTodo { get; set; } = "sp_usuario_traer";
        protected override string SPTraerUno { get; set; } = "sp_usuario_traeruno";
        protected override string SPActualizar { get; set; } = "sp_usuario_actualizar";
        protected override string SPInsertar { get; set; } = "sp_usuario_insertar";
        protected override string SPEliminar { get; set; } = "sp_usuario_eliminar";
        private string SPTraerPorPuesto { get; set; } = "sp_usuario_traer_por_puesto";
        private string SPCambiarPassword { get; set; } = "sp_usuario_cambiar_password";

        public IEnumerable<Usuario> TraerPorPuesto(EPuesto Puesto)
        {
            Usuario Entidad = new Usuario();
            Entidad.Puesto = Puesto;

            DataTable data = this.contexto.EjecutarQuery(SPTraerPorPuesto, this.PrepararParametros(EAccion.TraerPorPuesto, Entidad));

            foreach (DataRow row in data.Rows)
            {
                Usuario entidad = this.MapearDataRow(row);

                yield return entidad;
            }
        }

        public bool CambiarPassword(Usuario Entidad)
        {
            this.contexto.EjecutarNoQuery(SPCambiarPassword, this.PrepararParametros(EAccion.CambiarPassword, Entidad));

            return true;
        }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Usuario Entidad, int Elemento = 0)
        {
            SqlParameter Legajo = new SqlParameter();
            SqlParameter Nombre = new SqlParameter();
            SqlParameter Apellido = new SqlParameter();
            SqlParameter Puesto = new SqlParameter();
            SqlParameter User = new SqlParameter();
            SqlParameter Password = new SqlParameter();

            Legajo.ParameterName = "legajo";
            Nombre.ParameterName = "nombre";
            Apellido.ParameterName = "apellido";
            Puesto.ParameterName = "puesto";
            User.ParameterName = "user";
            Password.ParameterName = "password";

            Legajo.Value = Entidad.Legajo;
            Nombre.Value = Entidad.Nombre;
            Apellido.Value = Entidad.Apellido;
            Puesto.Value = Entidad.Puesto.ToString();
            Password.Value = Entidad.Password;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Legajo);
                    Parametros.Add(Nombre);
                    Parametros.Add(Apellido);
                    Parametros.Add(Puesto);
                    break;

                case EAccion.Traer:
                case EAccion.Eliminar:
                    Parametros.Add(Legajo);
                    break;

                case EAccion.TraerPorPuesto:
                    Parametros.Add(Puesto);
                    break;

                case EAccion.CambiarPassword:
                    Parametros.Add(Legajo);
                    Parametros.Add(Password);
                    break;

                default:                    
                    break;
            }

            return Parametros.ToArray();
        }

        internal override Usuario MapearDataRow(DataRow Row)
        {
            Usuario usu = new Usuario();

            usu.Legajo = Row["legajo"].ToString();
            usu.Nombre = Row["nombre"].ToString();
            usu.Apellido = Row["apellido"].ToString();
            usu.Puesto = (EPuesto)Enum.Parse(typeof(EPuesto), Row["puesto"].ToString(), true);
            usu.Password = Row["password"].ToString();

            return usu;
        }
    }
}