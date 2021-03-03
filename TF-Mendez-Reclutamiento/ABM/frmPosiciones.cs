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
    public partial class frmPosiciones : Form
    {
        private PosicionNegocio posicionNegocio = new PosicionNegocio();
        private EFormAccion accion;
        private Posicion posicion;

        public frmPosiciones()
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
            dgvPosiciones.Rows.Clear();

            List<Posicion> posiciones = this.posicionNegocio.TraerPosiciones();

            dgvPosiciones.ColumnCount = 7;
            dgvPosiciones.Columns[0].Name = "Código";
            dgvPosiciones.Columns[1].Name = "Nombre";
            dgvPosiciones.Columns[2].Name = "Descripción";
            dgvPosiciones.Columns[3].Name = "Perfil";
            dgvPosiciones.Columns[4].Name = "Equipo";
            dgvPosiciones.Columns[5].Name = "Oficina";
            dgvPosiciones.Columns[6].Name = "Estado";

            foreach (var item in posiciones)
            {
                string[] row = new string[] {
                    item.Codigo.ToString(),
                    item.Nombre,
                    item.Descripcion,
                    item.Perfil.Nombre,
                    item.Equipo.Nombre,
                    item.Oficina.Nombre,
                    item.Estado.ToString()
                };

                dgvPosiciones.Rows.Add(row);
            }

        }

        private void CargarComboBoxes()
        {
            cboPerfil.DataSource = this.posicionNegocio.TraerPerfiles();
            cboOficina.DataSource = this.posicionNegocio.TraerOficinas();
            cboEquipo.DataSource = this.posicionNegocio.TraerEquipos();

            cboPerfil.DisplayMember = "nombre";
            cboPerfil.ValueMember = "codigo";

            cboOficina.DisplayMember = "nombre";
            cboOficina.ValueMember = "nombre";

            cboEquipo.DisplayMember = "nombre";
            cboEquipo.ValueMember = "nombre";
        }

        private void CargarTextBoxes(Posicion posicion)
        {
            txtNombre.Text = posicion.Nombre;
            txtDescripcion.Text = posicion.Descripcion;
            txtCodigo.Text = posicion.Codigo.ToString();
            txtEstado.Text = posicion.Estado.ToString();

        }

        private void SeleccionarComboBoxes(Posicion posicion)
        {
            cboPerfil.SelectedIndex = cboPerfil.FindStringExact(posicion.Perfil.Nombre.ToString());
            cboOficina.SelectedIndex = cboOficina.FindStringExact(posicion.Oficina.Nombre.ToString());
            cboEquipo.SelectedIndex = cboEquipo.FindStringExact(posicion.Equipo.Nombre.ToString());
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
        }

        private void PrepararFaseInicial()
        {
            this.posicion = null;

            this.LimpiarTextBoxes(panel1);
            this.HabilitarTextBoxes(false, panel1);
            this.HabilitarComboBoxes(false, panel1);

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

            if (String.IsNullOrEmpty(txtDescripcion.Text))
                errores += "El campo DESCRIPCION es obligatorio\n";

            return errores;
        }

        private void dgvPosiciones_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvPosiciones.CurrentRow == null)
            {
                this.LimpiarTextBoxes();
                return;
            }

            this.posicion = this.posicionNegocio.TraerPosicion(dgvPosiciones.CurrentRow.Cells[0].Value.ToString());

            this.CargarTextBoxes(this.posicion);
            this.SeleccionarComboBoxes(this.posicion);

            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void frmPosiciones_Load(object sender, EventArgs e)
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.posicionNegocio.EliminarPosicion(txtCodigo.Text))
                    MessageBox.Show("Posición eliminada exitosamente", "Error");
                else
                    MessageBox.Show("Hubo un error al eliminar la posición", "Error");

                this.PrepararFaseInicial();
                this.CargarDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
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

                Posicion posicion = new Posicion();
                posicion.Codigo = String.IsNullOrEmpty(txtCodigo.Text) ? Guid.Empty : new Guid(txtCodigo.Text);
                posicion.Nombre = txtNombre.Text;
                posicion.Descripcion = txtDescripcion.Text;
                posicion.Estado = (EEstadoPosicion)Enum.Parse(typeof(EEstadoPosicion), txtEstado.Text, true);
                posicion.Equipo = (Equipo)cboEquipo.SelectedItem;
                posicion.Oficina = (Oficina)cboOficina.SelectedItem;
                posicion.Perfil = (Perfil)cboPerfil.SelectedItem;

                switch (this.accion)
                {
                    case EFormAccion.Agregar:
                        if (this.posicionNegocio.AgregarPosicion(posicion))
                        {
                            MessageBox.Show("Posición agregada correctamente");
                            this.PrepararFaseInicial();
                        }
                        else
                        {
                            MessageBox.Show("Hubo un error al agregar la posición");
                        }
                        break;

                    case EFormAccion.Modificar:
                        if (this.posicionNegocio.ModificarPosicion(posicion))
                        {
                            MessageBox.Show("Posición modificada correctamente");
                            this.PrepararFaseInicial();
                        }
                        else
                        {
                            MessageBox.Show("Hubo un error al modificar la posición");
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
