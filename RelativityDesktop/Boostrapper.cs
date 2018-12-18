using Caliburn.Micro;
using RelativityDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelativityDesktop
{
    public class Boostrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();

        public Boostrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {


            DisplayRootViewFor<ShellViewModel>();

        }

        protected override void Configure()
        {
            _container.Singleton<IEventAggregator, EventAggregator>();
        }


    }
}
