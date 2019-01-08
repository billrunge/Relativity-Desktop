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
    class GroupListViewModel : Screen
    {

        private BindableCollection<GroupListModel> _groups;

        public BindableCollection<GroupListModel> Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                _groups = value;
            }
        }

        public GroupListViewModel()
        {
            GetRestClient getRestClient = new GetRestClient();
            AdminHelper adminHelper = new AdminHelper();

            HttpClient client = getRestClient.GenerateRestClient();
            Groups = adminHelper.GetGroupList(client);

        }


    }
}
