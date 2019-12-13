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
        private static Week _weekTable;
        public Week()
        {
            Days = new List<Day>();
            for (int i = 0; i < 5; i++)
            {
                Days.Add(new Day(i + 1));
            }
        }

        public List<Day> Days { get; set; }
        public static Week WeekTable { get; set; }

    }
}
