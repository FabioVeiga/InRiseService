using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InRiseService.Domain.Coolers;
using InRiseService.Domain.MemoriesRam;
using InRiseService.Domain.MemoriesRom;
using InRiseService.Domain.MonitorsScreen;
using InRiseService.Domain.MotherBoards;
using InRiseService.Domain.PowerSupplies;

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

        [ForeignKey("MemoryRam")]
        public int? MemoryRamId { get; set; }
        [JsonIgnore]
        public MemoryRam? MemoryRam { get; set; }

        [ForeignKey("MemoryRom")]
        public int? MemoryRomId { get; set; }
        [JsonIgnore]
        public MemoryRom? MemoryRom { get; set; }

        [ForeignKey("MonitorScreen")]
        public int? MonitorScreenId { get; set; }
        [JsonIgnore]
        public MonitorScreen? MonitorScreen { get; set; }

        [ForeignKey("MotherBoard")]
        public int? MotherBoardId { get; set; }
        [JsonIgnore]
        public MotherBoard? MotherBoard { get; set; }

        [ForeignKey("PowerSupply")]
        public int? PowerSupplyId { get; set; }
        [JsonIgnore]
        public PowerSupply? PowerSupply { get; set; }
    }
}