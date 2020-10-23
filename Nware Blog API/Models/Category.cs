using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nware_Blog_API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Category(int id, string title)
        {
            this.Id = id;
            this.Title = title;
        }
    }
}