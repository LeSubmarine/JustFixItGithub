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
//Henrik
namespace JustFixIt.ViewModel
{
    class AdminEditTaskViewModel : INotifyPropertyChanged
    {
        #region Instancefield

        private int _selectedDay;
        private ObservableCollection<Order> _weekDay;
        private ObservableCollection<WorkTask> _theOrder;
        private int _selectedOrder;
        private int _selectedWorkTask;

        #endregion


        #region Constructor

        public AdminEditTaskViewModel()
        {
            ListOfDays = new ObservableCollection<string>(Day.ListOfDays);
            WeekDay = new ObservableCollection<Order>(Week.WeekTable.Days[0].Orders);
            WorkTasksTypes = new ObservableCollection<WorkTask>(WorkTask.WorkTasks);
            RemoveCommand = new RelayCommand(Remove);
            AddCommand = new RelayCommand(Add);
            SaveCommand = new RelayCommand(Save);
        }

        #endregion


        #region Properties

        public int SelectedWorkTaskFromOrder { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ObservableCollection<WorkTask> WorkTasksTypes { get; set; }

        public int SelectedWorkTask
        {
            get => _selectedWorkTask;
            set => _selectedWorkTask = value;
        }

        public ObservableCollection<WorkTask> TheOrder
        {
            get => _theOrder;
            set
            {
                _theOrder = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> ListOfDays { get; set; }

        public ObservableCollection<Order> WeekDay
        {
            get => _weekDay;
            set
            {
                _weekDay = value;
                OnPropertyChanged();
            }
        }

        public int SelectedDay
        {
            get => _selectedDay;
            set
            {
                _selectedDay = value;
                WeekDay = new ObservableCollection<Order>(Week.WeekTable.Days[SelectedDay].Orders);
            }
        }

        public int SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                if (SelectedOrder >= 0)
                {
                    TheOrder = new ObservableCollection<WorkTask>(Week.WeekTable.Days[SelectedDay].Orders[SelectedOrder].WorkTasks);
                }
            }
        }
    

    #endregion


        #region Methods
        public void Add()
        {
            TheOrder.Add(WorkTask.WorkTasks[SelectedWorkTask]);
            Week.WeekTable.Days[SelectedDay].Orders[SelectedOrder].WorkTasks.Add(WorkTask.WorkTasks[SelectedWorkTask]);
        }

        public void Remove()
        {
            if (SelectedWorkTaskFromOrder>= 0 && SelectedWorkTaskFromOrder < TheOrder.Count) 
            {
                Week.WeekTable.Days[SelectedDay].Orders[SelectedWorkTaskFromOrder].WorkTasks.RemoveAt(SelectedWorkTaskFromOrder);
                TheOrder.RemoveAt(SelectedWorkTaskFromOrder);
            }
        }

        public void Save()
        {
            for (int i = 0; i < Week.WeekTable.Days.Length; i++)
            {
                for (int k = 0; k < Week.WeekTable.Days[i].Orders.Count; k++)
                {
                    if (Week.WeekTable.Days[i].Orders[k].WorkTasks.Count == 0)
                    {
                        Week.WeekTable.Days[i].Orders.RemoveAt(k);
                        k--;
                    }
                }
            }
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
