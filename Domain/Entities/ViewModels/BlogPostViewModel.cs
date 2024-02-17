using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ViewModels
{
    public class BlogPostViewModel
    {
        public Guid Id { get; set; }

        public string Heading { get; set; }

        public string PageTitle { get; set; }

        public string Content { get; set; }

        public string ShortDescrption { get; set; }

        public string FeatureImageUrl { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublisheDate { get; set; }

        public string Author { get; set; }

        public bool Visible { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }

        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
