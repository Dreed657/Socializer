namespace Socializer.Web.Areas.Admin.Services.Common
{
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Dashboard;

    public interface IDashboardService
    {
        Task<DbHomeViewModel> GetHomeData();
    }
}
