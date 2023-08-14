using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema; 
using BlogApp.Core.common;
using System.Text.Json.Serialization;



namespace BlogApp.Core.entities
{
    public class Comment : commonEntity
    {
        public int CommentId { get; set; }

        [ForeignKey("Post")] // Specify the foreign key relationship to "Post"
        public int PostId { get; set; }

        public string Text { get; set; }

        [JsonIgnore]
        public Post? Post { get; set; }
    }
}
