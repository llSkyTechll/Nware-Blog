using MySql.Data.MySqlClient;
using Nware_Blog_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nware_Blog_API.Controllers
{
    public class ManageCategoryController : Controller
    {
        public ActionResult AddCategory()
        {
            return View();
        }

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
