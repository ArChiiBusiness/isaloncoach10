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
                Responses = await _db.Responses.CountAsync(),
                Customers = await _db.Responses.Select(c => c.Email).Distinct().CountAsync(),
                LastResponse = await _db.Responses.OrderByDescending(d => d.Timestamp).Select(d => d.Timestamp).FirstOrDefaultAsync()
            };
        }
    }
}
