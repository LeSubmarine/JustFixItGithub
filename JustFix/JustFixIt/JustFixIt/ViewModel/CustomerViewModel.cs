using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Store;
using JustFixIt.Annotations;
using JustFixIt.Model;

namespace JustFixIt.ViewModel
{
    class CustomerViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WorkTask> _order;

        public CustomerViewModel()
        {
            AddToCalenderCommand = new RelayCommand(AddToCalender);
            ListOfDays = new ObservableCollection<string>(Day.ListOfDays);
            Tasks = new ObservableCollection<WorkTask>(WorkTask.WorkTasks);
            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove);
            Order = new ObservableCollection<WorkTask>();
        }
        public ICommand AddToCalenderCommand { get; set; }
        public int SelectedDay { get; set; }
        public ObservableCollection<string> ListOfDays { get; set; }

        public void AddToCalender()
        {
            if (Week.WeekTable.Days[SelectedDay].TimeAvailable && Order.Count != 0)
            {
                Week.WeekTable.Days[SelectedDay].AddTask(new List<WorkTask>(Order));
                for (int i = 0; i < Order.Count;)
                {
                    Order.RemoveAt(i);
                }
            }
            MainViewModel.SaveWeek();
        }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public int SelectedTask { get; set; }
        public int SelectedTaskInOrder { get; set; }
        public ObservableCollection<WorkTask> Tasks { get; set; }

        public ObservableCollection<WorkTask> Order
        {
            get => _order;
            set
            {
                _order = value; OnPropertyChanged();
            }
        }

        #region PropertyChangeSupport
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion

        public void Remove()
        {
            Order.RemoveAt(SelectedTaskInOrder);
        }
        public void Add()
        {
            Order.Add(WorkTask.WorkTasks[SelectedTask]);
        }

    }
}
