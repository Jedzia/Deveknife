namespace Deveknife.Blades
{
    partial class VirtualDubUI
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dropLocationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Deveknife.Blades.Properties.Resources.documents;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(127, 122);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.Panel1GiveFeedback);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(433, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(235, 109);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(69, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 122);
            this.panel1.TabIndex = 2;
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.panel1_DragEnter);
            this.panel1.DragOver += new System.Windows.Forms.DragEventHandler(this.panel1_DragOver);
            this.panel1.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.Panel1GiveFeedback);
            // 
            // dropLocationLabel
            // 
            this.dropLocationLabel.AutoSize = true;
            this.dropLocationLabel.Location = new System.Drawing.Point(158, 273);
            this.dropLocationLabel.Name = "dropLocationLabel";
            this.dropLocationLabel.Size = new System.Drawing.Size(35, 13);
            this.dropLocationLabel.TabIndex = 3;
            this.dropLocationLabel.Text = "label1";
            // 
            // VirtualDubUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dropLocationLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "VirtualDubUI";
            this.Size = new System.Drawing.Size(797, 366);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label dropLocationLabel;
    }
}
