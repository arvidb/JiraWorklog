using JiraWorklog.Helpers;
using JiraWorklog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JiraWorklog.ViewModels
{
    class MainViewModel : ObservableObject
    {
        public ICommand LogoutCommand { get; }

        public MainViewModel(ICredentialService credentialService)
        {
            LogoutCommand = new RelayCommand(credentialService.ResetCredentials);
        }
    }
}
