using Microsoft.EntityFrameworkCore;
using RMVMTAssessment.Data;
using RMVMTAssessment.Exceptions;
using RMVMTAssessment.Interfaces;
using RMVMTAssessment.Models;

namespace RMVMTAssessment.Repository
{
    public class SuspensionTransactionRepository : ISuspensionTransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly ILogger<SuspensionTransactionRepository> _logger;

        public SuspensionTransactionRepository(ApplicationDbContext dbContext, ILogger<SuspensionTransactionRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<SuspensionTransaction> AddAsync(int driverLicenceNumber, DateTime startDate, DateTime endDate)
        {
            if (_logger.IsEnabled(LogLevel.Debug)) 
                _logger.LogDebug("validating a suspension period for a driver licence");

            //check if the period is valid
            if (startDate >= endDate) throw new InvalidPeriodException();

            //check and retrieve a valid driver licence
            var driverLicence = await _dbContext
                .Set<DriverLicence>()
                .Where(d => d.Number.Equals(driverLicenceNumber))
                .FirstOrDefaultAsync() ?? throw new DriverLicenceNotFoundException();

            //check if the driver licence is expired
            if (driverLicence.IsExpired()) throw new ExpiredDriverLicenceException();

            if (_logger.IsEnabled(LogLevel.Debug)) 
                _logger.LogDebug("adding a suspension for the driver licence");

            var suspensionTransaction = new SuspensionTransaction
            {
                Id = Guid.NewGuid(),
                Licence = driverLicence,
                StartDate = startDate.ToUniversalTime(),
                EndDate = endDate.ToUniversalTime()
            };

            //save a new suspension
            await _dbContext.Set<SuspensionTransaction>().AddAsync(suspensionTransaction);
            await _dbContext.SaveChangesAsync();

            return suspensionTransaction;
        }
    }
}
