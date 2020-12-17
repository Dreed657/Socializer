namespace Socializer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Socializer.Data.Common.Models;

    public class ChatMessage : BaseModel<int>
    {
        [Required]
        public string Content { get; set; }

        public int ChatGroupId { get; set; }

        public virtual ChatGroup ChatGroup { get; set; }

        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }
    }
}
