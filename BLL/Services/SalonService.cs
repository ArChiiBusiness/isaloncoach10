using BLL.Interfaces;
using BOL;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SalonService : ISalonService
    {
        public ApplicationDbContext _db;

        public SalonService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<SalonBOL>> GetAll()
        {
            return await _db.Salon
                 .Select(s => new SalonBOL
                 {
                     Id = s.SalonId,
                     Name = s.Name,
                     ContactName = s.ContactName,
                     Email = s.Email
                 }).ToListAsync();
        }

        public async Task<Guid> AddSalon(SalonBOL salon)
        {
            Salon salonDAL = new Salon
            {
                SalonId = salon.Id,
                ContactName = salon.ContactName,
                Name = salon.Name,
                Email = salon.Email
            };
            await _db.Salon.AddAsync(salonDAL);
            await _db.SaveChangesAsync();
            return salonDAL.SalonId;
        }

        public async Task<SalonBOL> GetSalonByName(string salonName)
        {
            return await _db.Salon.Where(s => s.Name == salonName)
                .Select(s => new SalonBOL
                {
                    Id = s.SalonId,
                    Name = s.Name,
                    ContactName = s.ContactName,
                    Email = s.Email
                }).FirstOrDefaultAsync();
        }

        public async Task<Guid> GetSalonIdByName(string salonName)
        {
            return await _db.Salon.Where(s => s.Name == salonName)
                .Select(s => s.SalonId).FirstOrDefaultAsync();
        }

        public async Task<string> GetSalonNameById(Guid salonId)
        {
            return await _db.Salon.Where(s => s.SalonId == salonId)
             .Select(s => s.Name).FirstOrDefaultAsync();
        }

        public async Task<bool> SalonExists(string salonName)
        {
            return await _db.Salon.Where(s => s.Name == salonName).AnyAsync();
        }

        public async Task<bool> DeleteSalon(Guid salonId)
        {
            _db.Actual.RemoveRange(await _db.Actual.Where(a => a.SalonId == salonId).ToListAsync());
            _db.Target.RemoveRange(await _db.Target.Where(t => t.SalonId == salonId).ToListAsync());
            _db.Salon.Remove(await _db.Salon.Where(s => s.SalonId == salonId).FirstOrDefaultAsync());
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<SalonBOL> GetSalonById(Guid salonId)
        {
            return await _db.Salon.Where(s => s.SalonId == salonId)
                 .Select(s => new SalonBOL
                 {
                     Id = s.SalonId,
                     Name = s.Name,
                     ContactName = s.ContactName,
                     Email = s.Email
                 }).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateSalon(SalonBOL salon)
        {
            Salon salonDAL = await _db.Salon.Where(s => s.SalonId == salon.Id).FirstOrDefaultAsync();
            salonDAL.Name = salon.Name;
            salonDAL.ContactName = salon.ContactName;
            salonDAL.Email = salon.Email;
            _db.Salon.Update(salonDAL);
            await _db.SaveChangesAsync();
            return true;
        }

        // Actuals
        public async Task<Guid> AddActualData(ActualBOL actual, Guid salonId)
        {
            // Add actual data
            Actual actualDAL = new Actual
            {
                ActualId = actual.Id,
                Timestamp = actual.Timestamp,
                Month = actual.Month,
                Year = actual.Year,
                ClientVisitsLastYear = actual.ClientVisitsLastYear,
                ClientVisitsMonth = actual.ClientVisitsMonth,
                IndividualClientVisitsLastYear = actual.IndividualClientVisitsLastYear,
                NewClientsMonth = actual.NewClientsMonth,
                RebooksMonth = actual.RebooksMonth,
                RetailMonth = actual.RetailMonth,
                SalonId = salonId,
                TotalTakings = actual.TotalTakings,
                WageBillMonth = actual.WageBillMonth,
                AverageBill = actual.AverageBill
            };
            await _db.Actual.AddAsync(actualDAL);
            await _db.SaveChangesAsync();
            return actualDAL.ActualId;
        }

        public async Task<List<ActualBOL>> GetActuals(Guid salonId)
        {
            return await _db.Actual.Where(a => a.SalonId == salonId)
                .OrderByDescending(a => a.Year)
                .ThenByDescending(a => a.Month)
                .Select(actual => new ActualBOL
                {
                    Id = actual.ActualId,
                    SalonId = actual.SalonId,
                    ClientVisitsLastYear = actual.ClientVisitsLastYear,
                    ClientVisitsMonth = actual.ClientVisitsMonth,
                    ContactName = actual.Salon.ContactName,
                    Email = actual.Salon.Email,
                    Month = actual.Month,
                    IndividualClientVisitsLastYear = actual.IndividualClientVisitsLastYear,
                    NewClientsMonth = actual.NewClientsMonth,
                    RebooksMonth = actual.RebooksMonth,
                    RetailMonth = actual.RetailMonth,
                    SalonName = actual.Salon.Name,
                    Timestamp = actual.Timestamp,
                    TotalTakings = actual.TotalTakings,
                    WageBillMonth = actual.WageBillMonth,
                    Year = actual.Year,
                    WagePercent = (actual.TotalTakings != 0) ? Math.Round((actual.WageBillMonth / actual.TotalTakings) * 100, 2) : 0,
                    RetailPercent = (actual.TotalTakings != 0) ? Math.Round((actual.RetailMonth / actual.TotalTakings) * 100, 2) : 0,
                    AverageBill = actual.AverageBill,
                    TotalTakingsYear = Math.Round(_db.Actual
                    .Where(a => a.Year == actual.Year && a.Month <= actual.Month)
                    .Select(a => a.TotalTakings).Sum() + _db.Target
                    .Where(t => t.SalonId == actual.SalonId && t.Year == actual.Year)
                    .Select(t => t.TotalYearTarget).FirstOrDefault() / 12 * (12 - actual.Month), 2),
                    AverageClientVisitsYear = (actual.IndividualClientVisitsLastYear != 0) ? Math.Round(actual.ClientVisitsLastYear / actual.IndividualClientVisitsLastYear, 2) : 0,
                    WeeksBetweenAppointments = (actual.IndividualClientVisitsLastYear != 0) ? Math.Round(52 / (Math.Round(actual.ClientVisitsLastYear / actual.IndividualClientVisitsLastYear, 2)), 2) : 0
                }).ToListAsync();
        }

        public async Task<ActualBOL> GetActualById(Guid actualId)
        {
            return await _db.Actual.Where(a => a.ActualId == actualId)
                .Select(actual => new ActualBOL
                {
                    Id = actual.ActualId,
                    SalonId = actual.SalonId,
                    ClientVisitsLastYear = actual.ClientVisitsLastYear,
                    ClientVisitsMonth = actual.ClientVisitsMonth,
                    ContactName = actual.Salon.ContactName,
                    Email = actual.Salon.Email,
                    Month = actual.Month,
                    IndividualClientVisitsLastYear = actual.IndividualClientVisitsLastYear,
                    NewClientsMonth = actual.NewClientsMonth,
                    RebooksMonth = actual.RebooksMonth,
                    RetailMonth = actual.RetailMonth,
                    SalonName = actual.Salon.Name,
                    Timestamp = actual.Timestamp,
                    TotalTakings = actual.TotalTakings,
                    WageBillMonth = actual.WageBillMonth,
                    Year = actual.Year,
                    WagePercent = (actual.TotalTakings != 0) ? Math.Round((actual.WageBillMonth / actual.TotalTakings) * 100, 2) : 0,
                    RetailPercent = (actual.TotalTakings != 0) ? Math.Round((actual.RetailMonth / actual.TotalTakings) * 100, 2) : 0,
                    AverageBill = actual.AverageBill,
                    TotalTakingsYear = Math.Round(_db.Actual
                    .Where(a => a.Year == actual.Year && a.Month <= actual.Month)
                    .Select(a => a.TotalTakings).Sum() + _db.Target
                    .Where(t => t.SalonId == actual.SalonId && t.Year == actual.Year)
                    .Select(t => t.TotalYearTarget).FirstOrDefault() / 12 * (12 - actual.Month), 2),
                    AverageClientVisitsYear = (actual.IndividualClientVisitsLastYear != 0) ? Math.Round(actual.ClientVisitsLastYear / actual.IndividualClientVisitsLastYear, 2) : 0,
                    WeeksBetweenAppointments = (actual.IndividualClientVisitsLastYear != 0) ? Math.Round(52 / (Math.Round(actual.ClientVisitsLastYear / actual.IndividualClientVisitsLastYear, 2)), 2) : 0
                }).FirstOrDefaultAsync();
        }

        public async Task<ActualBOL> GetActualByMonthYear(Guid salonId, int Year, int Month)
        {
            bool actualExists = await _db.Actual
                .Where(a => a.SalonId == salonId && a.Month == Month && a.Year == Year).AnyAsync();

            if (!actualExists)
            {
                return null;
            }
            else
            {
                return await _db.Actual.Where(a => a.SalonId == salonId && a.Month == Month && a.Year == Year)
                .Select(actual => new ActualBOL
                {
                    Id = actual.ActualId,
                    SalonId = actual.SalonId,
                    ClientVisitsLastYear = actual.ClientVisitsLastYear,
                    ClientVisitsMonth = actual.ClientVisitsMonth,
                    ContactName = actual.Salon.ContactName,
                    Email = actual.Salon.Email,
                    Month = actual.Month,
                    IndividualClientVisitsLastYear = actual.IndividualClientVisitsLastYear,
                    NewClientsMonth = actual.NewClientsMonth,
                    RebooksMonth = actual.RebooksMonth,
                    RetailMonth = actual.RetailMonth,
                    SalonName = actual.Salon.Name,
                    Timestamp = actual.Timestamp,
                    TotalTakings = actual.TotalTakings,
                    WageBillMonth = actual.WageBillMonth,
                    Year = actual.Year,
                    WagePercent = (actual.TotalTakings != 0) ? Math.Round((actual.WageBillMonth / actual.TotalTakings) * 100, 2) : 0,
                    RetailPercent = (actual.TotalTakings != 0) ? Math.Round((actual.RetailMonth / actual.TotalTakings) * 100, 2) : 0,
                    AverageBill = actual.AverageBill,
                    TotalTakingsYear = Math.Round(_db.Actual
                    .Where(a => a.Year == actual.Year && a.Month <= actual.Month)
                    .Select(a => a.TotalTakings).Sum() + _db.Target
                    .Where(t => t.SalonId == actual.SalonId && t.Year == actual.Year)
                    .Select(t => t.TotalYearTarget).FirstOrDefault() / 12 * (12 - actual.Month), 2),
                    AverageClientVisitsYear = (actual.IndividualClientVisitsLastYear != 0) ? Math.Round(actual.ClientVisitsLastYear / actual.IndividualClientVisitsLastYear, 2) : 0,
                    WeeksBetweenAppointments = (actual.IndividualClientVisitsLastYear != 0) ? Math.Round(52 / (Math.Round(actual.ClientVisitsLastYear / actual.IndividualClientVisitsLastYear, 2)), 2) : 0
                }).FirstOrDefaultAsync();
            }

        }

        public async Task<bool> DeleteActual(Guid actualId)
        {
            _db.Actual.Remove(await _db.Actual.Where(a => a.ActualId == actualId).FirstOrDefaultAsync());
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ActualBOL>> GetAllActuals()
        {
            return await _db.Actual
                .Select(actual => new ActualBOL
                {
                    Id = actual.ActualId,
                    SalonId = actual.SalonId,
                    ClientVisitsLastYear = actual.ClientVisitsLastYear,
                    ClientVisitsMonth = actual.ClientVisitsMonth,
                    ContactName = actual.Salon.ContactName,
                    Email = actual.Salon.Email,
                    Month = actual.Month,
                    IndividualClientVisitsLastYear = actual.IndividualClientVisitsLastYear,
                    NewClientsMonth = actual.NewClientsMonth,
                    RebooksMonth = actual.RebooksMonth,
                    RetailMonth = actual.RetailMonth,
                    SalonName = actual.Salon.Name,
                    Timestamp = actual.Timestamp,
                    TotalTakings = actual.TotalTakings,
                    WageBillMonth = actual.WageBillMonth,
                    Year = actual.Year,
                    WagePercent = (actual.TotalTakings != 0) ? Math.Round((actual.WageBillMonth / actual.TotalTakings) * 100, 2) : 0,
                    RetailPercent = (actual.TotalTakings != 0) ? Math.Round((actual.RetailMonth / actual.TotalTakings) * 100, 2) : 0,
                    AverageBill = actual.AverageBill,
                    TotalTakingsYear = Math.Round(_db.Actual
                    .Where(a => a.Year == actual.Year && a.Month <= actual.Month)
                    .Select(a => a.TotalTakings).Sum() + _db.Target
                    .Where(t => t.SalonId == actual.SalonId && t.Year == actual.Year)
                    .Select(t => t.TotalYearTarget).FirstOrDefault() /  12 * (12 - actual.Month), 2),
                    AverageClientVisitsYear = (actual.IndividualClientVisitsLastYear != 0) ? Math.Round(actual.ClientVisitsLastYear / actual.IndividualClientVisitsLastYear, 2) : 0,
                    WeeksBetweenAppointments = (actual.IndividualClientVisitsLastYear != 0) ? Math.Round(52 / (Math.Round(actual.ClientVisitsLastYear / actual.IndividualClientVisitsLastYear, 2)), 2) : 0
                }).ToListAsync();
        }

        //Target
        public async Task<Guid> AddTargetData(TargetBOL target, Guid salonId)
        {
            target.AverageClientVisitsYear = Math.Round(target.ClientVisitsLastYear / target.IndividualClientVisitsLastYear, 2);
            target.WeeksBetweenAppointments = Math.Round(52 / target.AverageClientVisitsYear, 2);
            Target targetDAL = new Target
            {
                ClientVisitsLastYear = target.ClientVisitsLastYear,
                ClientVisitsMonth = target.ClientVisitsMonth,
                IndividualClientVisitsLastYear = target.IndividualClientVisitsLastYear,
                NewClientsMonth = target.NewClientsMonth,
                RebooksMonth = target.RebooksMonth,
                RetailMonth = target.RetailMonth,
                SalonId = salonId,
                TargetId = target.Id,
                TotalTakings = target.TotalTakings,
                WageBillMonth = target.WageBillMonth,
                Year = target.Year,
                AverageBill = target.AverageBill,
                AverageClientVisitsYear = target.AverageClientVisitsYear,
                RetailPercent = target.RetailPercent,
                TotalYearTarget = target.TotalYearTarget,
                WagePercent = target.WagePercent,
                WeeksBetweenAppointments = target.WeeksBetweenAppointments
            };
            await _db.Target.AddAsync(targetDAL);
            await _db.SaveChangesAsync();
            return targetDAL.TargetId;
        }

        public async Task<List<TargetBOL>> GetTargets(Guid salonId)
        {
            return await _db.Target.Where(a => a.SalonId == salonId)
                .OrderByDescending(a => a.Year)
                .Select(target => new TargetBOL
                {
                    Id = target.TargetId,
                    SalonId = salonId,
                    ClientVisitsLastYear = target.ClientVisitsLastYear,
                    ClientVisitsMonth = target.ClientVisitsMonth,
                    IndividualClientVisitsLastYear = target.IndividualClientVisitsLastYear,
                    NewClientsMonth = target.NewClientsMonth,
                    RebooksMonth = target.RebooksMonth,
                    RetailMonth = target.RetailMonth,
                    SalonName = target.Salon.Name,
                    TotalTakings = target.TotalTakings,
                    WageBillMonth = target.WageBillMonth,
                    Year = target.Year,
                    WeeksBetweenAppointments = target.WeeksBetweenAppointments,
                    AverageBill = target.AverageBill,
                    AverageClientVisitsYear = target.AverageClientVisitsYear,
                    RetailPercent = target.RetailPercent,
                    TotalYearTarget = target.TotalYearTarget,
                    WagePercent = target.WagePercent
                }).ToListAsync();
        }

        public async Task<TargetBOL> GetTargetById(Guid targetId)
        {
            return await _db.Target.Where(a => a.TargetId == targetId)
                .Select(target => new TargetBOL
                {
                    Id = target.TargetId,
                    SalonId = target.SalonId,
                    ClientVisitsLastYear = target.ClientVisitsLastYear,
                    ClientVisitsMonth = target.ClientVisitsMonth,
                    IndividualClientVisitsLastYear = target.IndividualClientVisitsLastYear,
                    NewClientsMonth = target.NewClientsMonth,
                    RebooksMonth = target.RebooksMonth,
                    RetailMonth = target.RetailMonth,
                    SalonName = target.Salon.Name,
                    TotalTakings = target.TotalTakings,
                    WageBillMonth = target.WageBillMonth,
                    Year = target.Year,
                    WeeksBetweenAppointments = target.WeeksBetweenAppointments,
                    AverageBill = target.AverageBill,
                    AverageClientVisitsYear = target.AverageClientVisitsYear,
                    RetailPercent = target.RetailPercent,
                    TotalYearTarget = target.TotalYearTarget,
                    WagePercent = target.WagePercent
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTarget(Guid targetId)
        {
            _db.Target.Remove(await _db.Target.Where(t => t.TargetId == targetId).FirstOrDefaultAsync());
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<TargetBOL> GetTargetByYear(Guid salonId, int Year)
        {
            bool targetExists = await _db.Target
                .Where(t => t.SalonId == salonId && t.Year == Year).AnyAsync();

            if (!targetExists)
            {
                return null;
            }
            else
            {
                return await _db.Target.Where(t => t.SalonId == salonId && t.Year == Year)
                    .Select(target => new TargetBOL
                    {
                        Id = target.TargetId,
                        SalonId = target.SalonId,
                        ClientVisitsLastYear = target.ClientVisitsLastYear,
                        ClientVisitsMonth = target.ClientVisitsMonth,
                        IndividualClientVisitsLastYear = target.IndividualClientVisitsLastYear,
                        NewClientsMonth = target.NewClientsMonth,
                        RebooksMonth = target.RebooksMonth,
                        RetailMonth = target.RetailMonth,
                        SalonName = target.Salon.Name,
                        TotalTakings = target.TotalTakings,
                        WageBillMonth = target.WageBillMonth,
                        Year = target.Year,
                        WeeksBetweenAppointments = target.WeeksBetweenAppointments,
                        AverageBill = target.AverageBill,
                        AverageClientVisitsYear = target.AverageClientVisitsYear,
                        RetailPercent = target.RetailPercent,
                        TotalYearTarget = target.TotalYearTarget,
                        WagePercent = target.WagePercent
                    }).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> UpdateTarget(TargetBOL target)
        {
            target.AverageClientVisitsYear = Math.Round(target.ClientVisitsLastYear / target.IndividualClientVisitsLastYear, 2);
            target.WeeksBetweenAppointments = Math.Round(52 / target.AverageClientVisitsYear, 2);

            Target targetDAL = await _db.Target.Where(t => t.TargetId == target.Id).FirstOrDefaultAsync();
            targetDAL.Year = target.Year;
            targetDAL.WageBillMonth = target.WageBillMonth;
            targetDAL.TotalTakings = target.TotalTakings;
            targetDAL.RetailMonth = target.RetailMonth;
            targetDAL.RebooksMonth = target.RebooksMonth;
            targetDAL.NewClientsMonth = target.NewClientsMonth;
            targetDAL.IndividualClientVisitsLastYear = target.IndividualClientVisitsLastYear;
            targetDAL.ClientVisitsMonth = target.ClientVisitsMonth;
            targetDAL.ClientVisitsLastYear = target.ClientVisitsLastYear;
            targetDAL.WeeksBetweenAppointments = target.WeeksBetweenAppointments;
            targetDAL.WagePercent = target.WagePercent;
            targetDAL.TotalYearTarget = target.TotalYearTarget;
            targetDAL.AverageClientVisitsYear = target.AverageClientVisitsYear;
            targetDAL.AverageBill = target.AverageBill;
            targetDAL.RetailPercent = target.RetailPercent;
            _db.Target.Update(targetDAL);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
