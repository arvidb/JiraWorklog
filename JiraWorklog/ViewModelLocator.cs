using JiraWorklog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWorklog
{
    internal class ViewModelLocator
    {
        public MainViewModel MainViewModel => Bootstrapper.Resolve<MainViewModel>();
        public TrayPopupViewModel TrayPopupViewModel => Bootstrapper.Resolve<TrayPopupViewModel>();
        public NewWorklogViewModel NewWorklogViewModel => Bootstrapper.Resolve<NewWorklogViewModel>();
    }
}
