using System.ComponentModel.DataAnnotations;

namespace TraysAPI.Data.Models
{
    public class Tray
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public double Length { get; set; }
    }
}
