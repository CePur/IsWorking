using CalisiyorMu.Data;
using CalisiyorMu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace CalisiyorMu.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IStudyData studyData;

        public bool IsAdmin => HttpContext.User.HasClaim("IsAdmin", bool.TrueString);

        [BindProperty]
        public Study Study { get; set; }

        public IndexModel(IStudyData studyData)
        {
            this.studyData = studyData;
        }

        public IActionResult OnGet()
        {
            Study = studyData.GetLast();

            if (Study == null)
            {
                studyData.Create();
                Study = studyData.GetLast();
            }


            return Page();
        }

        public IActionResult OnPost()
        {
            Study = studyData.GetLast();
            Study.IsWorking = false;
            Study.EndTime = DateTimeOffset.UtcNow;
            studyData.Update(Study);

            return RedirectToPage("../Index");
        }

        public IActionResult OnPostBaslat()
        {
            studyData.Create();
            return RedirectToPage("../Index");
        }

    }
}