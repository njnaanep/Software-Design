using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.EfClasses;

namespace TEC_Interface.ViewModel.Candidate
{
    public class CandidateJobHistoryViewModel
    {
        private string _workedTo;
        public string HistoryId { get; set; }
        public string CompanyName { get; set; }
        public string WorkedFrom{ get; set; }

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

        public CandidateJobHistoryViewModel(JobHistory jobHistory)
        {
            CompanyName = jobHistory.CompanyLink.CompanyName;
            WorkedFrom = jobHistory.WorkedFrom.ToShortDateString();
            WorkedTo = jobHistory.WorkedTo.HasValue ? jobHistory.WorkedTo.Value.ToShortDateString() : "Present";
            WorkedHours = jobHistory.WorkedHours ?? double.NaN ;
            HistoryId = jobHistory.HistoryId;
        }
    }
}
