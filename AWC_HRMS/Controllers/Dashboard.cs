
using AWC_HRMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace AWC_HRMS_Web.Controllers
{

    [Authorize]
    
    public class Dashboard : Controller
    {
        // GET: Dashboard
        [AllowAnonymous]
        public IActionResult Index()
        {

            string Bretval0 = this.CandidateNotStarted();
            ViewBag.Vals0 = Bretval0;
            string Bretval1 = this.CandidateHalfDone();
            ViewBag.Vals1 = Bretval1;
            string Bretval2 = this.CandidateCompleted();
            ViewBag.Vals2 = Bretval2;

            return View();
        }

        // GET: Dashboard/Details/5

        public ActionResult AllCandidateDetails(int id)
        {
            var retval0 = "";
            var retval1 = "";
            var retval2 = "";

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("Usp_FetchAllCandidateData", con);
                check.CommandType = CommandType.StoredProcedure;
                check.Parameters.AddWithValue("@candidateID", "1076");

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())// Iterate through Candidate Master Table
                            if (reader.GetString(1) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval0 = reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" +
                                    reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|" +
                                    reader.GetString(7) + "|" + reader.GetDateTime(8) + "|" + reader.GetString(9) + "|" +
                                    reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetInt32(12) + "|" +
                                    reader.GetString(13) + "|" + reader.GetInt32(14) + "|" + reader.GetString(15) + "|" +
                                    reader.GetString(16) + "|" + reader.GetString(17) + "|" + reader.GetInt32(18) + "|" +
                                    reader.GetString(19) + "|" + reader.GetInt32(20) + "|" + reader.GetString(21) + "|" +
                                    reader.GetString(22) + "|" + reader.GetString(23) + "|" + reader.GetString(24) + "|" +
                                    reader.GetString(25) + "|" + reader.GetString(26) + "|" + reader.GetString(27) + "|" +
                                    reader.GetString(28) + "|" + reader.GetString(33) + "|" + reader.GetString(34) + "|" +
                                    reader.GetString(35) + "|" + reader.GetString(36) + "|" + reader.GetString(37);
                            }

                        reader.NextResult();

                        while (reader.Read())// Iterate through Candidate Education Table
                            if (reader.GetInt32(0) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval1 = reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetString(2) + "|" + reader.GetString(3) + "|" +
                                    reader.GetString(4) + "|" + reader.GetString(5) + "|" + reader.GetString(6) + "|" +
                                    reader.GetString(7) + "|" + reader.GetString(8) + "|" + reader.GetString(9) + "|" +
                                    reader.GetString(10) + "|" + reader.GetString(11) + "|" + reader.GetString(12) + "|" +
                                    reader.GetString(13) + "|" + reader.GetString(14) + "|" + reader.GetString(15) + "|" +
                                    reader.GetString(16) + "|" + reader.GetString(17) + "|" + reader.GetString(18) + "|" +
                                    reader.GetString(19) + "|" + reader.GetString(20) + "|" + reader.GetString(21) + "|" +
                                    reader.GetString(22) + "|" + reader.GetString(23) + "|" + reader.GetString(24) + "|" +
                                    reader.GetString(25) + "|" + reader.GetString(26) + "|" + reader.GetString(27) + "|" +
                                    reader.GetString(28);

                            }

                        reader.NextResult();

                        while (reader.Read())// Iterate through Candidate Employement Table
                            if (reader.GetInt32(0) != null)
                            {
                                //Console.WriteLine(reader.GetString(1));
                                retval2 = reader.GetInt32(0) + "|" + reader.GetString(1) + "|" + reader.GetInt32(2) + "|" + reader.GetInt32(3) + "|" + reader.GetInt32(4);

                            }

                    }
                }
            }

            @ViewBag.Vals0 = retval0;
            @ViewBag.Vals1 = retval1;
            @ViewBag.Vals2 = retval2;
            return View();
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //// GET: Dashboard/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Dashboard/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Dashboard/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Dashboard/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        [Authorize]
        [NonAction]
        public string CandidateCompleted()
        {
            var retval2 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("usp_CanCompleted", con);
                check.CommandType = CommandType.StoredProcedure;

                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        do
                        {
                            retval2 = reader.GetName(0) + "|" + reader.GetName(1) + "#";
                            Console.WriteLine("\t{0}\t{1}", reader.GetName(0), reader.GetName(1));
                            while (reader.Read())
                                retval2 = retval2 + reader.GetInt32(0) + "|" + reader.GetString(1) + "#";
                            //Console.WriteLine("\t{0}\t{1}", reader.GetInt32(0), reader.GetString(1));

                        }
                        while (reader.NextResult());

                        reader.Close();


                    }

                }

            }
            return retval2;
        }

        [Authorize]
        [NonAction]
        public string CandidateHalfDone()
        {
            var retval1 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("usp_CanHalfDone", con);
                check.CommandType = CommandType.StoredProcedure;


                using (var reader = check.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        do
                        {
                            retval1 = reader.GetName(0) + "|" + reader.GetName(1) + "#";
                            Console.WriteLine("\t{0}\t{1}", reader.GetName(0), reader.GetName(1));
                            while (reader.Read())
                                retval1 = retval1 + reader.GetInt32(0) + "|" + reader.GetString(1) + "#";
                            //Console.WriteLine("\t{0}\t{1}", reader.GetInt32(0), reader.GetString(1));

                        }
                        while (reader.NextResult());

                        reader.Close();


                    }

                }

            }
            return retval1;
        }

        [Authorize]
        [NonAction]
        public string CandidateNotStarted()
        {
            var retval0 = "";
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration configuration = builder.Build();
            string constring = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            using (SqlConnection con = new SqlConnection(constring))
            {
                //var id = HttpContext.Session.GetString("CUser");
                con.Open();
                SqlCommand check = new SqlCommand("usp_CanNotStarted", con);
                check.CommandType = CommandType.StoredProcedure;
                

                using (var reader = check.ExecuteReader())
                {

                    if(reader.HasRows)
                    {
                        
                        do
                        {
                            retval0 = reader.GetName(0) + "|" + reader.GetName(1) + "#";
                            Console.WriteLine("\t{0}\t{1}", reader.GetName(0), reader.GetName(1));
                            while (reader.Read())
                                retval0 = retval0 + reader.GetInt32(0) + "|" + reader.GetString(1) + "#";
                            //Console.WriteLine("\t{0}\t{1}", reader.GetInt32(0), reader.GetString(1));

                        }
                        while (reader.NextResult());
                        
                        reader.Close();

                        
                    }
                    
                }
                
            }
            return retval0;
        }
    }

    


}