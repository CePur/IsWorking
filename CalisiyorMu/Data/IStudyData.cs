using System.Collections.Generic;
using CalisiyorMu.Models;

namespace CalisiyorMu.Data
{
    public interface IStudyData
    {
        Study GetLast();

        IEnumerable<Study> GetAll();

        void Update(Study updatedStudy);

        void Create();

        void DeleteAll();

        int Commit();

        int PomodoroCount();

        double WeekAvarage();
    }
}