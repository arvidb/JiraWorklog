using JiraWorklog.Common;
using JiraWorklog.Helpers;
using JiraWorklog.Models;
using JiraWorklog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JiraWorklog.Events;

namespace JiraWorklog.ViewModels
{
    public class NewWorklogViewModel : BaseDialogViewModel
    {
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ICommand SetHoursCommand { get; }

        private readonly IIssueTrackerService _issueTrackerService;
        
        public JiraWorklogEntry NewEntry { get; set; } = new JiraWorklogEntry() {
            Started = DateTime.Now,
            TimeSpentSeconds = 8*3600
        };

        public NewWorklogViewModel(IIssueTrackerService issueTrackerService)
        {
            _issueTrackerService = issueTrackerService;

            SaveCommand = new RelayCommand(CloseDialog);
            CancelCommand = new RelayCommand(CancelDialog);

            SetHoursCommand = new RelayCommand<string>(SetHours);
        }

        private void SetHours(string hours)
        {
            if (ulong.TryParse(hours, out ulong result))
            {
                NewEntry.TimeSpentSeconds = result * 3600;
            }
        }
    }
}
