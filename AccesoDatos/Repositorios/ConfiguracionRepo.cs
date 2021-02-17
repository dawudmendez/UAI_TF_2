using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AccesoDatos.Enums;
using Entidad.Negocio;

namespace AccesoDatos.Repositorios
{
    class ConfiguracionRepo : Repositorio<Configuracion>
    {
        private OficinaRepo OficinaRepo = new OficinaRepo();

        //Configuración sólo puede traer y actualizar
        protected override string SPTraerTodo { get; set; } = "sp_configuracion_traer";
        protected override string SPActualizar { get; set; } = "sp_configuracion_actualizar";
        protected override string SPTraerUno { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }        
        protected override string SPInsertar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        protected override string SPEliminar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Configuracion Entidad)
        {
            SqlParameter Cuit = new SqlParameter();
            SqlParameter RazonSocial = new SqlParameter();
            SqlParameter OficinaPrincipal = new SqlParameter();

            Cuit.ParameterName = "cuit";
            RazonSocial.ParameterName = "razonsocial";
            OficinaPrincipal.ParameterName = "oficinaprincipal";

            Cuit.Value = Entidad.Cuit;
            RazonSocial.Value = Entidad.RazonSocial;
            OficinaPrincipal.Value = Entidad.OficinaPrincipal.Nombre;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Cuit);
                    Parametros.Add(RazonSocial);
                    Parametros.Add(OficinaPrincipal);
                    break;

                case EAccion.Traer:
                case EAccion.Eliminar:
                    Parametros.Add(Cuit);
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
            conf.OficinaPrincipal = this.OficinaRepo.Traer(new Oficina { Nombre = Row["oificinaprincipal"].ToString() });

            return conf;
        }
    }
}
