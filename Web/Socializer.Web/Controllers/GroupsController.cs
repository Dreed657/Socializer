using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Socializer.Data.Models;

namespace Socializer.Web.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Groups;
    using Socializer.Web.ViewModels.Groups;

    public class GroupsController : Controller
    {
        private readonly IGroupService groupService;
        private readonly UserManager<ApplicationUser> userManger;

        public GroupsController(IGroupService groupService, UserManager<ApplicationUser> userManger)
        {
            this.groupService = groupService;
            this.userManger = userManger;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await this.groupService.GetAll<GroupShortViewModel>();
            return this.View(groups);
        }

        public async Task<IActionResult> CreateRequest(GroupRequestInputModel model, string returnUrl)
        {
            var user = await this.userManger.GetUserAsync(this.User);
            var result = await this.groupService.CreateGroupRequest(model, user);

            return this.Redirect(returnUrl);
        }
    }
}
