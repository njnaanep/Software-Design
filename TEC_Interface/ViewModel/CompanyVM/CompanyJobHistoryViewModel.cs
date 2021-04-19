using System;
using DataLayer.EfClasses;

namespace TEC_Interface.ViewModel.Company
{
    public class CompanyJobHistoryViewModel
    {
        private string _workedTo;
        public string HistoryId { get; set; }
        public string CandidateName { get; set; }
        public string WorkedFrom { get; set; }

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
            }
        }

        public double WorkedHours { get; set; }

        public CompanyJobHistoryViewModel(JobHistory history)
        {
            HistoryId = history.HistoryId;
            CandidateName = history.CandidateLink.CandidateFullName;
            WorkedFrom = history.WorkedFrom.ToShortDateString();
            WorkedTo = history.WorkedTo.HasValue ? history.WorkedTo.Value.ToShortDateString() : "Present";
            WorkedHours = history.WorkedHours ?? double.NaN;
        }
    }
}