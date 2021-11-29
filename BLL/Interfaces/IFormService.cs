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
        Task<bool> SubmitData(FormDataBOL form);
        Task<List<FormDataBOL>> GetFormDataAll();
    }

}
