using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelativityDesktop.ViewModels
{
    class AdminViewModel : Conductor<Object>
    {

        public AdminViewModel()
        {
            AdminActivateWindow.Parent = this;
            AdminActivateWindow.OpenItem(new WorkspaceListViewModel());
        }

        public void WorkspaceButton()
        {
            AdminActivateWindow.OpenItem(new WorkspaceListViewModel());
        }

        public void UserButton()
        {
            AdminActivateWindow.OpenItem(new UserListViewModel());
        }

        public void GroupButton()
        {
            AdminActivateWindow.OpenItem(new GroupListViewModel());
        }




        public static class AdminActivateWindow
        {
            public static AdminViewModel Parent;

            public static void OpenItem(IScreen t)
            {
                Parent.ActivateItem(t);
            }
        }
    }


}
