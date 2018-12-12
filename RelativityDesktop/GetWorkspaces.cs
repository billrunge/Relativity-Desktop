using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RelativityDesktop
{
    class GetWorkspaces
    {

        public List<Workspace> Workspaces { get; private set; }

        public GetWorkspaces()
        {
            Workspaces = new List<Workspace>();
        }

        public List<Workspace> GetWorkspacesList(HttpClient client)
        {
            Workspaces = new List<Workspace>();
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

                Workspace workspace = new Workspace
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
