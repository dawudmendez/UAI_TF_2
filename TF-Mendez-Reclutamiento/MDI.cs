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
    public partial class MDI : Form
    {
        private Form MDIChild;
        public MDI()
        {
            InitializeComponent();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MDI_Load(object sender, EventArgs e)
        {
            this.Text = "Sistema de Reclutamiento de Profesionales";
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private bool CerrarChilds()
        {
            if (MDIChild == null)
                return true;

            if (MDIChild.Name.ToUpper() == "FRMLOGIN")
            {
                MDIChild.Close();
                MDIChild.Dispose();

                return true;
            }

            string mensaje = "Está seguro que desea cerrar? Todos los cambios no guardados se perderán.";
            string titulo = "Cerrar ventana";
            MessageBoxButtons botones = MessageBoxButtons.YesNo;
            DialogResult resultado = MessageBox.Show(mensaje, titulo, botones);

            if (resultado == DialogResult.Yes)
            {
                MDIChild.Close();
                MDIChild.Dispose();

                return true;
            }
            else
            {
                return false;
            }

        }

        private void OpenForm(Form form, string Caption)
        {

            if (CerrarChilds())
            {
                MDIChild = form;
                MDIChild.MdiParent = this;
                MDIChild.Text = Caption;
                MDIChild.WindowState = FormWindowState.Maximized;
                MDIChild.StartPosition = FormStartPosition.CenterParent;
                MDIChild.ControlBox = false;
                MDIChild.Show();
            }
            
        }

        private void tecnologíasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmTecnologia();
            OpenForm(form, "Tecnologías");
        }

        private void tecnologíasToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmConfiguracion();
            OpenForm(form, "Configuración");
        }

        private void oficinasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmOficina();
            OpenForm(form, "Oficinas");
        }

        private void usuariosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form form = new frmUsuario();
            OpenForm(form, "Usuarios");
        }

        private void equiposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmEquipo();
            OpenForm(form, "Equipos");
        }

        private void posicionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmPosicion();
            OpenForm(form, "Posicion");
        }

        private void candidatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmCandidato();
            OpenForm(form, "Candidatos");
        }

        private void procesosDeSelecciónToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form form = new frmProcesoSeleccion();
            OpenForm(form, "Procesos de Selección");
        }

        private void entrevistasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new frmEntrevistas();
            OpenForm(form, "Entrevista");
        }
    }
}
