namespace Socializer.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Posts;

    public class ProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }

        public DateTime Birthdate { get; set; }

        public ICollection<PostViewModel> Posts { get; set; }
    }
}
