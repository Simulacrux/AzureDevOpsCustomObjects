using System;
using System.Collections.Generic;
using System.Linq;
using AzureDevOpsCustomObjects.Extensions;
using AzureDevOpsCustomObjects.WorkItems;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace AzureDevOpsCustomObjects
{
    public class WorkItemReader
    {
        public WorkItemReader(string uri, string personalAccessToken, string projectName)
        {
            Uri = new Uri(uri);
            PersonalAccessToken = personalAccessToken;
            ProjectName = projectName;

            var credentials = new VssBasicCredential(string.Empty, PersonalAccessToken);

            var connection = new VssConnection(Uri, credentials);
            WorkItemTrackingHttpClient = connection.GetClient<WorkItemTrackingHttpClient>();
        }

        private WorkItemTrackingHttpClient WorkItemTrackingHttpClient { get; }

        private Uri Uri { get; }

        private string ProjectName { get; }

        private string PersonalAccessToken { get; }

        public IEnumerable<AzureDevOpsWorkItem> All()
        {
            var workItemQuery = new Wiql()
            {
                Query = "Select * " +
                        "From WorkItems " +
                        $"Where[System.TeamProject] = '{this.ProjectName}' "
            };

            var workItemQueryResult = this.WorkItemTrackingHttpClient.QueryByWiqlAsync(workItemQuery).Result;

            if (!workItemQueryResult.WorkItems.Any())
                return new List<AzureDevOpsWorkItem>();

            var skip = 0;
            const int batchSize = 150; // Maximum allowed numbers are 200
            List<WorkItemReference> workItemRefs;
            IList<WorkItem> workItems = new List<WorkItem>();
            do
            {
                workItemRefs = workItemQueryResult.WorkItems.Skip(skip).Take(batchSize).ToList();
                if (workItemRefs.Any())
                {
                    // get next batch of work items
                    workItems.AddRange(this.WorkItemTrackingHttpClient.GetWorkItemsAsync(workItemRefs.Select(wir => wir.Id)).Result);
                }

                skip += batchSize;
            } while (workItemRefs.Count() == batchSize);

            return workItems.Select(item => item.ToAzureDevOpsWorkItem()).OfType<AzureDevOpsWorkItem>();
        }

        public AzureDevOpsWorkItem Read(int workItemId)
        {
            var workItemQuery = new Wiql()
            {
                Query = "Select * " +
                        "From WorkItems " +
                        $"Where [System.TeamProject] = '{this.ProjectName}' AND [System.Id] = {workItemId} "
            };

            var workItemQueryResult = this.WorkItemTrackingHttpClient.QueryByWiqlAsync(workItemQuery).Result;

            if (!workItemQueryResult.WorkItems.Any())
                return null;

            
            var workItem = this.WorkItemTrackingHttpClient.GetWorkItemsAsync(workItemQueryResult.WorkItems.Select(wir => wir.Id)).Result;
            
            return workItem.Single().ToAzureDevOpsWorkItem();
        }
    }
}