using System.Threading.Tasks;
using BlogApp.Core.entities; // Assuming 'Post' is defined in 'BlogApp.Core.entities' namespace

namespace BlogApp.Application.persitance.contacts
{
    public interface IPostRepository : IGenericRepository<Post>
    {
       
    }
}
