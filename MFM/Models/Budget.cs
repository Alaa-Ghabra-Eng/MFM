using Microsoft.Build.Framework;

namespace MFM.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public Boolean IsActive {get;set;} // to show budget in the new transaction dialoug or not...
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CurrentFunds { get; set; }
        public decimal AllocatedFunds { get; set; }
        public string FundPercentage => String.Format("{0}%", Math.Round((double)(100 * CurrentFunds / AllocatedFunds), 2));
        public ApplicationUser CreatorUser { get; set; }
        public DateTime Created { get; set; }
        public DateTime ExpiresAt {get;set;} // is set during creation 
        public Boolean IsRenewable { get; set; } // Does this budget gets renewed next month ?
        public decimal LastCurrentFunds { get; set; } // To store its last current fund before renewing next month if is renewable

    }
}
