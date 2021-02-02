using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CalisiyorMu.Areas.Identity.Data
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string NickName { get; set; }
    }
}