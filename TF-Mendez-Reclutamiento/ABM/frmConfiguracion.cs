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
    public partial class frmConfiguracion : Form
    {
        private ConfiguracionNegocio configuracionNegocio = new ConfiguracionNegocio();
        private OficinaNegocio oficinaNegocio = new OficinaNegocio();

        public frmConfiguracion()
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

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            Configuracion conf = this.configuracionNegocio.TraerConfiguracion();

            this.CargarComboBoxes();

            if (conf == null)
                return;
                        
            this.CargarTextBoxes(conf);
            this.SeleccionarComboBoxes(conf);
        }

        private void CargarComboBoxes()
        {
            cboOficinaPrincipal.DataSource = this.oficinaNegocio.TraerTodo().ToList();

            cboOficinaPrincipal.DisplayMember = "nombre";
            cboOficinaPrincipal.ValueMember = "nombre";
        }

        private void CargarTextBoxes(Configuracion configuracion)
        {
            txtRazonSocial.Text = configuracion.RazonSocial;
            txtCuit.Text = configuracion.Cuit;

        }

        private void SeleccionarComboBoxes(Configuracion configuracion)
        {
            cboOficinaPrincipal.SelectedValue = configuracion.OficinaPrincipal.Nombre;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Configuracion conf = new Configuracion();
            conf.Cuit = txtCuit.Text;
            conf.RazonSocial = txtRazonSocial.Text;
            conf.OficinaPrincipal = (Oficina)cboOficinaPrincipal.SelectedItem;

            if (this.configuracionNegocio.ActualizarConfiguracion(conf))
                MessageBox.Show("Configuración modificada correctamente");
            else
                MessageBox.Show("Hubo un error al modificar la configuración");
        }
    }
}
