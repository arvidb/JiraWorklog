using Autofac;
using Autofac.Core;
using JiraWorklog.Services;
using JiraWorklog.ViewModels;
using JiraWorklog.ViewModels.Design;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JiraWorklog
{
    public class Bootstrapper
    {
        private static Logger Logger => LogManager.GetCurrentClassLogger();

        private static ILifetimeScope _rootScope;

        private bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());

        /// <summary>
        /// instantiate with external builder
        /// Used by e.g. unit tests
        /// </summary>
        /// <param name="builder"></param>
        public Bootstrapper(ContainerBuilder builder)
        {
            _rootScope?.Dispose();
            _rootScope = builder.Build();
        }

        /// <summary>
        /// Create with default application builder
        /// </summary>
        public Bootstrapper()
        {
            Logger.Debug("Running Bootstrap");

            if (_rootScope == null)
            {
                Logger.Debug("Setting up IOC container");

                var builder = new ContainerBuilder();

                // Services
                builder.RegisterType<JiraService>().As<IIssueTrackerService>().SingleInstance();
                builder.RegisterType<WPFUIService>().As<IUIService>().SingleInstance();
                builder.RegisterType<CredentialService>().As<ICredentialService>().SingleInstance();

                // View models
                builder.RegisterType<MainViewModel>();
                builder.RegisterType<NewWorklogViewModel>();
                builder.RegisterType<LoginViewModel>();

                if (IsInDesignMode)
                {
                    builder.RegisterType<DesignTrayViewModel>().As<ITrayPopupViewModel>();
                }
                else
                {
                    builder.RegisterType<TrayPopupViewModel>().As<ITrayPopupViewModel>();
                }

                _rootScope = builder.Build();
            }
            else
            {
                Logger.Error("Bootrapper already initialized");
            }
        }

        public static T Resolve<T>()
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>();
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(parameters);
        }
    }
}
