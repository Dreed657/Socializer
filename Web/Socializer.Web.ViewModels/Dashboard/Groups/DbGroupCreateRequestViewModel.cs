namespace Socializer.Web.ViewModels.Groups.Dashboard
{
    using System;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Data.Models.Enums;

    public class DbGroupCreateRequestViewModel : IMapFrom<GroupCreateRequest>
    {
        public int Id { get; set; }

        public ApplicationUser Creator { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
