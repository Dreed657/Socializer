namespace Socializer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    using Socializer.Data.Common.Models;
    using Socializer.Data.Models.Enums;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Posts = new HashSet<Post>();
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<PostLike>();
            this.Friends = new HashSet<ApplicationUser>();
            this.Groups = new HashSet<GroupMember>();
        }

        // Custom Info
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public string ProfileImageId { get; set; }

        public virtual Image ProfileImage { get; set; }

        public string CoverImageId { get; set; }

        public virtual Image CoverImage { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<PostLike> Likes { get; set; }

        public virtual ICollection<ApplicationUser> Friends { get; set; }

        public virtual ICollection<GroupMember> Groups { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        // Identity info
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
