using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBlogPostService : IDisposable
    {
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();

        Task<BlogPost> GetBlogPostAsync(Guid id);

        Task<BlogPost> InsertBlogPostAsync(BlogPost BlogPost);

        BlogPost? UpdateBlogPostAsynce(BlogPost BlogPost);

        Task<BlogPost?> DeleteBlogPostAsynce(Guid id);
        BlogPost? DeleteBlogPostAsynce(BlogPost BlogPost);
        Task SaveAsync();
    }
}
