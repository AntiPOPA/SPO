﻿namespace AntiPOPA
{
    partial class Laba4
    {
        
        private System.ComponentModel.IContainer components = null;

        
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
            this.Letuchka = new System.Windows.Forms.Button();
            this.ProgrammText = new System.Windows.Forms.TextBox();
            this.Tokens = new System.Windows.Forms.TextBox();
            this.VvodPolsk = new System.Windows.Forms.TextBox();
            this.Output = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Letuchka
            // 
            this.Letuchka.Location = new System.Drawing.Point(21, 188);
            this.Letuchka.Name = "Letuchka";
            this.Letuchka.Size = new System.Drawing.Size(150, 23);
            this.Letuchka.TabIndex = 0;
            this.Letuchka.Text = "Nazhmi menya";
            this.Letuchka.UseVisualStyleBackColor = true;
            this.Letuchka.Click += new System.EventHandler(this.Letuchka_Click);
            // 
            // ProgrammText
            // 
            this.ProgrammText.Location = new System.Drawing.Point(21, 27);
            this.ProgrammText.Name = "uText";
            this.ProgrammText.Size = new System.Drawing.Size(225, 20);
            this.ProgrammText.TabIndex = 1;
            // 
            // Tokens
            // 
            this.Tokens.Location = new System.Drawing.Point(21, 73);
            this.Tokens.Name = "Tokens";
            this.Tokens.Size = new System.Drawing.Size(225, 20);
            this.Tokens.TabIndex = 2;
            // 
            // PolskaVudkaDobrovudka
            // 
            this.VvodPolsk.Location = new System.Drawing.Point(21, 115);
            this.VvodPolsk.Name = "VvodPolsk";
            this.VvodPolsk.Size = new System.Drawing.Size(225, 20);
            this.VvodPolsk.TabIndex = 3;
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(21, 162);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(225, 20);
            this.Output.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 255);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.VvodPolsk);
            this.Controls.Add(this.Tokens);
            this.Controls.Add(this.ProgrammText);
            this.Controls.Add(this.Letuchka);
            this.Name = "Laba4";
            this.Text = "Laba4";
            this.Load += new System.EventHandler(this.Laba4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Letuchka;
        private System.Windows.Forms.TextBox ProgrammText;
        private System.Windows.Forms.TextBox Tokens;
        private System.Windows.Forms.TextBox VvodPolsk;
        private System.Windows.Forms.TextBox Output;
    }
}

