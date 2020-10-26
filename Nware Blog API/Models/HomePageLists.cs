using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nware_Blog_API.Models
{
    public class HomePageLists
    {
        public List<CategoryModel> Categories { get; set; }
        public List<PostModel> Posts { get; set; }

        public HomePageLists(List<CategoryModel> categories, List<PostModel> post)
        {
            this.Categories = categories;
            this.Posts = post;
        }
    }
}