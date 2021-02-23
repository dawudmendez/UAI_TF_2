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
    public partial class frmCandidatos : Form
    {
        public frmCandidatos()
        {
            InitializeComponent();

            panelCandidato.Dock = DockStyle.Fill;
            panelExperiencia.Dock = DockStyle.Fill;
            panelEducacion.Dock = DockStyle.Fill;

            panelCandidato.AutoScroll = true;
            panelExperiencia.AutoScroll = true;
            panelEducacion.AutoScroll = true;

            panelCandidato.BringToFront();

            this.Size = new Size(787, 800);
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

        private void btnCandSiguiente_Click(object sender, EventArgs e)
        {
            panelExperiencia.BringToFront();
        }

        private void btnExpSiguiente_Click(object sender, EventArgs e)
        {
            panelEducacion.BringToFront();
        }
    }
}
