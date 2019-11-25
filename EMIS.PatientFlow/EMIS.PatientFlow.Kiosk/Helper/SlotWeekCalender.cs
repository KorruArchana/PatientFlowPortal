using System;
using System.ComponentModel;
using System.Globalization;

namespace EMIS.PatientFlow.Kiosk.Helper
{
	public class SlotWeekCalender
	{
        public SlotWeekCalender()
		{
			Monday = new DaySlotMapping();
			Tuesday = new DaySlotMapping();
			Wednesday = new DaySlotMapping();
			Thursday = new DaySlotMapping();
			Friday = new DaySlotMapping();
			Saturday = new DaySlotMapping();
			Sunday = new DaySlotMapping();
		}

		public DaySlotMapping Monday { get; set; }

		public DaySlotMapping Tuesday { get; set; }

		public DaySlotMapping Wednesday { get; set; }

		public DaySlotMapping Thursday { get; set; }

		public DaySlotMapping Friday { get; set; }

		public DaySlotMapping Saturday { get; set; }

		public DaySlotMapping Sunday { get; set; }
        
		public void SetDateRow(DateTime date, int slot)
		{
			var day = date.DayOfWeek;
			switch (day)
			{
				case DayOfWeek.Monday:
					Monday = new DaySlotMapping(date, slot);
                    if(Monday.Date < DateTime.Today.Date)
                    {
                        Monday.SlotCount = 0;
                    }
					break;
				case DayOfWeek.Tuesday:
					Tuesday = new DaySlotMapping(date, slot);
                    if (Tuesday.Date < DateTime.Today.Date)
                    {
                        Tuesday.SlotCount = 0;
                    }
                    break;
				case DayOfWeek.Wednesday:
					Wednesday = new DaySlotMapping(date, slot);
                    if (Wednesday.Date < DateTime.Today.Date)
                    {
                        Wednesday.SlotCount = 0;
                    }
                    break;
				case DayOfWeek.Thursday:
					Thursday = new DaySlotMapping(date, slot);
                    if (Thursday.Date < DateTime.Today.Date)
                    {
                        Thursday.SlotCount = 0;
                    }
                    break;
				case DayOfWeek.Friday:
					Friday = new DaySlotMapping(date, slot);
                    if (Friday.Date < DateTime.Today.Date)
                    {
                        Friday.SlotCount = 0;
                    }
                    break;
				case DayOfWeek.Saturday:
					Saturday = new DaySlotMapping(date, slot);
                    if (Saturday.Date < DateTime.Today.Date)
                    {
                        Saturday.SlotCount = 0;
                    }
                    break;
				case DayOfWeek.Sunday:
					Sunday = new DaySlotMapping(date, slot);
                    if (Sunday.Date < DateTime.Today.Date)
                    {
                        Sunday.SlotCount = 0;
                    }
                    break;
			}
		}

        public void SetIsSelected(DateTime date)
        {
            ResetIsSelected();

            var day = date.DayOfWeek;
            switch (day)
            {
                case DayOfWeek.Monday:
                    Monday.IsSelected = true;
                    break;
                case DayOfWeek.Tuesday:
                    Tuesday.IsSelected = true;
                    break;
                case DayOfWeek.Wednesday:
                    Wednesday.IsSelected = true;
                    break;
                case DayOfWeek.Thursday:
                    Thursday.IsSelected = true;
                    break;
                case DayOfWeek.Friday:
                    Friday.IsSelected = true;
                    break;
                case DayOfWeek.Saturday:
                    Saturday.IsSelected = true;
                    break;
                case DayOfWeek.Sunday:
                    Sunday.IsSelected = true;
                    break;
            }
        }

        private void ResetIsSelected()
        {
            Monday.IsSelected = Tuesday.IsSelected = Wednesday.IsSelected =
                Thursday.IsSelected = Friday.IsSelected = Saturday.IsSelected = Sunday.IsSelected = false;
        }
    }

	public class DaySlotMapping : INotifyPropertyChanged
	{

		private DateTime _date;
		private bool _isSelected;
		private int _slotCount;


		public DaySlotMapping()
		{
			Date = new DateTime(2000, 1, 1);
			Day = 0;
            Month = string.Empty;
			SlotCount = 0;
		}

		public DaySlotMapping(DateTime date, int slot)
		{
			Date = date;
			Day = date.Day;
            Month = Date.ToString("MMM");
            SlotCount = slot;

		}

		public bool IsToday
        {
            get
            {
                return Date.Date == DateTime.Today.Date;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
                RaisePropertyChanged("Day");
                RaisePropertyChanged("Month");
            }
        }

        public int Day { get; set; }

        public string Month { get; set; }

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        public int SlotCount
        {
            get
            {
                return _slotCount;
            }
            set
            {
                _slotCount = value;
                RaisePropertyChanged("SlotCount");
                RaisePropertyChanged("IsSlotPresent");
            }
        }

		public bool IsSlotPresent
		{
			get
            {
                return SlotCount > 0;
            }
		}

		public bool IsValidDate
		{
			get { return Day > 0; }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}