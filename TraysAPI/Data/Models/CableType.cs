using System.ComponentModel.DataAnnotations;

namespace TraysAPI.Data.Models
{
    public class CableType
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

        public double Weight { get; set; }

        public double Diameter { get; set; }
    }
}