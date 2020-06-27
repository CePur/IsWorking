using CalisiyorMu.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalisiyorMu.Data
{
    public class InMemoryStudyData : IStudyData
    {
        private readonly List<Study> studies;

        public InMemoryStudyData()
        {
            studies = new List<Study>()
            {
                new Study{ Id=1, IsWorking= false, StartTime = new DateTimeOffset(2020,5,17,20,0,0,TimeSpan.Zero) },
                new Study{ Id=2, IsWorking= false, StartTime = new DateTimeOffset(2020,5,17,21,0,0,TimeSpan.Zero) },
                new Study{ Id=3, IsWorking= false, StartTime = new DateTimeOffset(2020,5,17,22,0,0,TimeSpan.Zero) }
            };
        }



        public IEnumerable<Study> GetAll()
        {
            return studies;
        }

        public Study GetLast()
        {
            return studies.Last();
        }

        public void Create()
        {
            Study newStudy = new Study
            {
                Id = studies.Max(s => s.Id) + 1,
                IsWorking = true,
                StartTime = DateTimeOffset.UtcNow
            };
            studies.Add(newStudy);

        }


        public void Update(Study updatedStudy)
        {
            var study = studies.SingleOrDefault(s => s.Id == updatedStudy.Id);
            if (study != null)
            {
                study.Id = updatedStudy.Id;
                study.StartTime = updatedStudy.StartTime;
                study.IsWorking = false;
            }

        }

        public int Commit()
        {
            return 0;
        }

        public void DeleteAll()
        {

            studies.Clear();

        }

        public int PomodoroCount()
        {
            throw new NotImplementedException();
        }

        public double WeekAvarage()
        {
            throw new NotImplementedException();
        }
    }
}
