using System.Threading.Tasks;
using BlogApp.Core.entities; // Assuming 'Comment' is defined in 'BlogApp.Core.entities' namespace

namespace BlogApp.Application.persitance.contacts
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        
    }
}
