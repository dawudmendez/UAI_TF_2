using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AccesoDatos.Enums;
using Entidad.Negocio;

namespace AccesoDatos.Repositorios
{
    public class ConfiguracionRepo : Repositorio<Configuracion>
    {
        private OficinaRepo OficinaRepo = new OficinaRepo();

        //Configuración sólo puede traer y actualizar
        protected override string SPTraerTodo { get; set; } = "sp_configuracion_traer";
        protected override string SPActualizar { get; set; } = "sp_configuracion_actualizar";
        protected override string SPTraerUno { get; set; }        
        protected override string SPInsertar { get; set; }
        protected override string SPEliminar { get; set; }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Configuracion Entidad, int Elemento = 0)
        {
            SqlParameter Cuit = new SqlParameter();
            SqlParameter RazonSocial = new SqlParameter();
            SqlParameter OficinaPrincipal = new SqlParameter();

            Cuit.ParameterName = "cuit";
            RazonSocial.ParameterName = "razonsocial";
            OficinaPrincipal.ParameterName = "nombre_oficinaprincipal";

            Cuit.Value = Entidad.Cuit;
            RazonSocial.Value = Entidad.RazonSocial;
            OficinaPrincipal.Value = Entidad.OficinaPrincipal != null ? Entidad.OficinaPrincipal.Nombre : null;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                    Parametros.Add(Cuit);
                    Parametros.Add(RazonSocial);
                    Parametros.Add(OficinaPrincipal);
                    break;

                default:
                    break;
            }

            return Parametros.ToArray();
        }

        internal override Configuracion MapearDataRow(DataRow Row)
        {
            Configuracion conf = new Configuracion();

            conf.Cuit = Row["cuit"].ToString();
            conf.RazonSocial = Row["razonsocial"].ToString();

            this.OficinaRepo = new OficinaRepo();
            conf.OficinaPrincipal = this.OficinaRepo.Traer(new Oficina { Nombre = Row["nombre_oficinaprincipal"].ToString() });

            return conf;
        }
    }
}
