using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Calls.Background;

namespace JustFixIt.Model
{ // Mike
    class Order
    {
        public Order()
        {

        }
        public Order(List<WorkTask> order)
        {
            WorkTasks = new List<WorkTask>(order);
        }
        public List<WorkTask> WorkTasks { get; set; }

        public override string ToString()
        {
            string returnString = "";
            foreach (WorkTask workTask in WorkTasks)
            {

                returnString += workTask.ToString();
                returnString += "\n";

            }
            return returnString;
        }
    }
}
