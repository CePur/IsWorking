using CalisiyorMu.Data;
using CalisiyorMu.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CalisiyorMu.Pages
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudiesController : ControllerBase
    {
        private readonly IStudyData studyData;

        [BindProperty]
        public Study Study { get; set; }

        public StudiesController(IStudyData studyData)
        {
            this.studyData = studyData;
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

        //// GET: api/Studies/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        //START
        // POST: api/Studies/start
        [HttpPost("{value}")]
        public void Post(string value)
        {
            if (value == "start")
            {

                Study = studyData.GetLast();
                if (Study.IsWorking == true)
                {
                    Study.IsWorking = false;
                    Study.EndTime = DateTimeOffset.UtcNow;
                    studyData.Update(Study);
                }
                studyData.Create();
            }
        }
        //STOP
        // PUT: api/Studies/stop
        [HttpPut("{value}")]
        public void Put(string value)
        {
            Study = studyData.GetLast();
            if (value == "stop" && Study.IsWorking == true)
            {
                Study.IsWorking = false;
                Study.EndTime = DateTimeOffset.UtcNow;
                studyData.Update(Study);
            }
        }

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
