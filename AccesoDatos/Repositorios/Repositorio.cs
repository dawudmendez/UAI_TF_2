using AccesoDatos.Contexto;
using AccesoDatos.Enums;
using Entidades.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Repositorios
{
    public abstract class Repositorio<T> where T : IEntidad
    {

        protected SQLContexto Contexto = new SQLContexto();

        protected abstract string SPTraerTodo { get; set; }
        protected abstract string SPTraerUno { get; set; }
        protected abstract string SPActualizar { get; set; }
        protected abstract string SPInsertar { get; set; }
        protected abstract string SPEliminar { get; set; }    

        public bool Actualizar(T Entidad)
        {
            this.Contexto.EjecutarNoQuery(SPActualizar, this.PrepararParametros(EAccion.Actualizar, Entidad));

            return true;
        }

        public bool Eliminar(T Entidad)
        {
            this.Contexto.EjecutarNoQuery(SPEliminar, this.PrepararParametros(EAccion.Eliminar, Entidad));

            return true;
        }

        public bool Insertar(T Entidad)
        {
            this.Contexto.EjecutarNoQuery(SPInsertar, this.PrepararParametros(EAccion.Insertar, Entidad));

            return true;
        }

        public T Traer(T Entidad)
        {
            DataTable data = this.Contexto.EjecutarQuery(SPTraerUno, this.PrepararParametros(EAccion.Traer, Entidad));


            foreach (DataRow row in data.Rows)
            {
                T entidad = this.MapearDataRow(row);

                return entidad;
            }

            return default;

        }

        public IEnumerable<T> TraerTodo()
        {
            DataTable data = this.Contexto.EjecutarQuery(SPTraerTodo);

            foreach (DataRow row in data.Rows)
            {
                T entidad = this.MapearDataRow(row);

                yield return entidad;
            }

            yield return default;
        }

        protected abstract SqlParameter[] PrepararParametros(EAccion Accion, T Entidad);

        internal abstract T MapearDataRow(DataRow Row);


    }
}
