namespace Socializer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Socializer.Data.Common.Models;
    using Socializer.Data.Models.Enums;

    public class FriendRequest : BaseModel<int>
    {
        public string SenderId { get; set; }

        /// <summary>
        /// Gets or sets logged in userId.
        /// </summary>
        [Required]
        public virtual ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets target userId.
        /// </summary>
        [Required]
        public virtual ApplicationUser Receiver { get; set; }

        public Status Status { get; set; }
    }
}
