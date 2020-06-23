using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalisiyorMu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalisiyorMu.Controllers
{

    /// <summary>
    /// This is a controller to expose an admin registration key. Note that this controller 
    /// allows anonymous access, and this mechanism SHOULD NOT be used for production code 
    /// without securing this endpoint!!! 
    /// </summary>
    /// 
    [Authorize(Policy = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class AdminTokenController : ControllerBase
    {
        private readonly AdminRegistrationTokenService _adminService;

        public AdminTokenController(AdminRegistrationTokenService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public ActionResult<ulong> Get() => _adminService.CreationKey;
    }
}

