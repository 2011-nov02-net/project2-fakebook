using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    public interface IPostRepo
    {
        Task<bool> CreatePost(Post post);
        Task<bool> DeletePost(int id);
        Task<List<Post>> GetAllPosts();
        Task<List<Post>> GetPostsById(int id);
        Task<List<Post>> GetPostsByUserId(int id);
        Task<bool> UpdatePost(Post post);
    }
}