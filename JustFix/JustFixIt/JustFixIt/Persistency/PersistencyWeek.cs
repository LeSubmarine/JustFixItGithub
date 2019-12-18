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
    class PersistencyWeek
    {
        private const string JsonWeekFileName = "Week.json";

        public static void SaveWeekAsJson(List<Week> weeks)
        {
            string adminsJsonString = JsonConvert.SerializeObject(weeks);
            SerializeNotesFileAsync(adminsJsonString, JsonWeekFileName);
        }

        public static async Task<List<Week>> LoadWeeksFromJsonAsync()
        {
            string weekJsonString = await DeserializeNotesFileAsync(JsonWeekFileName);
            List<Week> LoadedUsers = new List<Week>();
            foreach (Week week in (List<Week>)JsonConvert.DeserializeObject(weekJsonString, typeof(List<Week>)))
            {
                LoadedUsers.Add(week);
            }

            return LoadedUsers;
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