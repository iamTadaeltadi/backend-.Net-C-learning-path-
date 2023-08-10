using Microsoft.AspNetCore.Mvc;
using blogCRUDWithEFCore.Model;
using blog.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogManagerController : ControllerBase
    {
        private readonly BlogManager _blogManager;

        public BlogManagerController(BlogManager blogManager)
        {
            _blogManager = blogManager;
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var posts = await _blogManager.GetAllPostsAsync();
                if (posts.ToList().Count == 0)
                {
                    return NotFound("There are no posts yet.");
                }
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return HandleServerError(ex);
            }
        }

        [HttpGet("posts/{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            try
            {
                var post = await _blogManager.GetPostByIdAsync(postId);
                if (post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
            catch (Exception ex)
            {
                return HandleServerError(ex);
            }
        }

        [HttpPost("posts")]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            try
            {
                await _blogManager.AddPostAsync(post);
                return CreatedAtAction(nameof(GetPostById), new { postId = post.PostId }, post);
            }
            catch (Exception ex)
            {
                return HandleServerError(ex);
            }
        }

        [HttpPut("posts/{postId}")]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody] Post updatedPost)
        {
            try
            {
                if (postId != updatedPost.PostId)
                {
                    return BadRequest();
                }

                await _blogManager.UpdatePostAsync(updatedPost);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleServerError(ex);
            }
        }

        [HttpDelete("posts/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                if (await _blogManager.DeletePostAsync(postId))
                {
                    return NoContent();
                }
                return NotFound($"Post with ID {postId} not found.");
            }
            catch (Exception ex)
            {
                return HandleServerError(ex);
            }
        }
       

        private IActionResult HandleServerError(Exception ex)
        {
           
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
