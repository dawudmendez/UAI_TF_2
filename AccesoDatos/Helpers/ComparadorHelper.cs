using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Helpers
{
    public static class ComparadorHelper
    {
        public static bool CompararCon(this object Este, object Objeto)
        {
            if (ReferenceEquals(Este, Objeto))
                return true;

            if ((Este == null) || (Objeto == null))
                return false;

            if (Este.GetType() != Objeto.GetType())
                return false;

            bool resultado = true;

            foreach (var property in Objeto.GetType().GetProperties())
            {
                var EsteValor = property.GetValue(Este);
                var ObjetoValor = property.GetValue(Objeto);
                if (!EsteValor.Equals(ObjetoValor))
                    resultado = false;
            }

            return resultado;
        }
    }
}
