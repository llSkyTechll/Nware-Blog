using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Nware_Blog_API.Models;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace Nware_Blog_API.Controllers
{
    [System.Web.Http.RoutePrefix("categories")]
    public class CategoriesController : ApiController
    {

        public IHttpActionResult Get()
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

            if (categories.Count == 0)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }

            return Ok(categories);
        }

        public IHttpActionResult Get(int id)
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
                categories.Add(new CategoryModel(Convert.ToInt32(fetchQuery["id"]), fetchQuery["title"].ToString()));
            }

            if (categories.Count == 0)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        [Route("{id:int}/posts")]
        public IHttpActionResult GetPostsByCategory(int id)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id, title, publicationDate, content, categoryId FROM post WHERE categoryId = @id";

            query.Parameters.AddWithValue("@id", id);

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

            if (posts.Count == 0)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }

            return Ok(posts);
        }
    
    }
}
