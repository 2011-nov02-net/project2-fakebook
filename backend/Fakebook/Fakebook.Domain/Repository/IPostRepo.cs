using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    public interface IPostRepo
    {
        Task<int> CreatePostAsync(Post post);
        Task<Post> GetPostByIdAsync(int id);
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetPostsByIdAsync(int id);
        Task<List<Post>> GetPostsByUserIdAsync(int id);
        Task<int> CountLikesAsync(int id);
        Task<bool> UpdatePostAsync(Post post);
        Task<bool> LikePostAsync(int id, int userId);
        Task<bool> UnlikePostAsync(int id, int userId);
        Task<bool> DeletePostAsync(int id);
        Task<List<Post>> GetFollowingPosts(int id);
    }
}