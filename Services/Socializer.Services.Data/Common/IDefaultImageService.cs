namespace Socializer.Services.Data.Common
{
    using System.Threading.Tasks;

    using Socializer.Data.Models;

    public interface IDefaultImageService
    {
        Task<Image> GetDefaultCoverImage();

        Task<Image> GetDefaultProfileImage();
    }
}
