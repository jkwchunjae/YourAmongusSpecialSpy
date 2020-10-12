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
        MissionManager _missionManager;
        List<RecordData> _prevAmongusImage;

        readonly TimeSpan CaptureDelay = TimeSpan.FromMilliseconds(50);

        private int _beginIndex;
        private int _endIndex;
        private int _imageIndex;

        public Form1()
        {
            InitializeComponent();
            Delay.Value = (int)CaptureDelay.TotalMilliseconds;
            GifTestTimer.Tick += GifTestTimer_Tick;

            _recorder = new AmongusRecorder();
            _recorder.OnStop += (s, data) =>
            {
                ImageBeginIndex.Maximum = data.Count - 1;
                ImageEndIndex.Maximum = data.Count - 1;
                _prevAmongusImage = data;
            };

            _missionManager = new MissionManager();
            _missionManager.OnEvent += (s, data) =>
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        if (data.Mission != null)
                        {
                            this.Text = $"미션 중: {data.Mission.GetName()}";
                        }

                        if (!string.IsNullOrWhiteSpace(data.Message))
                        {
                            this.Text = data.Message;
                        }
                    }));
                }
            };

            Stop_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            Stop.Enabled = true;
            _recorder.Start(CaptureDelay);
            _missionManager.Run(TimeSpan.FromMilliseconds(500));
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Start.Enabled = true;
            Stop.Enabled = false;
            _recorder.Stop();
            _missionManager.Stop();
        }

        private void Run_Click(object sender, EventArgs e)
        {
            //_recorder.Run(pictureBox1);
        }

        private void Capture_Click(object sender, EventArgs e)
        {
            var dir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads\\amongus-captures\\missions"));
            var pngPath = new DirectoryInfo(Path.Combine(dir.FullName, "png"));
            var bmpPath = new DirectoryInfo(Path.Combine(dir.FullName, "bmp"));

            if (!pngPath.Exists)
                Directory.CreateDirectory(dir.FullName);

            if (!bmpPath.Exists)
                Directory.CreateDirectory(dir.FullName);

            Amongus.GetImage().Save(Path.Combine(pngPath.FullName, $"{Guid.NewGuid()}.png"), ImageFormat.Png);
            Amongus.GetImage().Save(Path.Combine(bmpPath.FullName, $"{Guid.NewGuid()}.bmp"), ImageFormat.Bmp);
        }

        private void ImageTrackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                var trackBar = (TrackBar)sender;
                pictureBox1.Image = _prevAmongusImage[trackBar.Value].Image;
            }
            catch
            { }
        }

        private void TestGifButton_Click(object sender, EventArgs e)
        {
            GifTestTimer.Stop();

            GifTestTimer.Interval = (int)Delay.Value;
            _beginIndex = ImageBeginIndex.Value;
            _endIndex = ImageEndIndex.Value;
            _imageIndex = _beginIndex;

            GifTestTimer.Start();
        }

        private void GifTestTimer_Tick(object sender, EventArgs e)
        {
            var index = Math.Max(0, Math.Min(_prevAmongusImage.Count - 1, _imageIndex));
            pictureBox1.Image = _prevAmongusImage[index].Image;

            if (_imageIndex >= _endIndex)
            {
                GifTestTimer.Stop();
            }
            _imageIndex++;
        }

        private void CreateGifButton_Click(object sender, EventArgs e)
        {
            var imageData = _prevAmongusImage
                .Select((x, i) => new { Data = x, Index = i })
                .Where(x => x.Index >= ImageBeginIndex.Value)
                .Where(x => x.Index <= ImageEndIndex.Value)
                .Select(x => x.Data)
                .ToList();

            GifManager.CreateGif(imageData, (int)Delay.Value);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop_Click(null, null);
        }
    }
}
