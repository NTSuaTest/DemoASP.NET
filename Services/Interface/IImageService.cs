using Demo.DTOs;
using Demo.Entity;

namespace Demo.Services.Interface
{
    public interface IImageService
    {
        List<ImageDTO> getImage();
    }
}
