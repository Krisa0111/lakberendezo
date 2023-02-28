using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace lakberendezo.Models
{
    public class RoomModel
    {
        [Display(Name ="Matrix")]
        public string[,] Matrix { get; set; }
        [Required]
        [Display(Name = "Width")]
        public int Width { get; set; }

        [Required]
        [Display(Name = "Height")]
        public int Height { get; set; }
    }
}
