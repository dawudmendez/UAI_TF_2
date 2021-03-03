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


namespace TF_Mendez_Reclutamiento.ABM
{
    public partial class frmPerfiles : Form
    {
        private PerfilNegocio perfilNegocio = new PerfilNegocio();
        private EFormAccion accion;
        private frmTecnologias frmTecnologias = new frmTecnologias();
        private Perfil perfil;

        public frmPerfiles()
        {
            InitializeComponent();
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

        private void CargarDataGridPerfiles()
        {
            dgvPerfiles.Rows.Clear();

            List<Perfil> perfiles = this.perfilNegocio.TraerPerfiles();

            dgvPerfiles.ColumnCount = 4;
            dgvPerfiles.Columns[0].Name = "Código";
            dgvPerfiles.Columns[1].Name = "Nombre";
            dgvPerfiles.Columns[2].Name = "Categoría";
            dgvPerfiles.Columns[3].Name = "Años de experiencia";

            foreach (var item in perfiles)
            {
                string[] row = new string[] { item.Codigo.ToString(), item.Nombre, item.Categoria.ToString(), item.AniosExperiencia.ToString() };

                dgvPerfiles.Rows.Add(row);
            }

        }

        private void CargarDataGridTecnologias(Perfil perfil)
        {

            dgvTecnologias.Rows.Clear();

            dgvTecnologias.ColumnCount = 3;
            dgvTecnologias.Columns[0].Name = "Código";
            dgvTecnologias.Columns[1].Name = "Nombre";
            dgvTecnologias.Columns[2].Name = "Tipo";

            foreach (var item in perfil.Tecnologias)
            {
                string[] row = new string[] { item.Codigo.ToString(), item.Nombre, item.Tipo.ToString() };
                dgvTecnologias.Rows.Add(row);
            }
        }

        private void CargarComboBoxes()
        {
            cboCategoria.DataSource = Enum.GetNames(typeof(ECategoria));
        }

        private void CargarTextBoxes(Perfil perfil)
        {
            txtNombre.Text = perfil.Nombre;
            txtCodigo.Text = perfil.Codigo.ToString();
            txtAniosExperiencia.Text = perfil.AniosExperiencia.ToString();

        }

        private void SeleccionarComboBoxes(Perfil perfil)
        {
            cboCategoria.SelectedIndex = cboCategoria.FindStringExact(perfil.Categoria.ToString());
        }

        private void PrepararFaseAgregar()
        {
            this.accion = EFormAccion.Agregar;

            this.LimpiarTextBoxes(panel1);
            this.HabilitarTextBoxes(true, panel1);
            this.HabilitarComboBoxes(true, panel1);

            txtCodigo.Enabled = false;

            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnAgregarTecnologia.Enabled = false;
            btnQuitarTecnologia.Enabled = false;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;

            dgvTecnologias.Enabled = false;
        }

        private void PrepararFaseEditar()
        {
            this.accion = EFormAccion.Modificar;

            this.HabilitarTextBoxes(true, panel1);
            this.HabilitarComboBoxes(true, panel1);

            txtCodigo.Enabled = false;

            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnAgregarTecnologia.Enabled = true;
            btnQuitarTecnologia.Enabled = false;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;

            dgvTecnologias.Enabled = true;
        }

        private void PrepararFaseInicial()
        {
            this.perfil = null;

            this.LimpiarTextBoxes(panel1);
            this.HabilitarTextBoxes(false, panel1);
            this.HabilitarComboBoxes(false, panel1);

            this.CargarDataGridPerfiles();
            this.CargarComboBoxes();
            dgvTecnologias.Rows.Clear();

            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnAgregarTecnologia.Enabled = false;
            btnQuitarTecnologia.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;

            dgvTecnologias.Enabled = false;
        }

        private string ValidarCampos(EFormAccion Accion)
        {
            string errores = "";

            if (String.IsNullOrEmpty(txtNombre.Text))
                errores += "El campo NOMBRE es obligatorio\n";

            if (String.IsNullOrEmpty(txtAniosExperiencia.Text))
                errores += "El campo AÑOS DE EXPERIENCIA es obligatorio\n";

            return errores;
        }

        private void dgvPerfiles_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvPerfiles.CurrentRow == null)
            {
                this.LimpiarTextBoxes();
                return;
            }

