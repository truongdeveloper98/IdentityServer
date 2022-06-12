using System.ComponentModel.DataAnnotations;

namespace InspectionAPI.Data.ViewModel
{
    public class StatusViewModel
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string StatusOption { get; set; } = string.Empty;
    }
}