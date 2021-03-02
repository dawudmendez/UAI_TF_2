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
    public class EquipoRepo : Repositorio<Equipo>
    {
        private UsuarioRepo UsuarioRepo = new UsuarioRepo();

        protected override string SPTraerTodo { get; set; } = "sp_equipo_traer";
        protected override string SPTraerUno { get; set; } = "sp_equipo_traeruno";
        protected override string SPActualizar { get; set; } = "sp_equipo_actualizar";
        protected override string SPInsertar { get; set; } = "sp_equipo_insertar";
        protected override string SPEliminar { get; set; } = "sp_equipo_eliminar";

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Equipo Entidad, int Elemento = 0)
        {
            SqlParameter Nombre = new SqlParameter();
            SqlParameter Descripcion = new SqlParameter();
            SqlParameter LegajoLider = new SqlParameter();
            SqlParameter LegajoManager = new SqlParameter();

            Nombre.ParameterName = "nombre";
            Descripcion.ParameterName = "descripcion";
            LegajoLider.ParameterName = "legajo_lider";
            LegajoManager.ParameterName = "legajo_manager";

            Nombre.Value = Entidad.Nombre;
            Descripcion.Value = Entidad.Descripcion;
            LegajoLider.Value = Entidad.Lider?.Legajo;
            LegajoManager.Value = Entidad.Manager?.Legajo;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Nombre);
                    Parametros.Add(Descripcion);
                    Parametros.Add(LegajoLider);
                    Parametros.Add(LegajoManager);
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

        internal override Equipo MapearDataRow(DataRow Row)
        {
            Equipo equi = new Equipo();

            equi.Nombre = Row["nombre"].ToString();
            equi.Descripcion = Row["descripcion"].ToString();
            equi.Lider = this.UsuarioRepo.Traer(new Usuario { Legajo = Row["legajo_lider"].ToString() });
            equi.Manager = this.UsuarioRepo.Traer(new Usuario { Legajo = Row["legajo_manager"].ToString() });

            return equi;
        }
    }
}
