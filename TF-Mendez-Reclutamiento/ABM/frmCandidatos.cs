using Entidad.Negocio;
using Negocio.ABM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TF_Mendez_Reclutamiento.Enums;
using TF_Mendez_Reclutamiento.Helpers;
using Entidad.Enums;
using System.Text.RegularExpressions;

namespace TF_Mendez_Reclutamiento.ABM
{
    public partial class frmCandidatos : Form
    {
        private CandidatoNegocio candidatoNegocio = new CandidatoNegocio();
        private EFormAccion accion;
        private frmTecnologias frmTecnologias = new frmTecnologias();

        private Candidato candidato;
        private Educacion educacion;
        private Experiencia experiencia;

        public frmCandidatos()
        {
            InitializeComponent();

            panelCandidato.Dock = DockStyle.Fill;
            panelExperiencia.Dock = DockStyle.Fill;
            panelEducacion.Dock = DockStyle.Fill;

            panelCandidato.AutoScroll = true;
            panelExperiencia.AutoScroll = true;
            panelEducacion.AutoScroll = true;

            panelCandidato.BringToFront();

            this.Size = new Size(787, 800);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparam = base.CreateParams;
                handleparam.ExStyle |= 0x02000000;
                return handleparam;
            }
        }

        private void CargarDataGridCandidatos()
        {
            dgvCandidatos.Rows.Clear();
            List<Candidato> candidatos = new List<Candidato>();

            try
            {
                candidatos = this.candidatoNegocio.TraerCandidatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }            

            dgvCandidatos.ColumnCount = 5;
            dgvCandidatos.Columns[0].Name = "CUIL";
            dgvCandidatos.Columns[1].Name = "DNI";
            dgvCandidatos.Columns[2].Name = "Nombre";
            dgvCandidatos.Columns[3].Name = "Apellido";
            dgvCandidatos.Columns[4].Name = "Fecha de Nacimiento";

            foreach (var item in candidatos)
            {
                string[] row = new string[] { item.Cuil, item.DNI.ToString(), item.Nombre, item.Apellido, item.FecNac.ToString() };

                dgvCandidatos.Rows.Add(row);
            }

        }

        private void CargarDataGridExperiencia()
        {
            dgvExperiencia.Rows.Clear();

            List<Experiencia> experiencia = this.candidatoNegocio.TraerExperiencias(this.candidato.Cuil);

            dgvExperiencia.ColumnCount = 7;
            dgvExperiencia.Columns[0].Name = "Código";
            dgvExperiencia.Columns[1].Name = "Puesto";
            dgvExperiencia.Columns[2].Name = "Categoría";
            dgvExperiencia.Columns[3].Name = "Empresa";
            dgvExperiencia.Columns[4].Name = "Descripcion";
            dgvExperiencia.Columns[5].Name = "Desde";
            dgvExperiencia.Columns[6].Name = "Hasta";

            foreach (var item in experiencia)
            {
                string[] row = new string[] {
                    item.Codigo.ToString(),
                    item.Puesto,
                    item.Categoria.ToString(),
                    item.Empresa,
                    item.Descripcion,
                    item.FechaDesde.ToString(),
                    item.FechaHasta.ToString()
                };

                dgvExperiencia.Rows.Add(row);
            }

        }

