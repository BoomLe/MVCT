using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCT.Models.Attandance
{
    public class Attandances :BaseEntity
    {
        public int Id { set; get; }


        [DataType(DataType.DateTime)]
        public DateTime DataTime { set; get; }

        public string UserId { set; get; }

        public AppUser User { set; get; }
    }
}
