using Entidad.Negocio;
using Negocio.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TF_Mendez_Reclutamiento.ABM;

namespace TF_Mendez_Reclutamiento
{
    public partial class frmLogin : Form
    {
        public delegate void LoginEvent();
        public LoginEvent LoggedIn;

        public frmLogin()
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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.ForeColor = Color.Red;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!UserHelper.Login(txtLegajo.Text, txtPassword.Text))
            {
                lblError.Text = "Usuario o password inextistentes";
                return;
            }                

            LoggedIn();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
