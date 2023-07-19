using RMVMTAssessment.Dtos;
using RMVMTAssessment.Models;

namespace RMVMTAssessment.Converters
{
    public class SuspensionTransactionConverter
    {
        public static SuspensionTransactionDto ConvertToDto(SuspensionTransaction model)
        {
            return new SuspensionTransactionDto
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                IsExpired = model.IsExpired(),
                IsArchived = model.IsArchived()
            };
        }

        public static IList<SuspensionTransactionDto> ConvertToDtos(IList<SuspensionTransaction>? list)
        {
            var dtos = new List<SuspensionTransactionDto>();

            if (list != null)
                foreach (var suspentionTransaction in list)
                    dtos.Add(ConvertToDto(suspentionTransaction));

            return dtos.OrderByDescending(s => s.StartDate).ToList();
        }
    }
}
