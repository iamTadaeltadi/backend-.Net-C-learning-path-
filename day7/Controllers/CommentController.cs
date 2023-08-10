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
    public class CommentManagerController : ControllerBase
    {
        private readonly CommentManager _commentManager;

        public CommentManagerController(CommentManager commentManager)
        {
            _commentManager = commentManager;
        }

        [HttpGet("posts/{postId}/comments")]
        public async Task<IActionResult> GetAllComments(int postId)
        {
            try
            {
                var comments = await _commentManager.GetAllCommentsAsync(postId);
                if (comments.Count == 0)
                {
                    return NotFound();
                }
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return HandleServerError(ex);
            }
        }

        [HttpGet("comments/{commentId}")]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            try
            {
                var comment = await _commentManager.GetCommentByIdAsync(commentId);
                if (comment == null)
                {
                    return NotFound($"Comment with ID {commentId} not found.");
                }
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return HandleServerError(ex);
            }
        }

        [HttpPost("posts/{postId}/comments")]
        public async Task<IActionResult> CreateComment(int postId, [FromBody] Comment comment)
        {
            try
            {
                if (await _commentManager.CreateCommentAsync(comment, postId))
                {
                    return CreatedAtAction(nameof(CreateComment), new { commentId = comment.CommentId }, comment);
                }

                return NotFound("Post not found.");
            }
            catch (Exception ex)
            {
                return HandleServerError(ex);
            }
        }

        [HttpPut("comments/{commentId}")]
        public async Task<IActionResult> UpdateComment(int commentId, [FromBody] Comment updatedComment)
        {
            try
            {
                if (commentId != updatedComment.CommentId)
                {
                    return BadRequest();
                }

                await _commentManager.UpdateCommentAsync(updatedComment);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleServerError(ex);
            }
        }

        [HttpDelete("comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                if (await _commentManager.DeleteCommentAsync(commentId))
                {
                    return NoContent();
                }
                return NotFound();
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
