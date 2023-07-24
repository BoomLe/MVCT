using System.ComponentModel.DataAnnotations;


namespace MVCT.Models
{

    public class Timesheets
    {
        [Key] // Đánh dấu thuộc tính là khóa chính
        public int Id { get; set; } // Id sẽ được tự động tăng

        public string UserId { get; set; }

        public string? UserCheckId { get; set; }

        public int? TimeWork { get; set; }

        public string? WorkingContent { get; set; }

        public string? State { get; set; } = "No"; // duyệt chưa
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool? CheckIn { get ; set; } = false;
    }

}
