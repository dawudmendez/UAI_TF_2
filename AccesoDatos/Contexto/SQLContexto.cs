using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Contexto
{
    public class SQLContexto : IDBContexto
    {
        private SqlConnection Conexion;

        private void Conectar()
        {
            Conexion = new SqlConnection("Server=localhost;Database=SGRP;User Id=SGRP_dbo;Password=1234;");
            Conexion.Open();
        }

        private void Desconectar()
        {
            if (Conexion != null)
            {
                Conexion.Close();
                Conexion.Dispose();
            }

        }

        public DataTable EjecutarQuery(string StoredProcedure, params SqlParameter[] Parametros)
        {

            SqlCommand Comando = new SqlCommand();
            DataTable DataTable = new DataTable();
            SqlDataAdapter DataAdapter = new SqlDataAdapter();

            try
            {
                Conectar();
                Comando.Connection = Conexion;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = StoredProcedure;

                foreach (SqlParameter par in Parametros)
                    Comando.Parameters.Add(par);

                DataAdapter.SelectCommand = Comando;
                DataAdapter.Fill(DataTable);
                DataAdapter.Dispose();
                Comando.Dispose();

            }
            catch (SqlException ex)
            {
                if (Comando.Transaction != null)
                {
                    Comando.Transaction.Rollback();
                }

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
            catch (Exception ex)
            {
                if (Comando.Transaction != null)
                {
                    Comando.Transaction.Rollback();
                }

                throw ex;
            }
            finally
            {
                this.Desconectar();
            }

            return DataTable;
        }

        public void EjecutarNoQuery(string StoredProcedure, params SqlParameter[] Parametros)
        {
            SqlCommand Comando = new SqlCommand();
            DataTable DataTable = new DataTable();
            SqlDataAdapter DataAdapter = new SqlDataAdapter();

            try
            {
                this.Conectar();
                Comando.Connection = Conexion;
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.CommandText = StoredProcedure;
                Comando.Transaction = Conexion.BeginTransaction();

                foreach (SqlParameter par in Parametros)
                    Comando.Parameters.Add(par);

                Comando.ExecuteNonQuery();
                Comando.Transaction.Commit();
                Comando.Dispose();
            }
            catch (SqlException ex)
            {
                if (Comando.Transaction != null)
                {
                    Comando.Transaction.Rollback();
                }

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
            catch (Exception ex)
            {
                if (Comando.Transaction != null)
                {
                    Comando.Transaction.Rollback();
                }

                throw ex;
            }
            finally
            {
                this.Desconectar();
            }
        }
    }
}
