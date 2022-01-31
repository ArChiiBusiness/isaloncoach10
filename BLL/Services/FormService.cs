using BLL.Interfaces;
using BOL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using System;

namespace BLL.Services
{
    public class FormService : IFormService
    {
        public ApplicationDbContext _db;
        private ISalonService _salonService;

        public FormService(ApplicationDbContext db, ISalonService salonService)
        {
            _db = db;
            _salonService = salonService;
        }

        public async Task<bool> DeleteResponse(Guid id)
        {
            _db.Actual.Remove(await _db.Actual.Where(r => r.ActualId == id).FirstAsync());
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ActualBOL> GetFormData(Guid id)
        {
            return await _db.Actual.Where(f => f.ActualId == id)
                .Select(data => new ActualBOL
                {
                    Id = data.ActualId,
                    Timestamp = DateTime.Now,
                    ClientVisitsMonth = data.ClientVisitsMonth,
                    RebooksMonth = data.RebooksMonth,
                    NewClientsMonth = data.NewClientsMonth
                }).FirstOrDefaultAsync();
        }

        public async Task<List<ActualBOL>> GetFormDataAll()
        {
            return await _db.Actual.Select(data => new ActualBOL
            {
                Id = data.ActualId,
                Timestamp = DateTime.Now,
                ClientVisitsMonth = data.ClientVisitsMonth,
                RebooksMonth = data.RebooksMonth,
                NewClientsMonth = data.NewClientsMonth
            }).ToListAsync();
        }

        public async Task<bool> SubmitActualData(ActualBOL data)
        {
            // Add salon if not exists
            bool salonExists = await _salonService.SalonExists(data.SalonName);
            Guid salonId = Guid.NewGuid();
            if (salonExists)
            {
                salonId = await _salonService.GetSalonIdByName(data.SalonName);
            }
            else
            {
                Salon salon = new Salon
                {
                    SalonId = salonId,
                    ContactName = data.ContactName,
                    Email = data.Email,
                    Name = data.SalonName
                };
                await _db.Salon.AddAsync(salon);
                await _db.SaveChangesAsync();
            }

            // Add actual data
            Actual actual = new Actual
            {
                ActualId = data.Id,
                Timestamp = data.Timestamp,
                Month = data.Month,
                Year = data.Year,
                ClientVisitsLastYear = data.ClientVisitsLastYear,
                ClientVisitsMonth = data.ClientVisitsMonth,
                IndividualClientVisitsLastYear = data.IndividualClientVisitsLastYear,
                NewClientsMonth = data.NewClientsMonth,
                RebooksMonth = data.RebooksMonth,
                RetailMonth = data.RetailMonth,
                SalonId = salonId,
                TotalTakings = data.TotalTakings,
                WageBillMonth = data.WageBillMonth
            };
            await _db.Actual.AddAsync(actual);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
