using CalisiyorMu.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalisiyorMu.Data
{
    public class SqlStudyData : IStudyData
    {
        private readonly CalisiyorMuDbContext dbContext;

        public SqlStudyData(CalisiyorMuDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public void Create()
        {
            Study newStudy = new Study
            {
                IsWorking = true,
                StartTime = DateTimeOffset.UtcNow
            };
            dbContext.Add(newStudy);
            dbContext.SaveChanges();

        }

        public void DeleteAll()
        {
            if (dbContext.Studies != null)
            {
                dbContext.Studies.RemoveRange(dbContext.Studies);
                dbContext.SaveChanges();
            }

        }

        public IEnumerable<Study> GetAll()
        {
            return dbContext.Studies;
        }

        public int PomodoroCount()
        {
            return dbContext.Studies.Where(s => s.StartTime.Date == DateTimeOffset.UtcNow.Date).Count();
        }

        public Study GetLast()
        {
            return dbContext.Studies.OrderBy(s => s.Id).LastOrDefault();
        }

        public void Update(Study updatedStudy)
        {
            var study = dbContext.Attach(updatedStudy);
            study.State = EntityState.Modified;
            dbContext.SaveChanges();

        }

        public double WeekAvarage()
        {
            var sevenDayAgo = DateTimeOffset.UtcNow.Add(-TimeSpan.FromDays(7));
            return dbContext.Studies.Where(s => s.StartTime.Date >= sevenDayAgo).Count() / 7.0;
        }
    }
}
