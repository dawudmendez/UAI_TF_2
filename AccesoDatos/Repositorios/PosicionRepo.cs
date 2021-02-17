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
    class PosicionRepo : Repositorio<Posicion>
    {
        private PerfilRepo PerfilRepo = new PerfilRepo();
        private EquipoRepo EquipoRepo = new EquipoRepo();
        private OficinaRepo OficinaRepo = new OficinaRepo();

        protected override string SPTraerTodo { get; set; } = "sp_posicion_traer";
        protected override string SPTraerUno { get; set; } = "sp_posicion_traeruno";
        protected override string SPActualizar { get; set; } = "sp_posicion_actualizar";
        protected override string SPInsertar { get; set; } = "sp_posicion_insertar";
        protected override string SPEliminar { get; set; } = "sp_posicion_eliminar";

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Posicion Entidad)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Nombre = new SqlParameter();
            SqlParameter Descripcion = new SqlParameter();
            SqlParameter CodigoPerfil = new SqlParameter();
            SqlParameter NombreEquipo = new SqlParameter();
            SqlParameter Estado = new SqlParameter();
            SqlParameter NombreOficina = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Nombre.ParameterName = "nombre";
            Descripcion.ParameterName = "descripcion";
            CodigoPerfil.ParameterName = "codigo_perfil";
            NombreEquipo.ParameterName = "equipo";
            Estado.ParameterName = "estado";
            NombreOficina.ParameterName = "oficina";

            Codigo.Value = Entidad.Codigo;
            Nombre.Value = Entidad.Nombre;
            Descripcion.Value = Entidad.Descripcion;
            CodigoPerfil.Value = Entidad.Perfil.Codigo;
            NombreEquipo.Value = Entidad.Equipo.Nombre;
            Estado.Value = Entidad.Estado;
            NombreOficina.Value = Entidad.Oficina.Nombre;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Nombre);
                    Parametros.Add(Descripcion);
                    Parametros.Add(CodigoPerfil);
                    Parametros.Add(NombreEquipo);
                    Parametros.Add(Estado);
                    Parametros.Add(NombreOficina);
                    break;

                case EAccion.Insertar:
                    Parametros.Add(Nombre);
                    Parametros.Add(Descripcion);
                    Parametros.Add(CodigoPerfil);
                    Parametros.Add(NombreEquipo);
                    Parametros.Add(Estado);
                    Parametros.Add(NombreOficina);
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

        internal override Posicion MapearDataRow(DataRow Row)
        {
            Posicion posi = new Posicion();

            posi.Codigo = new Guid(Row["id"].ToString());
            posi.Nombre = Row["nombre"].ToString();
            posi.Descripcion = Row["descripcion"].ToString();
            posi.Perfil = this.PerfilRepo.Traer(new Perfil { Codigo = new Guid(Row["id_perfil"].ToString()) });
            posi.Equipo = this.EquipoRepo.Traer(new Equipo { Nombre = Row["equipo"].ToString() });
            posi.Estado = (EEstadoPosicion)Enum.Parse(typeof(EEstadoPosicion), Row["estado"].ToString(), true);
            posi.Oficina = this.OficinaRepo.Traer(new Oficina { Nombre = Row["oficina"].ToString() });

            return posi;
        }
    }
}
