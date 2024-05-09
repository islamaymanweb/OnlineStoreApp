using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Helpers;
using OnlineStore.Interfaces;
using OnlineStore.Models;
using OnlineStore.Models.Comments;
using OnlineStore.Models.ViewModels;

namespace OnlineStore.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public List<Post> GetAllPost()
        {
            return _context.Posts.ToList();
        }
        public IndexViewModel GetAllPosts(int pageNumber,
               string category,
               string search)
        {
            Func<Post, bool> InCategory = (post) =>
            {
                return post.Category.ToLower().Equals(category.ToLower());
            };

            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = _context.Posts.AsNoTracking().AsQueryable();


            if (!String.IsNullOrEmpty(category))
                query = query.Where(x => InCategory(x));
            if (!String.IsNullOrEmpty(search))
                query = query.Where(x => EF.Functions.Like(x.Title, $"%{search}")
                              || EF.Functions.Like(x.Body, $"%{search}")
                              || EF.Functions.Like(x.Description, $"%{search}"));
            int postsCount = query.Count();
            int pageCount = (int)Math.Ceiling((double)postsCount / pageSize);

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                PageCount = pageCount,
                NextPage = postsCount > skipAmount + pageSize,
                Pages = PageHelper.PageNumbers(pageNumber, pageCount).ToList(),
                Category = category,
                Search = search,
                Posts = query
                  .Skip(skipAmount)
                  .Take(pageSize)
                  .ToList()
            };
        }

        public Post GetPost(int id)
        {
            return _context.Posts
                    .Include(p => p.MainComments)
                    .ThenInclude(m => m.SubComments)
                    .FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _context.Posts.Remove(GetPost(id));
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
        }

        public void AddSubComment(SubComment comment)
        {
            _context.SubComments.Add(comment);
        }

        public async Task<bool> SaveChangeAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}

