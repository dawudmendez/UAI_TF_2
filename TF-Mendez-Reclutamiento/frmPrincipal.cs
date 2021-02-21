using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TF_Mendez_Reclutamiento.Reportes;
using TF_Mendez_Reclutamiento.ABM;
using Entidad.Negocio;

namespace TF_Mendez_Reclutamiento
{
    public partial class frmPrincipal : Form
    {

        private Form FormActivo = null;
        private List<string> Path = new List<string>();

        public frmPrincipal()
        {
            InitializeComponent();
            this.CustomizarDisenio();
        }

        private void CustomizarDisenio()
        {
            panelAdministracion.Visible = false;
            panelPosiciones.Visible = false;
            panelReportes.Visible = false;
            panelSeleccion.Visible = false;
            
        }

        public void CustomizarForm(Form Form)
        {
            Form.TopLevel = false;
            Form.FormBorderStyle = FormBorderStyle.None;
            Form.Dock = DockStyle.Fill;
            Form.BackColor = panelChildForm.BackColor;

            foreach (var item in Form.Controls.OfType<Label>())
            {
                item.ForeColor = Color.LightGray;
            }
        }

        private void EsconderSubmenues()
        {
            if (panelAdministracion.Visible)
                panelAdministracion.Visible = false;

            if (panelPosiciones.Visible)
                panelPosiciones.Visible = false;

            if (panelReportes.Visible)
                panelReportes.Visible = false;

            if (panelSeleccion.Visible)
                panelSeleccion.Visible = false;
        }

        private void MostrarSubmenu(Panel Panel)
        {
            if (!Panel.Visible)
            {
                this.EsconderSubmenues();
                Panel.Visible = true;
            }
            else
            {
                Panel.Visible = false;
            }
        }

        private void btnMenuPosiciones_Click(object sender, EventArgs e)
        {
            this.MostrarSubmenu(panelPosiciones);
        }

        private void btnMenuSeleccion_Click(object sender, EventArgs e)
        {
            this.MostrarSubmenu(panelSeleccion);
        }

        private void btnMenuReportes_Click(object sender, EventArgs e)
        {
            this.MostrarSubmenu(panelReportes);
        }

        private void btnMenuAdministracion_Click(object sender, EventArgs e)
        {
            this.MostrarSubmenu(panelAdministracion);
        }

        private void btnMenuAyuda_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Ayuda");
            this.EsconderSubmenues();
            this.AbrirChildForm(new frmAyuda());
        }

        private void btnMenuLogout_Click(object sender, EventArgs e)
        {
            this.EsconderSubmenues();
            Path.Clear();
            Path.Add("Iniciar Sesión");
            this.AbrirChildForm(new frmLogin());
        }

        private void btnMenuCandidatos_Click(object sender, EventArgs e)
        {
            this.EsconderSubmenues();
            Path.Clear();
            Path.Add("Candidatos");
            this.AbrirChildForm(new frmCandidatos());
        }
        
        private void AbrirChildForm(Form Child)
        {
            if (FormActivo != null)
                FormActivo.Close();            

            FormActivo = Child;

            this.CustomizarForm(Child);
            
            panelChildForm.Controls.Add(Child);
            panelChildForm.Tag = Child;
            Child.BringToFront();

            this.MostrarPath();

            if (!btnCerrar.Visible)
                btnCerrar.Visible = true;

            Child.Show();

        }

        private void MostrarPath()
        {
            string path = "";

            foreach (string item in Path)
            {
                path += item + " / ";
            }

            if(!String.IsNullOrEmpty(path))
                path = path.Substring(0, path.Length - 2);

            lblPath.Text = path;
        }

        private void CerrarChildActivo()
        {
            FormActivo.Close();
            FormActivo = null;
            btnCerrar.Visible = false;
            Path.Clear();
        }

        private void btnPosPerfiles_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Posiciones");
            Path.Add("Perfiles");
            this.AbrirChildForm(new frmDataGrid());            
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Path.Clear();
            this.MostrarPath();
            this.CerrarChildActivo();
        }

        private void btnPosPosiciones_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Posiciones");
            Path.Add("Posiciones");
            this.AbrirChildForm(new frmPosiciones());
        }

        private void btnProEntrevista_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Selección");
            Path.Add("Entrevistas");
            this.AbrirChildForm(new frmEntrevistas());
        }

        private void btnRepProceso_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Reportes");
            Path.Add("Procesos de selección");
            this.AbrirChildForm(new frmReporteProcesosSeleccion());
        }

        private void btnRepPosicion_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Reportes");
            Path.Add("Posiciones");
            this.AbrirChildForm(new frmReportePosiciones());
        }

        private void btnRepCandidato_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Reportes");
            Path.Add("Candidatos");
            this.AbrirChildForm(new frmReporteCandidatos());
        }

        private void btnRepTecnologia_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Reportes");
            Path.Add("Tecnologías");
            this.AbrirChildForm(new frmReporteTecnologias());
        }

        private void btnAdminConfiguracion_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Administración");
            Path.Add("Configuración");
            this.AbrirChildForm(new frmConfiguracion());
        }

        private void btnAdminOficina_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Administración");
            Path.Add("Oficinas");
            this.AbrirChildForm(new frmOficinas());
        }

        private void btnAdminUsuario_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Administración");
            Path.Add("Usuarios");
            this.AbrirChildForm(new frmUsuarios());
        }

        private void btnAdminEquipo_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Administración");
            Path.Add("Equipos");
            this.AbrirChildForm(new frmEquipo());
        }

        private void btnProSeleccion_Click(object sender, EventArgs e)
        {
            Path.Clear();
            Path.Add("Selección");
            Path.Add("Procesos de selección");
            this.AbrirChildForm(new frmProcesosSeleccion());
        }
    }
}
