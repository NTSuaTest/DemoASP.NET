using Demo.DTOs;
using Demo.Entity;
using Demo.Services.Interface;

namespace Demo.Services.Implement
{
    public class ImageService : IImageService
    {
        private readonly DbAPIContext _dbapiContext;

        public ImageService(DbAPIContext dbapiContext)
        {
            _dbapiContext = dbapiContext;
        }

        public List<ImageDTO> getImage()
        {
            var imageList = _dbapiContext.Images.Select(x => new ImageDTO(x)).ToList();
            return imageList;
        }
    }
}
