using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Candidate
{
    public class AddCandidateViewModel 
    {
        private CandidateService _candidateService;

        public AddCandidateViewModel(CandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        private DateTime _birthDate= DateTime.Now.AddYears(-18); 

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                var age = DateTime.Now.Year - _birthDate.Year; ;

                if (_birthDate.DayOfYear < DateTime.Now.DayOfYear)
                    age = (DateTime.Now.Year - _birthDate.Year) - 1;

                if (age < 18) MessageBox.Show($"Candidate is Underage");
                
                
                _birthDate = value;
            }
        }

        public string Gender { get; set; }
        public string Address { get; set; }
        public string ContactNum { get; set; }
        public string Email { get; set; }

        public List<string> Genders { get; set; } = Enum.GetNames(typeof(GenderEnum)).ToList();

        private enum GenderEnum
        {
            Male,
            Female,
            Gay,
            Lesbian,
            Bisexual
        }

        public CandidateViewModel AssociatedCandidate { get; set; }

        public void Add()
        {
            var candidate = new DataLayer.EfClasses.Candidate
            {
                CandidateFirstName = FirstName,
                CandidateMiddleName= MiddleName,
                CandidateLastName= LastName,
                CandidateGender = Gender,
                CandidateBirthDate = BirthDate,
                CandidateAddress = Address,
                CandidateContactNumber = ContactNum,
                CandidateEmail = Email
            };

            _candidateService.AddCandidate(candidate);


            AssociatedCandidate = new CandidateViewModel(candidate);

        }

    }
}