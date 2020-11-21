namespace Socializer.Web.ViewModels.Groups
{
    using System;

    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Mapping;

    public class GroupShortViewModel : IMapFrom<Group>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
