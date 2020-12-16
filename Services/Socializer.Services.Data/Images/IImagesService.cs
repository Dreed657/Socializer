namespace Socializer.Services.Data.Images
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IImagesService
    {
        Task<T> GetImageByName<T>(string name);

        Task<IEnumerable<T>> GetAllImageByUserId<T>(string userId);
    }
}
