using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourAmongusSpecialSpy.Mission
{
    public interface IMission
    {
        string GetName();
        bool IsMyMission(Bitmap image);
        void StartMission(Bitmap image);
    }
}
