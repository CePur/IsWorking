using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CalisiyorMu.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CalisiyorMu.Areas.Identity.Pages.Account.Manage
{
    public class EnableAuthenticatorModel : PageModel
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ILogger<EnableAuthenticatorModel> logger;
        private readonly UrlEncoder urlEncoder;

        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";



        public EnableAuthenticatorModel(
            UserManager<AppUser> userManager,
            ILogger<EnableAuthenticatorModel> logger,
            UrlEncoder urlEncoder)
        {
            this.userManager = userManager;
            this.logger = logger;
            this.urlEncoder = urlEncoder;
        }



        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }

        [TempData]
        public string[] RecoveryCodes { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Verification Code")]
            public string Code { get; set; }
        }



        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            await LoadSharedKeyAndQrCodeUriAsync(user);

            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadSharedKeyAndQrCodeUriAsync(user);

                return Page();
            }

            // Strip spaces and hypens
            string verificationCode = Input.Code.Replace(" ", string.Empty).Replace("-", string.Empty);

            bool is2FaTokenValid = await userManager.VerifyTwoFactorTokenAsync(
                user,
                userManager.Options.Tokens.AuthenticatorTokenProvider,
                verificationCode);

            if (!is2FaTokenValid)
            {
                ModelState.AddModelError("Input.Code", "Verification code is invalid.");
                await LoadSharedKeyAndQrCodeUriAsync(user);

                return Page();
            }

            await userManager.SetTwoFactorEnabledAsync(user, true);
            string userId = await userManager.GetUserIdAsync(user);
            logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);

            StatusMessage = "Your authenticator app has been verified.";

            if (await userManager.CountRecoveryCodesAsync(user) == 0)
            {
                var recoveryCodes = await userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                RecoveryCodes = recoveryCodes.ToArray();

                return RedirectToPage("./ShowRecoveryCodes");
            }
            else
            {
                return RedirectToPage("./TwoFactorAuthentication");
            }
        }



        private async Task LoadSharedKeyAndQrCodeUriAsync(AppUser user)
        {
            // Load the authenticator key & QR code URI to display on the form
            string unformattedKey = await userManager.GetAuthenticatorKeyAsync(user);

            if (string.IsNullOrEmpty(unformattedKey))
            {
                await userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await userManager.GetAuthenticatorKeyAsync(user);
            }

            SharedKey = FormatKey(unformattedKey);

            string email = await userManager.GetEmailAsync(user);
            AuthenticatorUri = GenerateQrCodeUri(email, unformattedKey);
        }



        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;

            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }

            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }



        private string GenerateQrCodeUri(string email, string unformattedKey) =>
            string.Format(
                AuthenticatorUriFormat,
                urlEncoder.Encode("CalisiyorMu"),
                urlEncoder.Encode(email),
                unformattedKey);
    }
}