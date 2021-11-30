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

        public FormService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> DeleteResponse(Guid id)
        {
            _db.Responses.Remove(await _db.Responses.Where(r => r.Id == id).FirstAsync());
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<FormDataBOL> GetFormData(Guid id)
        {
            return await _db.Responses.Where(f => f.Id == id)
                .Select(data => new FormDataBOL
                {
                    Id = data.Id,
                    Timestamp = DateTime.Now,
                    Name = data.Name,
                    SalonName = data.SalonName,
                    Email = data.Email,
                    Month = data.Month,
                    TotalMonthlyTakings = data.TotalMonthlyTakings,
                    RetailMonthUSD = data.RetailMonthUSD,
                    WageBillMonthUSD = data.WageBillMonthUSD,
                    ClientVisitsMonth = data.ClientVisitsMonth,
                    TargetMonthUSD = data.TargetMonthUSD,
                    TargetClientsMonth = data.TargetClientsMonth,
                    RebooksMonth = data.RebooksMonth,
                    TotalClientVisitsYear = data.TotalClientVisitsYear,
                    TotalIndividualClientVisitsYear = data.TotalIndividualClientVisitsYear,
                    NewClientsMonth = data.NewClientsMonth,
                    PastYearTotalTakings = data.PastYearTotalTakings,
                    TotalClientsInDatabase = data.TotalClientsInDatabase
                }).FirstOrDefaultAsync();
        }

        public async Task<List<FormDataBOL>> GetFormDataAll()
        {
            return await _db.Responses.Select(data => new FormDataBOL
            {
                Id = data.Id,
                Timestamp = DateTime.Now,
                Name = data.Name,
                SalonName = data.SalonName,
                Email = data.Email,
                Month = data.Month,
                TotalMonthlyTakings = data.TotalMonthlyTakings,
                RetailMonthUSD = data.RetailMonthUSD,
                WageBillMonthUSD = data.WageBillMonthUSD,
                ClientVisitsMonth = data.ClientVisitsMonth,
                TargetMonthUSD = data.TargetMonthUSD,
                TargetClientsMonth = data.TargetClientsMonth,
                RebooksMonth = data.RebooksMonth,
                TotalClientVisitsYear = data.TotalClientVisitsYear,
                TotalIndividualClientVisitsYear = data.TotalIndividualClientVisitsYear,
                NewClientsMonth = data.NewClientsMonth,
                PastYearTotalTakings = data.PastYearTotalTakings,
                TotalClientsInDatabase = data.TotalClientsInDatabase
            }).ToListAsync();
        }

        public async Task<bool> SubmitData(FormDataBOL data)
        {
            FormData formData = new FormData
            {
                Id = data.Id,
                Timestamp = DateTime.Now,
                Name = data.Name,
                SalonName = data.SalonName,
                Email = data.Email,
                Month = data.Month,
                TotalMonthlyTakings = data.TotalMonthlyTakings,
                RetailMonthUSD = data.RetailMonthUSD,
                WageBillMonthUSD = data.WageBillMonthUSD,
                ClientVisitsMonth = data.ClientVisitsMonth,
                TargetMonthUSD = data.TargetMonthUSD,
                TargetClientsMonth = data.TargetClientsMonth,
                RebooksMonth = data.RebooksMonth,
                TotalClientVisitsYear = data.TotalClientVisitsYear,
                TotalIndividualClientVisitsYear = data.TotalIndividualClientVisitsYear,
                NewClientsMonth = data.NewClientsMonth,
                PastYearTotalTakings = data.PastYearTotalTakings,
                TotalClientsInDatabase = data.TotalClientsInDatabase
            };
            await _db.Responses.AddAsync(formData);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
