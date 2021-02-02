using System;
using System.Threading.Tasks;
using CalisiyorMu.Data;
using CalisiyorMu.Hubs;
using CalisiyorMu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CalisiyorMu.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudiesController : ControllerBase
    {
        private readonly IStudyData studyData;
        private readonly IHubContext<WorkHub> workHub;

        [BindProperty]
        public Study Study { get; set; }



        public StudiesController(IStudyData studyData, IHubContext<WorkHub> workHub)
        {
            this.studyData = studyData;
            this.workHub = workHub;
        }



        // GET: api/Studies
        [HttpGet]
        public Study Get()
        {
            Study = studyData.GetLast();

            if (Study == null)
            {
                studyData.Create();
                Study = studyData.GetLast();
            }

            return Study;
        }



        //START
        // POST: api/Studies/start
        [HttpPost("{value}")]
        public async void Post(string value)
        {
            if (value == "start")
            {
                Study = studyData.GetLast();

                if (Study.IsWorking)
                {
                    Study.IsWorking = false;
                    Study.EndTime = DateTimeOffset.UtcNow;
                    studyData.Update(Study);
                }

                studyData.Create();
                await ChangeStatus();
            }
        }



        //STOP
        // PUT: api/Studies/stop
        [HttpPut("{value}")]
        public async void Put(string value)
        {
            Study = studyData.GetLast();

            if (value == "stop" && Study.IsWorking)
            {
                Study.IsWorking = false;
                await ChangeStatus();
                Study.EndTime = DateTimeOffset.UtcNow;
                studyData.Update(Study);
            }
        }



        public async Task ChangeStatus()
        {
            await workHub.Clients.All.SendAsync("ReceiveMessage");
        }
    }
}