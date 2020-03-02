using MyFirstWebApplication1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebApplication1.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(Guid PostId);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post postToUpdate);
        Task<bool> DeletePostAsync(Guid PostId);
        Task<bool> UserOwnsPostAsync(Guid postId, string userId);
    }
}
