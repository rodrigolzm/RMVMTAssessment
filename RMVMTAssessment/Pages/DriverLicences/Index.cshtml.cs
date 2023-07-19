using Microsoft.AspNetCore.Mvc.RazorPages;
using RMVMTAssessment.Converters;
using RMVMTAssessment.Dtos;
using RMVMTAssessment.Interfaces;

namespace RMVMTAssessment.Pages.DriverLicenses
{
    public class IndexModel : PageModel
    {
        private readonly IDriverLicenceRepository _driverLicenceRepository;

        public IList<DriverLicenceDto> DriverLicences { get; set; } = default!;

        public IndexModel(IDriverLicenceRepository driverLicenceRepository)
        {
            _driverLicenceRepository = driverLicenceRepository ?? throw new ArgumentNullException(nameof(driverLicenceRepository));
        }

        public async Task OnGetAsync()
        {
            if (_driverLicenceRepository != null)
            {
                this.DriverLicences = DriverLicenceConverter.ConvertToDtos(await _driverLicenceRepository.GetAllAsync());
            }
        }
    }
}
