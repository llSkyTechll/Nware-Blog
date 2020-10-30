using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nware_Blog_API.Models
{
    public class HomePageLists
    {
        public List<CategoryModel> categories { get; set; }
        public List<PostModel> posts { get; set; }

        public HomePageLists(List<CategoryModel> categories, List<PostModel> post)
        {
            this.categories = categories;
            this.posts = post;
        }

        public HomePageLists()
        {
        }
    }
}