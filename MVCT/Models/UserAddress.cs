using System.ComponentModel.DataAnnotations;

namespace MVCT.Models
{
    public class UserAddress
    {
        [Key] 
        public int Id { get; set; } 
        public string IdUser { get; set; }
        public int IdCity { get; set; }
        public int IdDistrict { get; set;}

        public UserAddress() { }    
    }
}
