
namespace GestionMatos
{
    partial class FormConnection
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
            this.connexion = new System.Windows.Forms.GroupBox();
            this.pass_field = new System.Windows.Forms.TextBox();
            this.id_field = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.Label();
            this.annuler = new System.Windows.Forms.Button();
            this.valider = new System.Windows.Forms.Button();
            this.connexion.SuspendLayout();
            this.SuspendLayout();
            // 
            // connexion
            // 
            this.connexion.Controls.Add(this.pass_field);
            this.connexion.Controls.Add(this.id_field);
            this.connexion.Controls.Add(this.pass);
            this.connexion.Controls.Add(this.id);
            this.connexion.Location = new System.Drawing.Point(11, 11);
            this.connexion.Margin = new System.Windows.Forms.Padding(2);
            this.connexion.Name = "connexion";
            this.connexion.Padding = new System.Windows.Forms.Padding(2);
            this.connexion.Size = new System.Drawing.Size(279, 107);
            this.connexion.TabIndex = 0;
            this.connexion.TabStop = false;
            this.connexion.Text = "Connexion";
            // 
            // pass_field
            // 
            this.pass_field.Location = new System.Drawing.Point(118, 61);
            this.pass_field.Margin = new System.Windows.Forms.Padding(2);
            this.pass_field.Name = "pass_field";
            this.pass_field.PasswordChar = '*';
            this.pass_field.Size = new System.Drawing.Size(133, 20);
            this.pass_field.TabIndex = 2;
            // 
            // id_field
            // 
            this.id_field.Location = new System.Drawing.Point(118, 25);
            this.id_field.Margin = new System.Windows.Forms.Padding(2);
            this.id_field.Name = "id_field";
            this.id_field.Size = new System.Drawing.Size(133, 20);
            this.id_field.TabIndex = 1;
            // 
            // pass
            // 
            this.pass.AutoSize = true;
            this.pass.Location = new System.Drawing.Point(13, 64);
            this.pass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(77, 13);
            this.pass.TabIndex = 1;
            this.pass.Text = "Mot de passe :";
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.Location = new System.Drawing.Point(13, 27);
            this.id.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(59, 13);
            this.id.TabIndex = 0;
            this.id.Text = "Identifiant :";
            // 
            // annuler
            // 
            this.annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.annuler.Location = new System.Drawing.Point(166, 137);
            this.annuler.Margin = new System.Windows.Forms.Padding(2);
            this.annuler.Name = "annuler";
            this.annuler.Size = new System.Drawing.Size(79, 23);
            this.annuler.TabIndex = 6;
            this.annuler.Text = "Annuler";
            this.annuler.UseVisualStyleBackColor = true;
            this.annuler.Click += new System.EventHandler(this.annuler_Click);
            // 
            // valider
            // 
            this.valider.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.valider.Location = new System.Drawing.Point(69, 137);
            this.valider.Margin = new System.Windows.Forms.Padding(2);
            this.valider.Name = "valider";
            this.valider.Size = new System.Drawing.Size(79, 23);
            this.valider.TabIndex = 5;
            this.valider.Text = "Valider";
            this.valider.UseVisualStyleBackColor = true;
            this.valider.Click += new System.EventHandler(this.valider_Click);
            // 
            // FormConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.annuler;
            this.ClientSize = new System.Drawing.Size(313, 178);
            this.Controls.Add(this.annuler);
            this.Controls.Add(this.valider);
            this.Controls.Add(this.connexion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connexion";
            this.connexion.ResumeLayout(false);
            this.connexion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox connexion;
        private System.Windows.Forms.Label pass;
        private System.Windows.Forms.Label id;
        private System.Windows.Forms.TextBox pass_field;
        private System.Windows.Forms.TextBox id_field;
        private System.Windows.Forms.Button annuler;
        private System.Windows.Forms.Button valider;
    }
}