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
        private int _selectedWorkTaskFromOrder;

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

        public int SelectedWorkTaskFromOrder
        {
            get => _selectedWorkTaskFromOrder;
            set { _selectedWorkTaskFromOrder = value; OnPropertyChanged();}
        }

        public ICommand RemoveCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ObservableCollection<WorkTask> WorkTasksTypes { get; set; }

        public int SelectedWorkTask
        {
            get => _selectedWorkTask;
            set { _selectedWorkTask = value; OnPropertyChanged(); }
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
                SelectedOrder = (Week.WeekTable.Days[SelectedDay].Orders.Count > 0 ? 0 : -1);
                SelectedWorkTaskFromOrder = (SelectedOrder == 0 ? Week.WeekTable.Days[SelectedDay].Orders[SelectedOrder].WorkTasks.Count > 0 ? 0 : -1 : -1);
                WeekDay = new ObservableCollection<Order>(Week.WeekTable.Days[SelectedDay].Orders);
            }
        }

        public int SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                SelectedWorkTaskFromOrder = (SelectedOrder == 0 ? Week.WeekTable.Days[SelectedDay].Orders[SelectedOrder].WorkTasks.Count > 0 ? 0 : -1 : -1);
                if (SelectedOrder >= 0)
                {
                    TheOrder = new ObservableCollection<WorkTask>(Week.WeekTable.Days[SelectedDay].Orders[SelectedOrder].WorkTasks);
                }
                else
                {
                    TheOrder = new ObservableCollection<WorkTask>();
                }
                OnPropertyChanged();
            }
        }
    

        #endregion


        #region Methods
        public void Add()
        {
            if (TheOrder != null)
            {
                if (SelectedOrder < Week.WeekTable.Days[SelectedDay].Orders.Count && SelectedOrder >= 0)
                {
                    int tempSelectedOrder = SelectedOrder;
                    TheOrder.Add(WorkTask.WorkTasks[SelectedWorkTask]);
                    Week.WeekTable.Days[SelectedDay].Orders[SelectedOrder].WorkTasks.Add(WorkTask.WorkTasks[SelectedWorkTask]);
                    for (int i = 0; i < WeekDay.Count; i++)
                    {
                        WeekDay[i] = Week.WeekTable.Days[SelectedDay].Orders[i];
                    }
                    SelectedOrder = tempSelectedOrder;
                }
            }
        }

        public void Remove()
        {
            if (TheOrder != null)
            {
                if (SelectedOrder < Week.WeekTable.Days[SelectedDay].Orders.Count)
                {
                    if (SelectedWorkTaskFromOrder >= 0 && SelectedWorkTaskFromOrder < TheOrder.Count)
                    {
                        int tempSelectedOrder = SelectedOrder;
                        Week.WeekTable.Days[SelectedDay].Orders[SelectedOrder].WorkTasks.RemoveAt(SelectedWorkTaskFromOrder);
                        TheOrder.RemoveAt(SelectedWorkTaskFromOrder);
                        for (int i = 0; i < WeekDay.Count; i++)
                        {
                            WeekDay[i] = Week.WeekTable.Days[SelectedDay].Orders[i];
                        }
                        SelectedOrder = tempSelectedOrder;
                    }
                }
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
                    SelectedDay = SelectedDay;
                }
            }
            MainViewModel.SaveWeek();
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
