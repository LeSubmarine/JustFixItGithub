using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustFixIt.Model
{ // Tobias
    class WorkTask
    {
        private static List<WorkTask> _workTasks;

        #region Constructor
        public WorkTask(string name, int price)
        {
            Name = name;
            Price = price;
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public int Price { get; set; }

        public static List<WorkTask> WorkTasks
        {
            get
            {
                return _workTasks;
            }
            set => _workTasks = value;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Price)}: {Price}";
        }

        #endregion
    }
}
