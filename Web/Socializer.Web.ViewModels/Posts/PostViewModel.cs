namespace Socializer.Web.ViewModels.Posts
{
    using System;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Users;

    public class PostViewModel : IMapFrom<Post>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}
