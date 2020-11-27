namespace Socializer.Web.ViewModels.Users
{
    using AutoMapper;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Mapping;

    public class ShortGroupMemberViewModel : IMapFrom<GroupMember>
    {
        public string Id { get; set; }

        public string MemberUserName { get; set; }

        public string MemberFirstName { get; set; }

        public string MemberLastName { get; set; }

        public GroupRole Role { get; set; }
    }
}
