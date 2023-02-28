using System.ComponentModel.DataAnnotations;

namespace lakberendezo.Models
{
    public class FurnitureModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public int Height { get; set; }
    }
}
