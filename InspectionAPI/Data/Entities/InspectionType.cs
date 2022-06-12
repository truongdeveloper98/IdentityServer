using System.ComponentModel.DataAnnotations;

namespace InspectionAPI.Data.Entities
{
    public class InspectionType
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string InspectionTypeName { get; set; } = string.Empty;

        public virtual IEnumerable<Inspection> Inspections { get; set; }

    }
}
