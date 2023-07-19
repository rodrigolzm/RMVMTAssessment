using System.ComponentModel.DataAnnotations;

namespace RMVMTAssessment.Models
{
    public class SuspensionTransaction
    {
        private const int ARCHIVE_YEAR_LIMIT = 5;

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DriverLicence? Licence { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsExpired()
        {
            return this.EndDate.Date < DateTime.UtcNow.Date;
        }

        public bool IsArchived()
        {
            return this.EndDate.AddYears(ARCHIVE_YEAR_LIMIT).Date < DateTime.UtcNow.Date;
        }
    }
}
