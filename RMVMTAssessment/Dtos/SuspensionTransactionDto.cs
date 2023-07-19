using System.ComponentModel.DataAnnotations;

namespace RMVMTAssessment.Dtos
{
    public class SuspensionTransactionDto
    {
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Expired")]
        public bool IsExpired { get; set; }

        [Display(Name = "Archived")]
        public bool IsArchived { get; set; }
    }
}
