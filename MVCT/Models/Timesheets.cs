using System.ComponentModel.DataAnnotations;


namespace MVCT.Models
{

    public class Timesheets
    {
        [Key] 
        public int Id { get; set; } 

        public string UserId { get; set; }

        public string? UserCheckId { get; set; }

        public string? TimeWork { get; set; }

        public string? WorkingContent { get; set; }

        public string? State { get; set; } = "No"; // duyệt chưa
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? TimeCheckout { get; set; }

        public bool? CheckIn { get ; set; } = false;
    }

}
