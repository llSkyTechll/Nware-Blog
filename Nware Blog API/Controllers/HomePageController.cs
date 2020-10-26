using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Nware_Blog_API.Models;

namespace Nware_Blog_API.Controllers
{
    public class HomePageController : Controller
    {
        public ActionResult HomePage()
        {
            var categories = new List<CategoryModel>();

            categories = GetAllCategories();

            var posts = new List<PostModel>();

            posts = GetAllPosts();

            HomePageLists homePageLists = new HomePageLists(categories, posts);

            ViewBag.Title = "Home Page";

            return View(categories);
        }

        public List<CategoryModel> GetAllCategories()
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id,title FROM category";

            var categories = new List<CategoryModel>();

            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }

            MySqlDataReader fetchQuery = query.ExecuteReader();

            while (fetchQuery.Read())
            {
                categories.Add(new CategoryModel(Convert.ToInt32(fetchQuery["id"]), fetchQuery["title"].ToString()));
            }

            return categories;
        }

        public List<PostModel> GetAllPosts()
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id,title,publicationDate,content,categoryId FROM post";

            var posts = new List<PostModel>();

            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }

            MySqlDataReader fetchQuery = query.ExecuteReader();

            while (fetchQuery.Read())
            {
                posts.Add(new PostModel(Convert.ToInt32(fetchQuery["id"]), fetchQuery["title"].ToString(), (DateTime)fetchQuery["publicationDate"], fetchQuery["content"].ToString(), Convert.ToInt32(fetchQuery["categoryId"])));
            }

            return posts;
        }

    }
}
