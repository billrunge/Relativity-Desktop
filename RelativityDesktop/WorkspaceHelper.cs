using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RelativityDesktop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace RelativityDesktop
{
    internal class WorkspaceHelper
    {

        //public BindableCollection<WorkspaceModel.Document> GetDocumentList(HttpClient client, int workspaceArtifactId)
        //{            
        //    List<string> fieldList = new List<string>
        //    {
        //        "Artifact ID",
        //        "Control Number"
        //    };


        //    DocumentRequest documentRequest = new DocumentRequest()
        //    {
        //        fields = fieldList
        //    };

        //    string queryInputJSON = JsonConvert.SerializeObject(documentRequest);


        //    StringContent content = new StringContent(queryInputJSON, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = client.PostAsync($"/Relativity.REST/Workspace/{workspaceArtifactId}/Document/QueryResult", content).Result;

        //    string jsonString;

        //    if (response.IsSuccessStatusCode)
        //    {
        //        jsonString = response.Content.ReadAsStringAsync().Result;
        //    }
        //    else
        //    {
        //        MessageBox.Show(response.ToString());
        //        throw new Exception("Failed to get documents from Relativity");
        //    }

        //    JObject jsonObject = JObject.Parse(jsonString);

        //    BindableCollection<WorkspaceModel.Document> documents = new BindableCollection<WorkspaceModel.Document>();

        //    foreach (var doc in jsonObject["Results"])
        //    {
        //        if (!int.TryParse(doc["Artifact ID"].ToString(), out int artifactId))
        //        {
        //            throw new Exception("Unable to cast document Artifact ID to Int32");
        //        }


        //        WorkspaceModel.Document document = new WorkspaceModel.Document()
        //        {
        //            ArtifactId = artifactId,
        //            Identifier = doc["Control Number"].ToString()
        //        };

        //        documents.Add(document);
        //    }


        //    return documents;

        //}

        public DataTable GetDocumentList(HttpClient client, int workspaceArtifactId)
        {
            List<string> fieldList = new List<string>
            {
                "Artifact ID",
                "Control Number",
                "File Name"
            };


            DocumentRequest documentRequest = new DocumentRequest()
            {
                fields = fieldList
            };

            string queryInputJSON = JsonConvert.SerializeObject(documentRequest);


            StringContent content = new StringContent(queryInputJSON, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"/Relativity.REST/Workspace/{workspaceArtifactId}/Document/QueryResult", content).Result;

            string jsonString;

            if (response.IsSuccessStatusCode)
            {
                jsonString = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                MessageBox.Show(response.ToString());
                throw new Exception("Failed to get documents from Relativity");
            }

            JObject jsonObject = JObject.Parse(jsonString);
            DataTable documents = new DataTable();

            List<string> columnNames = new List<string>();

            foreach (var field in jsonObject["Results"][0]["__Fields"])
            {
                string columnName = field["Name"].ToString();
                documents.Columns.Add(columnName);
                columnNames.Add(columnName);
            }
           
            foreach(var result in jsonObject["Results"])
            { 
                DataRow newDocument = documents.NewRow();
                foreach(var column in columnNames)
                {
                    newDocument[column] = result[column];
                }

                documents.Rows.Add(newDocument);

            }

            return documents;

        }


        public class DocumentRequest
        {
            //public string condition { get; set; }
            public List<string> fields { get; set; } = new List<string>();
        }
    }
}
