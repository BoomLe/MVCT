using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCT.Models.UserReport
{
    public class UserReports
    {
        [Key]
        
        public int Id { set; get; }
        [DataType(DataType.DateTime)]
        public DateTime SendnDate { set; get; }
        public string Description { set; get; }

        public string  Messages { set; get; }

        public string UserId { set; get; }

        public AppUser User { set; get; }

    }
}
