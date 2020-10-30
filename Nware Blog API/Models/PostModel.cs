using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Nware_Blog_API.Models
{
    public class PostModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime publicationDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string content { get; set; }
        public int categoryId { get; set; }

        public PostModel(int id, string title, DateTime publicationDate, string content, int categoryId)
        {
            this.id = id;
            this.title = title;
            this.publicationDate = publicationDate;
            this.content = content;
            this.categoryId = categoryId;
        }

        public PostModel()
        {
        }
    }
}