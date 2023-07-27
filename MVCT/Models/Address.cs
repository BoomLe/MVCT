using System.ComponentModel.DataAnnotations;

namespace MVCT.Models
{
    public class Address
    {
        [Key] 
        public int Id { get; set; } 
        public string Place { get; set; }
        public int? DependId { get; set; }
    }
}
