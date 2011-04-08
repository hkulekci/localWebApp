namespace loader
{
    partial class Loader
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
            this.run = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.ListBox();
            this.close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // run
            // 
            this.run.Location = new System.Drawing.Point(292, 269);
            this.run.Name = "run";
            this.run.Size = new System.Drawing.Size(121, 38);
            this.run.TabIndex = 0;
            this.run.Text = "Run";
            this.run.UseVisualStyleBackColor = true;
            this.run.Click += new System.EventHandler(this.run_Click);
            // 
            // log
            // 
            this.log.FormattingEnabled = true;
            this.log.Location = new System.Drawing.Point(12, 12);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(528, 251);
            this.log.TabIndex = 1;
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(419, 269);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(121, 38);
            this.close.TabIndex = 2;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // Loader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 319);
            this.Controls.Add(this.close);
            this.Controls.Add(this.log);
            this.Controls.Add(this.run);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Loader";
            this.ShowInTaskbar = false;
            this.Text = "Loader";
            this.Load += new System.EventHandler(this.run_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button run;
        private System.Windows.Forms.ListBox log;
        private System.Windows.Forms.Button close;
    }
}

