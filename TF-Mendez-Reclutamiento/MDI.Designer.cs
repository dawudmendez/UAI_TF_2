namespace TF_Mendez_Reclutamiento
{
    partial class MDI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.candidatosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.posicionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.procesosSeleccionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oficinasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usuariosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.equiposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tecnologíasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.procesosDeSelecciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.posicionesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.candidatosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tecnologíasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.procesosDeSelecciónToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.entrevistasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.candidatosToolStripMenuItem,
            this.posicionesToolStripMenuItem,
            this.procesosSeleccionToolStripMenuItem,
            this.usuariosToolStripMenuItem,
            this.reportesToolStripMenuItem,
            this.iniciarSesiónToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // candidatosToolStripMenuItem
            // 
            this.candidatosToolStripMenuItem.Name = "candidatosToolStripMenuItem";
            this.candidatosToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.candidatosToolStripMenuItem.Text = "Candidatos";
            this.candidatosToolStripMenuItem.Click += new System.EventHandler(this.candidatosToolStripMenuItem_Click);
            // 
            // posicionesToolStripMenuItem
            // 
            this.posicionesToolStripMenuItem.Name = "posicionesToolStripMenuItem";
            this.posicionesToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.posicionesToolStripMenuItem.Text = "Posiciones";
            this.posicionesToolStripMenuItem.Click += new System.EventHandler(this.posicionesToolStripMenuItem_Click);
            // 
            // procesosSeleccionToolStripMenuItem
            // 
            this.procesosSeleccionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.procesosDeSelecciónToolStripMenuItem1,
            this.entrevistasToolStripMenuItem});
            this.procesosSeleccionToolStripMenuItem.Name = "procesosSeleccionToolStripMenuItem";
            this.procesosSeleccionToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.procesosSeleccionToolStripMenuItem.Text = "Selección";
            // 
            // usuariosToolStripMenuItem
            // 
            this.usuariosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.procesosDeSelecciónToolStripMenuItem,
            this.posicionesToolStripMenuItem1,
            this.candidatosToolStripMenuItem1,
            this.tecnologíasToolStripMenuItem1});
            this.usuariosToolStripMenuItem.Name = "usuariosToolStripMenuItem";
            this.usuariosToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.usuariosToolStripMenuItem.Text = "Reportes";
            this.usuariosToolStripMenuItem.Click += new System.EventHandler(this.usuariosToolStripMenuItem_Click);
            // 
            // reportesToolStripMenuItem
            // 
            this.reportesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraciónToolStripMenuItem,
            this.oficinasToolStripMenuItem,
            this.usuariosToolStripMenuItem1,
            this.equiposToolStripMenuItem,
            this.tecnologíasToolStripMenuItem});
            this.reportesToolStripMenuItem.Name = "reportesToolStripMenuItem";
            this.reportesToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
            this.reportesToolStripMenuItem.Text = "Administración";
            this.reportesToolStripMenuItem.Click += new System.EventHandler(this.reportesToolStripMenuItem_Click);
            // 
            // iniciarSesiónToolStripMenuItem
            // 
            this.iniciarSesiónToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.iniciarSesiónToolStripMenuItem.Name = "iniciarSesiónToolStripMenuItem";
            this.iniciarSesiónToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.iniciarSesiónToolStripMenuItem.Text = "Iniciar Sesión";
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar Sesión";
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            this.configuraciónToolStripMenuItem.Click += new System.EventHandler(this.configuraciónToolStripMenuItem_Click);
            // 
            // oficinasToolStripMenuItem
            // 
            this.oficinasToolStripMenuItem.Name = "oficinasToolStripMenuItem";
            this.oficinasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.oficinasToolStripMenuItem.Text = "Oficinas";
            this.oficinasToolStripMenuItem.Click += new System.EventHandler(this.oficinasToolStripMenuItem_Click);
            // 
            // usuariosToolStripMenuItem1
            // 
            this.usuariosToolStripMenuItem1.Name = "usuariosToolStripMenuItem1";
            this.usuariosToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.usuariosToolStripMenuItem1.Text = "Usuarios";
            this.usuariosToolStripMenuItem1.Click += new System.EventHandler(this.usuariosToolStripMenuItem1_Click);
            // 
            // equiposToolStripMenuItem
            // 
            this.equiposToolStripMenuItem.Name = "equiposToolStripMenuItem";
            this.equiposToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.equiposToolStripMenuItem.Text = "Equipos";
            this.equiposToolStripMenuItem.Click += new System.EventHandler(this.equiposToolStripMenuItem_Click);
            // 
            // tecnologíasToolStripMenuItem
            // 
            this.tecnologíasToolStripMenuItem.Name = "tecnologíasToolStripMenuItem";
            this.tecnologíasToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tecnologíasToolStripMenuItem.Text = "Tecnologías";
            this.tecnologíasToolStripMenuItem.Click += new System.EventHandler(this.tecnologíasToolStripMenuItem_Click);
            // 
            // procesosDeSelecciónToolStripMenuItem
            // 
            this.procesosDeSelecciónToolStripMenuItem.Name = "procesosDeSelecciónToolStripMenuItem";
            this.procesosDeSelecciónToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.procesosDeSelecciónToolStripMenuItem.Text = "Procesos de Selección";
            // 
            // posicionesToolStripMenuItem1
            // 
            this.posicionesToolStripMenuItem1.Name = "posicionesToolStripMenuItem1";
            this.posicionesToolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.posicionesToolStripMenuItem1.Text = "Posiciones";
            // 
            // candidatosToolStripMenuItem1
            // 
            this.candidatosToolStripMenuItem1.Name = "candidatosToolStripMenuItem1";
            this.candidatosToolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.candidatosToolStripMenuItem1.Text = "Candidatos";
            // 
            // tecnologíasToolStripMenuItem1
            // 
            this.tecnologíasToolStripMenuItem1.Name = "tecnologíasToolStripMenuItem1";
            this.tecnologíasToolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.tecnologíasToolStripMenuItem1.Text = "Tecnologías";
            this.tecnologíasToolStripMenuItem1.Click += new System.EventHandler(this.tecnologíasToolStripMenuItem1_Click);
            // 
            // procesosDeSelecciónToolStripMenuItem1
            // 
            this.procesosDeSelecciónToolStripMenuItem1.Name = "procesosDeSelecciónToolStripMenuItem1";
            this.procesosDeSelecciónToolStripMenuItem1.Size = new System.Drawing.Size(189, 22);
            this.procesosDeSelecciónToolStripMenuItem1.Text = "Procesos de selección";
            this.procesosDeSelecciónToolStripMenuItem1.Click += new System.EventHandler(this.procesosDeSelecciónToolStripMenuItem1_Click);
            // 
            // entrevistasToolStripMenuItem
            // 
            this.entrevistasToolStripMenuItem.Name = "entrevistasToolStripMenuItem";
            this.entrevistasToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.entrevistasToolStripMenuItem.Text = "Entrevistas";
            this.entrevistasToolStripMenuItem.Click += new System.EventHandler(this.entrevistasToolStripMenuItem_Click);
            // 
            // MDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MDI";
            this.Text = "Sistema de Reclutamiento de Profesionales";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MDI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem candidatosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem posicionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem procesosSeleccionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oficinasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usuariosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem equiposToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tecnologíasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem procesosDeSelecciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem posicionesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem candidatosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tecnologíasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem procesosDeSelecciónToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem entrevistasToolStripMenuItem;
    }
}

