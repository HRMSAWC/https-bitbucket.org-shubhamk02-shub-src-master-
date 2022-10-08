using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AWC_HRMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Mail;

namespace HRMS_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotController : ControllerBase
    {
        private readonly ILogger<ForgotController> _logger;
        public ForgotController(ILogger<ForgotController> logger)
        {
            _logger = logger;
        }
        string link=GetURL();

        [HttpPost]
        [Route("api/HRMS_WebAPI/Forgot/ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                int i;
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("USP_EmployeeForgotPassword", con);
                    //cmd.Parameters.AddWithValue("@candidateId", Id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Emp_Name", model.UserName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            i = Convert.ToInt32(reader["Result"]);
                            if (i == 1)
                            {
                                return NotFound();
                            }
                            else
                            {
                                sendmail(model.Email,link);
                            }
                        }
                    }
                    con.Close();
                }

            }
            return Ok();
        }
        [NonAction]
        public void sendmail(string Email, string Link)
        {
            string br = "";
            br += "<br>";
            string body = "";
            string url = "<a href='https://localhost:7228/api/Forgot/api/HRMS_WebAPI/Forgot/ResetNewPassword/?Email=" + Email + "' class='a'> " + Link + " </a>";

            body += "<p>" + "Hello" + "Dear Employee, " + "Greetings of the Day, " + br + "This is auto generated email, Please Click on the Link to regenerate your password " + br + url + " " + br + "" + br + "Thankyou."
+ "</p>";

            MailAddress from = new MailAddress("akashchoudhary199404@gmail.com");
            MailAddress to = new MailAddress(Email);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Reset Password Link";
            message.IsBodyHtml = true;
            message.Body = body;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("akashchoudhary199404@gmail.com", "ogqhffxlkwmmwiyv"),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
                Console.WriteLine("Mail Sent");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        private static List<char> characters = new List<char>() {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
                                                        'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
                                                        'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                                        'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'
        };

        [NonAction]
        public static string GetURL()
        {
            string URL = "";
            Random rand = new Random();
            // run the loop till I get a string of 10 characters
            for (int i = 0; i < 11; i++)
            {
                // Get random numbers, to get either a character or a number...
                int random = rand.Next(0, 3);
                if (random == 1)
                {
                    // use a number
                    random = rand.Next(0, numbers.Count);
                    URL += numbers[random].ToString();
                }
                else
                {
                    random = rand.Next(0, characters.Count);
                    URL += characters[random].ToString();
                }
            }



            return URL;
        }



        [HttpPost]
        [Route("api/HRMS_WebAPI/Forgot/ResetNewPassword")]
        public IActionResult ResetNewPassword([FromBody] NewPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
                IConfiguration configuration = builder.Build();
                string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
                int i;
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("USP_EmployeeResetNewPassword", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@NewPassword", model.Password);
                    i=cmd.ExecuteNonQuery();
                    con.Close();

                    if(i== 1)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode (500);    
                    }
                }

            }
            return Ok();
        }

    }
}
