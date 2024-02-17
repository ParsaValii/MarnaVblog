using Domain.Entities;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly MarnaDbContext _context;

        public BlogPostService(MarnaDbContext marnaDbContext)
        {
            _context = marnaDbContext;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await _context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetBlogPostAsync(Guid id)
        {
            var BlogPost = await _context.BlogPosts.FindAsync(id);
            return BlogPost;
        }

        public async Task<BlogPost> InsertBlogPostAsync(BlogPost BlogPost)
        {
            await _context.BlogPosts.AddAsync(BlogPost);
            return BlogPost;
        }

        public BlogPost? UpdateBlogPostAsynce(BlogPost BlogPost)
        {
            _context.Entry(BlogPost).State = EntityState.Modified;
            return BlogPost;
        }

        public async Task<BlogPost?> DeleteBlogPostAsynce(Guid id)
        {
            var BlogPost = await GetBlogPostAsync(id);
            DeleteBlogPostAsynce(BlogPost);
            return BlogPost;
        }

        public BlogPost? DeleteBlogPostAsynce(BlogPost BlogPost)
        {
            _context.Entry(BlogPost).State = EntityState.Deleted;
            return BlogPost;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
