﻿namespace PripravaNaZkousku
{
    partial class Form6
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
            this.btnGeneratePoints = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGeneratePoints
            // 
            this.btnGeneratePoints.Location = new System.Drawing.Point(713, 401);
            this.btnGeneratePoints.Name = "btnGeneratePoints";
            this.btnGeneratePoints.Size = new System.Drawing.Size(75, 23);
            this.btnGeneratePoints.TabIndex = 0;
            this.btnGeneratePoints.Text = "generate points";
            this.btnGeneratePoints.UseVisualStyleBackColor = true;
            this.btnGeneratePoints.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnGeneratePoints_MouseClick);
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGeneratePoints);
            this.Name = "Form6";
            this.Text = "Form6";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form6_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGeneratePoints;
    }
}