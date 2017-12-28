using Hardcodet.Wpf.TaskbarNotification;
using JiraWorklog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JiraWorklog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TrayPopupControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is TrayPopupViewModel vm)
            {
                vm.TrayItemExecuted +=
                    (s, ee) => tbIcon.TrayPopupResolved.IsOpen = false;
            }
        }

        private void tbIcon_TrayPopupOpen(object sender, RoutedEventArgs e)
        {
            if (sender is TaskbarIcon tbIcon &&
                tbIcon.TrayPopup is FrameworkElement fe &&
                fe.DataContext is TrayPopupViewModel vm)
            {
                if (vm.ReloadCommand.CanExecute(null))
                {
                    vm.ReloadCommand.Execute(null);
                }
            }
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}