            this.perfil = this.perfilNegocio.TraerPerfil(dgvPerfiles.CurrentRow.Cells[0].Value.ToString());

            this.CargarTextBoxes(this.perfil);
            this.SeleccionarComboBoxes(this.perfil);
            this.CargarDataGridTecnologias(this.perfil);

            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void dgvTecnologias_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvTecnologias.CurrentRow == null)
                return;

            btnQuitarTecnologia.Enabled = true;
            txtTecnologia.Text = dgvTecnologias.CurrentRow.Cells[0].Value.ToString();
        }

        private void frmPerfiles_Load(object sender, EventArgs e)
        {
            this.PrepararDataGridViews();
            this.PrepararComboBoxes();
            this.PrepararFaseInicial();
            this.frmTecnologias.TecnologiaSeleccionada += this.SeleccionarTecnologia;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseAgregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseEditar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseInicial();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string errores = this.ValidarCampos(this.accion);

                if (!String.IsNullOrEmpty(errores))
                {
                    throw new Exception(errores);
                }

                Perfil perfil = new Perfil();
                perfil.Codigo = String.IsNullOrEmpty(txtCodigo.Text) ? Guid.Empty : new Guid(txtCodigo.Text);
                perfil.Nombre = txtNombre.Text;
                perfil.Categoria = (ECategoria)Enum.Parse(typeof(ECategoria), cboCategoria.SelectedItem.ToString(), true);
                perfil.AniosExperiencia = Convert.ToInt32(txtAniosExperiencia.Text);

                perfil.Tecnologias = new List<Tecnologia>();

                foreach (DataGridViewRow item in dgvTecnologias.Rows)
                {
                    perfil.Tecnologias.Add(this.perfilNegocio.TraerTecnologia(item.Cells[0].Value.ToString()));
                }

                switch (this.accion)
                {
                    case EFormAccion.Agregar:
                        if (this.perfilNegocio.AgregarPerfil(perfil))
                        {
                            MessageBox.Show("Perfil agregado correctamente");
                            this.PrepararFaseInicial();
                        }
                        else
                        {
                            MessageBox.Show("Hubo un error al agregar el perfil");
                        }
                        break;

                    case EFormAccion.Modificar:
                        if (this.perfilNegocio.ModificarPerfil(perfil))
                        {
                            MessageBox.Show("Perfil modificado correctamente");
                            this.PrepararFaseInicial();
                        }
                        else
                        {
                            MessageBox.Show("Hubo un error al modificar el perfil");
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

        public void SeleccionarTecnologia(Tecnologia Tecnologia)
        {
            if (this.perfil.Tecnologias.Any(x => x.Codigo == Tecnologia.Codigo))
            {
                MessageBox.Show("La tecnología ya está en el listado", "Advertencia");
                return;
            }

            this.perfil.Tecnologias.Add(Tecnologia);

            this.CargarDataGridTecnologias(this.perfil);
            this.frmTecnologias.Close();
        }

        private void btnAgregarTecnologia_Click(object sender, EventArgs e)
        {
            btnQuitarTecnologia.Enabled = false;
            this.frmTecnologias.ShowDialog(this);
        }

        private void btnQuitarTecnologia_Click(object sender, EventArgs e)
        {
            this.perfil.Tecnologias.Remove(this.perfil.Tecnologias.FirstOrDefault(x => x.Codigo == new Guid(txtTecnologia.Text)));
            this.CargarDataGridTecnologias(perfil);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.perfilNegocio.EliminarPerfil(txtCodigo.Text))
                    MessageBox.Show("Perfil eliminado exitosamente", "Éxito");
                else
                    MessageBox.Show("Hubo un error al eliminar el perfil", "Error");

                this.PrepararFaseInicial();
                this.CargarDataGridPerfiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
