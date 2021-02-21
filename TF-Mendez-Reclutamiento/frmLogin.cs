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

namespace TF_Mendez_Reclutamiento
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.User = txtUser.Text;
            usuario.Password = txtPassword.Text;

            if (LoginHelper.Login(usuario))
                this.Close();
        }
    }
}
