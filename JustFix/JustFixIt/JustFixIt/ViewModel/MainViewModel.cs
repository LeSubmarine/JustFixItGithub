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
            //Week.WeekTable = new Week();
            //MainViewModel.AllUsers.Add(new AdminUser("13", "Admin", "Admin", "sss", "11122233", "Xd@Lmao.dk"));
            //MainViewModel.AllUsers.Add(new CustomerUser("14", "Customer", "Customer", "ss", "11122234", "Eyyy@yoyo.dk"));
            //MainViewModel.AllUsers.Add(new MechanicUser("15", "Mechanic", "Mechanic", "sds", "11122235", "gfto@IDontNeedNoDocumentationLmao.Ik'LæsMinKodeYo"));
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

            var users = await PersistencyUser.LoadUsersFromJsonAsync();
            foreach (var user in users)
            {
                AllUsers.Add(user);
            }
        }

        public static async void LoadWeeks()
        {
            var weeks = await PersistencyWeek.LoadWeeksFromJsonAsync();

            foreach (var week in weeks)
            {
                Week.WeekTable = week;
            }
        }
        #endregion
    }
}
