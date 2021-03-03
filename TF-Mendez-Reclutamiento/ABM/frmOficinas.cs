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
    public partial class frmOficinas : Form
    {
        private OficinaNegocio oficinaNegocio = new OficinaNegocio();
        private EFormAccion accion;

        public frmOficinas()
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
            dgvOficinas.Rows.Clear();

            List<Oficina> oficinas = this.oficinaNegocio.TraerTodo();

            dgvOficinas.ColumnCount = 8;
            dgvOficinas.Columns[0].Name = "Nombre";
            dgvOficinas.Columns[1].Name = "Calle";
            dgvOficinas.Columns[2].Name = "Numero";
            dgvOficinas.Columns[3].Name = "Localidad";
            dgvOficinas.Columns[4].Name = "Provincia";
            dgvOficinas.Columns[5].Name = "Teléfono";
            dgvOficinas.Columns[6].Name = "Email";
            dgvOficinas.Columns[7].Name = "Sitio Web";

            foreach (var item in oficinas)
            {
                string[] row = new string[] { item.Nombre, item.Direccion.Calle,
                    item.Direccion.Numero.ToString(), item.Direccion.Localidad, item.Direccion.Provincia,
                    item.Contacto.Telefono, item.Contacto.Email, item.Contacto.SitioWeb.ToString() };

                dgvOficinas.Rows.Add(row);
            }

        }

        private void CargarTextBoxes(Oficina oficina)
        {
            txtNombre.Text = oficina.Nombre;

            txtDirCodigo.Text = oficina.Direccion.Codigo.ToString();
            txtCalle.Text = oficina.Direccion.Calle;
            txtNumero.Text = oficina.Direccion.Numero.ToString();
            txtPiso.Text = oficina.Direccion.Piso;
            txtLocalidad.Text = oficina.Direccion.Localidad;
            txtProvincia.Text = oficina.Direccion.Provincia;
            txtCodigoPostal.Text = oficina.Direccion.CodigoPostal;
            txtBarrio.Text = oficina.Direccion.Barrio;
            txtCiudad.Text = oficina.Direccion.Ciudad;
            txtTorre.Text = oficina.Direccion.Torre;

            txtConCodigo.Text = oficina.Contacto.Codigo.ToString();
            txtEmail.Text = oficina.Contacto.Email;
            txtTelefono.Text = oficina.Contacto.Telefono;
            txtSitioWeb.Text = oficina.Contacto.SitioWeb.ToString();
        }

        private void PrepararFaseAgregar()
        {
            this.accion = EFormAccion.Agregar;

            this.LimpiarTextBoxes();
            this.HabilitarTextBoxes(true);

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
            txtNombre.Enabled = false;

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

            this.CargarDataGrid();

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
                Oficina oficina = this.oficinaNegocio.Traer(txtNombre.Text);
                if (oficina != null)
                    errores += "Ya existe una oficina con este nombre\n";
            }

            if(String.IsNullOrEmpty(txtTelefono.Text))
                errores += "El campo TELÉFONO es obligatorio\n";

            if (String.IsNullOrEmpty(txtEmail.Text))
                errores += "El campo EMAIL es obligatorio\n";

            if (String.IsNullOrEmpty(txtProvincia.Text))
                errores += "El campo PROVINCIA es obligatorio\n";

            if (String.IsNullOrEmpty(txtLocalidad.Text))
                errores += "El campo LOCALIDAD es obligatorio\n";

            if (String.IsNullOrEmpty(txtCiudad.Text))
                errores += "El campo CIUDAD es obligatorio\n";

            if (String.IsNullOrEmpty(txtCalle.Text))
                errores += "El campo CALLE es obligatorio\n";

            if (String.IsNullOrEmpty(txtNumero.Text))
                errores += "El campo NÚMERO es obligatorio\n";

            if (String.IsNullOrEmpty(txtCodigoPostal.Text))
                errores += "El campo CÓDIGO POSTAL es obligatorio\n";

            return errores;
        }

        private void frmOficinas_Load(object sender, EventArgs e)
        {
            this.PrepararDataGridViews();
            this.PrepararFaseInicial();
        }

        private void dgvOficinas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dgvOficinas.CurrentRow == null)
            {
                this.LimpiarTextBoxes();
                return;
            }

            Oficina oficina = this.oficinaNegocio.Traer(dgvOficinas.CurrentRow.Cells[0].Value.ToString());

            this.CargarTextBoxes(oficina);
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseAgregar();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseEditar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string errores = this.ValidarCampos(this.accion);

            if (!String.IsNullOrEmpty(errores))
            {
                MessageBox.Show("Error: " + errores);
                return;
            }

            Oficina oficina = new Oficina();
            oficina.Direccion = new Direccion();
            oficina.Contacto = new Contacto();

            oficina.Nombre = txtNombre.Text;

            oficina.Direccion.Codigo = !String.IsNullOrEmpty(txtDirCodigo.Text) ? new Guid(txtDirCodigo.Text) : Guid.Empty;
            oficina.Direccion.Calle = txtCalle.Text;
            oficina.Direccion.Numero = Convert.ToInt64(txtNumero.Text);
            oficina.Direccion.Piso = txtPiso.Text;
            oficina.Direccion.Localidad = txtLocalidad.Text;
            oficina.Direccion.Provincia = txtProvincia.Text;
            oficina.Direccion.CodigoPostal = txtCodigoPostal.Text;
            oficina.Direccion.Barrio = txtBarrio.Text;
            oficina.Direccion.Ciudad = txtCiudad.Text;
            oficina.Direccion.Torre = txtTorre.Text;
            oficina.Direccion.Departamento = txtDepartamento.Text;

            oficina.Contacto.Codigo = !String.IsNullOrEmpty(txtConCodigo.Text) ? new Guid(txtConCodigo.Text) : Guid.Empty;
            oficina.Contacto.Email = txtEmail.Text;
            oficina.Contacto.Telefono = txtTelefono.Text;
            oficina.Contacto.SitioWeb = new Uri(txtSitioWeb.Text);


            switch (this.accion)
            {
                case EFormAccion.Agregar:
                    if(this.oficinaNegocio.Agregar(oficina))
                    {
                        MessageBox.Show("Oficina agregada correctamente");
                        this.PrepararFaseInicial();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al agregar la oficina");
                    }
                    break;

                case EFormAccion.Modificar:
                    if (this.oficinaNegocio.Modificar(oficina))
                    {
                        MessageBox.Show("Oficina modificada correctamente");
                        this.PrepararFaseInicial();
                    }
                    else
                    {
                        MessageBox.Show("Hubo un error al modificar la oficina");
                    }
                    break;

                default:
                    break;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.PrepararFaseInicial();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de eliminar esta oficina?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            if (this.oficinaNegocio.Eliminar(txtNombre.Text))
            {
                MessageBox.Show("Oficina eliminada correctamente");
                this.PrepararFaseInicial();
            }
            else
            {
                MessageBox.Show("Hubo un error al eliminar la oficina");
            }
        }
    }
}
