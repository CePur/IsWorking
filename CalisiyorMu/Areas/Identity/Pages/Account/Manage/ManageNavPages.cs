using System;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CalisiyorMu.Areas.Identity.Pages.Account.Manage
{
    public static class ManageNavPages
    {
        public static string Index
        {
            get { return "Index"; }
        }

        public static string Email
        {
            get { return "Email"; }
        }

        public static string ChangePassword
        {
            get { return "ChangePassword"; }
        }

        public static string DownloadPersonalData
        {
            get { return "DownloadPersonalData"; }
        }

        public static string DeletePersonalData
        {
            get { return "DeletePersonalData"; }
        }

        public static string ExternalLogins
        {
            get { return "ExternalLogins"; }
        }

        public static string PersonalData
        {
            get { return "PersonalData"; }
        }

        public static string TwoFactorAuthentication
        {
            get { return "TwoFactorAuthentication"; }
        }

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);

        public static string DeletePersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeletePersonalData);

        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);



        private static string PageNavClass(ViewContext viewContext, string page)
        {
            string activePage = viewContext.ViewData["ActivePage"] as string
                                ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);

            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}