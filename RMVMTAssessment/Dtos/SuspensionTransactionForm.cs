using System.ComponentModel.DataAnnotations;

namespace RMVMTAssessment.Dtos
{
    public class SuspensionTransactionForm
    {
        [Display(Name = "Driver Licence Number")]
        [Required]
        public int? DriverLicenceNumber { get; set; }

        [Display(Name = "Start Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "End Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddYears(1);
    }
}
