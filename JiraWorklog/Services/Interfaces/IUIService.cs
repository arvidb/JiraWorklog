using System.Collections.Generic;

namespace JiraWorklog.Services
{

    public interface IUIService
    {
        /// <summary>
        /// Sets the busy state to busy and changes the mouse cursor to wait cursor. 
        /// Busy state is automatically restored when application becomes idle
        /// </summary>
        void SetBusyState();

        bool? ShowModalDialog(string title, object datacontext, bool canResize = true);
    }
}
