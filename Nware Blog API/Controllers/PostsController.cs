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
    public class PostsController : ApiController
    {
        public IHttpActionResult Get()
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id,title,publicationDate,content,categoryId FROM post WHERE DATEDIFF(publicationDate, '" + DateTime.Today.ToString("yyyy-MM-dd") + "') < 0 ORDER BY publicationDate DESC";

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

        public IHttpActionResult Get(int id)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id, title, publicationDate, content, categoryId FROM post WHERE id = @id";

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
                return NotFound();
            }

            return Ok(posts);
        }
    }
}
