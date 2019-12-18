using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
