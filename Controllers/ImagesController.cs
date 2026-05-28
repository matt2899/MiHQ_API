using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                // File upload
                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

                blogImage = await imageRepository.Upload(file, blogImage);

                // Convert domain model to DTO
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Title = blogImage.Title,
                    Url = blogImage.Url,
                    DateCreated = blogImage.DateCreated,
                };

                return Ok(response);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImage()
        {
            var images = await this.imageRepository.GetImages();

            var response = new List<BlogImageDto>();

            foreach (var image in images)
            {
                response.Add(new BlogImageDto {
                    Id = image.Id,
                    Title = image.Title,
                    FileName = image.FileName,
                    FileExtension = image.FileExtension,
                    Url = image.Url,
                    DateCreated = image.DateCreated,
                });
            }

            return Ok(response);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtension = new String[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }
    }
}
