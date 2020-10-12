using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YourAmongusSpecialSpy
{
    public partial class MissionControllerForm : Form
    {
        MissionManager _missionManager;


        public MissionControllerForm()
        {
            InitializeComponent();

            InitMissionManager();

            InitMissionCheckbox();

            Stop_Click(null, null);
        }

        private void InitMissionManager()
        {
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
        }

        private void InitMissionCheckbox()
        {
            var checkedMissions = Config.GetCheckedMissions();
            _missionManager.UpdateEnableMission(checkedMissions);
            
            _missionManager.Missions
                .OrderBy(x => x.GetName())
                .Select((x, i) => new { Index = i, Mission = x })
                .ToList()
                .ForEach(x =>
                {
                    var checkbox = new CheckBox();
                    checkbox.Text = x.Mission.GetName();
                    checkbox.Size = new Size(200, 20);
                    checkbox.Location = new Point(15, x.Index * 25 + 50);
                    checkbox.Checked = checkedMissions.Contains(x.Mission.GetName());
                    checkbox.CheckedChanged += (s, e) =>
                    {
                        var missions = new List<string>();
                        if (((CheckBox)s).Checked)
                        {
                            missions = Config.AddCheckedMissions(x.Mission.GetName());
                        }
                        else
                        {
                            missions = Config.RemoveCheckedMissions(x.Mission.GetName());
                        }
                        _missionManager.UpdateEnableMission(missions);
                    };
                    this.Controls.Add(checkbox);
                });
        }


        private void MissionControllerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop_Click(null, null);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            Stop.Enabled = true;
            _missionManager.Run(TimeSpan.FromMilliseconds(500));
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Start.Enabled = true;
            Stop.Enabled = false;
            _missionManager.Stop();
        }
    }
}
