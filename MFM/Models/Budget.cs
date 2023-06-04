using Microsoft.Build.Framework;

namespace MFM.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CurrentFunds { get; set; }
        public decimal AllocatedFunds { get; set; }
        public string FundPercentage =>String.Format("{0}%",Math.Round((double)(100 * CurrentFunds/AllocatedFunds),2)) ;
        public ApplicationUser CreatorUser { get; set; }
        public DateTime Created {get;set;}
        public DateTime? ExpiresAt{get;set;} = DateTime.Now.AddDays(30); // By default, budget are expired after one month
    }
}
