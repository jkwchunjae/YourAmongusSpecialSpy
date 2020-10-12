using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YourAmongusSpecialSpy.Mission;

namespace YourAmongusSpecialSpy
{
    public class MissionManager
    {
        private readonly List<IMission> _missions = new List<IMission>();
        private bool _stop = false;
        private List<string> _enableMissions = new List<string>();

        public event EventHandler<(IMission Mission, string Message)> OnEvent;
        public IEnumerable<IMission> Missions => _missions;

        public MissionManager()
        {
            _missions.Add(new FixWiringMission());
            _missions.Add(new UnlockManifoldsMission());
            _missions.Add(new SwipeCardMission());
            _missions.Add(new UploadDownloadDataMission());
            _missions.Add(new StartReactorMission());
            _missions.Add(new CalibrateDistributorMission());
            _missions.Add(new CleanO2FilterMission());
            _missions.Add(new ClearAsteroidsMission());
            _missions.Add(new PrimeShieldsMission());
            _missions.Add(new DivertPowerMission());
            _missions.Add(new AcceptDivertedPowerMission());
            _missions.Add(new EmptyGarbageMission());
            _missions.Add(new StabilizeSteeringMission());
            _missions.Add(new ChartCourseMission());
            _missions.Add(new AlignEngineOutputMission());
            _missions.Add(new FuelEnginesMission());
        }

        private List<IMission> GetEnableMissions()
        {
            lock (this)
            {
                return _missions
                    .Where(x => _enableMissions.Contains(x.GetName()))
                    .ToList();
            }
        }

        public void Run(TimeSpan interval)
        {
            new Thread(new ThreadStart(() =>
            {
                _stop = false;
                while (_stop == false)
                {
                    var image = Amongus.GetImage();

                    if (image.Width != 1376 || image.Height != 807)
                    {
                        OnEvent?.Invoke(this, (null, "해상도를 맞추세요: 1360 * 768"));
                        Thread.Sleep(interval);
                        continue;
                    }

                    var findMissions = GetEnableMissions()
                        .Where(x =>
                        {
                            try { return x.IsMyMission(image); }
                            catch { return false; }
                        }).ToList();

                    if (findMissions.Count == 1)
                    {
                        var mission = findMissions.First();

                        OnEvent?.Invoke(this, (mission, string.Empty));
                        mission.StartMission(image);
                    }
                    else if (findMissions.Count > 1)
                    {

                    }
                    else
                    {
                        OnEvent?.Invoke(this, (null, "Play"));
                    }

                    Thread.Sleep(interval);
                }
            })).Start();
        }

        public void Stop()
        {
            _stop = true;
        }

        public void UpdateEnableMission(List<string> missions)
        {
            lock (this)
            {
                _enableMissions = missions;
            }
        }
    }
}
