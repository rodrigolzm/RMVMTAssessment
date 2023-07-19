using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RMVMTAssessment.Exceptions;
using RMVMTAssessment.Interfaces;
using RMVMTAssessment.Models;
using RMVMTAssessment.Repository;
using RMVMTAssessment.Tests.Data;

namespace RMVMTAssessment.Tests.Repository
{
    public class SuspensionTransactionTests : TestDbContext
    {
        private readonly int _invalidDriverLicenceNumber = 1;

        private readonly int _expiredDriverLicenceNumber = 3257767;

        private readonly int _activeDriverLicenceNumber = 3870596;

        private readonly DateTime _currentDate = DateTime.Now;

        private readonly ISuspensionTransactionRepository _suspensionTransactionRepository;

        public SuspensionTransactionTests()
        {
            _suspensionTransactionRepository = new SuspensionTransactionRepository(
                _dbContext,
                new Mock<ILogger<SuspensionTransactionRepository>>().Object
            );
        }

        /// <summary>
        /// Test if the repository throws an exception for a invalid period.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestInvalidSuspensionPeriod()
        {
            await Assert.ThrowsAsync<InvalidPeriodException>(
                async () => await _suspensionTransactionRepository
                .AddAsync(_activeDriverLicenceNumber, _currentDate, _currentDate.AddYears(-1))
            );
        }

        /// <summary>
        /// Test if the repository throws an exception for a invalid driver licence (not found).
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestNotFoundDriverLicence()
        {
            await Assert.ThrowsAsync<DriverLicenceNotFoundException>(
                async () => await _suspensionTransactionRepository
                .AddAsync(_invalidDriverLicenceNumber, _currentDate, _currentDate.AddYears(1))
            );
        }

        /// <summary>
        /// Test if the repository throws an exception for an expired driver licence.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestExpiredDriverLicence()
        {
            await Assert.ThrowsAsync<ExpiredDriverLicenceException>(
                async () => await _suspensionTransactionRepository
                .AddAsync(_expiredDriverLicenceNumber, _currentDate, _currentDate.AddYears(1))
            );
        }

        /// <summary>
        /// Test if the repository saves a new suspension for a driver licence making it suspended.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestAddSuspensionMakingDriverLicenceSuspended()
        {
            var suspension = await _suspensionTransactionRepository
                .AddAsync(_activeDriverLicenceNumber, _currentDate, _currentDate.AddYears(1));
            Assert.NotNull(suspension);

            var driverLicence = await _dbContext
                .Set<DriverLicence>()
                .Include(d => d.Suspensions)
                .Where(d => d.Number.Equals(_activeDriverLicenceNumber))
                .FirstOrDefaultAsync();
            Assert.NotNull(driverLicence);
            Assert.NotNull(driverLicence.Suspensions);
            Assert.Contains(driverLicence.Suspensions, s => s.Id.Equals(suspension.Id));

            Assert.True(driverLicence.IsSuspended());
        }

        /// <summary>
        /// Test if the repository saves a new expired suspension for a driver licence making it not suspended.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TestAddExpiredSuspensionMakingDriverLicenceNotSuspended()
        {
            var suspension = await _suspensionTransactionRepository
                .AddAsync(_activeDriverLicenceNumber, _currentDate.AddYears(-2), _currentDate.AddYears(-1));
            Assert.NotNull(suspension);

            var driverLicence = await _dbContext
                .Set<DriverLicence>()
                .Include(d => d.Suspensions)
                .Where(d => d.Number.Equals(_activeDriverLicenceNumber))
                .FirstOrDefaultAsync();
            Assert.NotNull(driverLicence);
            Assert.NotNull(driverLicence.Suspensions);
            Assert.Contains(driverLicence.Suspensions, s => s.Id.Equals(suspension.Id));

            Assert.False(driverLicence.IsSuspended());
        }

        /// <summary>
        /// Test if the suspension is archived properly.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void TestArchivedSuspension()
        {
            var suspension = new SuspensionTransaction
            {
                Id = Guid.NewGuid(),
                Licence = new DriverLicence
                {
                    Id = Guid.NewGuid(),
                    Number = 1,
                    Name = "unit test name",
                    IssueAt = _currentDate.AddYears(-1),
                    ExpireAt = _currentDate.AddYears(4)
                },
                StartDate = _currentDate.AddYears(-10),
                EndDate = _currentDate.AddYears(-8)
            };
            Assert.True(suspension.IsArchived());
        }
    }
}