        private void CargarDataGridEducacion()
        {
            dgvEducacion.Rows.Clear();

            List<Educacion> educaciones = this.candidatoNegocio.TraerEducaciones(this.candidato.Cuil);

            dgvEducacion.ColumnCount = 9;
            dgvEducacion.Columns[0].Name = "Código";
            dgvEducacion.Columns[1].Name = "Carrera";
            dgvEducacion.Columns[2].Name = "Institución";
            dgvEducacion.Columns[3].Name = "Tipo de carrera";
            dgvEducacion.Columns[4].Name = "Duración";
            dgvEducacion.Columns[5].Name = "Rubro";
            dgvEducacion.Columns[6].Name = "Estado";
            dgvEducacion.Columns[7].Name = "Fecha Inicio";
            dgvEducacion.Columns[8].Name = "Fecha Fin";

            foreach (var item in educaciones)
            {
                string[] row = new string[] {
                    item.Codigo.ToString(),
                    item.Carrera,
                    item.Institucion,
                    item.TipoCarrera.ToString(),
                    item.Duracion,
                    item.RubroCarrera.ToString(),
                    item.Estado.ToString(),
                    item.FechaDesde.ToString(),
                    item.FechaHasta.ToString()
                };

                dgvEducacion.Rows.Add(row);
            }

        }

        private void CargarDataGridTecnologias(Experiencia experiencia)
        {

            dgvTecnologias.Rows.Clear();

            dgvTecnologias.ColumnCount = 3;
            dgvTecnologias.Columns[0].Name = "Código";
            dgvTecnologias.Columns[1].Name = "Nombre";
            dgvTecnologias.Columns[2].Name = "Tipo";

            foreach (var item in experiencia.Tecnologias)
            {
                string[] row = new string[] { item.Codigo.ToString(), item.Nombre, item.Tipo.ToString() };
                dgvTecnologias.Rows.Add(row);
            }
        }

        private void CargarComboBoxes()
        {
            cboEduRubro.DataSource = Enum.GetNames(typeof(ERubroCarrera));
            cboExpCategoria.DataSource = Enum.GetNames(typeof(ECategoria));
            cboEduTipoCarrera.DataSource = Enum.GetNames(typeof(ETipoCarrera));
            cboEduEstado.DataSource = Enum.GetNames(typeof(EEstadoEducacion));
        }

        private void CargarTextBoxesCandidatos(Candidato candidato)
        {
            txtCandCUIL.Text = candidato.Cuil;
            txtCandDNI.Text = candidato.DNI.ToString();
            txtCandNombre.Text = candidato.Nombre;
            txtCandApellido.Text = candidato.Apellido;
            txtCandFechaNac.Text = candidato.FecNac.ToString();

            txtContCodigo.Text = candidato.Contacto.Codigo.ToString();
            txtContEmail.Text = candidato.Contacto.Email;
            txtContTelefono.Text = candidato.Contacto.Telefono;
            txtContSitioWeb.Text = candidato.Contacto.SitioWeb?.ToString();

            txtDirCodigo.Text = candidato.Direccion.Codigo.ToString();
            txtDirProvincia.Text = candidato.Direccion.Provincia;
            txtDirLocalidad.Text = candidato.Direccion.Localidad;
            txtDirCiudad.Text = candidato.Direccion.Ciudad;
            txtDirBarrio.Text = candidato.Direccion.Barrio;
            txtDirCalle.Text = candidato.Direccion.Calle;
            txtDirNumero.Text = candidato.Direccion.Numero.ToString();
            txtDirCodigoPostal.Text = candidato.Direccion.CodigoPostal;
            txtDirTorre.Text = candidato.Direccion.Torre;
            txtDirPiso.Text = candidato.Direccion.Piso;
            txtDirDepartamento.Text = candidato.Direccion.Departamento;
        }

        private void CargarTextBoxesExperiencia(Experiencia experiencia)
        {
            txtExpCodigo.Text = experiencia.Codigo.ToString();
            txtExpEmpresa.Text = experiencia.Empresa;
            txtExpPuesto.Text = experiencia.Puesto;
            txtExpFechaDesde.Text = experiencia.FechaDesde.ToString();
            txtExpFechaHasta.Text = experiencia.FechaHasta.ToString();
            txtExpDescripcion.Text = experiencia.Descripcion;
        }

