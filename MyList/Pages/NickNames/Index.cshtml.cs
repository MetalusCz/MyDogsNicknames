using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MyList.Pages.NickNames
{
    public class IndexModel : PageModel
    {
        public List<Info> listInfo = new List<Info>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-AD4FCDO\\SQLEXPRESS;Initial Catalog=MyListDB;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM MyDogNicknames";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Info inf = new Info();
                                inf.id = "" + reader.GetInt32(0);
                                inf.nickname = reader.GetString(1);
                                inf.created_at = reader.GetDateTime(2).ToString();

                                listInfo.Add(inf);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Error something is wrong" + e.ToString());
            }
        }
    }
    public class Info
    {
        public String id { get; set; }
        public String nickname { get; set; }
        public String created_at { get; set; }  
    }
}
