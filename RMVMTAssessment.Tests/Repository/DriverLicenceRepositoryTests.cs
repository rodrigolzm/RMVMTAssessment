using Microsoft.EntityFrameworkCore;
using RMVMTAssessment.Models;
using RMVMTAssessment.Tests.Data;

namespace RMVMTAssessment.Tests.Repository
{
    public class DriverLicenceRepositoryTests : TestDbContext
    {
        /// <summary>
        /// Test if the database was seed properly with three driver licences.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestDatabaseSeed()
        {
            var licences = await _dbContext.Set<DriverLicence>().ToListAsync();
            Assert.Equal(3, licences.Count);
        }
    }
}