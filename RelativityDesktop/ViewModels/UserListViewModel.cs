﻿using Caliburn.Micro;
using RelativityDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RelativityDesktop.ViewModels
{
    class UserListViewModel : Screen
    {

        private BindableCollection<UserListModel> _users;
        public BindableCollection<UserListModel> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }

        public UserListViewModel()
        {
            GetRestClient getRestClient = new GetRestClient();
            AdminHelper adminHelper = new AdminHelper();

            HttpClient client = getRestClient.GenerateRestClient();
            Users = adminHelper.GetUserList(client);

        }



    }
}

