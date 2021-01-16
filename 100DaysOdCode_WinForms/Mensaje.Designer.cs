namespace _100DaysOdCode_WinForms
{
    partial class Mensaje
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
            this.lblMensaje = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnNoAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(39, 23);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(349, 61);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "NoTexto";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAceptar
            // 
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAceptar.Location = new System.Drawing.Point(165, 100);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnOpcion);
            // 
            // btnNoAceptar
            // 
            this.btnNoAceptar.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNoAceptar.Location = new System.Drawing.Point(212, 100);
            this.btnNoAceptar.Name = "btnNoAceptar";
            this.btnNoAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnNoAceptar.TabIndex = 1;
            this.btnNoAceptar.Text = "NO";
            this.btnNoAceptar.UseVisualStyleBackColor = true;
            this.btnNoAceptar.Visible = false;
            this.btnNoAceptar.Click += new System.EventHandler(this.btnOpcion);
            // 
            // Mensaje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 144);
            this.Controls.Add(this.btnNoAceptar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lblMensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Mensaje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mensaje del sistema";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnNoAceptar;
    }
}