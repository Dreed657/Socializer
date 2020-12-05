namespace Socializer.Web.ViewModels.Posts
{
    using System;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public string Content { get; set; }

        public string CreatorUserName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
