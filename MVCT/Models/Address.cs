using System.ComponentModel.DataAnnotations;

namespace MVCT.Models
{
    public class Address
    {
        [Key] // Đánh dấu thuộc tính là khóa chính
        public int Id { get; set; } // Id sẽ được tự động tăng
        public string Place { get; set; }
        public int? DependId { get; set; }
    }
}
