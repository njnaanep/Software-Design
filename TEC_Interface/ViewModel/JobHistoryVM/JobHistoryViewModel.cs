using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using DataLayer.EfClasses;
using TEC_Interface.Annotations;

namespace TEC_Interface.ViewModel.Company
{
    public class JobHistoryViewModel : INotifyPropertyChanged
    {
        private string _historyId;
        private string _candidateName;
        private string _companyName;
        private double _workedHours;
        private string _workedFrom;
        private string? _workedTo;
        private string _candidateNum;
        private string _companyId;

        public string HistoryId
        {
            get => _historyId;
            internal set
            {
                _historyId = value;
                OnPropertyChanged(nameof(HistoryId));
            }
        }

        public string CandidateNum
        {
            get => _candidateNum;
            set
            {
                _candidateNum = value;
                OnPropertyChanged(nameof(CandidateNum));
            }
        }

        public string CompanyId
        {
            get => _companyId;
            set
            {
                _companyId = value;
                OnPropertyChanged(nameof(CompanyId));
            }
        }

        public string CandidateName
        {
            get => _candidateName;
            set
            {
                _candidateName = value;
                OnPropertyChanged(nameof(CandidateName));
            }
        }
        public string CompanyName
        {
            get => _companyName;
            internal set
            {
                _companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }
        public double? WorkedHours
        {
            get => _workedHours;
            set
            {
                if (value != null) _workedHours = (double) value;
                OnPropertyChanged(nameof(WarningException));
            }
        }
        public string WorkedFrom
        {
            get => _workedFrom;
            internal set
            {
                _workedFrom = value;
                OnPropertyChanged(nameof(WorkedFrom));
            }
        }
        public string WorkedTo
        {
            get => _workedTo;

            set
            {
                if (string.IsNullOrEmpty(value))
                    _workedTo = "Present";
                else if (DateTime.TryParse(value, out var result))
                    _workedTo = DateTime.Now < result ? "Present" : value;
                else
                    _workedTo = value;
                        
                OnPropertyChanged(nameof(WorkedTo));
            }
        }


        public JobHistoryViewModel(JobHistory history )
        {
            HistoryId = history.HistoryId;
            CandidateName = history.CandidateLink.CandidateFullName;
            CompanyName = history.CompanyLink.CompanyName;
            WorkedFrom = history.WorkedFrom.ToShortDateString();
            WorkedHours = history.WorkedHours ;

            WorkedTo = history.WorkedTo?.ToShortDateString();

            CompanyId = history.CompanyId;
            CandidateNum = history.CandidateNumber;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      
        }
    }
}