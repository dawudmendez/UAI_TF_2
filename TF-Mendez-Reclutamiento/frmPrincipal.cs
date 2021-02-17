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
    public partial class frmPrincipal : Form
    {
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
            this.EsconderSubmenues();
        }

        private void btnMenuLogout_Click(object sender, EventArgs e)
        {
            this.EsconderSubmenues();
        }

        private void btnMenuCandidatos_Click(object sender, EventArgs e)
        {
            this.EsconderSubmenues();
            this.AbrirChildForm(new frmCandidatos());
        }

        private Form FormActivo = null;
        private void AbrirChildForm(Form Child)
        {
            if (FormActivo != null)
                FormActivo.Close();            

            FormActivo = Child;
            Child.TopLevel = false;
            Child.FormBorderStyle = FormBorderStyle.None;
            Child.Dock = DockStyle.Fill;
            Child.BackColor = panelChildForm.BackColor;
            panelChildForm.Controls.Add(Child);
            panelChildForm.Tag = Child;
            Child.BringToFront();

            if (!btnCerrar.Visible)
                btnCerrar.Visible = true;

            Child.Show();

        }

        private void CerrarChildActivo()
        {
            FormActivo.Close();
            FormActivo = null;
            btnCerrar.Visible = false;
        }

        private void btnPosPerfiles_Click(object sender, EventArgs e)
        {
            this.AbrirChildForm(new frmPerfiles());
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.CerrarChildActivo();
        }

        private void btnPosPosiciones_Click(object sender, EventArgs e)
        {
            this.AbrirChildForm(new frmPosiciones());
        }

        private void btnProEntrevista_Click(object sender, EventArgs e)
        {
            this.AbrirChildForm(new frmProcesosSeleccion());
        }
    }
}
