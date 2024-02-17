using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITagService : IDisposable
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();

        Task<Tag> GetTagAsync(Guid id);

        Task<Tag> InsertTagAsync(Tag tag);

        Tag? UpdateTagAsynce(Tag tag);

        Task<Tag?> DeleteTagAsynce(Guid id);
        Tag? DeleteTagAsynce(Tag tag);
        Task SaveAsync();
    }
}
