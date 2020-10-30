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
    public class ManageCategoryController : Controller
    {
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModel category)
        {
            if (ValidateIfCategoryExists(category))
            {
                AddCategoryDB(category);
                return RedirectToAction("HomePage", "HomePage", "HomePage");
            }

            CategoryModel wrongValue = new CategoryModel(-1, "Wrong value");
            return View(wrongValue);
        }

        public void AddCategoryDB(CategoryModel category)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "INSERT INTO category (title) VALUE (@title)";

            query.Parameters.AddWithValue("@title", category.title);

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

        public bool ValidateIfCategoryExists(CategoryModel category)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT title FROM category WHERE title = @title AND id != @id";

            query.Parameters.AddWithValue("@title", category.title);

            query.Parameters.AddWithValue("@id", category.id);

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

        [HttpGet]
        public ActionResult EditCategory(int id = 0)
        {
            if (id == 0)
            {
                return View("AddCategory");
            }
            else
            {
                CategoryModel category = GetSpecificCategory(id);

                return View(category);
            }
        }

        [HttpPost]
        public ActionResult EditCategory(CategoryModel category)
        {
            if (category.id != 0)
            {
                if (ValidateIfCategoryExists(category))
                {
                    ModifyCategoryTitle(category);

                    return RedirectToAction("HomePage", "HomePage", "HomePage");
                }
            }

            CategoryModel wrongValue = new CategoryModel(-1, "Wrong value");
            return View(wrongValue);
        }

        public void ModifyCategoryTitle(CategoryModel category)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "UPDATE category SET title = @title WHERE id = @id";

            query.Parameters.AddWithValue("@id", category.id);

            query.Parameters.AddWithValue("@title", category.title);

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

        public CategoryModel GetSpecificCategory(int id)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id, title FROM category WHERE id = @id";

            query.Parameters.AddWithValue("@id", id);

            var category = new CategoryModel();

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
                category.id = Convert.ToInt32(fetchQuery["id"]);
                category.title = fetchQuery["title"].ToString();
            }

            return category;
        }

    }
}
