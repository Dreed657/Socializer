namespace Socializer.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Groups;
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
            var models = await this.groupService.GetAll<GroupShortViewModel>();
            return this.View(models);
        }

        public async Task<IActionResult> Details(int id)
        {
            var group = await this.groupService.GetRequestById<DashboardGroupViewModel>(id);
            return this.View(group);
        }

        public async Task<IActionResult> Requests()
        {
            var models = await this.groupService.GetAllRequests<GroupCreateRequestViewModel>();
            return this.View(models);
        }

        public async Task<IActionResult> ApproveRequest(int requestId)
        {
            await this.groupService.ApproveRequest(requestId);

            return this.RedirectToAction("Requests");
        }

        public async Task<IActionResult> DeclineRequest(int requestId)
        {
            await this.groupService.DeclineRequest(requestId);

            return this.RedirectToAction("Requests");
        }
    }
}
