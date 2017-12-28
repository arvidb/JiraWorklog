using JiraWorklog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JiraWorklog.Events;
using System.Windows.Input;
using JiraWorklog.Helpers;
using JiraWorklog.Services;

namespace JiraWorklog.ViewModels
{
    public class LoginViewModel : ObservableObject, IDialogResultVMHelper
    {
        public event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;

        public ICommand LoginCommand { get; }

        public string Url { get; set; }
        public string Username { get; set; }


        private string _message;
        public string Message
        {
            get => _message;
            private set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        private readonly ICredentialService _credentialService;
        private readonly IIssueTrackerService _issueTrackerService;

        public LoginViewModel(ICredentialService credentialService, IIssueTrackerService issueTrackerService)
        {
            _credentialService = credentialService;
            _issueTrackerService = issueTrackerService;

            Url = _credentialService.GetBaseURL();

            LoginCommand = new RelayCommand<object>(Login);
        }

        private async void Login(object pw)
        {
            var pwBox = pw as System.Windows.Controls.PasswordBox;
            _credentialService.StoreCredentials(Url, Username, pwBox.Password);

            var result = await _issueTrackerService.TryConnectAsync();
            if (result.Successfull)
            {
                this.RequestCloseDialog?.Invoke(this, new RequestCloseDialogEventArgs(true));
            }
            else
            {
                Message = result.Message;
            }
        }
    }
}
