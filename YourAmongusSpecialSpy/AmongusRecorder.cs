using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YourAmongusSpecialSpy
{
    public class RecordData
    {
        public DateTime Time { get; set; }
        public Bitmap Image { get; set; }
    }

    public class AmongusRecorder : System.ComponentModel.Component
    {
        private List<RecordData> _data = new List<RecordData>();

        private bool _stop;

        public event EventHandler<List<RecordData>> OnStop;

        public AmongusRecorder()
        {
        }

        public void Start(TimeSpan interval)
        {
            new Thread(new ThreadStart(() =>
            {
                _stop = false;
                _data.Clear();
                while (_stop == false)
                {
                    var image = Amongus.GetImage();
                    _data.Add(new RecordData
                    {
                        Image = image.ResizeImage(image.Width / 2, image.Height / 2),
                        Time = DateTime.Now,
                    });

                    while (true)
                    {
                        var first = _data[0];
                        if (DateTime.Now.Subtract(first.Time) > TimeSpan.FromMinutes(20))
                        {
                            _data.Remove(first);
                        }
                        else
                        {
                            break;
                        }
                    }

                    Thread.Sleep(interval);
                }
            })).Start();
        }

        public void Stop()
        {
            _stop = true;
            OnStop?.Invoke(this, _data);
        }

        public void Run(PictureBox picturebox)
        {
            if (!_stop)
                return;

            new Thread(new ThreadStart(() =>
            {
                var list = _data
                    .Select((x, i) => new
                    {
                        Data = x,
                        Index = i,
                    })
                    .ToList();
                foreach (var item in list)
                {
                    if (item.Data == _data.Last())
                        return;

                    var curr = item.Data;
                    var next = _data[item.Index + 1];

                    picturebox.Image = curr.Image;

                    Thread.Sleep(next.Time.Subtract(curr.Time));
                }
            })).Start();
        }
    }
}
