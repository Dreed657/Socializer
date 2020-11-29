namespace Socializer.Web.ViewModels.Groups.Dashboard
{
    using System;
    using System.Collections.Generic;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Users;

    public class DbGroupViewModel : IMapFrom<Group>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<ShortGroupMemberViewModel> Members { get; set; }
    }
}
