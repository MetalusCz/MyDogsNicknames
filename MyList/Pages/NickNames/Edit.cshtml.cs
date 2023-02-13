using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime;

namespace MyList.Pages.NickNames
{
    public class EditModel : PageModel
    {
        public Info inf = new Info();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=DESKTOP-AD4FCDO\\SQLEXPRESS;Initial Catalog=MyListDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM MyDogNicknames WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                inf.id = "" + reader.GetInt32(0);
                                inf.nickname = reader.GetString(1);
                                inf.created_at = reader.GetDateTime(2).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                errorMessage = e.Message;
                return;
            }
        }

        public void OnPost()
        {
            inf.id = Request.Form["id"];
            inf.nickname = Request.Form["nickname"];

            if (inf.nickname.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-AD4FCDO\\SQLEXPRESS;Initial Catalog=MyListDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE MyDogNicknames " +
                                 "SET nickname=@nickname " +
                                 "WHERE id=@id;" ;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nickname", inf.nickname);
                    command.Parameters.AddWithValue("@id", inf.id);    

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

                errorMessage = e.Message;
                return;
            }
            Response.Redirect("/NickNames/Index");
        }
    }
}
