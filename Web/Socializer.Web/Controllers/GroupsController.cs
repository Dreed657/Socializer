using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
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

        public async Task<IActionResult> Index(int groupId)
        {
            var group = await this.groupService.GetByIdAsync<GroupViewModel>(groupId);
            return this.View(group);
        }

        public async Task<IActionResult> Discover()
        {
            var groups = await this.groupService.GetAllAsync<GroupShortViewModel>();
            return this.View(groups);
        }

        public async Task<IActionResult> CreateRequest(GroupRequestInputModel model, string returnUrl)
        {
            var user = await this.userManger.GetUserAsync(this.User);
            var result = await this.groupService.CreateGroupRequestAsync(model, user);

            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> JoinGroup(int groupId, string returnUrl)
        {
            var userId = this.userManger.GetUserId(this.User);
            await this.groupService.AddMemberToGroupAsync(groupId, userId);

            return this.Redirect(returnUrl);
        }
    }
}
