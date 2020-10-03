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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.Capture = new System.Windows.Forms.Button();
            this.ImageBeginIndex = new System.Windows.Forms.TrackBar();
            this.Delay = new System.Windows.Forms.NumericUpDown();
            this.ImageEndIndex = new System.Windows.Forms.TrackBar();
            this.TestGifButton = new System.Windows.Forms.Button();
            this.CreateGifButton = new System.Windows.Forms.Button();
            this.GifTestTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBeginIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageEndIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 113);
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
            // ImageBeginIndex
            // 
            this.ImageBeginIndex.Location = new System.Drawing.Point(12, 42);
            this.ImageBeginIndex.Name = "ImageBeginIndex";
            this.ImageBeginIndex.Size = new System.Drawing.Size(628, 45);
            this.ImageBeginIndex.TabIndex = 5;
            this.ImageBeginIndex.Scroll += new System.EventHandler(this.ImageTrackBar_Scroll);
            // 
            // Delay
            // 
            this.Delay.Location = new System.Drawing.Point(344, 12);
            this.Delay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Delay.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Delay.Name = "Delay";
            this.Delay.Size = new System.Drawing.Size(58, 21);
            this.Delay.TabIndex = 8;
            this.Delay.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // ImageEndIndex
            // 
            this.ImageEndIndex.Location = new System.Drawing.Point(12, 73);
            this.ImageEndIndex.Name = "ImageEndIndex";
            this.ImageEndIndex.Size = new System.Drawing.Size(628, 45);
            this.ImageEndIndex.TabIndex = 9;
            this.ImageEndIndex.Scroll += new System.EventHandler(this.ImageTrackBar_Scroll);
            // 
            // TestGifButton
            // 
            this.TestGifButton.Location = new System.Drawing.Point(408, 13);
            this.TestGifButton.Name = "TestGifButton";
            this.TestGifButton.Size = new System.Drawing.Size(75, 23);
            this.TestGifButton.TabIndex = 10;
            this.TestGifButton.Text = "Test gif";
            this.TestGifButton.UseVisualStyleBackColor = true;
            this.TestGifButton.Click += new System.EventHandler(this.TestGifButton_Click);
            // 
            // CreateGifButton
            // 
            this.CreateGifButton.Location = new System.Drawing.Point(489, 13);
            this.CreateGifButton.Name = "CreateGifButton";
            this.CreateGifButton.Size = new System.Drawing.Size(75, 23);
            this.CreateGifButton.TabIndex = 11;
            this.CreateGifButton.Text = "Create gif";
            this.CreateGifButton.UseVisualStyleBackColor = true;
            this.CreateGifButton.Click += new System.EventHandler(this.CreateGifButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 482);
            this.Controls.Add(this.CreateGifButton);
            this.Controls.Add(this.TestGifButton);
            this.Controls.Add(this.ImageEndIndex);
            this.Controls.Add(this.Delay);
            this.Controls.Add(this.ImageBeginIndex);
            this.Controls.Add(this.Capture);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBeginIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageEndIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button Capture;
        private System.Windows.Forms.TrackBar ImageBeginIndex;
        private System.Windows.Forms.NumericUpDown Delay;
        private System.Windows.Forms.TrackBar ImageEndIndex;
        private System.Windows.Forms.Button TestGifButton;
        private System.Windows.Forms.Button CreateGifButton;
        private System.Windows.Forms.Timer GifTestTimer;
    }
}

