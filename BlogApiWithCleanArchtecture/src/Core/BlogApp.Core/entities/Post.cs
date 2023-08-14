using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using BlogApp.Core.common;
namespace BlogApp.Core.entities{

    public class Post: commonEntity
    {
        public Post()
        {
            Comments = new List<Comment>(); // Initialize the Comments collection in the constructor
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public ICollection<Comment> Comments { get; set; }}


}