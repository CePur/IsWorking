using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CalisiyorMu.Areas.Identity.Data
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string NickName { get; set; }
    }
}