        private void CargarTextBoxesEducacion(Educacion educacion)
        {
            txtEduCodigo.Text = educacion.Codigo.ToString();
            txtEduInstitucion.Text = educacion.Institucion;
            txtEduCarrera.Text = educacion.Carrera;
            txtEduDuracion.Text = educacion.Duracion;
            txtEduFechaInicio.Text = educacion.FechaDesde.ToString();
            txtEduFechaFin.Text = educacion.FechaHasta.ToString();
        }

        private void SeleccionarComboBoxesEducacion(Educacion educacion)
        {
            cboEduRubro.SelectedIndex = cboEduRubro.FindStringExact(educacion.RubroCarrera.ToString());
            cboEduTipoCarrera.SelectedIndex = cboEduTipoCarrera.FindStringExact(educacion.TipoCarrera.ToString());
            cboEduEstado.SelectedIndex = cboEduEstado.FindStringExact(educacion.Estado.ToString());
        }

        private void SeleccionarComboBoxesExperiencia(Experiencia experiencia)
        {
            cboExpCategoria.SelectedIndex = cboExpCategoria.FindStringExact(experiencia.Categoria.ToString());
        }

        private void PrepararFaseAgregarCandidato()
        {
            this.accion = EFormAccion.Agregar;

            this.LimpiarTextBoxes(panelCandidato);
            this.HabilitarTextBoxes(true, panelCandidato);
            this.HabilitarComboBoxes(true, panelCandidato);

            txtContCodigo.Enabled = false;
            txtDirCodigo.Enabled = false;

            btnCandAgregar.Enabled = false;
            btnCandEditar.Enabled = false;
            btnCandEliminar.Enabled = false;

            btnCandGuardar.Enabled = true;
            btnCandCancelar.Enabled = true;
            btnCandSiguiente.Enabled = false;
        }

        private void PrepararFaseEditarCandidato()
        {
            this.accion = EFormAccion.Modificar;

            this.HabilitarTextBoxes(true, panelCandidato);
            this.HabilitarComboBoxes(true, panelCandidato);

            txtContCodigo.Enabled = false;
            txtDirCodigo.Enabled = false;

            btnCandAgregar.Enabled = false;
            btnCandEditar.Enabled = false;
            btnCandEliminar.Enabled = false;

            btnCandGuardar.Enabled = true;
            btnCandCancelar.Enabled = true;
            btnCandSiguiente.Enabled = false;
        }

        private void PrepararFaseInicialCandidato()
        {
            this.candidato = null;

            this.LimpiarTextBoxes(panelCandidato);
            this.HabilitarTextBoxes(false, panelCandidato);
            this.HabilitarComboBoxes(false, panelCandidato);

            this.CargarDataGridCandidatos();
            this.CargarComboBoxes();

            btnCandAgregar.Enabled = true;
            btnCandEditar.Enabled = false;
            btnCandEliminar.Enabled = false;

            btnCandGuardar.Enabled = false;
            btnCandCancelar.Enabled = false;
            btnCandSiguiente.Enabled = false;
        }

        private void PrepararFaseAgregarEducacion()
        {
            this.accion = EFormAccion.Agregar;

            this.LimpiarTextBoxes(panelEducacion);
            this.HabilitarTextBoxes(true, panelEducacion);
            this.HabilitarComboBoxes(true, panelEducacion);
            txtEduCodigo.Enabled = false;

            btnEduAgregar.Enabled = false;
            btnEduEditar.Enabled = false;
            btnEduEliminar.Enabled = false;

            btnEduGuardar.Enabled = true;
            btnEduCancelar.Enabled = true;

            dgvTecnologias.Enabled = true;
        }

        private void PrepararFaseEditarEducacion()
        {
            this.accion = EFormAccion.Modificar;

            this.HabilitarTextBoxes(true, panelEducacion);
            this.HabilitarComboBoxes(true, panelEducacion);
            txtEduCodigo.Enabled = false;

            btnEduAgregar.Enabled = false;
            btnEduEditar.Enabled = false;
            btnEduEliminar.Enabled = false;

            btnEduGuardar.Enabled = true;
            btnEduCancelar.Enabled = true;

            dgvTecnologias.Enabled = true;
        }

