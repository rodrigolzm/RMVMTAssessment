using RMVMTAssessment.Dtos;
using RMVMTAssessment.Models;
using System.Collections;

namespace RMVMTAssessment.Converters
{
    public class DriverLicenceConverter
    {
        public static DriverLicenceDto ConvertToDto(DriverLicence model) {

            return new DriverLicenceDto
            {
                Number = model.Number,
                Name = model.Name,
                IssueAt = model.IssueAt,
                ExpireAt = model.ExpireAt,
                IsExpired = model.IsExpired(),
                IsSuspended = model.IsSuspended(),
                Suspensions = SuspensionTransactionConverter.ConvertToDtos(model.Suspensions?.ToList())
            };
        }

        public static IList<DriverLicenceDto> ConvertToDtos(IList<DriverLicence>? list)
        {
            var dtos = new List<DriverLicenceDto>();

            if (list != null)
                foreach (var driverLicence in list)
                    dtos.Add((DriverLicenceDto)ConvertToDto(driverLicence));

            return dtos;
        }
    }
}
