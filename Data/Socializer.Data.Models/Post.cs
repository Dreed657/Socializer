namespace Socializer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Socializer.Data.Common.Models;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Likes = new HashSet<PostLike>();
        }

        public string Content { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<PostLike> Likes { get; set; }
    }
}
