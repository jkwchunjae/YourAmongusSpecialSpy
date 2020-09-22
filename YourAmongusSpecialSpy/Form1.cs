using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YourAmongusSpecialSpy
{
    public partial class Form1 : Form
    {
        private delegate void ResizeFormDelegate(int width, int height);
        AmongusRecorder _recorder;
        List<RecordData> _prevAmongusImage;
        GifManager _gifManager;

        public Form1()
        {
            InitializeComponent();

            _recorder = new AmongusRecorder();
            _recorder.OnStop += (s, data) =>
            {
                ImageTrackBar.Maximum = data.Count;
                _prevAmongusImage = data;
            };

            Stop_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            Stop.Enabled = true;
            _recorder.Start(TimeSpan.FromMilliseconds(100));
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Start.Enabled = true;
            Stop.Enabled = false;
            _recorder.Stop();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            //_recorder.Run(pictureBox1);
        }

        private void Capture_Click(object sender, EventArgs e)
        {
            var dir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads\\amongus-captures"));
            if (!dir.Exists)
                Directory.CreateDirectory(dir.FullName);

            Amongus.GetImage().Save(Path.Combine(dir.FullName, $"{Guid.NewGuid()}.bmp"), ImageFormat.Bmp);
        }

        private void ImageTrackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = _prevAmongusImage[ImageTrackBar.Value].Image;
            }
            catch
            { }
        }

        private void GifStart_Click(object sender, EventArgs e)
        {
            _gifManager = new GifManager();
            _gifManager.Init(_prevAmongusImage, ImageTrackBar.Value);
        }

        private void GifEnd_Click(object sender, EventArgs e)
        {
            _gifManager?.Complete(ImageTrackBar.Value);
        }
    }
}