        private void PrepararFaseInicialEducacion()
        {
            this.candidato = null;

            this.LimpiarTextBoxes(panelEducacion);
            this.HabilitarTextBoxes(false, panelEducacion);
            this.HabilitarComboBoxes(false, panelEducacion);
            txtEduCodigo.Enabled = false;

            this.CargarDataGridCandidatos();
            this.CargarComboBoxes();

            btnEduAgregar.Enabled = true;
            btnEduEditar.Enabled = false;
            btnEduEliminar.Enabled = false;

            btnEduGuardar.Enabled = false;
            btnEduCancelar.Enabled = false;

            dgvTecnologias.Enabled = false;
        }

        private void PrepararFaseAgregarExperiencia()
        {
            this.accion = EFormAccion.Agregar;

            this.LimpiarTextBoxes(panelExperiencia);
            this.HabilitarTextBoxes(true, panelExperiencia);
            this.HabilitarComboBoxes(true, panelExperiencia);
            txtExpCodigo.Enabled = false;

            btnExpAgregar.Enabled = false;
            btnExpEditar.Enabled = false;
            btnExpEliminar.Enabled = false;

            btnExpGuardar.Enabled = true;
            btnExpCancelar.Enabled = true;
        }

        private void PrepararFaseEditarExperiencia()
        {
            this.accion = EFormAccion.Modificar;

            this.HabilitarTextBoxes(true, panelExperiencia);
            this.HabilitarComboBoxes(true, panelExperiencia);
            txtExpCodigo.Enabled = false;

            btnExpAgregar.Enabled = false;
            btnExpEditar.Enabled = false;
            btnExpEliminar.Enabled = false;

            btnExpGuardar.Enabled = true;
            btnExpCancelar.Enabled = true;
        }

        private void PrepararFaseInicialExperiencia()
        {
            this.candidato = null;

            this.LimpiarTextBoxes(panelExperiencia);
            this.HabilitarTextBoxes(false, panelExperiencia);
            this.HabilitarComboBoxes(false, panelExperiencia);
            txtExpCodigo.Enabled = false;

            this.CargarDataGridCandidatos();
            this.CargarComboBoxes();

            btnExpAgregar.Enabled = true;
            btnExpEditar.Enabled = false;
            btnExpEliminar.Enabled = false;

            btnExpGuardar.Enabled = false;
            btnExpCancelar.Enabled = false;
        }

        private string ValidarCamposCandidato()
        {
            string errores = "";

            if (String.IsNullOrEmpty(txtCandCUIL.Text))
                errores += "El campo CUIL es obligatorio\n";

            if (String.IsNullOrEmpty(txtCandDNI.Text))
                errores += "El campo DNI es obligatorio\n";

            if (String.IsNullOrEmpty(txtCandNombre.Text))
                errores += "El campo NOMBRE es obligatorio\n";

            if (String.IsNullOrEmpty(txtCandApellido.Text))
                errores += "El campo APELLIDO es obligatorio\n";

            if (String.IsNullOrEmpty(txtCandFechaNac.Text))
                errores += "El campo FECHA DE NACIMIENTO es obligatorio\n";

            if (!DateTime.TryParse(txtCandFechaNac.Text, out _))
                errores += "El campo FECHA DE NACIMIENTO debe contener una fecha válida\n";

            if (String.IsNullOrEmpty(txtContEmail.Text))
                errores += "El campo EMAIL es obligatorio\n";

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(txtContEmail.Text);
            if (!match.Success)
                errores += "El campo EMAIL debe contener un email válido\n";

            if (String.IsNullOrEmpty(txtContTelefono.Text))
                errores += "El campo TELEFONO es obligatorio\n";

            if (!String.IsNullOrEmpty(txtContSitioWeb.Text) && !Uri.TryCreate(txtContSitioWeb.Text, UriKind.Absolute, out _))
                errores += "El campo SITIO WEB debe contener una URL válida\n";

            if (String.IsNullOrEmpty(txtDirProvincia.Text))
                errores += "El campo PROVINCIA es obligatorio\n";

            if (String.IsNullOrEmpty(txtDirLocalidad.Text))
                errores += "El campo LOCALIDAD es obligatorio\n";

            if (String.IsNullOrEmpty(txtDirCiudad.Text))
                errores += "El campo CIUDAD es obligatorio\n";

            if (String.IsNullOrEmpty(txtDirCalle.Text))
                errores += "El campo CALLE es obligatorio\n";

            if (String.IsNullOrEmpty(txtDirNumero.Text))
                errores += "El campo NÚMERO es obligatorio\n";

            if (!long.TryParse(txtDirNumero.Text, out _))
                errores += "El campo NÚMERO debe contener un número válido\n";

            return errores;
        }

