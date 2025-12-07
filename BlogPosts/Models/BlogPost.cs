using System.ComponentModel.DataAnnotations;

namespace BlogPosts.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Title is required!!!")]
        [StringLength(100, ErrorMessage ="Title cannot be longer than 100 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Blog content is required!!!")]
        [MinLength(10, ErrorMessage ="Content must be at least 20 characters long.")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Published Date is required!!!")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(BlogPost), nameof(ValidatePublishedDate))]
        public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
        //foreign key to user
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public static ValidationResult ValidatePublishedDate(
    DateTime publishedDate,
    ValidationContext context)
        {
            if (publishedDate > DateTime.UtcNow)
            {
                return new ValidationResult("Published date cannot be in the future date!!!");
            }

            return ValidationResult.Success;
        }

    }
}
