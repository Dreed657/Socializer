namespace Socializer.Web.Areas.Admin.Services
{
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Dashboard;

    public interface IDashboardService
    {
        Task<DbHomeViewModel> GetHomeData();
    }
}
