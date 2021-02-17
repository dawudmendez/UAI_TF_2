using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Contexto
{
    public interface IDBContexto
    {
        void EjecutarNoQuery(string StoredProcedure, params SqlParameter[] Parametros);
        DataTable EjecutarQuery(string StoredProcedure, params SqlParameter[] Parametros);
    }
}