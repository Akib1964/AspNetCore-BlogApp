namespace BlogPosts.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //foreign key to Blogpost
        public int PostId { get; set; }
        public BlogPost Post { get; set; }

    }
}
