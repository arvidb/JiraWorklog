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
    public class NewWorklogViewModel : IDialogResultVMHelper
    {
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ICommand SetHoursCommand { get; }

        private readonly IIssueTrackerService _issueTrackerService;
        
        public event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;
        
        public JiraWorklogEntry NewEntry { get; set; } = new JiraWorklogEntry() {
            Started = DateTime.Now,
            TimeSpentSeconds = 8*3600
        };

        public NewWorklogViewModel(IIssueTrackerService issueTrackerService)
        {
            _issueTrackerService = issueTrackerService;

            SaveCommand = new RelayCommand(() => this.RequestCloseDialog?.Invoke(this, new RequestCloseDialogEventArgs(true)));
            CancelCommand = new RelayCommand(() => this.RequestCloseDialog?.Invoke(this, new RequestCloseDialogEventArgs(false)));

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
