using Microsoft.EntityFrameworkCore;
using RMVMTAssessment.Models;

namespace RMVMTAssessment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<DriverLicence>? DriverLicences { get; set; }

        public DbSet<SuspensionTransaction>? SuspensionTransactions { get; set; }
    }
}
