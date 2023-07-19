using RMVMTAssessment.Models;

namespace RMVMTAssessment.Interfaces
{
    public interface ISuspensionTransactionRepository
    {
        public Task<SuspensionTransaction> AddAsync(int driverLicenceNumber, DateTime startDate, DateTime endDate);
    }
}
