using System;
using DataLayer.EfClasses;
using ServiceLayer;
using static System.DateTime;
using static System.Double;

namespace TEC_Interface.ViewModel.Company
{
    public class EditJobHistoryViewModel
    {
        private JobHistoryService _historyService;

        public string JobHistoryId { get; }
        public string CandidateNum { get; }
        public string CompanyId { get; }

        public EditJobHistoryViewModel(JobHistoryViewModel jobHistoryToEdit, JobHistoryService historyService)
        {
            HistoryToEdit = jobHistoryToEdit;
            _historyService = historyService;

            JobHistoryId = jobHistoryToEdit.HistoryId;
            CandidateNum = jobHistoryToEdit.CandidateNum;
            CompanyId = jobHistoryToEdit.CompanyId;

            CopyFields(HistoryToEdit);
        }

        private void CopyFields(JobHistoryViewModel historyToEdit)
        {
            Candidate = historyToEdit.CandidateName;
            Company = historyToEdit.CompanyName;
            WorkedFrom = DateTime.Parse(historyToEdit.WorkedFrom);
            WorkHours = historyToEdit.WorkedHours;

            if (TryParse(historyToEdit.WorkedTo, out DateTime result)) WorkedTo = result;
        }


        public JobHistoryViewModel HistoryToEdit { get; set; }

        public string Candidate { get; set; }
        public string Company { get; set; }
        public DateTime WorkedFrom { get; set; }
        public DateTime? WorkedTo { get; set; }
        public double? WorkHours { get; set; }

        public void Edit()
        {
            var updatedHistory = new JobHistory
            {
                HistoryId = JobHistoryId,
                CompanyId = CompanyId,
                CandidateNumber = CandidateNum,
                WorkedFrom = WorkedFrom,
                WorkedTo = WorkedTo,
                WorkedHours = WorkHours
            };

            HistoryToEdit.WorkedFrom = WorkedFrom.ToShortDateString();
            HistoryToEdit.WorkedTo = WorkedTo?.ToShortDateString();
            HistoryToEdit.WorkedHours = WorkHours;


            _historyService.UpdateJobHistory(updatedHistory);
        }
    }
}