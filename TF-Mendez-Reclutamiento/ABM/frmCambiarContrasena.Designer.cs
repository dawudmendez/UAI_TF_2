
namespace TF_Mendez_Reclutamiento.ABM
{
    partial class frmCambiarContrasena
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtActual = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNueva = new System.Windows.Forms.TextBox();
            this.btnCambiar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtActual);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNueva);
            this.panel1.Controls.Add(this.btnCambiar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 195);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Contraseña actual";
            // 
            // txtActual
            // 
            this.txtActual.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtActual.Location = new System.Drawing.Point(117, 10);
            this.txtActual.Name = "txtActual";
            this.txtActual.Size = new System.Drawing.Size(210, 20);
            this.txtActual.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Contraseña nueva";
            // 
            // txtNueva
            // 
            this.txtNueva.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNueva.Location = new System.Drawing.Point(117, 46);
            this.txtNueva.Name = "txtNueva";
            this.txtNueva.Size = new System.Drawing.Size(210, 20);
            this.txtNueva.TabIndex = 2;
            // 
            // btnCambiar
            // 
            this.btnCambiar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCambiar.Location = new System.Drawing.Point(12, 144);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(315, 40);
            this.btnCambiar.TabIndex = 3;
            this.btnCambiar.Text = "Cambiar Contraseña";
            this.btnCambiar.UseVisualStyleBackColor = true;
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // frmCambiarContrasena
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 195);
            this.Controls.Add(this.panel1);
            this.Name = "frmCambiarContrasena";
            this.Text = "frmCambiarContrasena";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtActual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNueva;
        private System.Windows.Forms.Button btnCambiar;
    }
}