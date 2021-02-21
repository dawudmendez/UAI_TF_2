using Entidad.Enums;
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
    public partial class frmUsuario : Form
    {
        private UsuarioNegocio usuarioNegocio;
        public frmUsuario()
        {
            InitializeComponent();
            this.usuarioNegocio = new UsuarioNegocio();
        }

        private Usuario MapearUsuario()
        {
            Usuario usu = new Usuario();

            usu.Apellido = txtApellido.Text;
            usu.Legajo = txtLegajo.Text;
            usu.Nombre = txtNombre.Text;
            usu.Password = txtPassword.Text;
            usu.Puesto = (EPuesto) Enum.Parse(typeof(EPuesto), cboPuesto.SelectedItem.ToString(), true);
            usu.User = txtUser.Text;
            //usu.Password = txtPassword.Text;

            return usu;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Usuario usu = this.MapearUsuario();
            this.usuarioNegocio.CrearUsuario(usu);

        }
    }
}
