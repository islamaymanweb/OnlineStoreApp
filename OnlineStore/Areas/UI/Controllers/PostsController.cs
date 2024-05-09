using Microsoft.AspNetCore.Mvc;
using OnlineStore.Interfaces;

namespace OnlineStore.Areas.UI.Controllers
{
    [Area("UI")]
    public class PostsController : Controller
    {
        private readonly IPostRepository _repo;
        public PostsController(IPostRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts=_repo.GetAllPost().ToList();
            return View(posts);
        }
        public IActionResult Details(int id)
        {
            return View(_repo.GetPost(id));
        }

    }
}
