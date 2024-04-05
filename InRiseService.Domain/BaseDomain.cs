using System.ComponentModel.DataAnnotations;

namespace InRiseService.Domain
{
    public class BaseDomain
    {
        [Key]
        public int Id { get; set; }
        public DateTime InsertIn { get; set; }
        public DateTime UpdateIn { get; set; }
        public DateTime DeleteIn { get; set; }
    }
}