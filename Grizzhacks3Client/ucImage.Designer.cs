namespace Grizzhacks3Client
{
    partial class ucImage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pAnimation = new System.Windows.Forms.Panel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbCopy = new System.Windows.Forms.PictureBox();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            this.SuspendLayout();
            // 
            // pAnimation
            // 
            this.pAnimation.Controls.Add(this.pbImage);
            this.pAnimation.Controls.Add(this.panel2);
            this.pAnimation.Controls.Add(this.panel1);
            this.pAnimation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pAnimation.Location = new System.Drawing.Point(5, 5);
            this.pAnimation.Name = "pAnimation";
            this.pAnimation.Size = new System.Drawing.Size(205, 255);
            this.pAnimation.TabIndex = 0;
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Margin = new System.Windows.Forms.Padding(0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(205, 186);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 2;
            this.pbImage.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 186);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(205, 17);
            this.panel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pbCopy);
            this.panel1.Controls.Add(this.pbSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 203);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 52);
            this.panel1.TabIndex = 3;
            // 
            // pbCopy
            // 
            this.pbCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.pbCopy.Dock = System.Windows.Forms.DockStyle.Right;
            this.pbCopy.Image = global::Grizzhacks3Client.Properties.Resources.copy;
            this.pbCopy.Location = new System.Drawing.Point(106, 0);
            this.pbCopy.Name = "pbCopy";
            this.pbCopy.Size = new System.Drawing.Size(99, 52);
            this.pbCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbCopy.TabIndex = 1;
            this.pbCopy.TabStop = false;
            this.pbCopy.Click += new System.EventHandler(this.pbCopy_Click);
            this.pbCopy.MouseEnter += new System.EventHandler(this.pbCopy_MouseEnter);
            this.pbCopy.MouseLeave += new System.EventHandler(this.pbCopy_MouseLeave);
            // 
            // pbSave
            // 
            this.pbSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.pbSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbSave.Image = global::Grizzhacks3Client.Properties.Resources.icons8_folder_64;
            this.pbSave.Location = new System.Drawing.Point(0, 0);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(99, 52);
            this.pbSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSave.TabIndex = 0;
            this.pbSave.TabStop = false;
            this.pbSave.Click += new System.EventHandler(this.pbSave_Click);
            this.pbSave.MouseEnter += new System.EventHandler(this.pbSave_MouseEnter);
            this.pbSave.MouseLeave += new System.EventHandler(this.pbSave_MouseLeave);
            // 
            // ucImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pAnimation);
            this.Name = "ucImage";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(215, 265);
            this.pAnimation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pAnimation;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.PictureBox pbCopy;
        private System.Windows.Forms.Panel panel2;
    }
}
