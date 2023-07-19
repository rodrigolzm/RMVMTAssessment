using System.Collections;

namespace RMVMTAssessment.Interfaces
{
    public interface IConverter
    {
        public object ConvertToDto(object model);

        public IList ConvertToDtos(IList list);
    }
}
