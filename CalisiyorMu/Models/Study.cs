using System;

namespace CalisiyorMu.Models
{
    public class Study
    {
        public int Id { get; set; }
        public bool IsWorking { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}