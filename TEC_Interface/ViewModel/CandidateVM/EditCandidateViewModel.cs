using System;
using System.Collections.Generic;
using System.Linq;
using ServiceLayer;
using TEC_Interface.ViewModel.Candidate;

namespace TEC_Interface.ViewModel.CandidateVM
{
    public class EditCandidateViewModel
    {
        private CandidateService _candidateService;

        public EditCandidateViewModel(CandidateViewModel candidateToEdit, CandidateService candidateService)
        {
            CandidateToEdit = candidateToEdit;
            _candidateService = candidateService;

            CandidateNum = candidateToEdit.CandidateNumber;
            CopyEditableFields(candidateToEdit);
        }

        private void CopyEditableFields(CandidateViewModel candidate)
        {
            FirstName= candidate.FirstName;
            MiddleName= candidate.MiddleName;
            LastName = candidate.LastName;
            Gender = candidate.Gender;
            Birthdate = Convert.ToDateTime(candidate.Birthdate);
            Address = candidate.Address;
            ContactNum = candidate.ContactNumber;
            Email = candidate.Email;
        }

        public string CandidateNum { get; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ContactNum { get; set; }

        public List<string> GenderList { get; set; } = Enum.GetNames(typeof(GenderEnum)).ToList();

        private enum GenderEnum
        {
            Male,
            Female,
            Gay,
            Lesbian,
            Bisexual
        }


        public CandidateViewModel CandidateToEdit { get; set; }

        public void Edit()
        {
            var newCandidate = new DataLayer.EfClasses.Candidate
            {
                CandidateNumber = CandidateToEdit.CandidateNumber,
                CandidateFirstName = FirstName,
                CandidateMiddleName = MiddleName,
                CandidateLastName = LastName,
                CandidateGender = Gender,
                CandidateAddress = Address,
                CandidateBirthDate = Birthdate,
                CandidateContactNumber = ContactNum,
                CandidateEmail = Email
            };

            CandidateToEdit.FirstName = FirstName;
            CandidateToEdit.MiddleName = MiddleName;
            CandidateToEdit.LastName= LastName;
            CandidateToEdit.Gender= Gender;
            CandidateToEdit.Birthdate= Birthdate.ToShortDateString();
            CandidateToEdit.Address= Address;
            CandidateToEdit.Email= Email;
            CandidateToEdit.ContactNumber= ContactNum;
            CandidateToEdit.Age = newCandidate.CandidateAge;


            _candidateService.UpdateCandidate(newCandidate);
        }
    }
}