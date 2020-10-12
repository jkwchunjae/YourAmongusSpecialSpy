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
        private List<IMission> _missions = new List<IMission>();
        private bool _stop = false;

        public event EventHandler<IMission> OnFindMission;

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

        public void Run(TimeSpan interval)
        {
            new Thread(new ThreadStart(() =>
            {
                _stop = false;
                while (_stop == false)
                {
                    var image = Amongus.GetImage();

                    var findMissions = _missions
                        .Where(x =>
                        {
                            try { return x.IsMyMission(image); }
                            catch { return false; }
                        }).ToList();

                    if (findMissions.Count == 1)
                    {
                        var mission = findMissions.First();

                        OnFindMission?.Invoke(this, mission);
                        mission.StartMission(image);
                    }
                    else if (findMissions.Count > 1)
                    {

                    }
                    else
                    {
                        OnFindMission?.Invoke(this, null);
                    }

                    Thread.Sleep(interval);
                }
            })).Start();
        }

        public void Stop()
        {
            _stop = true;
        }
    }
}