        private string ValidarCamposEducacion()
        {
            string errores = "";

            if (String.IsNullOrEmpty(txtEduInstitucion.Text))
                errores += "El campo INSTITUCIÓN es obligatorio\n";

            if (String.IsNullOrEmpty(txtEduCarrera.Text))
                errores += "El campo CARRERA es obligatorio\n";

            if (String.IsNullOrEmpty(txtEduDuracion.Text))
                errores += "El campo DURACIÓN es obligatorio\n";

            if (String.IsNullOrEmpty(txtEduFechaInicio.Text))
                errores += "El campo FECHA DE INICIO es obligatorio\n";

            if (!DateTime.TryParse(txtEduFechaInicio.Text, out _))
                errores += "El campo FECHA DE INICIO debe contener una fecha válida\n";

            if (String.IsNullOrEmpty(txtEduFechaFin.Text))
                errores += "El campo FECHA DE FINALIZACIÓN es obligatorio\n";

            if (!DateTime.TryParse(txtEduFechaFin.Text, out _))
                errores += "El campo FECHA DE FINALIZACIÓN debe contener una fecha válida\n";

            return errores;
        }

        private string ValidarCamposExperiencia()
        {
            string errores = "";

            if (String.IsNullOrEmpty(txtExpEmpresa.Text))
                errores += "El campo EMPRESA es obligatorio\n";

            if (String.IsNullOrEmpty(txtExpPuesto.Text))
                errores += "El campo PUESTO es obligatorio\n";

            if (String.IsNullOrEmpty(txtExpFechaDesde.Text))
                errores += "El campo FECHA INICIO es obligatorio\n";

            if (!DateTime.TryParse(txtExpFechaDesde.Text, out _))
                errores += "El campo FECHA INICIO debe contener una fecha válida\n";

            if (String.IsNullOrEmpty(txtExpFechaHasta.Text))
                errores += "El campo FECHA FIN es obligatorio\n";

            if (!DateTime.TryParse(txtExpFechaHasta.Text, out _))
                errores += "El campo FECHA FIN debe contener una fecha válida\n";

            if (String.IsNullOrEmpty(txtExpDescripcion.Text))
                errores += "El campo DESCRIPCIÓN es obligatorio\n";

            return errores;
        }

        public void SeleccionarTecnologia(Tecnologia Tecnologia)
        {
            if (this.experiencia.Tecnologias.Any(x => x.Codigo == Tecnologia.Codigo))
            {
                MessageBox.Show("La tecnología ya está en el listado", "Advertencia");
                return;
            }

            this.experiencia.Tecnologias.Add(Tecnologia);

            this.CargarDataGridTecnologias(this.experiencia);
            this.frmTecnologias.Close();
        }

        private void btnCandSiguiente_Click(object sender, EventArgs e)
        {
            this.PrepararFaseInicialExperiencia();
            panelExperiencia.BringToFront();
        }

