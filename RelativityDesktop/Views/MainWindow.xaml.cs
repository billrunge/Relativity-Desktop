using log4net;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


namespace RelativityDesktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public List<Workspace> Workspaces { get; set; } = new List<Workspace>();

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainView_Closing;

            RenderWorkspaceList();


            workspaces.ItemsSource = Workspaces;




        }

        private void Workspaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Workspace selWorkspace = (Workspace)workspaces.SelectedItem;

        }

        private void RenderWorkspaceList()
        {
            GetRestClient getRestClient = new GetRestClient();
            GetWorkspaces getWorkspaces = new GetWorkspaces();


            Workspaces = getWorkspaces.GetWorkspacesList(getRestClient.GenerateRestClient());

            //foreach (var workspace in workspaces)
            //{
            //    Workspaces.Items.Add(workspace.Name);
            //}

        }

        private void MainView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*
                if (((MainViewModel)(this.DataContext)).Data.IsModified)
                if (!((MainViewModel)(this.DataContext)).PromptSaveBeforeExit())
                {
                    e.Cancel = true;
                    return;
                }
            */
            Log.Info("Closing App");
        }
    }
}
