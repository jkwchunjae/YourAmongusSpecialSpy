using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy
{
    public static class Config
    {
        private static string _enabledMissionPath = "amongus-mission.txt";
        public static List<string> GetCheckedMissions()
        {
            if (File.Exists(_enabledMissionPath))
            {
                return File.ReadAllLines(_enabledMissionPath, new UTF8Encoding(false))
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        public static List<string> RemoveCheckedMissions(string missionName)
        {
            var list = GetCheckedMissions().Where(x => x != missionName).ToList();
            SaveCheckedMissions(list);
            return list;
        }

        public static List<string> AddCheckedMissions(string missionName)
        {
            var list = GetCheckedMissions();
            if (list.Contains(missionName))
                return list;

            list.Add(missionName);
            SaveCheckedMissions(list);
            return list;
        }

        private static void SaveCheckedMissions(List<string> missions)
        {
            File.WriteAllLines(_enabledMissionPath, missions, new UTF8Encoding(false));
        }
    }
}
