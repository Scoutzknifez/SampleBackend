using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace SampleBackend.Controllers
{
    [ApiController]
    public class SampleController : ControllerBase {
        const string DB_CONNECTION = "server=127.0.0.1;user=root;password=ADMIN_PASSWORD;database=blog";
        const string API_HEADER = "/API/";

        [HttpGet]
        [Route(API_HEADER)]
        public async Task<IActionResult> GetResponse() {
            using MySqlConnection connection = new MySqlConnection(DB_CONNECTION);
            await connection.OpenAsync();

            string commandInput = "SELECT * FROM blogs ORDER BY `time` DESC;";

            using MySqlCommand command = new MySqlCommand(commandInput, connection);
            using MySqlDataReader reader = await command.ExecuteReaderAsync();

            List<Blog> blogList = new List<Blog>();

            while (await reader.ReadAsync())
            {
                string id = reader.GetString(0);
                long time = reader.GetInt64(1);
                string title = reader.GetString(2);
                string text = reader.GetString(3);

                blogList.Add(new Blog(id, time, title, text));
            }

            var returnObject = new Dictionary<string, object>
            {
                { "blogs", blogList }
            };

            return Ok(returnObject);
        }

        [HttpPost]
        [Route(API_HEADER)]
        public async Task<IActionResult> PostBlog(Blog inputBlog)
        {
            using MySqlConnection connection = new MySqlConnection(DB_CONNECTION);
            await connection.OpenAsync();

            DateTimeOffset now = DateTimeOffset.UtcNow;
            long unixTimeMillis = now.ToUnixTimeMilliseconds();

            string commandInput = "INSERT INTO blogs VALUES(UUID(), " + unixTimeMillis + ", \"" + inputBlog.Title + "\", \"" + inputBlog.Text + "\")";

            using MySqlCommand command = new MySqlCommand(commandInput, connection);
            using MySqlDataReader reader = await command.ExecuteReaderAsync();

            var returnObject = new Dictionary<string, object>
            {
                { "result", "success" },
                { "time", unixTimeMillis },
                { "title", inputBlog.Title },
                { "text", inputBlog.Text }
            };

            return Ok(returnObject);
        }

        [HttpDelete]
        [Route(API_HEADER)]
        public async Task<IActionResult> DeleteBlog(string id)
        {
            using MySqlConnection connection = new MySqlConnection(DB_CONNECTION);
            await connection.OpenAsync();

            string commandInput = "DELETE FROM blogs WHERE id = '" + id + "';";

            using MySqlCommand command = new MySqlCommand(commandInput, connection);
            using MySqlDataReader reader = await command.ExecuteReaderAsync();

            var returnObject = new Dictionary<string, object>
            {
                { "result", "success" }
            };

            return Ok(returnObject);
        }

        [HttpPut]
        [Route(API_HEADER)]
        public async Task<IActionResult> EditBlog(Blog editedBlog)
        {
            using MySqlConnection connection = new MySqlConnection(DB_CONNECTION);
            await connection.OpenAsync();

            string commandInput = "UPDATE blogs SET title = '" + editedBlog.Title + "', text = '" + editedBlog.Text + "' WHERE `id` = '" + editedBlog.ID +"';";

            using MySqlCommand command = new MySqlCommand(commandInput, connection);
            using MySqlDataReader reader = await command.ExecuteReaderAsync();

            var returnObject = new Dictionary<string, object>
            {
                { "result", "success" }
            };

            return Ok(returnObject);
        }
    }
}
