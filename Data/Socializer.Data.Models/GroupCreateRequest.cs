namespace Socializer.Data.Models
{
    using Socializer.Data.Common.Models;
    using Socializer.Data.Models.Enums;

    public class GroupCreateRequest : BaseModel<int>
    {
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }
    }
}
