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
            List<Oficina> oficinas = this.oficinaNegocio.TraerTodo().ToList();

            if (oficinas == null)
            {
                MessageBox.Show("no hay oficinas");
            }

            cboOficinaPrincipal.DataSource = oficinas;

            if (conf == null)
                return;

            txtCuit.Text = conf.Cuit;
            txtRazonSocial.Text = conf.RazonSocial;
        }
    }
}
