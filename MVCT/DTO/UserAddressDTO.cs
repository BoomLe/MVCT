namespace MVCT.DTO
{
    public class UserAddressDTO
    {
        public int Id { get; set; } 
        public string IdUser { get; set; }
        public int IdCity { get; set; }
        public int IdDistrict { get; set; }

        public string NameCity { get; set; }
        public string NameDistrict { get; set;}
    }
}
