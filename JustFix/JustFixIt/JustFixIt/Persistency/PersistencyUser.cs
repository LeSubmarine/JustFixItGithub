using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using JustFixIt.Model;
using Newtonsoft.Json;

namespace JustFixIt.Persistency
{
    class PersistencyUser
    {
        private static string jsonAdminsFileName = "Admins.json";
        private static string jsonCustomersFileName = "Customers.json";
        private static string jsonMechanicsFileName = "Mechanics.json";

        public static void SaveUsersAsJson(List<User> Users)
        {
            //[0] = admin, [1] = customer, [2] = mechanic
            List<User>[] SortedUsers = { new List<User>(), new List<User>(), new List<User>() };
            foreach (User user in Users)
            {
                SortedUsers[(int)user.PersonType].Add(user);
            }

            //Admin saving
            string adminsJsonString = JsonConvert.SerializeObject(SortedUsers[0]);
            SerializeNotesFileAsync(adminsJsonString, jsonAdminsFileName);
            //Customer saving
            string customersJsonString = JsonConvert.SerializeObject(SortedUsers[1]);
            SerializeNotesFileAsync(customersJsonString, jsonCustomersFileName);
            //Mechanic saving
            string mechanicsJsonString = JsonConvert.SerializeObject(SortedUsers[2]);
            SerializeNotesFileAsync(mechanicsJsonString, jsonMechanicsFileName);
        }


        public static async Task<List<AdminUser>> LoadAdminsFromJsonAsync()
        {
            string notesJsonString = await DeserializeNotesFileAsync(jsonAdminsFileName);
            if (notesJsonString != null)
                return (List<AdminUser>)JsonConvert.DeserializeObject(notesJsonString, typeof(List<AdminUser>));
            return null;
        }
        public static async Task<List<CustomerUser>> LoadCustomersFromJsonAsync()
        {
            string notesJsonString = await DeserializeNotesFileAsync(jsonCustomersFileName);
            if (notesJsonString != null)
                return (List<CustomerUser>)JsonConvert.DeserializeObject(notesJsonString, typeof(List<CustomerUser>));
            return null;
        }
        public static async Task<List<MechanicUser>> LoadMechanicsFromJsonAsync()
        {
            string notesJsonString = await DeserializeNotesFileAsync(jsonMechanicsFileName);
            if (notesJsonString != null)
                return (List<MechanicUser>)JsonConvert.DeserializeObject(notesJsonString, typeof(List<MechanicUser>));
            return null;
        }



        private static async void SerializeNotesFileAsync(string notesJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, notesJsonString);
        }


        private static async Task<string> DeserializeNotesFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch
            {
                return null;
            }
        }
    }
}
