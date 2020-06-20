using CalisiyorMu.Data;
using CalisiyorMu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;

namespace CalisiyorMu.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStudyData studyData;

        public Study Study { get; set; }
        public int ElapsedMinutes { get; set; }
        public int ElapsedHours { get; set; }
        public int Pomodoro { get; set; }
        public string EndTimeTurkeyStr { get; set; }
        public DateTimeOffset StartTimeTurkey { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IStudyData studyData)
        {
            _logger = logger;
            this.studyData = studyData;
        }

        public IActionResult OnGet()
        {
            Study = studyData.GetLast();

            if (Study == null) { return RedirectToPage("./Admin/Index"); }


            var elapsedTime = DateTimeOffset.UtcNow - Study.StartTime;

            if (Study.IsWorking == true && elapsedTime > new TimeSpan(1, 0, 0))
            {
                OnPost();
            }

            Pomodoro = studyData.PomodoroCount();
            ElapsedHours = elapsedTime.Hours;
            ElapsedMinutes = elapsedTime.Minutes;


            TimeZoneInfo tst;

            tst = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

            DateTimeOffset EndTimeTurkey = TimeZoneInfo.ConvertTime(Study.EndTime, tst);

            EndTimeTurkeyStr = EndTimeTurkey.DateTime.ToString(new CultureInfo("tr-TR"));

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
