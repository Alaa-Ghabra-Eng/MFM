using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace MFM.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public Category Category { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public ApplicationUser CreatorUser { get; set; }
        public OuterParty OuterParty { get; set; }
        public DateTime Created { get; set; }
        public bool IsIncome { get; set; }
        public Budget? budget { get; set; } 
    }
}
