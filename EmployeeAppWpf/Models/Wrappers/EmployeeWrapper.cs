using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmployeeAppWpf.Models.Wrappers
{
    public class EmployeeWrapper : IDataErrorInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public double Salary { get; set; }
        public DateTime StartWorkingDate { get; set; }
        public DateTime? EndWorkingDate { get; set; }
        public bool FireOrNot { get; set; }

        private bool _isFirstNameValid;
        private bool _isLastNameValid;
        private bool _isSalaryValid;
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Pole Imię jest wymagane.";
                            _isFirstNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isFirstNameValid = true;
                        }
                        break;
                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Pole Nazwisko jest wymagane.";
                            _isLastNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLastNameValid = true;
                        }
                        break;
                    case nameof(Salary):
                        if (double.IsNaN(Salary))
                        {
                            Error = "Pole Zarobki jest wymagane.";
                            _isSalaryValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isSalaryValid = true;
                        }
                        break;
                    default:
                        break;
                }

                return Error;
            }
        }
        public string Error { get; set; }
        public bool IsValid
        {
            get
            {
                return _isFirstNameValid && _isLastNameValid && _isSalaryValid;
            }
        }
    }
}
