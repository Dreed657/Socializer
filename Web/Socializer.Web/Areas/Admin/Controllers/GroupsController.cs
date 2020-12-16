namespace Socializer.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Groups;
    using Socializer.Web.ViewModels.Dashboard.Groups;
    using Socializer.Web.ViewModels.Groups;
    using Socializer.Web.ViewModels.Groups.Dashboard;

    public class GroupsController : DashboardController
    {
        private readonly IGroupService groupService;

        public GroupsController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public async Task<IActionResult> All()
        {
            var models = await this.groupService.GetAllAsync<GroupShortViewModel>();
            return this.View(models);
        }

        public async Task<IActionResult> Details(int groupId)
        {
            var groupViewModel = await this.groupService.GetByIdAsync<DbGroupViewModel>(groupId);
            var dbComplexModel = new DbDetailGroupComplexModel() { ViewModel = groupViewModel };

            return this.View(dbComplexModel);
        }

        public async Task<IActionResult> Edit(DbDetailGroupComplexModel model, int groupId)
        {
            if (!this.ModelState.IsValid)
            {
                this.ModelState.AddModelError("123", "Model is not valid!");
            }

            if (!await this.groupService.DbUpdateGroup(model.InputModel, groupId))
            {
                this.TempData["Error"] = "Something went wrong with the request!";
            }

            return this.RedirectToAction(nameof(this.Details), new { groupId = groupId });
        }

        public async Task<IActionResult> Requests()
        {
            var models = await this.groupService.GetAllRequestsAsync<DbGroupCreateRequestViewModel>();
            return this.View(models);
        }

        public async Task<IActionResult> ApproveRequest(int requestId)
        {
            await this.groupService.ApproveRequestAsync(requestId);

            return this.RedirectToAction("Requests");
        }

        public async Task<IActionResult> DeclineRequest(int requestId)
        {
            await this.groupService.DeclineRequestAsync(requestId);

            return this.RedirectToAction("Requests");
        }
    }
}
