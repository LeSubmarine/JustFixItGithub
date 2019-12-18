using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;

namespace JustFixIt.Model
{
    class Week
    {
        public Week()
        {
            Days = new Day[5];
            for (int i = 0; i < 5; i++)
            {
                Days[i] = new Day(i + 1);
            }
        }

        public Day[] Days { get; set; }
        public static Week WeekTable { get; set; }

    }
}
