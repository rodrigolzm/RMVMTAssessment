using System.ComponentModel.DataAnnotations;

namespace RMVMTAssessment.Models
{
    public class DriverLicence
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public int Number { get; set; }
        
        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime IssueAt { get; set; }

        [Required]
        public DateTime ExpireAt { get; set; }

        public ICollection<SuspensionTransaction>? Suspensions { get; set;}

        public bool IsExpired()
        {
            return this.ExpireAt.Date < DateTime.UtcNow.Date;
        }

        public bool IsSuspended() 
        {
            if (this.Suspensions != null)
                foreach(var suspension in this.Suspensions)
                    if (suspension.StartDate.Date <= DateTime.UtcNow.Date && suspension.EndDate.Date >= DateTime.UtcNow.Date) return true;
            return false;
        }
    }
}
