using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace blogCRUDWithEFCore.Model
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>(); // Initialize the Comments collection in the constructor
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
    }
}
