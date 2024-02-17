using Domain;
using Domain.Entities;
using Domain.Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace MarnaVblog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        private readonly MarnaDbContext _context;
        private ITagService _tagService;
        private IBlogPostService _blogPostService;

        public BlogPostsController(MarnaDbContext context, ITagService tagService, IBlogPostService blogPostService)
        {
            _context = context;
            _tagService = tagService;
            _blogPostService = blogPostService;
        }

        // GET: Admin/BlogPosts
        public async Task<IActionResult> Index()
        {
            return View(await _blogPostService.GetAllBlogPostsAsync());
        }

        // GET: Admin/BlogPosts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: Admin/BlogPosts/Create
        public async Task<IActionResult> Create()
        {
            var tags = await _tagService.GetAllTagsAsync();
            var model = new BlogPostViewModel 
            {
                Tags = tags.Select(m => new SelectListItem {Text=m.DisplayName, Value = m.Id.ToString() })
            };
            return View(model);
        }

        // POST: Admin/BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostViewModel blogPostViewModel)
        {
            var SelectedTags = new List<Tag>();
            foreach (var selectedtag in blogPostViewModel.SelectedTags)
            {
                var selectedTagId = Guid.Parse(selectedtag);
                var existingtag = await _tagService.GetTagAsync(selectedTagId);
                if (existingtag != null)
                {
                    SelectedTags.Add(existingtag);
                }
            }
            var blogPost = new BlogPost
            {
                Heading = blogPostViewModel.Heading,
                Author = blogPostViewModel.Author,
                Content = blogPostViewModel.Content,
                FeatureImageUrl = blogPostViewModel.FeatureImageUrl,
                PageTitle = blogPostViewModel.PageTitle,
                PublisheDate = blogPostViewModel.PublisheDate,
                ShortDescrption = blogPostViewModel.ShortDescrption,
                UrlHandle = blogPostViewModel.UrlHandle,
                Visible = blogPostViewModel.Visible,
                Tags = SelectedTags
            };
            await _blogPostService.InsertBlogPostAsync(blogPost);
            await _blogPostService.SaveAsync();
            return RedirectToAction("Index");
        }

        // GET: Admin/BlogPosts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _blogPostService.GetBlogPostAsync(id.Value);
            if (blogPost == null)
            {
                return NotFound();
            }
            var tags = await _tagService.GetAllTagsAsync();
            var model = new EditBlogPostViewModel
            {
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeatureImageUrl = blogPost.FeatureImageUrl,
                PageTitle = blogPost.PageTitle,
                Heading = blogPost.Heading,
                Id = blogPost.Id,
                PublisheDate = blogPost.PublisheDate,
                ShortDescrption = blogPost.ShortDescrption,
                UrlHandle = blogPost.UrlHandle,
                Visible = blogPost.Visible,
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.Name, Value = x.Id.ToString()
                }),
                SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
            };

            return View(model);
        }

        // POST: Admin/BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Heading,PageTitle,Content,ShortDescrption,FeatureImageUrl,UrlHandle,PublisheDate,Author,Visible")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // GET: Admin/BlogPosts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: Admin/BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(id);
            if (blogPost != null)
            {
                _context.BlogPosts.Remove(blogPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(Guid id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
}
