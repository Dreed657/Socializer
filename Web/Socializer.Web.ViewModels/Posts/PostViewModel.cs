using Socializer.Data.Models.Enums;

namespace Socializer.Web.ViewModels.Posts
{
    using System;
    using System.Collections.Generic;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Users;

    public class PostViewModel : IMapFrom<Post>
    {
        public PostViewModel()
        {
            this.Comments = new HashSet<CommentViewModel>();
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ApplicationUser Creator { get; set; }

        public Group Group { get; set; }

        public Image Image { get; set; }

        public bool InGroup => this.Group != null;

        public PrivacyStatus Privacy { get; set; }

        public int CommentsCount => this.Comments.Count;

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
