using Microsoft.Build.Framework;

namespace MFM.Models
{
    public class Budget
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal CurrentFunds { get; set; }
        public decimal AllocatedFunds { get; set; }
        public float TotalFunds  => (float)(CurrentFunds / AllocatedFunds) * 100;
        public ApplicationUser CreatorUser { get; set; }
    }
}
