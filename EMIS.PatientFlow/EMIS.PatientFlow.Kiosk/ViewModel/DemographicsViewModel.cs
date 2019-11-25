using EMIS.PatientFlow.Kiosk.Enum;
using EMIS.PatientFlow.Kiosk.Helper;
using EMIS.PatientFlow.Kiosk.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace EMIS.PatientFlow.Kiosk.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    ///  
    /// </para>
    /// </summary>
    public class DemographicsViewModel : ViewModelBase
    {
        DispatcherTimer timer; 
          
        /// <summary>
        /// Initializes a new instance of the DemographicsViewModel class.
        /// </summary>
        public DemographicsViewModel()
        {
            InitializeControls();
        }
         
        private string _demographicsTitle;

        public string DemographicsTitle
        {
            get { return _demographicsTitle; }
            set { _demographicsTitle = value; RaisePropertyChanged("DemographicsTitle"); }
        }

        private string _demographicsInstruction;

        public string DemographicsInstruction
        {
            get { return _demographicsInstruction; }
            set { _demographicsInstruction = value; RaisePropertyChanged("DemographicsInstruction"); }
        }

        private string _nextButtonText;

        public string NextButtonText
        {
            get { return _nextButtonText; }
            set { _nextButtonText = value; RaisePropertyChanged("NextButtonText"); }
        }

        private AppPages _homePageID;

        public AppPages HomePageID
        {
            get { return _homePageID; }
            set { _homePageID = value; RaisePropertyChanged("HomePageID"); }
        }

        private AppPages _backPageID;

        public AppPages BackPageID
        {
            get { return _backPageID; }
            set { _backPageID = value; RaisePropertyChanged("BackPageID"); }
        }

        private List<DemographicDisplay> _demographicsList;
        public List<DemographicDisplay> DemographicsList
        {
            get
            {
                return _demographicsList;
            }
            set
            {
                _demographicsList = value;
                RaisePropertyChanged("DemographicsList");
            }
        }

        private List<DemographicDisplay> _demographicsUpdateList;
        public List<DemographicDisplay> DemographicsUpdateList
        {
            get
            {
                return _demographicsUpdateList;
            }
            set
            {
                _demographicsUpdateList = value;
                RaisePropertyChanged("DemographicsUpdateList");
            }
        }

        private RelayCommand<int> _yesCommand;

        public RelayCommand<int> YesCommand
        {
            get
            {
                return _yesCommand
                    ?? (_yesCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              foreach (var item in DemographicsList)
                                              {
                                                  item.ButtonNoImagePath = "/Assets/Images/check_bg.png";
                                                  item.ButtonYesImagePath = "/Assets/Images/check_yes.png";
                                                  item.IsChecked = true;
                                                  if (p == item.DemographicID)
                                                  {
                                                      item.ButtonNoImagePath = "/Assets/Images/check_bg.png";
                                                      item.ButtonYesImagePath = "/Assets/Images/check_yes.png";
                                                      item.IsChecked = true;
                                                  }
                                              }
                                              Messenger.Default.Send(AppPages.Demographics);
                                          }));
            }
        }

        private RelayCommand<int> _noCommand;

        public RelayCommand<int> NoCommand
        {
            get
            {
                return _noCommand
                    ?? (_noCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              foreach (var item in DemographicsList)
                                              {
                                                  if (p == item.DemographicID)
                                                  {
                                                      item.ButtonYesImagePath = "/Assets/Images/check_bg.png";
                                                      item.ButtonNoImagePath = "/Assets/Images/check_no.png";
                                                      item.IsChecked = false;
                                                  }
                                              }

                                              Messenger.Default.Send(AppPages.Demographics);
                                          }));
            }
        }


        private RelayCommand<int> _backCommand;

        public RelayCommand<int> BackCommand
        {
            get
            {
                return _backCommand
                    ?? (_backCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              Messenger.Default.Send(p);
                                          }));
            }
        }

        private RelayCommand<int> _restartCommand;

        public RelayCommand<int> RestartCommand
        {
            get
            {
                return _restartCommand
                    ?? (_restartCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              Messenger.Default.Send(p);
                                          }));
            }
        }

        private RelayCommand<int> _nextCommand;

        public RelayCommand<int> NextCommand
        {
            get
            {
                return _nextCommand
                    ?? (_nextCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              List<DemographicDisplay> demographicsUpdateList = new List<DemographicDisplay>();

                                              foreach (var item in DemographicsList)
                                              {
                                                  if (item.IsChecked == false)
                                                  {
                                                      demographicsUpdateList.Add(item);
                                                  }
                                              }

                                              if (demographicsUpdateList.Count > 0)
                                              {
                                                  DemographicsUpdateList = demographicsUpdateList;
                                                  PopupVisibility = Visibility.Visible;
                                              }
                                              else
                                              {
                                                  Messenger.Default.Send(AppPages.SurveyQuestions);
                                              }

                                          }));
            }
        }

        private RelayCommand<int> _saveCommand;

        public RelayCommand<int> SaveCommand
        {
            get
            {
                return _saveCommand
                    ?? (_saveCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              List<DemographicDisplay> demographicsUpdateList = new List<DemographicDisplay>();
                                              demographicsUpdateList = DemographicsUpdateList;

                                              foreach (var demographicUpdateItem in demographicsUpdateList)
                                              {
                                                  foreach (var demographicItem in GlobalVariables.DemographicsList)
                                                  {
                                                      if (demographicUpdateItem.DemographicID == demographicItem.DemographicID)
                                                      {
                                                          demographicItem.DemographicValue = demographicUpdateItem.DemographicValue;
                                                      }
                                                  }
                                              }

                                              var temp = GlobalVariables.DemographicsList;

                                              FillDemographics();
                                              SavedVisibility = Visibility.Visible;
                                              timer = new DispatcherTimer();
                                              timer.Interval = TimeSpan.FromSeconds(1);
                                              timer.Tick += timer_Tick;
                                              timer.Start();
                                          }));
            }
        }

        private RelayCommand<int> _cancelCommand;

        public RelayCommand<int> CancelCommand
        {
            get
            {
                return _cancelCommand
                    ?? (_cancelCommand = new RelayCommand<int>(
                                          p =>
                                          {
                                              PopupVisibility = Visibility.Collapsed;
                                          }));
            }
        }

        private Visibility _popupVisibility;
        public Visibility PopupVisibility
        {
            get
            {
                return _popupVisibility;
            }
            set
            {
                _popupVisibility = value;
                RaisePropertyChanged("PopupVisibility");
            }
        }

        private Visibility _savedVisibility;
        public Visibility SavedVisibility
        {
            get
            {
                return _savedVisibility;
            }
            set
            {
                _savedVisibility = value;
                RaisePropertyChanged("SavedVisibility");
            }
        }
         
        /// <summary>
        /// Method to initialize controls of UI
        /// </summary>
        private void InitializeControls()
        {
            HomePageID = BackPageID = AppPages.HomePage;
            DemographicsTitle = Constants.DemographicsTitle;
            DemographicsInstruction = Constants.DemographicsInstruction;
            NextButtonText = Constants.DemographicsNextText;

            PopupVisibility = Visibility.Collapsed;
            SavedVisibility = Visibility.Collapsed;

            FillDemographics();
        }
        /// <summary>
        /// Method to fill Demographic details
        /// </summary>
        private void FillDemographics()
        {
            List<DemographicDisplay> demographicDisplayList = new List<DemographicDisplay>();

            foreach (var demographicItem in GlobalVariables.DemographicsList)
            {
                DemographicDisplay demographicDisplay = new DemographicDisplay();

                demographicDisplay.DemographicID = demographicItem.DemographicID;
                demographicDisplay.DemographicDetail = demographicItem.DemographicDetail;

                demographicDisplay.DemographicValue = demographicItem.DemographicValue;

                if (Regex.IsMatch(demographicItem.DemographicValue, "^[0-9]+$", RegexOptions.Compiled))
                {
                    demographicDisplay.DemographicDisplayValue = new String('X', demographicItem.DemographicValue.Length - 3)
                  + demographicItem.DemographicValue.Substring(demographicItem.DemographicValue.Length - 3);

                }
                else
                {
                    demographicDisplay.DemographicDisplayValue = demographicItem.DemographicValue;
                }

                demographicDisplay.IsChecked = demographicItem.IsChecked;

                if (demographicItem.IsChecked)
                {
                    demographicDisplay.IsChecked = demographicItem.IsChecked;
                    demographicDisplay.ButtonYesImagePath = "/Assets/Images/check_yes.png";
                    demographicDisplay.ButtonNoImagePath = "/Assets/Images/check_bg.png";
                }
                else
                {
                    demographicDisplay.IsChecked = demographicItem.IsChecked;
                    demographicDisplay.ButtonYesImagePath = "/Assets/Images/check_bg.png";
                    demographicDisplay.ButtonNoImagePath = "/Assets/Images/check_no.png";
                }

                demographicDisplay.YesText = "Yes";
                demographicDisplay.NoText = "No";

                demographicDisplayList.Add(demographicDisplay);


            }
            DemographicsList = demographicDisplayList;
        }

        /// <summary>
        /// Event to stop the timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Tick(object sender, EventArgs e)
        {
            SavedVisibility = Visibility.Collapsed;
            PopupVisibility = Visibility.Collapsed;
            if (timer != null)
            {
                timer.Stop();
            }

        }
         
    }
}