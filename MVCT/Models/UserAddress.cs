using System.ComponentModel.DataAnnotations;

namespace MVCT.Models
{
    public class UserAddress
    {
        [Key] // Đánh dấu thuộc tính là khóa chính
        public int Id { get; set; } // Id sẽ được tự động tăng
        public string IdUser { get; set; }
        public int IdCity { get; set; }
        public int IdDistrict { get; set;}

        public UserAddress() { }    
    }
}
