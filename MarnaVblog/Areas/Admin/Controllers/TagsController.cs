using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Entities;
using Services.Interfaces;
using Services.Services;

namespace MarnaVblog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {
        private readonly MarnaDbContext _context = new MarnaDbContext();
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = new TagService(_context);
        }

        // GET: Admin/Tags
        public async Task<IActionResult> Index()
        {
            return View(await _tagService.GetAllTagsAsync());
        }

        // GET: Admin/Tags/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagService.GetTagAsync(id.Value);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Admin/Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DisplayName")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                tag.Id = Guid.NewGuid();
                await _tagService.InsertTagAsync(tag);
                await _tagService.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Admin/Tags/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagService.GetTagAsync(id.Value);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Admin/Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,DisplayName")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _tagService.UpdateTagAsynce(tag);
                    await _tagService.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }

        // GET: Admin/Tags/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _tagService.GetTagAsync(id.Value);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Admin/Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tag = await _tagService.GetTagAsync(id);
            if (tag != null)
            {
                _tagService.DeleteTagAsynce(tag);
            }

            await _tagService.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(Guid id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
