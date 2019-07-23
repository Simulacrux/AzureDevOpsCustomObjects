using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOpsCustomObjects
{
    public class Connection
    {
        HttpClient _client = new HttpClient();

        public async Task<bool> CreateConnection(string organization, string project, string personalAccessToken)
        {
            //try
            //{
            using (_client = new HttpClient())
            {
                string credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", personalAccessToken)));

                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                //using (HttpResponseMessage response = await _client.GetAsync(GetProjects(organization)))
                using (HttpResponseMessage response = await _client.GetAsync(GetWorkItems(organization, project)))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
            }
            return true;
            //}
            //catch (Exception ex)
            //{
            //    //Console.WriteLine(ex.ToString());
            //}

            /*
            Next:
            
            1. Get a work item by ID
            2. Create a feature with title, description, target date and tags
            
            3. Add attachment (email) to work item
            
            4. Create a PBI
            5. Get the PBI
            6. Attach PBI to feature


              */
        }



        private string GetProjectsURL(string organization)
        {
            return "https://dev.azure.com/{organization}/_apis/projects";
        }

        private string GetWorkItemsURL(string organization, string project)
        {
            return "https://dev.azure.com/{organization}/{project}/_apis/wit/workitems?ids={ids}&api-version=5.0";
            //return "https://dev.azure.com/{organization}/{project}/_apis/wit/workitems?api-version=5.0";
        }

        private string GetCreateFeatureURL(string organization, string project)
        {
            string type = "Feature";
            return "https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/${type}?api-version=5.0";
        }

        private string GetCreateFeatureRequest()
        {
            string request = @"" +
            "[" +
            "  {" +
            "    \"op\": \"add\"," +
            "    \"path\": \"/fields/System.Title\"," +
            "    \"from\": null," +
            "    \"value\": \"Sample task\"" +
            "  }" +
            "]";

            return request;
        }
    }
}
