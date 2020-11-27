namespace Socializer.Web.ViewModels.Dashboard.Users
{
    using System.Linq;

    using AutoMapper;
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Common;

    public class DbUserInputModel : EditUserInputModel, IMapFrom<ApplicationUser>
    {
        public bool IsVerified { get; set; }
    }
}
