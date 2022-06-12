using System.ComponentModel.DataAnnotations;

namespace InspectionAPI.Data.Entities
{
    public class Inspection
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string InspectionName { get; set; } = string.Empty;

        [StringLength(20)]
        public string Status { get; set; } = string.Empty;

        [StringLength(20)]
        public string Comment { get; set; } = string.Empty;

        public int ÍnpectionTypeId { get; set; }
        public virtual InspectionType InspectionType { get; set; }
    }
}
