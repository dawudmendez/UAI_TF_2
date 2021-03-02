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
    public partial class frmTecnologias : Form
    {
        private TecnologiaNegocio tecnologiaNegocio = new TecnologiaNegocio();
        private EFormAccion accion;
        private Tecnologia tecnologia;

        public event SeleccionarEvent TecnologiaSeleccionada;
        public delegate void SeleccionarEvent(Tecnologia tecnologia);




        public frmTecnologias()
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

        private void CargarDataGrid()
        {
            dgvTecnologias.Rows.Clear();

            List<Tecnologia> tecnologias = this.tecnologiaNegocio.Traer().ToList();

            dgvTecnologias.ColumnCount = 3;
            dgvTecnologias.Columns[0].Name = "Código";
            dgvTecnologias.Columns[1].Name = "Nombre";
            dgvTecnologias.Columns[2].Name = "Tipo";

            foreach (var item in tecnologias)
            {
                string[] row = new string[] { item.Codigo.ToString(), item.Nombre, item.Tipo.ToString() };

                dgvTecnologias.Rows.Add(row);
            }

        }

        private void CargarComboBoxes()
        {
            cboTipo.DataSource = Enum.GetNames(typeof(ETipoTecnologia));
        }

        private void CargarTextBoxes(Tecnologia tecnologia)
        {
            txtNombre.Text = tecnologia.Nombre;
            txtCodigo.Text = tecnologia.Codigo.ToString();

        }

        private void SeleccionarComboBoxes(Tecnologia tecnologia)
        {
            cboTipo.SelectedIndex = cboTipo.FindStringExact(tecnologia.Tipo.ToString());
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

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;

            btnSeleccionar.Enabled = false;
            btnCerrar.Enabled = false;
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

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;

            btnSeleccionar.Enabled = false;
            btnCerrar.Enabled = false;
        }

        private void PrepararFaseInicial()
        {
            this.LimpiarTextBoxes(panel1);
            this.HabilitarTextBoxes(false, panel1);
            this.HabilitarComboBoxes(false, panel1);

            this.CargarDataGrid();
            this.CargarComboBoxes();

            btnSeleccionar.Enabled = false;
            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;

            btnSeleccionar.Enabled = false;
            btnCerrar.Enabled = true;
        }

        private string ValidarCampos(EFormAccion Accion)
        {
            string errores = "";

            if (String.IsNullOrEmpty(txtNombre.Text))
                errores += "El campo NOMBRE es obligatorio\n";

            return errores;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseInicial();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseAgregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseEditar();
        }

        private void frmTecnologias_Load(object sender, EventArgs e)
        {
            this.PrepararDataGridViews();
            this.PrepararComboBoxes();
            this.PrepararFaseInicial();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string errores = this.ValidarCampos(this.accion);

            if (!String.IsNullOrEmpty(errores))
            {
                MessageBox.Show("Error: " + errores);
                return;
            }

            Tecnologia tecnologia = new Tecnologia();
            tecnologia.Codigo = String.IsNullOrEmpty(txtCodigo.Text) ? Guid.Empty : new Guid (txtCodigo.Text);
            tecnologia.Nombre = txtNombre.Text;
            tecnologia.Tipo = (ETipoTecnologia)Enum.Parse(typeof(ETipoTecnologia), cboTipo.SelectedItem.ToString(), true);

            switch (this.accion)
            {
                case EFormAccion.Agregar:
                    if (this.tecnologiaNegocio.Agregar(tecnologia))
                    {
                        MessageBox.Show("Tecnología agregada correctamente");
                        this.PrepararFaseInicial();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al agregar la tecnología");
                    }
                    break;

                case EFormAccion.Modificar:
                    if (this.tecnologiaNegocio.Modificar(tecnologia))
                    {
                        MessageBox.Show("Tecnología modificada correctamente");
                        this.PrepararFaseInicial();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al modificar la tecnología");
                    }
                    break;

                default:
                    break;
            }
        }

        private void dgvTecnologias_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvTecnologias.CurrentRow == null)
            {
                this.LimpiarTextBoxes();
                return;
            }

            this.tecnologia = this.tecnologiaNegocio.Traer(dgvTecnologias.CurrentRow.Cells[0].Value.ToString());

            this.CargarTextBoxes(this.tecnologia);
            this.SeleccionarComboBoxes(this.tecnologia);
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnSeleccionar.Enabled = true;
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            this.TecnologiaSeleccionada(this.tecnologia);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tecnologiaNegocio.Eliminar(txtCodigo.Text))
                    MessageBox.Show("Tecnología eliminada exitosamente", "Error");
                else
                    MessageBox.Show("Hubo un error al eliminar la tecnología", "Error");

                this.PrepararFaseInicial();
                this.CargarDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
