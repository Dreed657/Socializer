namespace Socializer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using Socializer.Data.Models;
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

        [HttpGet("/{groupName}/{groupId}")]
        public async Task<IActionResult> Index(string groupName, int groupId)
        {
            var group = await this.groupService.GetByIdAsync<GroupViewModel>(groupId);
            return this.View(group);
        }

        [HttpGet("group/{groupId}/Members")]
        public async Task<IActionResult> Members(int groupId)
        {
            var group = await this.groupService.GetByIdAsync<GroupMembersViewModel>(groupId);
            return this.View(group);
        }

        [HttpGet("/groups")]
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
