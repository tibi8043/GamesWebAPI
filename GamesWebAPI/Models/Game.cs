using System.ComponentModel.DataAnnotations;

namespace GamesWebAPI.Models {
    public class Game {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public PlatformType Type { get; set; }
        [Required]
        public int Price { get; set; }
        public DateTime CreatedTime { get; set; }
        
    }

    public enum PlatformType
    {
        XBOX,
        PS,
        PC
    }
}
