namespace InRiseService.Application.DTOs
{
    public class BaseDto
    {
        public int Id { get; set; }
        public bool Active { get; set; } = true;
        public DateTime InsertIn { get; set; } = DateTime.Now;
        public DateTime? UpdateIn { get; set; }
        public DateTime? DeleteIn { get; set; }
    }
}