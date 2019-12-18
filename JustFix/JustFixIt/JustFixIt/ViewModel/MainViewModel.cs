using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustFixIt.Model;
using JustFixIt.Persistency;

namespace JustFixIt.ViewModel
{// Henrik
    class MainViewModel
    {
        #region Constructor
        public MainViewModel()
        {
            NavigationPage = typeof(MainPage);
            WorkTask.WorkTasks = new List<WorkTask>();
            WorkTask.WorkTasks.Add(new WorkTask("Oil change", 2500));
            WorkTask.WorkTasks.Add(new WorkTask("Tire change", 800));
            LoadUsersAndWeek();
        }
        #endregion


        #region Properties
        public static List<User> AllUsers { get; set; } = new List<User>();
        public static User ActiveUser { get; set; }
        public static System.Type NavigationPage { get; set; }
        #endregion


        #region Methods

        public static void SaveUsers()
        {
            PersistencyUser.SaveUsersAsJson(AllUsers);
        }

        public static void SaveWeek()
        {
            PersistencyWeek.SaveWeekAsJson(new List<Week>() {Week.WeekTable});
        }

        public static void LoadUsersAndWeek()
        {
            LoadUser();
            LoadWeeks();
        }

        public static async void LoadUser()
        {

            var admins = await PersistencyUser.LoadAdminsFromJsonAsync();
            if (admins == null)
            {
                admins = new List<AdminUser>(){new AdminUser("12","Admin","Admin","Adminguy","12345679","skraadda@forlora.du")};
            }
            foreach (var admin in admins)
            {
                AllUsers.Add(admin);
            }

            var customers = await PersistencyUser.LoadCustomersFromJsonAsync();
            if (customers == null)
            {
                customers = new List<CustomerUser>() {new CustomerUser("13", "Customer", "Customer", "Customerguy", "12345678", "xd@Nahman.lmao")};
            }

            foreach (var customer in customers)
            {
                AllUsers.Add(customer);
            }
            var mechanics = await PersistencyUser.LoadMechanicsFromJsonAsync();
            if (mechanics == null)
            {
                mechanics = new List<MechanicUser>() { new MechanicUser("14", "Customer", "Customer", "Customerguy", "12345678", "xd@Nahman.lmao") };
            }

            foreach (var mechanic in mechanics)
            {
                AllUsers.Add(mechanic);
            }
        }

        public static async void LoadWeeks()
        {
            var weeks = await PersistencyWeek.LoadWeeksFromJsonAsync();
            
            if (weeks == null)
            {
                Week.WeekTable = new Week();
            }
            else
            {
                foreach (var week in weeks)
                {
                    Week.WeekTable = week;
                }
            }
        }
        #endregion
    }
}
