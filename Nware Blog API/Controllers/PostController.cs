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
    public class PostController : ApiController
    {
        // GET api/values
        public List<PostModel> Get()
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

        // GET api/values/5
        public List<PostModel> Get(int id)
        {
            MySqlConnection conn = WebApiConfig.sqlConnection();

            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT id,title,publicationDate,content,categoryId FROM post WHERE id = @id";

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

            return posts;
        }

        // POST api/values
        public void Post([FromBody] dynamic value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }
}
