using System.Collections.ObjectModel;
using System.Linq;
using ServiceLayer;

namespace TEC_Interface.ViewModel.Company
{
    public class JobHistoryListViewModel
    {
        private JobHistoryService _jobHistoryService;
        private string _searchText;

        public JobHistoryListViewModel(JobHistoryService jobHistoryService)
        {
            _jobHistoryService = jobHistoryService;

            JobHistoryList = new ObservableCollection<JobHistoryViewModel>(
                JobHistoryService.GetJobHistories()
                    .Select(c => new JobHistoryViewModel(c)));
        }


        public ObservableCollection<JobHistoryViewModel> JobHistoryList { get; set; }

        public void SearchJobHistory(string searchString)
        {
            JobHistoryList.Clear();

            var histories = _jobHistoryService.SearchJobHistories(searchString);

            foreach (var history in histories)
            {
                var historyModel = new JobHistoryViewModel(history);

                JobHistoryList.Add(historyModel);
            }
        }

        public JobHistoryViewModel SelectedJobHistory { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchJobHistory(_searchText);
            }
        }


    }
}