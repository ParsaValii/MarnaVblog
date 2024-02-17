using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class TagService : ITagService
    {
        private readonly MarnaDbContext _context;

        public TagService(MarnaDbContext marnaDbContext)
        {
            _context = marnaDbContext;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagAsync(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            return tag;
        }

        public async Task<Tag> InsertTagAsync(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            return tag;
        }

        public Tag? UpdateTagAsynce(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;
            return tag;
        }

        public async Task<Tag?> DeleteTagAsynce(Guid id)
        {
            var tag = await GetTagAsync(id);
            DeleteTagAsynce(tag);
            return tag;
        }

        public Tag? DeleteTagAsynce(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Deleted;
            return tag;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
