using System.ComponentModel.DataAnnotations;

namespace MVCT.Models.Attandance
{
    public class AttandanceInfo
    {

        public string Username { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }

        public DateTime CurrentDate { get; set; }

    }
}
