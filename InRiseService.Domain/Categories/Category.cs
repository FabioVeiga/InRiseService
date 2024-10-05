using System.Text.Json.Serialization;
using InRiseService.Domain.Softwares;

namespace InRiseService.Domain.Categories
{
    public class Category : BaseDomain
    {
        public string Name { get; set; } = default!;
        
        [JsonIgnore]
        public ICollection<Software>? Softwares { get; set; }
    }
}
