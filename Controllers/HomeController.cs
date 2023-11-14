using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplication1.Models;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Exception Exception;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeModel home = new HomeModel();
            return View(home);
        }
        public List<HomeModel> GetData()
        {
            List<HomeModel> lst = new List<HomeModel>();
            string connstring = "server=SARANRAAJ\\SQLEXPRESS;database=text;user=sa;password=3ID1$";
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from StudentsInfo", conn);
            try
            {
                System.Data.DataSet ds = new System.Data.DataSet();
                da.Fill(ds);

                System.Data.DataTable dt = ds.Tables[0];

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    HomeModel m = new HomeModel();
                    m.StdContact = (string)row["StdContact"];
                    m.StdRegNumber = (string)row["StdRegNumber"];
                    m.StdName = (String)row["StdName"];
                    m.StdEmail = (String)row["StdEmail"];
                    lst.Add(m);
                }
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    HomeModel m = new HomeModel();
                    lst.Remove(m);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
            return lst;
        }
        [HttpPost]
        public IActionResult User(HomeModel model)
        {
            string connstring = "server=SARANRAAJ\\SQLEXPRESS;database=text;user=sa;password=3ID1$";
            string query = "select * from StudentsInfo";
            SqlConnection connection = new SqlConnection(connstring);
            connection.Open();
            SqlCommand comm = new SqlCommand("insert into StudentsInfo values('" + model.StdRegNumber + "','" + model.StdName + "'," + model.StdContact + ",'" + model.StdEmail + "')", connection);
            //SqlCommand del = new SqlCommand("delete from StudentsInfo where @StdRegNumber ");
            try
            {
                comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                connection.Close();
            }
            List<HomeModel> lst = GetData();
            return View(lst);
        }
        [HttpGet]
        public IActionResult User()
        {
            List<HomeModel> lst = GetData();
            return View(lst);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Delete(string id)
        {
            string connstring = "server=SARANRAAJ\\SQLEXPRESS;database=text;user=sa;password=3ID1$";
            using (SqlConnection connection = new SqlConnection(connstring))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM StudentsInfo WHERE StdRegNumber = @StdRegNumber";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@StdRegNumber", id);
                    command.ExecuteNonQuery();
                }
            }

            return View("delete");
        }
    }
}
