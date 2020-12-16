﻿namespace Socializer.Web.ViewModels.Users
{
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;

    public class ShortUserViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}
