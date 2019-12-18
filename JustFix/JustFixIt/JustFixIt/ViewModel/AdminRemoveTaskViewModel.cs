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
    class AdminRemoveTaskViewModel :  INotifyPropertyChanged
    {
        #region Instancefield
        private int _selectedDay;
        private ObservableCollection<Order> _weekDay;
        #endregion

        #region Constructor
        public AdminRemoveTaskViewModel()
        {
            ListOfDays = new ObservableCollection<string>(Day.ListOfDays);
            WeekDay = new ObservableCollection<Order>(Week.WeekTable.Days[0].Orders);
            RemoveCommand = new RelayCommand(Remove);
        }
        #endregion

        #region Properties
        public ObservableCollection<string> ListOfDays { get; set; }

        public ObservableCollection<Order> WeekDay
        {
            get => _weekDay;
            set { _weekDay = value; OnPropertyChanged();}
        }

        public int SelectedDay
        {
            get => _selectedDay;
            set { _selectedDay = value;
                WeekDay = new ObservableCollection<Order>(Week.WeekTable.Days[SelectedDay].Orders);
            }
        }

        public int SelectedOrder { get; set; }
        public ICommand RemoveCommand { get; set; }
        #endregion


        #region Methods
        public void Remove()
        {
            if (SelectedOrder >= 0 && SelectedOrder < Week.WeekTable.Days[SelectedDay].Orders.Count)
            {
                Week.WeekTable.Days[SelectedDay].Orders.RemoveAt(SelectedOrder);
                WeekDay.RemoveAt(SelectedOrder);
                MainViewModel.SaveWeek();
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
