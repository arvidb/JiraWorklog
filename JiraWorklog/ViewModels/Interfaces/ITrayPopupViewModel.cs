using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using JiraWorklog.Models;

namespace JiraWorklog.ViewModels
{
    public interface ITrayPopupViewModel
    {
        ObservableCollection<JiraIssue> Issues { get; }
        ICommand ItemClickCommand { get; }
        ICommand ReloadCommand { get; }

        event EventHandler TrayItemExecuted;
    }
}