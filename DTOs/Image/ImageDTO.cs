using Demo.Entity;

namespace Demo.DTOs
{
    public class ImageDTO
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string CreateBy { get; set; }

        public ImageDTO(Image image)
        {
            Id = image.Id;
            Url = image.Url;
            CreateBy = image.CreateBy;
        }
    }
}
