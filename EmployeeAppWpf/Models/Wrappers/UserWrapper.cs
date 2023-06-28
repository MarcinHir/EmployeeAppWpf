using EmployeeAppWpf.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeAppWpf.Models.Wrappers
{
    public class UserWrapper : IDataErrorInfo
    {
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public bool Authorization
        {
            get
            {
                return Settings.Default.Authorization;
            }
            set
            {
                Settings.Default.Authorization = value;
            }
        }

        public string UserPassword { get; set; }
        public string Error { get; set; }
        //public int AccesLevelId { get; set; }

        private bool _isLoginValid;
        private bool _isPasswordValid;

        public string this[string columnName] 
        {
            get
            {
                switch (columnName)
                {
                    case nameof(UserLogin):
                        if (string.IsNullOrWhiteSpace(UserLogin))
                        {
                            Error = "Pole Login jest wymagane.";
                            _isLoginValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLoginValid = true;
                        }
                        break;
                    case nameof(UserPassword):
                        if (string.IsNullOrWhiteSpace(UserPassword))
                        {
                            Error = "Pole Hasło jest wymagane.";
                            _isPasswordValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPasswordValid = true;
                        }
                        break;                    
                    default:
                        break;
                }

                return Error;
            }
        }
        public bool IsValid
        {
            get
            {
                return _isLoginValid && _isPasswordValid;
            }
        }
        public void Save()
        {
            Settings.Default.Save();
        }


    }
}
