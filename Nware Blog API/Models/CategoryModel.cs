using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nware_Blog_API.Models
{
    public class CategoryModel
    {
        public int id { get; set; }
        public string title { get; set; }

        public CategoryModel(int id, string title)
        {
            this.id = id;
            this.title = title;
        }

        public CategoryModel()
        {
        }
    }
}