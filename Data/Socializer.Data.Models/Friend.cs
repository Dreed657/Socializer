namespace Socializer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Socializer.Data.Common.Models;

    public class Friend : BaseModel<int>
    {
        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }
    }
}
