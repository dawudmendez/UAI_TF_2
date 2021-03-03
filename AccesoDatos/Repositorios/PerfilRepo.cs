using AccesoDatos.Contexto;
using AccesoDatos.Enums;
using AccesoDatos.Interfaces;
using Entidad.Enums;
using Entidad.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class PerfilRepo : Repositorio<Perfil>
    {
        protected override string SPTraerTodo { get; set; } = "sp_perfil_traer";
        protected override string SPTraerUno { get; set; } = "sp_perfil_traeruno";
        protected override string SPActualizar { get; set; } = "sp_perfil_actualizar";
        protected override string SPInsertar { get; set; } = "sp_perfil_insertar";
        protected override string SPEliminar { get; set; } = "sp_perfil_eliminar";
        private string SPTraerTecnologias { get; set; } = "sp_perfil_traer_tecnologias";
        private string SPEliminarTecnologias { get; set; } = "sp_perfil_eliminar_tecnologias";
        private string SPAgregarTecnologia { get; set; } = "sp_perfil_insertar_tecnologia";

        public PerfilRepo(IDBContexto contexto) : base(contexto)
        {

        }

        private List<Tecnologia> TraerTecnologias(Perfil Perfil)
        {
            DataTable data = this.contexto.EjecutarQuery(SPTraerTecnologias, this.PrepararParametros(EAccion.TraerTecnologias, Perfil));

            List<Tecnologia> lista = new List<Tecnologia>();

            foreach (DataRow row in data.Rows)
            {
                //Tecnologia entidad = this.tecnologiaRepo.MapearDataRow(row);
                Tecnologia entidad = null;
                lista.Add(entidad);
            }

            return lista;
        }

        public new bool Insertar(Perfil Entidad)
        {
            this.contexto.EjecutarNoQuery(SPInsertar, this.PrepararParametros(EAccion.Insertar, Entidad));

            this.AgregarTecnologias(Entidad);

            return true;
        }

        public new bool Actualizar(Perfil Entidad)
        {
            this.contexto.EjecutarNoQuery(SPActualizar, this.PrepararParametros(EAccion.Actualizar, Entidad));

            this.AgregarTecnologias(Entidad);

            return true;
        }

        private void AgregarTecnologias(Perfil Entidad)
        {
            this.contexto.EjecutarNoQuery(SPEliminarTecnologias, this.PrepararParametros(EAccion.EliminarTecnologias, Entidad));

            int i = 0;
            foreach (var tecnologia in Entidad.Tecnologias)
            {
                this.contexto.EjecutarNoQuery(SPAgregarTecnologia, this.PrepararParametros(EAccion.AgregarTecnologia, Entidad, i));
                i++;
            }
        }

        protected override SqlParameter[] PrepararParametros(EAccion Accion, Perfil Entidad, int Elemento = 0)
        {
            SqlParameter Codigo = new SqlParameter();
            SqlParameter Nombre = new SqlParameter();
            SqlParameter Categoria = new SqlParameter();
            SqlParameter AniosExperiencia = new SqlParameter();
            SqlParameter CodigoTecnologia = new SqlParameter();

            Codigo.ParameterName = "codigo";
            Nombre.ParameterName = "nombre";
            Categoria.ParameterName = "categoria";
            AniosExperiencia.ParameterName = "aniosexperiencia";
            CodigoTecnologia.ParameterName = "codigo_tecnologia";

            Codigo.Value = Entidad.Codigo;
            Nombre.Value = Entidad.Nombre;
            Categoria.Value = Entidad.Categoria;
            AniosExperiencia.Value = Entidad.AniosExperiencia;

            List<SqlParameter> Parametros = new List<SqlParameter>();

            switch (Accion)
            {
                case EAccion.Actualizar:
                case EAccion.Insertar:
                    Parametros.Add(Codigo);
                    Parametros.Add(Nombre);
                    Parametros.Add(Categoria);
                    Parametros.Add(AniosExperiencia);
                    break;

                case EAccion.TraerTecnologias:
                case EAccion.EliminarTecnologias:
                case EAccion.Traer:
                case EAccion.Eliminar:
                    Parametros.Add(Codigo);
                    break;

                case EAccion.AgregarTecnologia:
                    CodigoTecnologia.Value = Entidad.Tecnologias[Elemento].Codigo;
                    Parametros.Add(Codigo);
                    Parametros.Add(CodigoTecnologia);
                    break;

                default:
                    break;
            }

            return Parametros.ToArray();
        }

        internal override Perfil MapearDataRow(DataRow Row)
        {
            Perfil perf = new Perfil();

            perf.Codigo = new Guid(Row["codigo"].ToString());
            perf.Nombre = Row["nombre"].ToString();
            perf.Categoria = (ECategoria)Enum.Parse(typeof(ECategoria), Row["categoria"].ToString(), true);
            perf.AniosExperiencia = Convert.ToInt32(Row["aniosexperiencia"].ToString());
            perf.Tecnologias = this.TraerTecnologias(perf);

            return perf;
        }
    }
}
