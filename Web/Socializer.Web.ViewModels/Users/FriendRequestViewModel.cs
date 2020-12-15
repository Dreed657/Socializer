namespace Socializer.Web.ViewModels.Users
{
    using System;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;

    public class FriendRequestViewModel : IMapFrom<FriendRequest>
    {
        public int Id { get; set; }

        public ApplicationUser Sender { get; set; }

        public ApplicationUser Receiver { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
