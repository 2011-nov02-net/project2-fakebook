using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    public interface IPostRepo
    {
        Task<bool> CreatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
        Task<Post> GetPostByIdAsync(int id);
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetPostsByIdAsync(int id);
        Task<List<Post>> GetPostsByUserIdAsync(int id);
        Task<bool> UpdatePostAsync(Post post);
        Task<List<Post>> GetFollowingPosts(int id);
    }
}