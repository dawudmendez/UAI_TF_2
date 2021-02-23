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
    public partial class frmAyuda : Form
    {
        public frmAyuda()
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://github.com/dawudmendez/UAI_TF_2");
        }

        private void frmAyuda_Load(object sender, EventArgs e)
        {
            lblCreditos.Text = "Sistema de Gestión de Reclutamiento de Profesionales - Versión 2.0\n";
            lblCreditos.Text += "Desarrollado por Muhammad Dawud Mendez para la Universidad Abierta Interamericana\n";
            lblCreditos.Text += "Carrera Analista Programador - Materia Proyecto Final\n";
            lblCreditos.Text += "Profesores: Darío Cardacci y Leonardo Battaglia\n";
        }
    }
}
