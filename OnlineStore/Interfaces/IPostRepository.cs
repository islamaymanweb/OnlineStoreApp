using OnlineStore.Models.Comments;
using OnlineStore.Models;
using OnlineStore.Models.ViewModels;

namespace OnlineStore.Interfaces
{
    public interface IPostRepository
    {
        Post GetPost(int id);

        IndexViewModel GetAllPosts(int pageNumber, string category, string search);
        List<Post> GetAllPost();
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);
        void AddSubComment(SubComment comment);
        Task<bool> SaveChangeAsync();
    }
}
