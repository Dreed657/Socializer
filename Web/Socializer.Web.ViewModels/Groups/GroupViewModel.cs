namespace Socializer.Web.ViewModels.Groups
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Posts;

    public class GroupViewModel : IMapFrom<Group>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int MembersCount { get; set; }

        public string CoverImageUrl { get; set; }

        public ICollection<PostViewModel> Posts { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Group, GroupViewModel>()
                .ForMember(
                    x => x.MembersCount,
                    opt => opt.MapFrom(y => y.Members.Count));
        }
    }
}
