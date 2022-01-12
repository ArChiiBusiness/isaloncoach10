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
    public class StatisticsService : IStatisticsService
    {
        private ApplicationDbContext _db;

        public StatisticsService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<StatisticsBOL> GetStatistics()
        {
            return new StatisticsBOL
            {
                Actuals = await _db.Actual.CountAsync(),
                Salons = await _db.Salon.CountAsync(),
                LastResponse = await _db.Actual.OrderByDescending(d => d.Timestamp).Select(d => d.Timestamp).FirstOrDefaultAsync()
            };
        }
    }
}
