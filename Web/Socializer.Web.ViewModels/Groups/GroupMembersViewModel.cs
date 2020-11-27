namespace Socializer.Web.ViewModels.Groups
{
    using System.Collections.Generic;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Users;

    public class GroupMembersViewModel : IMapFrom<Group>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MembersCount => this.Members.Count;

        public ICollection<ShortGroupMemberViewModel> Members { get; set; }
    }
}
