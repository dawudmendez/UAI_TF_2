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
    public class OficinaRepo : Repositorio<Oficina>
    {
        private DireccionRepo DireccionRepo = new DireccionRepo();
        private ContactoRepo ContactoRepo = new ContactoRepo();        

        protected override string SPTraerTodo { get; set; } = "sp_oficina_traer";
        protected override string SPTraerUno { get; set; } = "sp_oficina_traeruno";
        protected override string SPActualizar { get; set; } = "sp_oficina_actualizar";
        protected override string SPInsertar { get; set; } = "sp_oficina_insertar";
        protected override string SPEliminar { get; set; } = "sp_oficina_eliminar";

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Oficina Entidad)
        {
            SqlParameter Nombre = new SqlParameter();
            SqlParameter CodigoDireccion = new SqlParameter();
            SqlParameter CodigoContacto = new SqlParameter();

            Nombre.ParameterName = "nombre";
            CodigoDireccion.ParameterName = "codigo_direccion";
            CodigoContacto.ParameterName = "codigo_contacto";

            Nombre.Value = Entidad.Nombre;
            CodigoDireccion.Value = Entidad.Direccion?.Codigo;
            CodigoContacto.Value = Entidad.Contacto?.Codigo;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Nombre);
                    Parametros.Add(CodigoDireccion);
                    Parametros.Add(CodigoContacto);
                    break;

                case EAccion.Traer:
                case EAccion.Eliminar:
                    Parametros.Add(Nombre);
                    break;

                default:
                    break;
            }

            return Parametros.ToArray();
        }

        internal override Oficina MapearDataRow(DataRow Row)
        {
            Oficina ofi = new Oficina();

            ofi.Nombre = Row["nombre"].ToString();
            ofi.Direccion = this.DireccionRepo.Traer(new Direccion { Codigo = new Guid(Row["codigo_direccion"].ToString()) });
            ofi.Contacto = this.ContactoRepo.Traer(new Contacto { Codigo = new Guid(Row["codigo_contacto"].ToString()) });

            return ofi;
        }
    }
}
