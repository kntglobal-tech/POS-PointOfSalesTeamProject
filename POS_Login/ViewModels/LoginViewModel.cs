﻿using POS_DataLibrary;
using POS_ManagersApp;
using POS_ManagersApp.Views;
using POS_SellersApp;
using POS_SellersApp.Views;
using POS_ViewsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;


namespace POS_PointOfSales.ViewModels
{
    class LoginViewModel:ViewModel
    {
        private Database db;
        private DateTime CurrentTime { get; set; }
        public ActionCommand Login { get; set; }
        public LoginViewModel()
        {
            //TODO handle Exceptions
            db = new Database();
            Login = new ActionCommand(p=>OnLogin(UserName, Password),
                p=>CanLogin);
        }

        private string userName;
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
            SetProperty(ref userName, value);
            }
        }
        private string password;

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Password
        {
            get { return password; }
            set { password = value;
            SetProperty(ref password, value);
            }
        }
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                SetProperty(ref firstName, value);
            }
        }

        public bool CanLogin
        {
            get
            {
                return !String.IsNullOrWhiteSpace(UserName) &&
                        !String.IsNullOrWhiteSpace(Password);
            }
        }
        private void OnLogin(string userN, string pass)
        {
            if (userN != null)
            {
                var user = db.getUserByUserName(userN, pass);
                if (user != null)
                {
                            SellersMainWindow win = new SellersMainWindow();
                            win.Show();
                        }
                    else
                    {
                        MessageBox.Show("Could not authenticate user " + UserName);
                    }
            } else
            {
                MessageBox.Show("You didn't provide an username");

            }
        }

        /*
        public class SleepTrackerViewModel : INotifyPropertyChanged
        {
            private string _currentTime, _currentDate;
            public event PropertyChangedEventHandler PropertyChanged;

            public SleepTrackerViewModel()
            {
                CurrentDateText();
                DispatcherTimerSetup();
            }

            private void DispatcherTimerSetup()
            {
                DispatcherTimer dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                dispatcherTimer.Tick += new EventHandler(CurrentTimeText);
                dispatcherTimer.Start();
            }

            private void CurrentDateText()
            {
                CurrentDate = DateTime.Now.ToString("g");
            }

            private void CurrentTimeText(object sender, EventArgs e)
            {
                CurrentTime = DateTime.Now.ToString("HH:mm");
            }

            public string CurrentTime
            {
                get { return _currentTime; }
                set
                {
                    if (_currentTime != null)
                        _currentTime = value;

                    OnPropertyChanged("CurrentTime");
                }
            }

            public string CurrentDate
            {
                get { return _currentDate; }
                set
                {
                    if (_currentDate != value)
                        _currentDate = value;

                    OnPropertyChanged("CurrentDate");
                }
            }

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }*/
    }
}