namespace Socializer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Socializer.Data.Common.Models;
    using Socializer.Data.Models.Enums;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Likes = new HashSet<PostLike>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        public string Content { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }

        [Required]
        public PrivacyStatus Privacy { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<PostLike> Likes { get; set; }
    }
}
