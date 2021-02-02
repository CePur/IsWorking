using System;
using System.Globalization;
using CalisiyorMu.Data;
using CalisiyorMu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CalisiyorMu.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly IStudyData studyData;

        public Study Study { get; set; }
        public int ElapsedMinutes { get; set; }
        public int ElapsedHours { get; set; }
        public int Pomodoro { get; set; }
        public string WeekAvg { get; set; }
        public string EndTimeTurkeyStr { get; set; }
        public DateTimeOffset StartTimeTurkey { get; set; }



        public IndexModel(ILogger<IndexModel> logger, IStudyData studyData)
        {
            this.logger = logger;
            this.studyData = studyData;
        }



        public IActionResult OnGet()
        {
            Study = studyData.GetLast();

            if (Study == null) return RedirectToPage("./Admin/Index");


            var elapsedTime = DateTimeOffset.UtcNow - Study.StartTime;

            if (Study.IsWorking && elapsedTime > new TimeSpan(1, 0, 0))
            {
                OnPost();
            }

            double weekAvg = studyData.WeekAvarage();
            WeekAvg = weekAvg.ToString(weekAvg % 1 == 0 ? "F0" : "F2");
            Pomodoro = studyData.PomodoroCount();
            ElapsedHours = elapsedTime.Hours;
            ElapsedMinutes = elapsedTime.Minutes;


            var tst = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

            var endTimeTurkey = TimeZoneInfo.ConvertTime(Study.EndTime, tst);

            EndTimeTurkeyStr = endTimeTurkey.DateTime.ToString(new CultureInfo("tr-TR"));

            StartTimeTurkey = TimeZoneInfo.ConvertTime(Study.StartTime, tst);

            return Page();
        }



        // çalışmayı durdur
        public IActionResult OnPost()
        {
            Study = studyData.GetLast();
            Study.IsWorking = false;
            Study.EndTime = DateTimeOffset.UtcNow;
            studyData.Update(Study);

            return RedirectToPage("../Index");
        }
    }
}