        private void btnExpSiguiente_Click(object sender, EventArgs e)
        {
            panelEducacion.BringToFront();
        }

        private void dgvCandidatos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvCandidatos.CurrentRow == null)
            {
                this.LimpiarTextBoxes(panelCandidato);
                return;
            }

            this.candidato = this.candidatoNegocio.TraerCandidato(dgvCandidatos.CurrentRow.Cells[0].Value.ToString());

            this.CargarTextBoxesCandidatos(this.candidato);

            btnCandEditar.Enabled = true;
            btnCandEliminar.Enabled = true;
            btnCandSiguiente.Enabled = true;
        }

        private void btnCandAgregar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseAgregarCandidato();
        }

        private void btnCandEditar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseEditarCandidato();
        }

        private void btnCandEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.candidatoNegocio.EliminarCandidato(txtCandCUIL.Text))
                    MessageBox.Show("Candidato eliminado exitosamente", "Éxito");
                else
                    MessageBox.Show("Hubo un error al eliminar el candidato", "Error");

                this.PrepararFaseInicialCandidato();
                this.CargarDataGridCandidatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnCandCancelar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseInicialCandidato();
        }

        private void frmCandidatos_Load(object sender, EventArgs e)
        {
            this.PrepararDataGridViews();
            this.PrepararComboBoxes();
            this.PrepararFaseInicialCandidato();
            this.frmTecnologias.TecnologiaSeleccionada += SeleccionarTecnologia;
        }

        private void btnCandGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string errores = this.ValidarCamposCandidato();

                if (!String.IsNullOrEmpty(errores))
                {
                    throw new Exception(errores);
                }

                Candidato candidato = new Candidato();
                candidato.Cuil = txtCandCUIL.Text;
                candidato.DNI = Convert.ToInt64(txtCandDNI.Text);
                candidato.Nombre = txtCandNombre.Text;
                candidato.Apellido = txtCandApellido.Text;
                candidato.FecNac = Convert.ToDateTime(txtCandFechaNac.Text);

                candidato.Contacto = new Contacto
                {
                    Codigo = String.IsNullOrEmpty(txtContCodigo.Text) ? Guid.Empty : new Guid(txtContCodigo.Text),
                    Email = txtContEmail.Text,
                    Telefono = txtContTelefono.Text,
                    SitioWeb = String.IsNullOrEmpty(txtContSitioWeb.Text) ? null : new Uri(txtContSitioWeb.Text)
                };

                candidato.Direccion = new Direccion
                {
                    Codigo = String.IsNullOrEmpty(txtDirCodigo.Text) ? Guid.Empty : new Guid(txtDirCodigo.Text),
                    Provincia = txtDirProvincia.Text,
                    Localidad = txtDirLocalidad.Text,
                    Ciudad = txtDirCiudad.Text,
                    Barrio = txtDirBarrio.Text,
                    Calle = txtDirCalle.Text,
                    Numero = Convert.ToInt64(txtDirNumero.Text),
                    CodigoPostal = txtDirCodigoPostal.Text,
                    Torre = txtDirTorre.Text,
                    Piso = txtDirPiso.Text,
                    Departamento = txtDirDepartamento.Text
                };

                switch (this.accion)
                {
                    case EFormAccion.Agregar:
                        if (this.candidatoNegocio.AgregarCandidato(candidato))
                        {
                            MessageBox.Show("Candidato agregado correctamente");
                            this.PrepararFaseInicialCandidato();
                        }
                        else
                        {
                            MessageBox.Show("Hubo un error al agregar el candidato");
                        }
                        break;

                    case EFormAccion.Modificar:
                        if (this.candidatoNegocio.ModificarCandidato(candidato))
                        {
                            MessageBox.Show("Candidato modificado correctamente");
                            this.PrepararFaseInicialCandidato();
                        }
                        else
                        {
                            MessageBox.Show("Hubo un error al modificar el candidato");
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
