using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Contexto
{
    public class SQLContexto : IDBContexto, IDisposable
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private bool conectado = false;
        private bool disposedValue;

        public SQLContexto()
        {
            this.Conectar();
            this.IniciarTransaccion();
        }

        public void Conectar()
        {
            if (!this.conectado)
            {
                this.conexion = new SqlConnection("Server=localhost;Database=SGRP;User Id=SGRP_dbo;Password=1234;");
                this.conexion.Open();
                this.conectado = true;
            }            
        }

        public void IniciarTransaccion()
        {
            if (this.comando == null)
            {
                this.comando = new SqlCommand();
                this.comando.Transaction = this.conexion.BeginTransaction();
            }
        }

        public void CommitTransaccion()
        {
            if (this.comando != null)
                this.comando.Transaction?.Commit();
        }

        public void RollbackTransaction()
        {
            if (this.comando != null)
                this.comando.Transaction?.Rollback();
        }

        public void Desconectar()
        {
            if (this.conexion != null)
            {
                this.conexion.Close();
                this.conexion.Dispose();
                this.conectado = false;
            }

            if (this.comando != null)
            {
                this.comando.Dispose();
                this.comando = null;
            }                
        }

        public DataTable EjecutarQuery(string StoredProcedure, params SqlParameter[] Parametros)
        {

            SqlCommand Comando = new SqlCommand();
            DataTable DataTable = new DataTable();
            SqlDataAdapter DataAdapter = new SqlDataAdapter();

            try
            {
                Comando.Connection = this.conexion;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = StoredProcedure;

                if (this.comando != null)
                    Comando.Transaction = this.comando.Transaction;

                foreach (SqlParameter par in Parametros)
                    Comando.Parameters.Add(par);

                DataAdapter.SelectCommand = Comando;
                DataAdapter.Fill(DataTable);
                DataAdapter.Dispose();
                Comando.Dispose();

            }
            catch (SqlException ex)
            {
                this.Rollback();
                switch (ex.Number)
                {                    
                    case 547:
                        throw new InvalidOperationException("Este registro se encuentra asociado a otro", ex);

                    case 2601:
                        throw new InvalidOperationException("Elemento duplicado", ex);

                    default:
                        throw new InvalidOperationException("Operación inválida. Verifique su base de datos", ex);

                }
            }
            catch (Exception)
            {
                this.Rollback();
                throw;
            }

            return DataTable;
        }

        public void EjecutarNoQuery(string StoredProcedure, params SqlParameter[] Parametros)
        {
            SqlCommand Comando = new SqlCommand();

            try
            {
                Comando.Connection = this.conexion;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = StoredProcedure;

                if (this.comando != null)
                    Comando.Transaction = this.comando.Transaction;

                foreach (SqlParameter par in Parametros)
                    Comando.Parameters.Add(par);

                Comando.ExecuteNonQuery();
                Comando.Dispose();
            }
            catch (SqlException ex)
            {
                this.Rollback();
                var switchExpr = ex.Number;
                switch (switchExpr)
                {
                    case 547:
                        throw new InvalidOperationException("Este registro se encuentra asociado a otro", ex);

                    case 2601:
                        throw new InvalidOperationException("Elemento duplicado", ex);

                    default:
                        throw new InvalidOperationException("Operación inválida", ex);
                }
            }
            catch (Exception)
            {
                this.Rollback();
                throw;
            }
        }

        public void Finalizar()
        {
            this.CommitTransaccion();
            this.Desconectar();
        }

        public void Rollback()
        {
            this.RollbackTransaction();
            this.Desconectar();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                this.Finalizar();

                this.disposedValue = true;
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SQLContexto()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
