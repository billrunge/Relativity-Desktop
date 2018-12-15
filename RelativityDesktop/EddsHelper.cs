using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RelativityDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RelativityDesktop
{
    class EddsHelper
    {

        public string GetMatterName(HttpClient client, int artifactId)
        {
            Matter matter = new Matter
            {
                matterArtifactID = artifactId
            };

            string queryInputJSON = JsonConvert.SerializeObject(matter);


            StringContent content = new StringContent(queryInputJSON, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/Relativity.REST/api/Relativity.Services.Matter.IMatterModule/Matter%20Manager/ReadSingleAsync", content).Result;

            string jsonString;

            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception("Failed to get matter from Relativity");
            }

            JObject jsonObject = JObject.Parse(jsonString);

            return jsonObject["Name"].ToString();

        }

        public string GetClientName(HttpClient httpClient, int artifactId)
        {

            Client client = new Client
            {
                clientArtifactID = artifactId
            };

            string queryInputJSON = JsonConvert.SerializeObject(client);



            StringContent content = new StringContent(queryInputJSON, Encoding.UTF8, "application/json");
            HttpResponseMessage response = httpClient.PostAsync("/Relativity.REST/api/Relativity.Services.Client.IClientModule/Client%20Manager/ReadSingleAsync", content).Result;

            string jsonString;

            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new Exception("Failed to get client from Relativity");
            }

            JObject jsonObject = JObject.Parse(jsonString);

            return jsonObject["Name"].ToString();

        }

        public BindableCollection<WorkspaceListModel> GetWorkspacesList(HttpClient client)
        {
            BindableCollection<WorkspaceListModel>  workspaces = new BindableCollection<WorkspaceListModel>();

            WorkspaceSearchField searchField = new WorkspaceSearchField
            {
                fields = new List<string> { "*" }                
            };

            string queryInputJSON = JsonConvert.SerializeObject(searchField);



            StringContent content = new StringContent(queryInputJSON, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("Relativity.REST/Relativity/Workspace/QueryResult", content).Result;
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

                if (!int.TryParse(result["Matter ID"].ToString(), out int matterId))
                {
                    throw new Exception("Unable to cast workspace Matter ID to Int32");
                }

                if (!int.TryParse(result["Client"]["Artifact ID"].ToString(), out int clientId))
                {
                    throw new Exception("Unable to cast workspace Client ID to Int32");
                }

                WorkspaceListModel workspace = new WorkspaceListModel
                {
                    Name = result["Relativity Text Identifier"].ToString(),
                    ArtifactId = artifactId,
                    WorkspaceGuid = result["Guids"][0].ToString(),
                    Keywords = result["Keywords"].ToString(),
                    MatterId = matterId,
                    Matter = GetMatterName(client, matterId),
                    ClientId = clientId,
                    Client = GetClientName(client, clientId)
                };

                workspaces.Add(workspace);

            }
            return workspaces;
        }
    }

    public class Matter
    {
        public int matterArtifactID { get; set; }
    }

    public class Client
    {
        public int clientArtifactID { get; set; }
    }

    public class WorkspaceSearchField
    {
        public List<string> fields { get; set; } = new List<string>();
    }



}
