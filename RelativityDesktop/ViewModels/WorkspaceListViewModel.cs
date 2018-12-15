using Caliburn.Micro;
using RelativityDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RelativityDesktop.ViewModels
{
    class WorkspaceListViewModel : Conductor<Object>
    {
        private BindableCollection<WorkspaceListModel> _workspaces = new BindableCollection<WorkspaceListModel>();
        private WorkspaceListModel _selectedWorkspace;

        public BindableCollection<WorkspaceListModel> Workspaces
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

        public WorkspaceListModel SelectedWorkspace
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

        public WorkspaceListViewModel()
        {
            GetRestClient getRestClient = new GetRestClient();
            EddsHelper eddsHelper = new EddsHelper();


            HttpClient client = getRestClient.GenerateRestClient();        
            Workspaces = eddsHelper.GetWorkspacesList(client);
        }

        public void Workspace_Click()
        {
            ChangeActiveItem(new WorkspaceViewModel(), true);

            MessageBoxResult result = MessageBox.Show($"You clicked workspace {SelectedWorkspace.ArtifactId}");



        }


    }
}
