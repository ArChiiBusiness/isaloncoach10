using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISalonService
    {
        Task<List<SalonBOL>> GetAll();
        Task<Guid> AddSalon(SalonBOL salon);
        Task<bool> UpdateSalon(SalonBOL salon);
        Task<bool> SalonExists(string salonName);
        Task<SalonBOL> GetSalonByName(string salonName);
        Task<SalonBOL> GetSalonById(Guid salonId);
        Task<Guid> GetSalonIdByName(string salonName);
        Task<string> GetSalonNameById(Guid salonId);
        Task<bool> DeleteSalon(Guid salonId);

        // Actuals
        Task<Guid> AddActualData(ActualBOL actual, Guid salonId);
        Task<ActualBOL> GetActualById(Guid actualId);
        Task<ActualBOL> GetActualByMonthYear(Guid salonId,int Year, int Month);
        Task<List<ActualBOL>> GetActuals(Guid salonId);
        Task<bool> DeleteActual(Guid actualId);

        // Targets
        Task<Guid> AddTargetData(TargetBOL target, Guid salonId);
        Task<List<TargetBOL>> GetTargets(Guid salonId);
        Task<TargetBOL> GetTargetById(Guid targetId);
        Task<TargetBOL> GetTargetByYear(Guid salonId, int Year);
        Task<bool> UpdateTarget(TargetBOL target);
        Task<bool> DeleteTarget(Guid targetId);

    }
}
