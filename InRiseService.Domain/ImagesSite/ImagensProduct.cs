using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InRiseService.Domain.Coolers;

namespace InRiseService.Domain.ImagesSite
{
    public class ImagensProduct
    {
        [Key]
        public int Id { get; set; }
        public string Pathkey { get; set; } = default!;
        public string ImageName { get; set; } = default!;

        [ForeignKey("Cooler")]
        public int? CoolerId { get; set; }
        
        [JsonIgnore]
        public Cooler? Cooler { get; set; }
    }
}