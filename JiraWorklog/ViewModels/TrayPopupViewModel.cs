using JiraWorklog.Helpers;
using JiraWorklog.Models;
using JiraWorklog.Services;
using JiraWorklog.Windows;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace JiraWorklog.ViewModels
{
    public class TrayPopupViewModel : ObservableObject, ITrayPopupViewModel
    {
        private static Logger Logger => LogManager.GetCurrentClassLogger();

        public event EventHandler TrayItemExecuted;
        
        public ICommand ItemClickCommand { get; }
        public ICommand ReloadCommand { get; }

        public ObservableCollection<JiraIssue> Issues { get; private set; } = new ObservableCollection<JiraIssue>();

        private readonly IUIService _uIService;
        private readonly IIssueTrackerService _issueTrackerService;

        public TrayPopupViewModel(IUIService uIService, IIssueTrackerService issueTrackerService)
        {
            _uIService = uIService;
            _issueTrackerService = issueTrackerService;

            ItemClickCommand = new RelayCommand<JiraIssue>(OnItemClicked);
            ReloadCommand = new RelayCommand(Reload);
        }

        private async Task<bool> EnsureLoggedIn()
        {
            var success = await _issueTrackerService.TryConnectAsync();
            if (!success.Successfull)
            {
                var vm = Bootstrapper.Resolve<LoginViewModel>();
                if (_uIService.ShowModalDialog("Login", vm) == true)
                {
                    success = await _issueTrackerService.TryConnectAsync();
                    return success.Successfull;
                }
            }

            return success.Successfull;
        }

        private async void Reload()
        {
            if (await EnsureLoggedIn())
            {
                var result = await _issueTrackerService.GetIssuesInProgress();
                Issues = new ObservableCollection<JiraIssue>(result);
                RaisePropertyChanged(() => Issues);
            }
            else
            {
                Logger.Info("Unable to login");
            }
        }

        private async void OnItemClicked(JiraIssue issue)
        {
            var vm = Bootstrapper.Resolve<NewWorklogViewModel>();
            if (_uIService.ShowModalDialog("New work log", vm) == true)
            {
                await _issueTrackerService.SaveWorklogAsync(vm.NewEntry, issue?.Id);
            }

            TrayItemExecuted?.Invoke(this, new EventArgs());
        }
    }
}
