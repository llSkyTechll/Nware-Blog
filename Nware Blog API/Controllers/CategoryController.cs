using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Nware_Blog_API.Models;

namespace Nware_Blog_API.Controllers
{ 
    public class CategoryController : ApiController
    {
        public List<CategoryModel> Get()
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
                categories.Add(new CategoryModel(Convert.ToInt32(fetchQuery["id"]),fetchQuery["title"].ToString()));
            }

            return categories;
        }

        // GET api/values/5
        public List<CategoryModel> Get(int id)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id,title FROM category WHERE id = @id";

            query.Parameters.AddWithValue("@id", id);

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
                categories.Add(new CategoryModel(Convert.ToInt32(fetchQuery["id"]) ,fetchQuery["title"].ToString()));
            }

            return categories;
        }

        // POST api/values
        public void Post([FromBody] dynamic value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
