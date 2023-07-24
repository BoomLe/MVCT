namespace MVCT.DTO
{
    public class TimeSheetsDTO
    {
        public int? Id { get; set; }
        public string TimeWork { get; set; }
        public bool CheckIn { get; set; }
        public string WorkingContent { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? UserName { get; set;}

        public string? State { get; set; } = "No";
        public DateTime? TimeCheckout { get; set; }

        public string? TimeHadWork = "";
     }
}
