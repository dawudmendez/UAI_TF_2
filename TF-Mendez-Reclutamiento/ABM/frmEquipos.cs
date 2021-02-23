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

namespace TF_Mendez_Reclutamiento.ABM
{
    public partial class frmEquipos : Form
    {
        private EquiposNegocio equipoNegocio = new EquiposNegocio();
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private EFormAccion accion;

        public frmEquipos()
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

            //dgvOficinas.Rows?.Remove(dgvOficinas.Rows[0]);

            dgvEquipos.Rows.Clear();

            List<Equipo> equipos = this.equipoNegocio.TraerTodo().ToList();

            dgvEquipos.ColumnCount = 4;
            dgvEquipos.Columns[0].Name = "Nombre";
            dgvEquipos.Columns[1].Name = "Descripción";
            dgvEquipos.Columns[2].Name = "Team Leader";
            dgvEquipos.Columns[3].Name = "Manager";

            foreach (var item in equipos)
            {
                string[] row = new string[] { item.Nombre, item.Descripcion,
                    item.Lider.Legajo + ": " + item.Lider.Nombre + " " + item.Lider.Apellido,
                    item.Manager.Legajo + ": " + item.Manager.Nombre + " " + item.Manager.Apellido };

                dgvEquipos.Rows.Add(row);
            }

        }

        private void CargarComboBoxes()
        {
            cboManager.DataSource = this.usuarioNegocio.TraerPorPuesto(Entidad.Enums.EPuesto.Manager).ToList();
            cboTeamLeader.DataSource = this.usuarioNegocio.TraerPorPuesto(Entidad.Enums.EPuesto.Lider).ToList();

            cboManager.DisplayMember = "nombre";
            cboManager.ValueMember = "legajo";

            cboTeamLeader.DisplayMember = "nombre";
            cboTeamLeader.ValueMember = "legajo";
        }

        private void CargarTextBoxes(Equipo equipo)
        {
            txtNombre.Text = equipo.Nombre;
            txtDescripcion.Text = equipo.Descripcion;

        }

        private void SeleccionarComboBoxes(Equipo equipo)
        {
            cboManager.SelectedIndex = cboManager.FindStringExact(equipo.Manager.Legajo);
            cboTeamLeader.SelectedIndex = cboTeamLeader.FindStringExact(equipo.Lider.Legajo);
        }

        private void PrepararFaseAgregar()
        {
            this.accion = EFormAccion.Agregar;

            this.LimpiarTextBoxes(panel1);
            this.HabilitarTextBoxes(true, panel1);

            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void PrepararFaseEditar()
        {
            this.accion = EFormAccion.Modificar;

            this.HabilitarTextBoxes(true, panel1);

            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void PrepararFaseInicial()
        {
            this.LimpiarTextBoxes(panel1);
            this.HabilitarTextBoxes(false, panel1);

            this.CargarDataGrid();
            this.CargarComboBoxes();

            btnAgregar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private string ValidarCampos(EFormAccion Accion)
        {
            string errores = "";

            if (String.IsNullOrEmpty(txtNombre.Text))
                errores += "El campo NOMBRE es obligatorio\n";


            if (Accion == EFormAccion.Agregar && !String.IsNullOrEmpty(txtNombre.Text))
            {
                Equipo equipo = this.equipoNegocio.Traer(txtNombre.Text);
                if (equipo != null)
                    errores += "Ya existe una oficina con este nombre\n";
            }

            if (String.IsNullOrEmpty(txtDescripcion.Text))
                errores += "El campo DESCIPCIÓN es obligatorio\n";

            return errores;
        }

        private void frmEquipos_Load(object sender, EventArgs e)
        {
            this.PrepararDataGridViews();
            this.PrepararComboBoxes();
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

        private void dgvEquipos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvEquipos.CurrentRow == null)
            {
                this.LimpiarTextBoxes();
                return;
            }

            Equipo equipo = this.equipoNegocio.Traer(dgvEquipos.CurrentRow.Cells[0].Value.ToString());

            this.CargarTextBoxes(equipo);
            this.SeleccionarComboBoxes(equipo);
            btnEditar.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string errores = this.ValidarCampos(this.accion);

            if (!String.IsNullOrEmpty(errores))
            {
                MessageBox.Show("Error: " + errores);
                return;
            }

            Equipo equipo = new Equipo();
            equipo.Nombre = txtNombre.Text;
            equipo.Descripcion = txtDescripcion.Text;

            equipo.Manager = new Usuario { Legajo = cboManager.SelectedItem.ToString(), Puesto = Entidad.Enums.EPuesto.Manager };
            equipo.Lider = new Usuario { Legajo = cboTeamLeader.SelectedItem.ToString(), Puesto = Entidad.Enums.EPuesto.Lider };


            switch (this.accion)
            {
                case EFormAccion.Agregar:
                    if (this.equipoNegocio.Agregar(equipo))
                    {
                        MessageBox.Show("Equipo agregado correctamente");
                        this.PrepararFaseInicial();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al agregar el equipo");
                    }
                    break;

                case EFormAccion.Modificar:
                    if (this.equipoNegocio.Modificar(equipo))
                    {
                        MessageBox.Show("Equipo modificado correctamente");
                        this.PrepararFaseInicial();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al modificar el equipo");
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
