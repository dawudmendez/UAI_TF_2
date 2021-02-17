using AccesoDatos.Contexto;
using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    interface IRepositorio<T> where T : IEntidad
    {
        T Traer(T Entidad);
        IEnumerable<T> TraerTodo();
        bool Insertar(T Entidad);
        bool Actualizar(T Entidad);
        bool Eliminar(T Entidad);
    }
}
