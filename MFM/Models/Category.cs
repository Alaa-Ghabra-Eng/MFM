using System.Security.Policy;

namespace MFM.Models
{
    public class Category
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public string? Description { get; set; }
       public ApplicationUser CreaterUser { get; set; }

    }
}
