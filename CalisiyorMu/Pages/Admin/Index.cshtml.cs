using CalisiyorMu.Data;
using CalisiyorMu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using CalisiyorMu.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CalisiyorMu.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IStudyData studyData;
        private readonly IHubContext<WorkHub> workHub;

        public bool IsAdmin => HttpContext.User.HasClaim("IsAdmin", bool.TrueString);

        [BindProperty]
        public Study Study { get; set; }

        public IndexModel(IStudyData studyData,IHubContext<WorkHub> workHub)
        {
            this.studyData = studyData;
            this.workHub = workHub;
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

        //Stop
        public async Task<IActionResult> OnPostAsync()
        {
            Study = studyData.GetLast();
            Study.IsWorking = false;
            Study.EndTime = DateTimeOffset.UtcNow;
            studyData.Update(Study);
            await ChangeStatus();

            return RedirectToPage("../Index");
        }

        //Start
        public async Task<IActionResult> OnPostBaslatAsync()
        {
            studyData.Create();
            await ChangeStatus();
            return RedirectToPage("../Index");
        }

        public async Task ChangeStatus()
        {
            await workHub.Clients.All.SendAsync("ReceiveMessage");            
        }

    }
}