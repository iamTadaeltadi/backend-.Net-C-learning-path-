using System;
using System.ComponentModel.DataAnnotations.Schema; // Add this using directive
using System.Text.Json.Serialization;

namespace blogCRUDWithEFCore.Model
{
    public class Comment
    {
        public int CommentId { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public  Post? Post { get; set; }
    }
}
