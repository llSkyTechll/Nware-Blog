using MySql.Data.MySqlClient;
using Nware_Blog_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace Nware_Blog_API.Controllers
{
    public class ManagePostController : Controller
    {
        [HttpGet]
        public ActionResult AddPost()
        {
            LoadCategoryList();

            return View();
        }

        [HttpPost]
        public ActionResult AddPost(PostModel post)
        {
            LoadCategoryList();

            PostModel wrongValue = new PostModel();
            wrongValue.id = -1;

            if (ValidateIfPostExists(post))
            {
                if (ValidatePublicationDate(post.publicationDate))
                {
                    AddPostDB(post);
                    return RedirectToAction("HomePage", "HomePage", "HomePage");
                }
                else
                {
                    wrongValue.id = -2;
                }
            }

            return View(wrongValue);
        }

        public bool ValidatePublicationDate(DateTime publicationDate)
        {
            if (publicationDate == new DateTime())
            {
                return false;
            }
            return true;
        }

        public bool ValidateIfPostExists(PostModel post)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT title FROM post WHERE title = @title AND id != @id";

            query.Parameters.AddWithValue("@title", post.title);

            query.Parameters.AddWithValue("@id", post.id);

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
                return false;
            }

            return true;
        }

        public void AddPostDB(PostModel post)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "INSERT INTO post (title, publicationDate, content, categoryId) VALUE (@title, @publicationDate, @content, @categoryId)";

            query.Parameters.AddWithValue("@title", post.title);
            query.Parameters.AddWithValue("@publicationDate", post.publicationDate.Date);
            query.Parameters.AddWithValue("@content", post.content);
            query.Parameters.AddWithValue("@categoryId", post.categoryId);

            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }

            query.ExecuteNonQuery();
        }

        [HttpGet]
        public ActionResult EditPost(int id = 0)
        {
            LoadCategoryList();
            if (id == 0)
            {
                return View("AddPost");
            }
            else
            {
                PostModel post = GetSpecificPost(id);

                return View(post);
            }
        }

        [HttpPost]
        public ActionResult EditPost(PostModel post)
        {
            LoadCategoryList();

            PostModel wrongValue = new PostModel();
            wrongValue.id = -1;

            if (post.id != 0)
            {
                if (ValidateIfPostExists(post))
                {
                    if (ValidatePublicationDate(post.publicationDate))
                    {
                        ModifyPostDB(post);

                        return RedirectToAction("HomePage", "HomePage", "HomePage");
                    }

                    wrongValue.id = -2;
                }
            }

            return View(wrongValue);
        }

        public PostModel GetSpecificPost(int id)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id, title, publicationDate, content, categoryId FROM post WHERE id = @id";

            query.Parameters.AddWithValue("@id", id);

            var post = new PostModel();

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
                post.id = Convert.ToInt32(fetchQuery["id"]);
                post.title = fetchQuery["title"].ToString();
                post.publicationDate = Convert.ToDateTime(fetchQuery["publicationDate"]);
                post.content = fetchQuery["content"].ToString();
                post.categoryId = Convert.ToInt32(fetchQuery["categoryId"]);
            }

            return post;
        }

        public void LoadCategoryList()
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id, title FROM category";

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

            ViewBag.Categories = categories;
        }

        public void ModifyPostDB(PostModel post)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "UPDATE post SET title = @title, publicationDate = @publicationDate, content = @content, categoryId = @categoryId WHERE id = @id";

            query.Parameters.AddWithValue("@id", post.id);
            query.Parameters.AddWithValue("@title", post.title);
            query.Parameters.AddWithValue("@publicationDate", post.publicationDate);
            query.Parameters.AddWithValue("@content", post.content);
            query.Parameters.AddWithValue("@categoryId", post.categoryId);

            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }

            query.ExecuteNonQuery();
        }
    }
}