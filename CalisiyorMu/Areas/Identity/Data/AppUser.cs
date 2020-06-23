using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CalisiyorMu.Areas.Identity
{
    public class AppUser : IdentityUser
    {

        [Required]
        [MaxLength(100)]
        public string NickName { get; set; }
    }
}
