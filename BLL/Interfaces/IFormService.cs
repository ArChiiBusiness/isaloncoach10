using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace BLL.Interfaces
{
    public interface IFormService
    {
        Task<bool> SubmitActualData(ActualBOL form);
        Task<List<ActualBOL>> GetFormDataAll();
        Task<bool> DeleteResponse(Guid id);
        Task<ActualBOL> GetFormData(Guid id);
    }

}
