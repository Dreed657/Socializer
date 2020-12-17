namespace Socializer.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Users;
    using Socializer.Web.ViewModels.Common;

    public class UserPartialViewComponent : ViewComponent
    {
        private readonly IUserService userService;

        public UserPartialViewComponent(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var user = await this.userService.GetUserByIdAsync<UserPartialViewModel>(userId);
            return this.View(user);
        }
    }
}
