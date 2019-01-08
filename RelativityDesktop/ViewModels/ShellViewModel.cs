using Caliburn.Micro;
using RelativityDesktop.Models;
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
            //IsAuthenticated = true;
            //StartUp();
            ActivateWindow.Parent = this;
            ActivateWindow.OpenItem(new AdminViewModel());
        }


        //private void StartUp()
        //{
        //    if (IsAuthenticated)
        //    {
        //        ActivateItem(new WorkspaceListViewModel());
        //    }
        //}

        public static class ActivateWindow
        {
            public static ShellViewModel Parent;

            public static void OpenItem(IScreen t)
            {
                Parent.ActivateItem(t);
            }
        }



        //public void ChangeView()
        //{

        //    //ActivateItem(new WorkspaceViewModel(12345));
        //}




    }
}
