﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AccesoDatos.Enums;
using Entidad.Enums;
using Entidad.Negocio;

namespace AccesoDatos.Repositorios
{
    public class TecnologiaRepo : Repositorio<Tecnologia>
    {
        protected override string SPTraerTodo { get; set; } = "sp_tecnologia_traer";
        protected override string SPTraerUno { get; set; } = "sp_tecnologia_traeruno";
        protected override string SPActualizar { get; set; } = "sp_tecnologia_actualizar";
        protected override string SPInsertar { get; set; } = "sp_tecnologia_insertar";
        protected override string SPEliminar { get; set; } = "sp_tecnologia_eliminar";
        private string SPTraerPorPerfil { get; set; } = "sp_tecnologia_traer_por_perfil";

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Tecnologia Entidad)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Nombre = new SqlParameter();
            SqlParameter Tipo = new SqlParameter();
            SqlParameter Perfil = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Nombre.ParameterName = "nombre";
            Tipo.ParameterName = "tipo";
            Perfil.ParameterName = "perfil";

            Codigo.Value = Entidad.Codigo;
            Nombre.Value = Entidad.Nombre;
            Tipo.Value = Entidad.Tipo.ToString();

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Nombre);
                    Parametros.Add(Tipo);
                    break;

                case EAccion.Insertar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Nombre);
                    Parametros.Add(Tipo);
                    break;

                case EAccion.Traer:
                case EAccion.Eliminar:
                    Parametros.Add(Codigo);
                    break;

                default:
                    break;
            }

            return Parametros.ToArray();
        }

        internal override Tecnologia MapearDataRow(DataRow Row)
        {
            Tecnologia tec = new Tecnologia();

            tec.Codigo = new Guid(Row["id"].ToString());
            tec.Nombre = Row["nombre"].ToString();
            Enum.TryParse<ETipoTecnologia>(Row["tipo"].ToString(), out ETipoTecnologia tipo);
            tec.Tipo = tipo;

            return tec;
        }
    }
}