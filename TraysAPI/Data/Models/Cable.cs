using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraysAPI.Data.Models
{
    public class Cable
    {        
        public int Id { get; set; }

        public int Number { get; set; }

        public string CableInformation { get; set; }

        public string CableName { get; set; }

        [ForeignKey("CableType")]
        public int CableTypeId { get; set; }
        public CableType CableType { get; set; }

        public string FromDevice { get; set; }

        public string ToDevice { get; set; }

        public string Routing { get; set; }
    }
}
