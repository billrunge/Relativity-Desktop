using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using RelativityDesktop.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace RelativityDesktop
{
    class GetWorkspaces
    {

        public BindableCollection<WorkspaceModel> Workspaces { get; private set; }

        public GetWorkspaces()
        {
            Workspaces = new BindableCollection<WorkspaceModel>();
        }

        public BindableCollection<WorkspaceModel> GetWorkspacesList(HttpClient client)
        {
            Workspaces = new BindableCollection<WorkspaceModel>();
            HttpResponseMessage response = client.GetAsync("Relativity.REST/Relativity/Workspace").Result;
            string jsonString;

            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception("Failed to get workspaces from Relativity");
            }

            JObject jsonObject = JObject.Parse(jsonString);

            foreach (var result in jsonObject["Results"])
            {
                if (!int.TryParse(result["Artifact ID"].ToString(), out int artifactId))
                {
                    throw new Exception("Unable to cast workspace ArtifactID to Int32");
                }

                WorkspaceModel workspace = new WorkspaceModel
                {
                    Name = result["Relativity Text Identifier"].ToString(),
                    ArtifactId = artifactId,
                    WorkspaceGuid = result["Guids"][0].ToString(),
                    Keywords = result["Keywords"].ToString()

                };
                Workspaces.Add(workspace);

            }
            return Workspaces;

        }
    }
}
