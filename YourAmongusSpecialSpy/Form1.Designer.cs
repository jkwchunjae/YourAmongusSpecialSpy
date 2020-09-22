namespace YourAmongusSpecialSpy
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.Capture = new System.Windows.Forms.Button();
            this.ImageTrackBar = new System.Windows.Forms.TrackBar();
            this.GifStart = new System.Windows.Forms.Button();
            this.GifEnd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(628, 346);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(12, 12);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(50, 23);
            this.Start.TabIndex = 1;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(68, 13);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(50, 23);
            this.Stop.TabIndex = 2;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // Capture
            // 
            this.Capture.Location = new System.Drawing.Point(169, 12);
            this.Capture.Name = "Capture";
            this.Capture.Size = new System.Drawing.Size(75, 23);
            this.Capture.TabIndex = 4;
            this.Capture.Text = "Capture";
            this.Capture.UseVisualStyleBackColor = true;
            this.Capture.Click += new System.EventHandler(this.Capture_Click);
            // 
            // ImageTrackBar
            // 
            this.ImageTrackBar.Location = new System.Drawing.Point(12, 42);
            this.ImageTrackBar.Name = "ImageTrackBar";
            this.ImageTrackBar.Size = new System.Drawing.Size(628, 45);
            this.ImageTrackBar.TabIndex = 5;
            this.ImageTrackBar.Scroll += new System.EventHandler(this.ImageTrackBar_Scroll);
            // 
            // GifStart
            // 
            this.GifStart.Location = new System.Drawing.Point(294, 12);
            this.GifStart.Name = "GifStart";
            this.GifStart.Size = new System.Drawing.Size(68, 23);
            this.GifStart.TabIndex = 6;
            this.GifStart.Text = "Gif Start";
            this.GifStart.UseVisualStyleBackColor = true;
            this.GifStart.Click += new System.EventHandler(this.GifStart_Click);
            // 
            // GifEnd
            // 
            this.GifEnd.Location = new System.Drawing.Point(375, 12);
            this.GifEnd.Name = "GifEnd";
            this.GifEnd.Size = new System.Drawing.Size(68, 23);
            this.GifEnd.TabIndex = 7;
            this.GifEnd.Text = "Gif End";
            this.GifEnd.UseVisualStyleBackColor = true;
            this.GifEnd.Click += new System.EventHandler(this.GifEnd_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 482);
            this.Controls.Add(this.GifEnd);
            this.Controls.Add(this.GifStart);
            this.Controls.Add(this.ImageTrackBar);
            this.Controls.Add(this.Capture);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Capture;
        private System.Windows.Forms.TrackBar ImageTrackBar;
        private System.Windows.Forms.Button GifStart;
        private System.Windows.Forms.Button GifEnd;
    }
}

