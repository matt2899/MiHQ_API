using Microsoft.AspNetCore.Identity;

namespace CodePulse.API.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }

        public bool IsVisible { get; set; }
        public ICollection<Category> Categories { get; set; }

        // Author Relationship
        public string AuthorId { get; set; }
        public ApplicationUser? Author { get; set; }
    }
}
