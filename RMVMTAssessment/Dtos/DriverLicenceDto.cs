using System.ComponentModel.DataAnnotations;

namespace RMVMTAssessment.Dtos
{
    public class DriverLicenceDto
    {
        public int Number { get; set; }
        
        public string? Name { get; set; }

        [Display(Name = "Issue Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime IssueAt { get; set; }

        [Display(Name = "Expire Date")]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime ExpireAt { get; set; }

        [Display(Name = "Expired")]
        public bool IsExpired { get; set; }

        [Display(Name = "Suspended")]
        public bool IsSuspended { get; set; }

        public IList<SuspensionTransactionDto>? Suspensions { get; set; }
    }
}
