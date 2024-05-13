namespace AuthReadyAPI.DataLayer.DTOs
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public DateTime LastModifiedOnDate { get; set; }
        public string LastModifiedByUser { get; set; }
    }
}
