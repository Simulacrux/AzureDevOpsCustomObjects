using AzureDevOpsCustomObjects.Attributes;
using AzureDevOpsCustomObjects.Enumerations;
using System;

namespace AzureDevOpsCustomObjects.WorkItems
{
    public class AzureDevOpsCustomTicket : AzureDevOpsWorkItem
    {
        public AzureDevOpsCustomTicket()
        {
            DevOpsWorkItemType = AzureDevOpsWorkItemType.Ticket;
        }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.ValueArea")]
        public string ValueArea { get; set; }

        [AzureDevOpsPath("/fields/System.Description")]
        public string Description { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Scheduling.Effort")]
        public double? Effort { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.Common.AcceptanceCriteria")]
        public string AcceptanceCriteria { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.TCM.ReproSteps")]
        public string ReproSteps { get; set; }

        [AzureDevOpsPath("/fields/Microsoft.VSTS.TCM.SystemInfo")]
        public string SystemInfo { get; set; }

        [AzureDevOpsPath("/fields/Custom.AWDeskNo")]
        public Int64 AWDeskNo { get; set; }

        [AzureDevOpsPath("/fields/Custom.AWCustomerName")]
        public string AWCustomerName { get; set; }

        [AzureDevOpsPath("/fields/Custom.PFTicketNo")]
        public string PFTicketNo { get; set; }

        [AzureDevOpsPath("/fields/Custom.EntryDate")]
        public DateTime EntryDate { get; set; }

        [AzureDevOpsPath("/fields/Custom.Solution")]
        public string Solution { get; set; }     
        
    }
}