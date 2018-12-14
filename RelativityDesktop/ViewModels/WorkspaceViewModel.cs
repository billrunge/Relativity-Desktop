using Caliburn.Micro;
using RelativityDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RelativityDesktop.ViewModels
{
    class WorkspaceViewModel : Screen
    {
        private BindableCollection<WorkspaceModel> _workspaces = new BindableCollection<WorkspaceModel>();
        private WorkspaceModel _selectedWorkspace;

        public BindableCollection<WorkspaceModel> Workspaces
        {
            get
            {
                return _workspaces;
            }
            set
            {
                _workspaces = value;
            }
        }

        public WorkspaceModel SelectedWorkspace
        {
            get
            {
                return _selectedWorkspace;
            }
            set
            {
                _selectedWorkspace = value;
                NotifyOfPropertyChange(() => SelectedWorkspace);
            }
        }

        public WorkspaceViewModel()
        {
            GetRestClient getRestClient = new GetRestClient();
            GetWorkspaces getWorkspaces = new GetWorkspaces();

            HttpClient client = getRestClient.GenerateRestClient();            

            Workspaces = getWorkspaces.GetWorkspacesList(client);
        }

        private void Workspace_Click()
        {

        }


    }
}
