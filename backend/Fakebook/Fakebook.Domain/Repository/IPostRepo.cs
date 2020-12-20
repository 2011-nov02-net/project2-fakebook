using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Domain.Repository
{
    public interface IPostRepo
    {
        int CountLikes(int id);
        Task<int> CountLikesAsync(int id);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> DeletePostAsync(int id);
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetFollowingPosts(int id);
        Task<Post> GetPostByIdAsync(int id);
        Task<List<Post>> GetPostsByIdAsync(int id);
        Task<List<Post>> GetPostsByUserIdAsync(int id);
        Task<bool> LikePostAsync(int id, int userId);
        Task<bool> UnlikePostAsync(int id, int userId);
        Task<bool> UpdatePostAsync(Post post);
    }
}