using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AWC_HRMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using Microsoft.Net;
using System.Net;
using System;

namespace AWC_HRMS_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateLinkController : ControllerBase
    {
        private readonly ILogger<GenerateLinkController> _logger;
        public GenerateLinkController(ILogger<GenerateLinkController> logger)
        {
            _logger = logger;

        }
        string link = GetURL();
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult GenerateCandidateLink([FromBody] LinkGeneration linkgeneration)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            Random rand = new Random();
            string random = rand.Next(0, 1000000).ToString("D6");

            int i;

            if (linkgeneration == null)
            {
                return NotFound();

            }
            else
            {
                using (SqlConnection con = new SqlConnection(constring))
                {
                    //var id = HttpContext.Session.GetString("CUser");
                    //if (id == null || id == "0")
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                        con.Open();
                        SqlCommand check = new SqlCommand("Usp_LinkGeneration", con);
                        check.CommandType = CommandType.StoredProcedure;
                        check.Parameters.AddWithValue("@Candidate_name", linkgeneration.CandidateName);
                        check.Parameters.AddWithValue("@Candidate_Email", linkgeneration.CandidateEmail);
                        check.Parameters.AddWithValue("@Candidate_Contact_Number", linkgeneration.CandidateContactNumber);
                        check.Parameters.AddWithValue("@Link", link);
                    check.Parameters.AddWithValue("@verifification_code", random);
                    check.Parameters.AddWithValue("@dateofjoining", linkgeneration.DateOfJoining);
                        check.Parameters.AddWithValue("@CreatedBy", "Ankita");
                        i = check.ExecuteNonQuery();
                        if (i == -1)
                        {
                            sendmail(linkgeneration.CandidateEmail, linkgeneration.CandidateName, link);
                        }
                    //}

                }

            }
            return Ok();
        }


        private static List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        private static List<char> characters = new List<char>() {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
                                                        'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
                                                        'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                                        'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'
        };

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

        [NonAction]
        public void sendmail(string CEmail, string CName, String Link)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");


            string br = "";
            br += "<br>";

            string body = "";
            string url = "<a href='https://www.w3schools.com/?email="+CEmail +"' class='a'> " + Link + " </a>";

            body += "<p>" + "Hello" + " " + CName + " " + "Greetings of the Day, " + br + "This is auto generated email, Please Click on the Link " + url + " to start filling the onboarding Form."  + br + "Thankyou."
+ "</p>";

            MailAddress from = new MailAddress("akashchoudhary199404@gmail.com");
            MailAddress to = new MailAddress(CEmail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "On Boarding Form Link";
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

        [HttpPut]
        public IActionResult GenerateOTP([FromBody] Verification verification )
        {
            Random rand = new Random();
            string random = rand.Next(0, 1000000).ToString("D6");

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            int i;

            using (SqlConnection con = new SqlConnection(constring))
            {

                {
                    con.Open();
                    SqlCommand check = new SqlCommand("Usp_UpdateVerification", con);
                    check.CommandType = CommandType.StoredProcedure;
                    
                    check.Parameters.AddWithValue("@Candidate_Email", verification.EmailId);
                    check.Parameters.AddWithValue("@Verification_code", random);
                    
                    i = check.ExecuteNonQuery();
                    if (i > 0)
                    {
                        sendnewmail(verification.EmailId,random);
                    }
                }

            }


            return Ok();
        }

        [NonAction]
        public void sendnewmail(string CEmail, string rand)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");


            string br = "";
            br += "<br>";

            string body = "";
           

            body += "<p>" + " Use"+" "+ rand+" as Verification Code to start filling the onboarding Form." + br + "Thankyou."
+ "</p>";

            MailAddress from = new MailAddress("akashchoudhary199404@gmail.com");
            MailAddress to = new MailAddress(CEmail);
            MailMessage message = new MailMessage(from, to);
            message.Subject = "Verification Code";
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

    }
}
