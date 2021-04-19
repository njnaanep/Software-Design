using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TEC_Interface.ViewModel.Candidate
{
    public class CandidateViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyImpelementation

        private string _candidateNumber;
        private string _name;
        private string _gender;
        private string _address;
        private string _birthdate;
        private string _contactNumber;
        private string _email;
        private string _middleName;
        private string _fullName;
        private string _lastName;
        private int _age;

        public string CandidateNumber
        {
            get => _candidateNumber;
            internal set
            {
                _candidateNumber = value;
                OnPropertyChanged(nameof(CandidateNumber));
            }
        }

        public string FirstName 
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged(nameof(MiddleName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string FullName
        {
            get
            {
                return  _fullName = string.IsNullOrEmpty(MiddleName)?
                    $"{FirstName} {LastName}" :
                    $"{FirstName} {MiddleName[0]}. {LastName}";
            }
            internal set => _fullName = value;
        }

        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }
        public string Birthdate
        {
            get => _birthdate;
            set
            {
                _birthdate = value;
                OnPropertyChanged(nameof(Birthdate));
            }
        }
        public string ContactNumber
        {
            get => _contactNumber;
            set
            {
                _contactNumber = value;
                OnPropertyChanged(nameof(ContactNumber));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        #endregion

        public CandidateViewModel(DataLayer.EfClasses.Candidate candidate)
        {
            CandidateNumber = candidate.CandidateNumber;
            FirstName = candidate.CandidateFirstName;
            MiddleName = candidate.CandidateMiddleName;
            LastName = candidate.CandidateLastName;
            Gender = candidate.CandidateGender;
            Address = candidate.CandidateAddress;
            Birthdate = candidate.CandidateBirthDate.ToShortDateString();
            ContactNumber = candidate.CandidateContactNumber;
            Email = candidate.CandidateEmail;
            Age = candidate.CandidateAge;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}