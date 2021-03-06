﻿using AccesoDatos.Contexto;
using AccesoDatos.Enums;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Repositorios
{
    public abstract class Repositorio<T> where T : IEntidad
    {
        protected IDBContexto contexto;

        protected abstract string SPTraerTodo { get; set; }
        protected abstract string SPTraerUno { get; set; }
        protected abstract string SPActualizar { get; set; }
        protected abstract string SPInsertar { get; set; }
        protected abstract string SPEliminar { get; set; }

        public Repositorio(IDBContexto contexto)
        {
            this.contexto = contexto;
        }

        public bool Actualizar(T Entidad)
        {
            this.contexto.EjecutarNoQuery(SPActualizar, this.PrepararParametros(EAccion.Actualizar, Entidad));

            return true;
        }

        public bool Eliminar(T Entidad)
        {
            this.contexto.EjecutarNoQuery(SPEliminar, this.PrepararParametros(EAccion.Eliminar, Entidad));

            return true;
        }

        public bool Insertar(T Entidad)
        {
            this.contexto.EjecutarNoQuery(SPInsertar, this.PrepararParametros(EAccion.Insertar, Entidad));

            return true;
        }

        public T Traer(T Entidad)
        {
            DataTable data = this.contexto.EjecutarQuery(SPTraerUno, this.PrepararParametros(EAccion.Traer, Entidad));


            foreach (DataRow row in data.Rows)
            {
                T entidad = this.MapearDataRow(row);

                return entidad;
            }

            return default;

        }

        public List<T> TraerTodo()
        {
            DataTable data = this.contexto.EjecutarQuery(SPTraerTodo);

            List<T> lista = new List<T>();

            foreach (DataRow row in data.Rows)
            {
                T entidad = this.MapearDataRow(row);
                lista.Add(entidad);
            }

            return lista;
        }        

        protected abstract SqlParameter[] PrepararParametros(EAccion Accion, T Entidad, int Elemento = 0);

        internal abstract T MapearDataRow(DataRow Row);
    }
}
