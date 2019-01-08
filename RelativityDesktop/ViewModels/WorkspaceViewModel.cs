using Caliburn.Micro;
using System.Data;

namespace RelativityDesktop.ViewModels
{
    internal class WorkspaceViewModel : Screen
    {
        private DataTable _documentTable = new DataTable();


        public DataTable DocumentTable
        {
            get => _documentTable;
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

            DataTable documents = workspaceHelper.GetDocumentList(getRestClient.GenerateRestClient(), workspaceArtifactId);

                DocumentTable = documents;


        }




    }
}
