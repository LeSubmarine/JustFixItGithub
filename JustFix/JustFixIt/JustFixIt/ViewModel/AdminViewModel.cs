using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JustFixIt.Annotations;
using JustFixIt.Model;
using JustFixIt.View;

namespace JustFixIt.ViewModel
{// Jonas
    class AdminViewModel : INotifyPropertyChanged
    {
        #region Instance field
        private ObservableCollection<WorkTask> _order;
        #endregion


        #region Constructor

        public AdminViewModel()
        {
            Actions = new ObservableCollection<string>() {"Edit Tasks","Add Task","Remove Task"};
            AddToCalenderCommand = new RelayCommand(AddToCalender);
            ListOfDays = new ObservableCollection<string>(Day.ListOfDays);
            Tasks = new ObservableCollection<WorkTask>(WorkTask.WorkTasks);
            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove);
            Order = new ObservableCollection<WorkTask>();
        }

        #endregion


        #region Property

        public int SelectedDay { get; set; }
        public ObservableCollection<string> Actions { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public int SelectedTask { get; set; }
        public int SelectedTaskInOrder { get; set; }
        public ObservableCollection<WorkTask> Tasks { get; set; }
        public ICommand AddToCalenderCommand { get; set; }
        public ObservableCollection<string> ListOfDays { get; set; }
        public ObservableCollection<WorkTask> Order
        {
            get => _order;
            set
            {
                _order = value; OnPropertyChanged();
            }
        }
        #endregion


        #region Method
        public void AddToCalender()
        {
            if (Week.WeekTable.Days[SelectedDay].TimeAvailable && Order.Count != 0)
            {
                List<WorkTask> newOrder = new List<WorkTask>();
                foreach (WorkTask workTask in Order)
                {
                    newOrder.Add(workTask);
                }
                Week.WeekTable.Days[SelectedDay].AddTask(newOrder);
                for (int i = 0; i < Order.Count;)
                {
                    Order.RemoveAt(i);
                }
            }
            MainViewModel.SaveWeek();
        }
        public void Remove()
        {
            Order.RemoveAt(SelectedTaskInOrder);
        }
        public void Add()
        {
            Order.Add(WorkTask.WorkTasks[SelectedTask]);
        }
        #endregion

        #region PropertyChangeSupport

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
