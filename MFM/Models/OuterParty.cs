using System.Security.Policy;

namespace MFM.Models
{
    public class OuterParty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ApplicationUser CreatorUser { get; set; }
        public Category Category { get; set; }

    }
}
