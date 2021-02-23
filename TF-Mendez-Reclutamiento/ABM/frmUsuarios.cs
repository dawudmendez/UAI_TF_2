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
using TF_Mendez_Reclutamiento.Helpers;
using TF_Mendez_Reclutamiento.Enums;
using Entidad.Enums;

namespace TF_Mendez_Reclutamiento.ABM
{
    public partial class frmUsuarios : Form
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private EFormAccion accion;

        public frmUsuarios()
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
            dgvUsuarios.Rows.Clear();

            List<Usuario> usuarios = this.usuarioNegocio.TraerTodo().ToList();

            dgvUsuarios.ColumnCount = 4;
            dgvUsuarios.Columns[0].Name = "Legajo";
            dgvUsuarios.Columns[1].Name = "Nombre";
            dgvUsuarios.Columns[2].Name = "Apellido";
            dgvUsuarios.Columns[3].Name = "Puesto";

            foreach (var item in usuarios)
            {
                string[] row = new string[] { item.Legajo, item.Nombre, item.Apellido, item.Puesto.ToString() };

                dgvUsuarios.Rows.Add(row);
            }

        }

        private void CargarComboBoxes()
        {
            cboPuesto.DataSource = Enum.GetNames(typeof(EPuesto));
        }

        private void CargarTextBoxes(Usuario usuario)
        {
            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            txtLegajo.Text = usuario.Legajo;
        }

        private void SeleccionarComboBoxes(Usuario usuario)
        {
            cboPuesto.SelectedIndex = cboPuesto.FindStringExact(usuario.Puesto.ToString());
        }

        private void PrepararFaseAgregar()
        {
            this.accion = EFormAccion.Agregar;

            this.LimpiarTextBoxes();
            this.HabilitarTextBoxes(true);
            this.HabilitarComboBoxes(true);

            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void PrepararFaseEditar()
        {
            this.accion = EFormAccion.Modificar;

            this.HabilitarTextBoxes(true);
            this.HabilitarComboBoxes(true);
            txtLegajo.Enabled = false;

            btnAgregar.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void PrepararFaseInicial()
        {
            this.LimpiarTextBoxes();
            this.HabilitarTextBoxes(false);
            this.HabilitarComboBoxes(false);

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

            if (String.IsNullOrEmpty(txtLegajo.Text))
                errores += "El campo LEGAJO es obligatorio\n";

            if (Accion == EFormAccion.Agregar && !String.IsNullOrEmpty(txtLegajo.Text))
            {
                Usuario usuario = this.usuarioNegocio.Traer(txtLegajo.Text);
                if (usuario != null)
                    errores += "Ya existe un usuario con ese legajo\n";
            }

            if (String.IsNullOrEmpty(txtNombre.Text))
                errores += "El campo NOMBRE es obligatorio\n";

            if (String.IsNullOrEmpty(txtApellido.Text))
                errores += "El campo APELLIDO es obligatorio\n";

            return errores;
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            this.PrepararDataGridViews();
            this.PrepararComboBoxes();
            this.PrepararFaseInicial();
        }

        private void dgvUsuarios_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                this.LimpiarTextBoxes();
                return;
            }

            Usuario usuario = this.usuarioNegocio.Traer(dgvUsuarios.CurrentRow.Cells[0].Value.ToString());

            this.CargarTextBoxes(usuario);
            this.SeleccionarComboBoxes(usuario);
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

            Usuario usuario = new Usuario();

            usuario.Nombre = txtNombre.Text;
            usuario.Legajo = txtLegajo.Text;
            usuario.Apellido = txtApellido.Text;
            usuario.Puesto = (EPuesto)Enum.Parse(typeof(EPuesto), cboPuesto.SelectedItem.ToString(), true);
            usuario.Password = "";

            switch (this.accion)
            {
                case EFormAccion.Agregar:
                    if (this.usuarioNegocio.Agregar(usuario))
                    {
                        MessageBox.Show("Usuario agregado correctamente");
                        this.PrepararFaseInicial();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al agregar el usuario");
                    }
                    break;

                case EFormAccion.Modificar:
                    if (this.usuarioNegocio.Modificar(usuario))
                    {
                        MessageBox.Show("Usuario modificado correctamente");
                        this.PrepararFaseInicial();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al modificar el usuario");
                    }
                    break;

                default:
                    break;
            }
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de eliminar este usuario?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            if (this.usuarioNegocio.Eliminar(new Usuario { Legajo = txtLegajo.Text }))
            {
                MessageBox.Show("Usuario modificado correctamente");
                this.PrepararFaseInicial();
            }
            else
            {
                MessageBox.Show("Hubo un error al eliminar el usuario");
            }

        }
    }
}
