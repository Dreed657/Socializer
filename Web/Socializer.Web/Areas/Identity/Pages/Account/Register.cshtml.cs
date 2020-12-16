namespace Socializer.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Messaging;

    [AllowAnonymous]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "<Pending>")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IRepository<Image> imageRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;

        public RegisterModel(
            IRepository<Image> imageRepo,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            this.imageRepo = imageRepo;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var user = new ApplicationUser
            {
                FirstName = this.Input.FirstName,
                LastName = this.Input.LastName,
                UserName = $"{this.Input.FirstName.ToLower()}.{this.Input.LastName.ToLower()}",
                Email = this.Input.Email,
                Gender = this.Input.Gender,
                Birthdate = this.Input.Birthdate,
                ProfileImage = await this.imageRepo.All().FirstOrDefaultAsync(x => x.Name == "Default_Profile"),
                CoverImage = await this.imageRepo.All().FirstOrDefaultAsync(x => x.Name == "Default_Cover"),
            };

            user.Posts.Add(new Post()
            {
                Content = $"Born on {user.Birthdate.ToShortDateString()}",
                Privacy = PrivacyStatus.InProfile,
            });

            var result = await this.userManager.CreateAsync(user, this.Input.Password);

            if (result.Succeeded)
            {
                this.logger.LogInformation("User created a new account with password.");

                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = this.Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    protocol: this.Request.Scheme);

                await this.emailSender.SendEmailAsync("accounts@socializer.com", "Accounts", this.Input.Email, "Confirm your email", $"<h1>Hello {this.Input.FirstName} thank you for your time!</h1><h3>Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.</h3>");

                if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                }
                else
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }
            }

            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }

            return this.Page();
        }

        public class InputModel
        {
            [Required]
            [MinLength(3, ErrorMessage = "Firstname must be a least 3 characters long.")]
            public string FirstName { get; set; }

            [Required]
            [MinLength(3, ErrorMessage = "Lastname must be a least 3 characters long.")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Birthday")]
            public DateTime Birthdate { get; set; }

            [Required]
            public Gender Gender { get; set; }
        }
    }
}
