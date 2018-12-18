using Caliburn.Micro;
using RelativityDesktop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RelativityDesktop.ViewModels
{
    class WorkspaceViewModel : Screen
    {
        private DataTable _documentTable = new DataTable();


        public DataTable DocumentTable
        {
            get
            {
                return _documentTable;
            }
            set
            {
                _documentTable = value;
                NotifyOfPropertyChange(() => DocumentTable);
            }
        }
        //public WorkspaceModel Workspace { get; set; } = new WorkspaceModel();
        //public BindableCollection<WorkspaceModel.Document> Documents { get; set; }

        public WorkspaceViewModel(int workspaceArtifactId)
        {

            WorkspaceHelper workspaceHelper = new WorkspaceHelper();
            GetRestClient getRestClient = new GetRestClient();

            DocumentTable = workspaceHelper.GetDocumentList(getRestClient.GenerateRestClient(), workspaceArtifactId);

        }




    }
}
