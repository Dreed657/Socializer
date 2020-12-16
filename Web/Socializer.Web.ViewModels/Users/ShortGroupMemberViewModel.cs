namespace Socializer.Web.ViewModels.Users
{
    using AutoMapper;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Mapping;

    public class ShortGroupMemberViewModel : IMapFrom<GroupMember>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string MemberUserName { get; set; }

        public string MemberFirstName { get; set; }

        public string MemberLastName { get; set; }

        public string ProfileImageUrl { get; set; }

        public GroupRole Role { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<GroupMember, ShortGroupMemberViewModel>()
                .ForMember(
                    x => x.ProfileImageUrl,
                    opt =>
                        opt.MapFrom(y => y.Member.ProfileImage.Url));
        }
    }
}
