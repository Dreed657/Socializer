namespace Socializer.Data.Models
{
    using Socializer.Data.Common.Models;
    using Socializer.Data.Models.Enums;

    public class FriendRequest : BaseModel<int>
    {
        public string SenderId { get; set; }

        /// <summary>
        /// Gets or sets logged in userId.
        /// </summary>
        public ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets target userId.
        /// </summary>
        public ApplicationUser Receiver { get; set; }

        public Status Status { get; set; }
    }
}
