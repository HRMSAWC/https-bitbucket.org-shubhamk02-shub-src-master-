using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Session;
using AWC_HRMS.Models;
using Microsoft.AspNetCore.Cors;

namespace AWC_HRMS_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        //[HttpGet]
        //public IActionResult CheckLogin()
        //{
        //    return View();
        //}
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
          
        }
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public  IActionResult CheckLogin([FromBody] Login ObjClass)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            int i;

            if (ObjClass == null)
            {
                return BadRequest();

            }
            else
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    SqlCommand check = new SqlCommand("Usp_Check_User_Login", con);
                    check.CommandType = CommandType.StoredProcedure;
                    check.Parameters.AddWithValue("@Empid", ObjClass.EmployeeId);
                    check.Parameters.AddWithValue("@Password", ObjClass.Password);
                    using (var reader = check.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            i =  Convert.ToInt32(reader["Result"]);
                            if (i == 0)
                            {
                                return NotFound();
                            }
                            else
                            {
                               // HttpContext.Session.SetString("CUser", Convert.ToString(ObjClass.EmployeeId));
                                return Ok();
                                //return RedirectToAction("Index", "Dashboard");

                            }

                        }
                    }

                }

            }

            return Ok();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            //var sessionData = HttpContext.Session.GetString("CUser", Convert.ToString(obj.EmployeeId));
            //if (sessionData != null)
            //{ 
            //    Session.Abandon();
            //    return RedirectToAction("Index", "");
            //}


            try
            {


                //HttpContext.Session.Remove("CUser");

                //HttpContext.Session.Clear();


                // Cookies = new HttpCookie("WebTime");
                //Cookies.Value = "";
                //Cookies.Expires = DateTime.Now.AddHours(-1);
                //Response.Cookies.Add(Cookies);
                //HttpContext.Session.Clear();
                // Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                throw;
            }





            return Ok();

        }



    }
}
