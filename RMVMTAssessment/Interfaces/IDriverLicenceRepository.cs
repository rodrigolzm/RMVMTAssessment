using RMVMTAssessment.Models;

namespace RMVMTAssessment.Interfaces
{
    public interface IDriverLicenceRepository
    {
        public Task<IList<DriverLicence>> GetAllAsync();
    }
}
