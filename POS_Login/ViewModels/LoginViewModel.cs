﻿using POS_DataLibrary;
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
using System.Runtime.Remoting.Contexts;
using System.ComponentModel;
using System.Windows.Threading;

namespace POS_PointOfSales.ViewModels
{
   public class LoginViewModel:ViewModel
    {
      
        private Database db;

        
        private User user;

        public User User
        {
            get { return user; }
            set { user = value;
            SetProperty(ref user, value);
            }
        }

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
                    User = user;
                    UserName = null;
                    FirstName = null;
                    Messenger.Default.Send(User);

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

        
        
    }
}
