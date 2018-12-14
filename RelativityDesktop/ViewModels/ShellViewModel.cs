using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelativityDesktop.ViewModels
{
    class ShellViewModel : Conductor<Object>
    {
        public bool IsAuthenticated { get; set; }

        public ShellViewModel()
        {
            IsAuthenticated = true;
            StartUp();
        }


        private void StartUp()
        {
            if (IsAuthenticated)
            {
                ActivateItem(new WorkspaceViewModel());
            }
        }

    }
}
