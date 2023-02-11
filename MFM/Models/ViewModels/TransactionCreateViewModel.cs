
using System.ComponentModel.DataAnnotations;

namespace MFM.Models.ViewModels
{
    public class TransactionCreateViewModel
    {
        /*[Required]
        public Decimal Amount { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Description cannot exceed 50 Chars")]
        [Display(Name = "Transaction Description")]
        public string Description { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public OuterParty OuterParty { get; set; }*/
        public IEnumerable<Category> Categories { get; set; }
        public Category Category { get; set; }  
        public OuterParty OuterParty { get; set; }  
        public IEnumerable<OuterParty> OuterParties { get; set; } 
        public Transaction Transaction { get; set; }   
    }
}
