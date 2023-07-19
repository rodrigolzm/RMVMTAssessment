using Microsoft.EntityFrameworkCore;
using RMVMTAssessment.Data;
using RMVMTAssessment.Interfaces;
using RMVMTAssessment.Models;

namespace RMVMTAssessment.Repository
{
    public class DriverLicenceRepository: IDriverLicenceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly ILogger<DriverLicenceRepository> _logger;

        public DriverLicenceRepository(ApplicationDbContext dbContext, ILogger<DriverLicenceRepository> logger) 
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<IList<DriverLicence>> GetAllAsync()
        {
            if (_logger.IsEnabled(LogLevel.Debug)) 
                _logger.LogDebug("retrieving all driver licences");

            return await _dbContext
                .Set<DriverLicence>()
                .Include(d => d.Suspensions)
                .OrderBy(d => d.Name)
                .ToListAsync();
        }
    }
}
