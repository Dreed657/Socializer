namespace Socializer.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;

    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Images;
    using Socializer.Web.ViewModels.Posts;

    public class ProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public DateTime Birthdate { get; set; }

        public DateTime CreatedOn { get; set; }

        public Gender Gender { get; set; }

        public string ProfileImageUrl { get; set; }

        public string CoverImageUrl { get; set; }

        public ICollection<PostViewModel> Posts { get; set; }
    }
}
