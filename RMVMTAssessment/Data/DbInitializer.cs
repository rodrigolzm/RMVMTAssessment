using Microsoft.EntityFrameworkCore;
using RMVMTAssessment.Models;

namespace RMVMTAssessment.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Set<DriverLicence>().Any())
                return;

            var currentDate = DateTime.UtcNow;

            var licences = new DriverLicence[]
            {
                new DriverLicence
                {
                    Id=Guid.NewGuid(), 
                    Number=3257767, 
                    Name="John Oliver", 
                    IssueAt=currentDate.AddYears(-8), 
                    ExpireAt=currentDate.AddYears(-3)
                },
                new DriverLicence
                {
                    Id=Guid.NewGuid(), 
                    Number=3870596, 
                    Name="Stephen Cookson",
                    IssueAt=currentDate.AddYears(-2),
                    ExpireAt=currentDate.AddYears(3)
                },
                new DriverLicence
                {
                    Id=Guid.NewGuid(), 
                    Number=5786679, 
                    Name="Anna Smith",
                    IssueAt=currentDate.AddYears(-1),
                    ExpireAt=currentDate.AddYears(4)
                },
            };

            foreach (var driverLicence in licences)
                context.Set<DriverLicence>().Add(driverLicence);

            context.SaveChanges();
        }
        public static void Reset(ApplicationDbContext context)
        {
            context.Set<SuspensionTransaction>().RemoveRange(context.Set<SuspensionTransaction>());
            context.Set<DriverLicence>().RemoveRange(context.Set<DriverLicence>());
            context.SaveChanges();
            DbInitializer.Initialize(context);
        }
    }
}
