namespace Socializer.Web.ViewModels.Images
{
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;

    public class ImageViewModel : IMapFrom<Image>
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
