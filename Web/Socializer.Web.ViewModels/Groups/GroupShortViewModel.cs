namespace Socializer.Web.ViewModels.Groups
{
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;

    public class GroupShortViewModel : IMapFrom<Group>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
