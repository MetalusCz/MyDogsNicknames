using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Runtime;

namespace MyList.Pages.NickNames
{
    public class CreateModel : PageModel
    {
        public Info inf = new Info();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            inf.nickname = Request.Form["nickname"];

            if (inf.nickname.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            // save the new nick name into the db
            try
            {
                String connectionString = "Data Source=DESKTOP-AD4FCDO\\SQLEXPRESS;Initial Catalog=MyListDB;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO MyDogNicknames" +
                                 "(nickname) VALUES" +   
                                 "(@nickname);" ;
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nickname", inf.nickname);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }

            inf.nickname = "";
            successMessage = "New nick name added correctly";

            Response.Redirect("/NickNames/Index");
        }
    }
}
