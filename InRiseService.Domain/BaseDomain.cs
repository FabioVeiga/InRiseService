using System.ComponentModel.DataAnnotations;

namespace InRiseService.Domain
{
    public class BaseDomain
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime InsertIn { get; set; } = DateTime.Now;
        public DateTime? UpdateIn { get; set; }
        public DateTime? DeleteIn { get; set; }
    }
}