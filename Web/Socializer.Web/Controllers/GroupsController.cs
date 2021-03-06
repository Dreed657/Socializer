﻿namespace Socializer.Web.Controllers
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

        [Route("group/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var group = await this.groupService.GetByIdAsync<GroupViewModel>(id);

            var model = new GroupIndexComplexModel()
            {
                ViewModel = group,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GroupIndexComplexModel model, int id)
        {
            var result = await this.groupService.UpdateGroup(model.InputModel, id, this.userManger.GetUserId(this.User));

            return this.RedirectToAction(nameof(this.Index), new { id });
        }

        public async Task<IActionResult> Members(int groupId)
        {
            var group = await this.groupService.GetByIdAsync<GroupMembersViewModel>(groupId);
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

        [HttpPost]
        public async Task<IActionResult> JoinGroup(int groupId)
        {
            var userId = this.userManger.GetUserId(this.User);
            var groupName = await this.groupService.AddMemberToGroupAsync(groupId, userId);

            return this.RedirectToAction(nameof(this.Index), new { Id = groupId });
        }
    }
}
