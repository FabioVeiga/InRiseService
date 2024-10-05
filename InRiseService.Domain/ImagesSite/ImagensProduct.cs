using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InRiseService.Domain.Categories;
using InRiseService.Domain.Computers;
using InRiseService.Domain.Coolers;
using InRiseService.Domain.MemoriesRam;
using InRiseService.Domain.MemoriesRom;
using InRiseService.Domain.MonitorsScreen;
using InRiseService.Domain.MotherBoards;
using InRiseService.Domain.PowerSupplies;
using InRiseService.Domain.Processors;
using InRiseService.Domain.Softwares;
using InRiseService.Domain.Towers;
using InRiseService.Domain.VideoBoards;

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

        [ForeignKey("Processor")]
        public int? ProcessorId { get; set; }
        [JsonIgnore]
        public Processor? Processor { get; set; }

        [ForeignKey("VideoBoard")]
        public int? VideoBoardId { get; set; }
        [JsonIgnore]
        public VideoBoard? VideoBoard { get; set; }

        [ForeignKey("Tower")]
        public int? TowerId { get; set; }
        [JsonIgnore]
        public Tower? Tower { get; set; }

        [ForeignKey("Computer")]
        public int? ComputerId { get; set; }
        [JsonIgnore]
        public Computer? Computer { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }

        [ForeignKey("Software")]
        public int? SoftwareId { get; set; }
        [JsonIgnore]
        public Software? Software { get; set; }
    }
}